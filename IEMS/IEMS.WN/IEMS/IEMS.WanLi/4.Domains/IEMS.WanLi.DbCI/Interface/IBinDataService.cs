using IEMS.WanLi.VoDto;
using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.DbCI
{
   public interface IBinDataService : IDbCIService
    {
        StoreBinData GetStoreBinData(int x);
        DataTable GetProductStatistics();
        PageResult GetBinStore(DateTime Btime, DateTime Etime);
        PageResult GetBinStoreBatchno(DateTime Btime, DateTime Etime, string batchNo);
        PageResult GetWbsBinDataTable(PageResult pageResult);
        PageResult GetWbsBinDetDataTable(PageResult pageResult);
        DataTable GetErrBinNo();
        PageResult GetBinData(PageResult pageResult);
    }
}
