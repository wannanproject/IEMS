
namespace IEMS.WanLi.DbCI
{
    using MSTL.DbAccess;
    public interface ITmpLocOrderMonitorService : IDbCIService
    {
        /// <summary>
        /// 获取订单实时监控信息
        /// </summary>
        /// <returns></returns>
        PageResult GetTmpLocOrderMoitor(PageResult pageResult);
    }
}
