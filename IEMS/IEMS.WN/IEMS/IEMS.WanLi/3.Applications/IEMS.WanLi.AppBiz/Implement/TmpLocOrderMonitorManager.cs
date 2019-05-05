
using IEMS.WanLi.DbCI;
using MSTL.DbAccess;

namespace IEMS.WanLi.AppBiz
{
    internal class TmpLocOrderMonitorManager: ITmpLocOrderMonitorManager
    {
        #region GetTmpLocOrderMoitor
        public PageResult GetTmpLocOrderMoitor(PageResult pageResult)
        {
            var service = DbCIServiceFactory.CreateInstance<ITmpLocOrderMonitorService>();
            return service.GetTmpLocOrderMoitor(pageResult);
        }
        #endregion
    }
}
