using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using MSTL.DbAccess;
using IEMS.WanLi.Entity;
using IEMS.WanLi.DbRI;
using IEMS.WanLi.DbCI;
using MSTL.LogAgent;
using MSTL.ResultStruct;
using IEMS.WanLi.AppBiz.Common;
using MSTL.ResultStruct.McException;


namespace IEMS.WanLi.AppBiz
{
    internal class OutputBillManager : IOutputBillManager
    {
        #region 系统日志  log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion
        /// <summary>
        /// 获取单据信息
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public PageResult GetBillOutputData(PageResult pageResult)
        {
            var service = DbCIServiceFactory.CreateInstance<IOutputService>();
            return service.GetBillOutputData(pageResult);
        }

        #region 删除单据信息 DeleteBillData
        public string DeleteBillData(string orderNo)
        {
            var bill = GetWmsOrderOut(orderNo);
            var serviceHeader = TableViewServiceFactory.CreateInstance<IWbsOrderService>();
            var serviceLine = TableViewServiceFactory.CreateInstance<IWbsOrderLineService>();
            var transaction = TransactionServiceFatory.CreateInstance<ITransactionService>();
            try
            {
                transaction.BeginTransaction();
                serviceLine.Update(new WbsOrderLine() { LineStatus = 3 }, new WbsOrderLine() { OrderNo = orderNo });
                serviceHeader.Update(new WbsOrder() { OrderStatus = 3 }, new WbsOrder() { OrderNo = orderNo });
                transaction.CompleteTransaction();
            }
            catch (Exception ex)
            {
                transaction.RollbackTransaction();
                log.Error(ex);
                throw new VerifyException("数据删除失败");
            }
            return string.Empty;
        }
        #endregion
        public WbsOrder GetWmsOrderOut(string OrderNo)
        {
            var service = TableViewServiceFactory.CreateInstance<IWbsOrderService>();
            return service.GetEntityList(new WbsOrder() { OrderNo = OrderNo }).FirstOrDefault();
        }
        public WbsOrderLine[] GetWmsOrderLineOut(string OrderNo)
        {
            var service = TableViewServiceFactory.CreateInstance<IWbsOrderLineService>();
            return service.GetEntityList(new WbsOrderLine() { OrderNo = OrderNo }).ToArray();
        }
        /// <summary>
        /// 获取唯一单据号
        /// </summary>
        /// <returns></returns>
        public string GetNewBillNo()
        {
            return new BillNoManager().GetNewBillNo(2);
        }

        ///  查询站台信息
        /// </summary>
        /// <returns></returns>
        public PsbLoc[] GetEquipData(string LOCTypeNo)
        {
            IList<PsbLoc> lists = new List<PsbLoc>();
            var Equip = TableViewServiceFactory.CreateInstance<IPsbLocService>();
            var lst = Equip.GetEntityList(new PsbLoc { LocTypeNo = LOCTypeNo });
            foreach (var psbLocData in lst)
            {
                if (psbLocData.LocGroupNo != null)
                {
                    lists.Add(psbLocData);
                }
            }
            PsbLoc[] psbLoc = new PsbLoc[lists.Count];
            int i = 0;
            foreach (var list in lists)
            {
                psbLoc[i] = list;
                i++;
            }
            return psbLoc;
        }

        /// <summary>
        ///  查询品级
        /// </summary>
        /// <returns></returns>
        public PsbGrade[] GetGradeTable(PsbGrade GradeString)
        {
            var tdate = TableViewServiceFactory.CreateInstance<IPsbGradeService>();
            var where = new PsbGrade();
            return tdate.GetEntityList(where, "GRADE_NO").ToArray();

        }

        /// <summary>
        ///  查询MI号
        /// </summary>
        /// <returns></returns>
        public PsbMino[] GetMinoTable(PsbMino MinoString)
        {
            var tdate = TableViewServiceFactory.CreateInstance<IPsbMinoService>();
            var where = new PsbMino();
            return tdate.GetEntityList(where, "MINO").ToArray();

        }

