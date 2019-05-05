
namespace IEMS.WanLi.DbCI
{
    using MSTL.DbAccess;

    public interface ITaskService : IDbCIService
    {
        /// <summary>
        /// 获取任务信息
        /// </summary>
        /// <returns></returns>
        PageResult GetWbsTaskData(PageResult pageResult);
        /// <summary>
        /// 执行手动添加任务存储过程
        /// </summary>
        /// <param name="locNo"></param>
        /// <param name="palletId"></param>
        /// <returns></returns>
        bool ExecPROC_0304_CMD_STEP_ADJUST(string locNo, string palletId);
        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        PageResult GetTaskData(PageResult pageResult);
    }
}
