
namespace IEMS.WanLi.DbCI
{
    using MSTL.DbAccess;
    public interface ITmpLocCmdMonitorService : IDbCIService
    {
        /// <summary>
        /// 获取指令实时监控信息
        /// </summary>
        /// <returns></returns>
        PageResult GetTmpLocCmdMoitor(PageResult pageResult);
    }
}
