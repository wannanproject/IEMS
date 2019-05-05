using IEMS.WanLi.DbRI;
using IEMS.WanLi.DbCI;
using IEMS.WanLi.VoDto;
using IEMS.WanLi.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MSTL.DbAccess;


namespace IEMS.WanLi.AppBiz
{
    internal class BinDataManager : IBinDataManager
    {
        public StoreBinData GetStoreBinData(int x)
        {
            var service = DbCIServiceFactory.CreateInstance<IBinDataService>();
            return service.GetStoreBinData(x);
        }

        private void BkPsbBinStatus(string binNo)
        {

        }

        public DataTable GetEmptyBin()
        {
            var service = DbCIServiceFactory.CreateInstance<IBinDataService>();
            return service.GetErrBinNo();
        }

        public void ClearEmptyBin(string binNo)
        {
            var service = TableViewServiceFactory.CreateInstance<IPsbBinStatusService>();
            service.Update(new PsbBinStatus() { BinBizStatus = "_",BinStatus="_" }, new PsbBinStatus() { BinPlcNo = binNo });
            BkPsbBinStatus(binNo);
        }

        public void ChanageBinUseFlag(string binNo, int useFlag)
        {
            var service = TableViewServiceFactory.CreateInstance<IPsbBinStatusService>();
            service.Update(new PsbBinStatus() { UsedFlag = useFlag }, new PsbBinStatus() { BinPlcNo = binNo });
            BkPsbBinStatus(binNo);
        }

        public DataTable GetProductStatistics()
        {
            var service = DbCIServiceFactory.CreateInstance<IBinDataService>();
            return service.GetProductStatistics();
        }

        public string CreatOrder(string orderId, string orderNo, string binNo, string eLocNo)
        {
            var wbsOrder = TableViewServiceFactory.CreateInstance<IWbsOrderService>();
            var wbsOrderLine = TableViewServiceFactory.CreateInstance<IWbsOrderLineService>();
            try
            {
                wbsOrder.Insert(new WbsOrder()
                {
                    OrderNo = orderNo,
                    OrderStatus = 0,
                    OrderTypeNo = "100030",
                    OrderTypeModule = "O",
                    CreatedBy = "WMS",
                    OrderPriority = 1000,
                    SourceType = 0
                });

                wbsOrderLine.Insert(new WbsOrderLine()
                {
                    OrderLineGuid = Guid.NewGuid().ToString(),
                    OrderNo = orderNo,
                    LineId = int.Parse(orderId),
                    LineStatus = 0,
                    ElocNo = eLocNo,
                    RequireQty = 1,
                    LimitBinNo = binNo,
                    LinePriority = 1000
                });
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteBin(string binNo)
        {
            try
            {
                var service = ProcedureServiceFactory.CreateInstance<IProcWmsDeleteBinService>();
                service.ExcuteProcedure(new ProcWmsDeleteBin() { IBinNo = int.Parse(binNo) });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
    }
}
