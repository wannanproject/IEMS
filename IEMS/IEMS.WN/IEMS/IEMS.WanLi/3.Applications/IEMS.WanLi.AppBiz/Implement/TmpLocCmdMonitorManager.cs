
using IEMS.WanLi.DbCI;
using MSTL.DbAccess;

namespace IEMS.WanLi.AppBiz
{
    internal class TmpLocCmdMonitorManager: ITmpLocCmdMonitorManager
    {
        #region GetTmpLocCmdMoitor
        public PageResult GetTmpLocCmdMoitor(PageResult pageResult)
        {
            var service = DbCIServiceFactory.CreateInstance<ITmpLocCmdMonitorService>();
            return service.GetTmpLocCmdMoitor(pageResult);
        }
        #endregion
    }
}
