
using MSTL.DbAccess;

namespace IEMS.WanLi.DbCI
{
    internal class TmpLocCmdMonitorService : DbCIService, ITmpLocCmdMonitorService
    {
        public PageResult GetTmpLocCmdMoitor(PageResult pageResult)
        {
            string stmtId = "GetTmpLocCmdMoitor";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }
    }
}
