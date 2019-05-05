using MSTL.DbAccess;

namespace IEMS.WanLi.AppBiz
{
    public interface ITmpLocCmdMonitorManager : IAppBizManager
    {
        /// <summary>
        /// 根据页面查询条件获取指令实时监控列表
        /// </summary>
        /// <returns></returns>
        PageResult GetTmpLocCmdMoitor(PageResult pageResult);
    }
}
