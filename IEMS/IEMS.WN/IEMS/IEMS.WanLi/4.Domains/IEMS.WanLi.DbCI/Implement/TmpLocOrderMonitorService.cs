
using MSTL.DbAccess;

namespace IEMS.WanLi.DbCI
{
    internal class TmpLocOrderMonitorService : DbCIService, ITmpLocOrderMonitorService
    {
        public PageResult GetTmpLocOrderMoitor(PageResult pageResult)
        {
            string stmtId = "GetTmpLocOrderMoitor";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }
    }
}
