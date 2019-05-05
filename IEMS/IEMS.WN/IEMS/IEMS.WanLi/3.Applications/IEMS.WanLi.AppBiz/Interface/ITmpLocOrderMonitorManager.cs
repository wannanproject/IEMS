using MSTL.DbAccess;

namespace IEMS.WanLi.AppBiz
{
    public interface ITmpLocOrderMonitorManager : IAppBizManager
    {
        /// <summary>
        /// 根据页面查询条件获取订单实时监控列表
        /// </summary>
        /// <returns></returns>
        PageResult GetTmpLocOrderMoitor(PageResult pageResult);
    }
}
