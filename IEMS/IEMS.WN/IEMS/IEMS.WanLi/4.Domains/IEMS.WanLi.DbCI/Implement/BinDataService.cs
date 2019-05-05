using IEMS.WanLi.DbRI;
using IEMS.WanLi.Entity;
using IEMS.WanLi.VoDto;
using MSTL.DbAccess;
using MSTL.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.DbCI
{
    internal class BinDataService : DbCIService, IBinDataService
    {
        private string getMaxBinNo(object paem)
        {
            var dTable = this.GetDataTableByStatement("GetMaxBinNo", paem);
            foreach(DataRow dRow in dTable.Rows)
            {
               return dRow.StringValue("MaxBinNo");
            }
            return string.Empty;
        }
        public StoreBinData GetStoreBinData(int x)
        {
            var result = new StoreBinData();
            result.MaxBin = new BinData();
            var where = new Hashtable();
            var param = new Hashtable();
            param["BinStart"] = x.ToString("D2");
            where["where"] = param;
            if (x == 99)
            {
                result.MaxBin.BinNo = getMaxBinNo(null);
            }
            else
            {
                result.MaxBin.BinNo = getMaxBinNo(where);
            }
            
            var dTable = this.GetDataTableByStatement("GetBinDataByBinNoStart", where);
            var bins = new List<BinData>();
            foreach(DataRow dRow in dTable.Rows)
            {
                var bin= new BinData();
                bins.Add(bin);
                bin.BinNo = dRow.StringValue("BIN_PLC_NO");
                bin.UsedFlag = dRow.IntValue("USED_FLAG", 0);
                bin.BinSize = dRow.StringValue("BIN_SIZE");
                bin.BinStatus = dRow.StringValue("BIN_STATUS");
                bin.BinBizStatus = dRow.StringValue("BIN_BIZ_STATUS");
                bin.BinWeight = dRow.IntValue("BIN_WEIGHT", 0);
                bin.BinArea = dRow.StringValue("BIN_AREA");
                bin.CrnNo = dRow.StringValue("CRN_NO");
                bin.GroupNo = dRow.StringValue("GROUP_NO");
                bin.OrderLineGuid = dRow.StringValue("ORDER_LINE_GUID");
                bin.BinAgvNo = dRow.StringValue("BIN_AGV_NO");
                //bin.BinPlcNo = dRow.StringValue("BIN_PLC_NO");

                bin.PalletNo1 = dRow.StringValue("PALLET_NO1");
                bin.PalletNo2 = dRow.StringValue("PALLET_NO2");
                bin.MaterNo = dRow.StringValue("MATERIAL_NO");
                bin.MaterName = dRow.StringValue("MATER_NAME");
                bin.BatchNo = dRow.StringValue("BATCH_NO");
                bin.Grade = dRow.StringValue("GRADE");
                bin.ProductDate = dRow.StringValue("PRODUCT_DATE");

                bin.MaterMkind = dRow.StringValue("MATER_MKIND");
                bin.MaterType = dRow.StringValue("MATER_TYPE");
                bin.MaterSpec = dRow.StringValue("MATER_SPEC");
                bin.MaterDesc = dRow.StringValue("MATER_DESC");
                bin.Qty = dRow.StringValue("QTY");
                bin.CrnNo = dRow.StringValue("CRN_NO");
            }
            result.AllBin = bins.ToArray();
            return result;
        }

        public DataTable GetProductStatistics()
        {
            var dTable = this.GetDataTableByStatement("GetProductStatistics", null);
            return dTable;
        }

        public PageResult GetWbsBinDataTable(PageResult pageResult)
        {
            string stmtId = "GetWbsBinDataTable";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }

        public PageResult GetWbsBinDetDataTable(PageResult pageResult)
        {
            string stmtId = "GetWbsBinDetDataTable";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }

        /// <summary>
        /// 获取库存信息统计
        /// </summary>
        /// <param name="Btime"></param>
        /// <param name="Etime"></param>
        /// <returns></returns>
        public PageResult GetBinStore(DateTime Btime, DateTime Etime)
        {
            var transaction = TransactionServiceFatory.CreateInstance<ITransactionService>();
            try
            {
                var procService = ProcedureServiceFactory.CreateInstance<IProcGetBinstoreService>();
                transaction.BeginTransaction();
                var para = new ProcGetBinstore();
                para.IBtime = Btime;
                para.IEdate = Etime;
                procService.ExcuteProcedure(para);
                transaction.CompleteTransaction();

                var pageResult = new PageResult();
                var stmtId = "GetZ30BinStore";
                pageResult.StatementId = stmtId;
                return this.GetPageDataByReader(pageResult);
            }
            catch(Exception ex)
            {
                transaction.RollbackTransaction();
                return null;
            }
        }
        /// <summary>
        /// 获取库存信息统计
        /// </summary>
        /// <param name="Btime"></param>
        /// <param name="Etime"></param>
        /// <returns></returns>
        public PageResult GetBinStoreBatchno(DateTime Btime, DateTime Etime, string batchNo)
        {
            var transaction = TransactionServiceFatory.CreateInstance<ITransactionService>();
            try
            {
                var procService = ProcedureServiceFactory.CreateInstance<IProcGetBinstoreBatchnoService>();
                transaction.BeginTransaction();
                var para = new ProcGetBinstoreBatchno();
                para.IBtime = Btime;
                para.IEdate = Etime;
                para.IBatchNo = batchNo;
                procService.ExcuteProcedure(para);
                transaction.CompleteTransaction();

                var pageResult = new PageResult();
                var stmtId = "GetZ30BinStoreBatchNo";
                pageResult.StatementId = stmtId;
                return this.GetPageDataByReader(pageResult);
            }
            catch (Exception ex)
            {
                transaction.RollbackTransaction();
                return null;
            }
        }
        //GetErrBinNo
        public DataTable GetErrBinNo()
        {
            var dTable = this.GetDataTableByStatement("GetErrBinNo", null);
            return dTable;
        }

        public PageResult GetBinData(PageResult pageResult)
        {
            string stmtId = "GetBinData";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }
    }
}