        #region 添加单据信息  SaveOutputBill
        private bool IsMatchRegex(string input, string pattern)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            Regex textRegex = new Regex(pattern);
            return textRegex.IsMatch(input);
        }
        private bool VerifyBillData(WbsOrder bill, WbsOrderLine[] body)
        {
            var outHeaderService = TableViewServiceFactory.CreateInstance<IWbsOrderService>();
            #region 验证 主表数据
            #region 验证 单据类型
            var kind = bill.OrderTypeNo;
            if (kind == null)
            {
                throw new VerifyException("请选择单据类型，如果没有订单类型，请维护订单类型!");
            }
            #endregion
            #region 验证 异常出库
            var kindInt = bill.OrderTypeNo;
            if (kindInt == "100891" && body.Length > 1)
            {
                throw new VerifyException("异常出库每次建单据只允许一种物料!");
            }
            #endregion
            #endregion
            #region 验证 表体
            foreach (var line in body)
            {
                if (!string.IsNullOrWhiteSpace(line.LimitPalletId))
                {
                    line.LinePriority = line.LinePriority - 1;
                    if (line.LinePriority == 0)
                    {
                        line.LinePriority = 1;
                    }
                }
                if (bill.OrderTypeNo == "100058")
                {//空笼出库
                    line.MaterialNo = "BPALLET";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(line.LimitPalletId))
                    {
                        #region 验证 出库物料
                        if (string.IsNullOrWhiteSpace(line.MaterialNo))
                        {
                            throw new VerifyException("行号[" + line.LineId?.ToString("0") + "]，请选择出库物料!");
                        }
                        #endregion
                        #region 验证 物料等级
                        if (string.IsNullOrWhiteSpace(line.ProductGrade))
                        {
                            throw new VerifyException("行号[" + line.LineId?.ToString("0") + "]，请选择物料等级!");
                        }
                        #endregion
                    }
                }
                #region 验证 单号行号
                if (line.LineId == null)
                {
                    throw new VerifyException("行号[" + line.LineId?.ToString("0") + "]，请输入单号行号!");
                }
                #endregion
                #region 验证 库位编码是否存在

                var data = TableViewServiceFactory.CreateInstance<IPsbBinStatusService>();
                var eloc = TableViewServiceFactory.CreateInstance<IPsbLocService>();
                if (!string.IsNullOrWhiteSpace(line.LimitBinNo))
                {
                    PsbBinStatus ba = new PsbBinStatus();
                    ba.BinPlcNo = line.LimitBinNo;
                    var listbabin = data.GetEntityTable(ba);
                    if (listbabin.Rows.Count == 0)
                    {
                        throw new VerifyException("开始库位[" + line.LimitBinNo + "]，的库位编码不存在!");
                    }
                }
                if (!string.IsNullOrWhiteSpace(line.ElocNo))
                {
                    PsbLoc bat = new PsbLoc();
                    bat.LocNo = line.ElocNo;
                    var listbabint = eloc.GetEntityTable(bat);
                    if (listbabint.Rows.Count == 0)
                    {
                        throw new VerifyException("目的站台[" + line.ElocNo + "]，的库位编码不存在!");
                    }
                }
                #endregion
                #region 验证 工装编码

                if (!string.IsNullOrWhiteSpace(line.LimitPalletId))
                {
                    var Padata = TableViewServiceFactory.CreateInstance<IViewPalletService>();
                    var pallet = new ViewPallet();
                    pallet.RfidNo = line.LimitPalletId;
                    if (Padata.GetRowCount(pallet) == 0)
                    {
                        throw new VerifyException("工装编码[" + line.LimitPalletId + "]，不存在或者无效!");
                    }
                }

                #endregion
                #region 验证 单据数量
                if (line.RequireQty == null || line.RequireQty <= 0)
                {
                    throw new VerifyException("行号[" + line.LineId?.ToString("0") + "]，请输入单据数量且为非零正整数!");
                }
                #endregion
            }
            #endregion
            return true;
        }

        private bool VerifyAddBillData(WbsOrder bill, WbsOrderLine[] body)
        {
            if (!VerifyBillData(bill, body))
            {
                return false;
            }
            var outHeaderService = TableViewServiceFactory.CreateInstance<IWbsOrderService>();
            #region 验证 出库单号
            var count = outHeaderService.GetRowCount(new WbsOrder() { OrderNo = bill.OrderNo });
            if (count > 0)
            {
                throw new VerifyException("出库单号已存在!");
            }
            #endregion
            return true;
        }
        /// <summary>
        /// 添加单据信息
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public WbsOrder SaveOutputBill(WbsOrder bill, WbsOrderLine[] body)
        {
            var outHeaderService = TableViewServiceFactory.CreateInstance<IWbsOrderService>();
            var outLineService = TableViewServiceFactory.CreateInstance<IWbsOrderLineService>();
            var transaction = TransactionServiceFatory.CreateInstance<ITransactionService>();
            #region 单据验证
            if (!VerifyAddBillData(bill, body))
            {
                throw new VerifyException("单据验证失败!");
            }
            #endregion
            transaction.BeginTransaction();
            try
            {
                outHeaderService.Insert(bill);
                foreach (var line in body)
                {
                    outLineService.Insert(line);
                }
                transaction.CompleteTransaction();
            }
            catch (Exception ex)
            {
                transaction.RollbackTransaction();
                throw ex;
            }
            return outHeaderService.GetEntityList(new WbsOrder() { OrderNo = bill.OrderNo }).FirstOrDefault();
        }
        #endregion
        /// <summary>
        /// 获取唯一单据行GUID(添加界面)
        /// </summary>
        /// <returns></returns>
        public string GetNewLineGuid()
        {
            return new LineGuidManager().GetNewLineGuid(2);
        }

        /// <summary>
        /// 获取单据主信息(修改界面)
        /// </summary>
        /// <returns></returns>
        public WbsOrder GetWbsOrder(string OrderNo)
        {
            var service = TableViewServiceFactory.CreateInstance<IWbsOrderService>();
            return service.GetEntityList(new WbsOrder() { OrderNo = OrderNo }).FirstOrDefault();
        }
        /// <summary>
        /// 获取单据行信息(修改界面)
        /// </summary>
        /// <returns></returns>
        public WbsOrderLine GetWbsOrderLine(string OrderNo)
        {
            var service = TableViewServiceFactory.CreateInstance<IWbsOrderLineService>();
            return service.GetEntityList(new WbsOrderLine() { OrderNo = OrderNo }).FirstOrDefault();
        }
        /// <summary>
        /// 获取单据主信息
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public DataTable GetListOutBillData(string OrderNo)
        {
            var service = DbCIServiceFactory.CreateInstance<IOutputService>();
            var result = service.GetListOutBillLineData(OrderNo);
            //result.Columns.Add(new DataColumn() { ColumnName = "LOC_NAME", DataType = typeof(string) });
            var PsbLocDataService = TableViewServiceFactory.CreateInstance<IPsbLocService>();
            return result;
        }

        /// <summary>
        ///  更新出库行项目
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public WbsOrder UpdateOutputBill(WbsOrder bill, WbsOrderLine[] body)
        {
            var outHeaderService = TableViewServiceFactory.CreateInstance<IWbsOrderService>();
            var outLineService = TableViewServiceFactory.CreateInstance<IWbsOrderLineService>();
            try
            {
                foreach (var line in body)
                {
                    if (string.IsNullOrWhiteSpace(line.OrderLineGuid))
                    {
                        line.OrderNo = bill.OrderNo;
                        InsertOutputBillLine(bill, line);
                    }
                    else
                    {
                        UpdateOutputBillLine(bill, line);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outHeaderService.GetEntityList(new WbsOrder() { OrderNo = bill.OrderNo }).FirstOrDefault();
        }
        private void UpdateOutputBillLine(WbsOrder bill, WbsOrderLine line)
        {
            var outLineService = TableViewServiceFactory.CreateInstance<IWbsOrderLineService>();
            var update = new WbsOrderLine();
            update.MaterialNo = line.MaterialNo;
            update.ProductGrade = line.ProductGrade;
            update.ElocNo = line.ElocNo;
            update.LimitBinNo = line.LimitBinNo;
            update.DeleteFlag = line.DeleteFlag;
            update.LimitBinNo = line.LimitBinNo;
            update.LimitProductGuid = line.LimitProductGuid;
            update.LimitPalletId = line.LimitPalletId;
            var where = new WbsOrderLine();
            where.OrderLineGuid = line.OrderLineGuid;
            where.OrderNo = line.OrderNo;
            where.LineId = line.LineId;
            outLineService.Update(update, where);
        }
        private void InsertOutputBillLine(WbsOrder bill, WbsOrderLine line)
        {
            var outLineService = TableViewServiceFactory.CreateInstance<IWbsOrderLineService>();
            var insert = new WbsOrderLine();
            insert.OrderNo = line.OrderNo;
            insert.LineId = line.LineId;
            insert.LineStatus = line.LineStatus;
            insert.MaterialNo = line.MaterialNo;
            insert.ProductGrade = line.ProductGrade;
            insert.ElocNo = line.ElocNo;
            insert.RequireQty = line.RequireQty;
            insert.ActQty = line.ActQty;
            insert.ShipQty = line.ShipQty;
            insert.LimitBinNo = line.LimitBinNo;
            insert.LineTaskFlag = line.LineTaskFlag;
            insert.OrderLineGuid = Guid.NewGuid().ToString();
            insert.DeleteFlag = "N";
            insert.SortId = 1;
            insert.LinePriority = 50;
            insert.LockEndLoc = 1;
            outLineService.Insert(insert);
        }

       

      

    
    }
}
