using IEMS.WanLi.VoDto;
using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.AppBiz
{
    public interface IBinDataManager : IAppBizManager
    {
        StoreBinData GetStoreBinData(int x);
        void ChanageBinUseFlag(string crnNo, int useFlag);
        DataTable GetEmptyBin();
        void ClearEmptyBin(string binNo);
        DataTable GetProductStatistics();

        string CreatOrder(string orderId, string orderNo, string binNo, string eLocNo);
        void DeleteBin(string taskNo);
    }
}
