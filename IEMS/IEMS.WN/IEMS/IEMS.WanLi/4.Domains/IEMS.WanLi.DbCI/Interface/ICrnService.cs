
namespace IEMS.WanLi.DbCI
{
    using MSTL.DbAccess;
    using System.Data;

    public interface ICrnService : IDbCIService
    {
        DataTable GetCrnDataTable();
        /// <summary>
        /// 获取堆垛机叉异常状态
        /// </summary>
        /// <returns></returns>
        DataTable GetCrnForkErrorStatus();
    }
}
