using MSTL.DbAccess;
using IEMS.WanLi.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEMS.WanLi.AppBiz
{
    public interface ITaskManager : IAppBizManager
    {
        /// <summary>
        /// 根据页面查询条件获取任务列表
        /// </summary>
        /// <returns></returns>
        PageResult GetWbsTaskData(PageResult pageResult);

        /// <summary>
        /// 根据任务号获取任务信息
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        WbsTask GetWbsTask(long taskNo);

        /// <summary>
        /// 调整优先级
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="emerge"></param>
        /// <returns></returns>
        int[] ResetTaskEmerge(WbsTask[] tasks, int emerge);

        /// <summary>
        /// 强制取消
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="emerge"></param>
        /// <returns></returns>
        string CancelSelectTask(WbsTask[] tasks,string FinishDesc, int TaskNo);

        /// <summary>
        /// 强制完成
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="emerge"></param>
        /// <returns></returns>
        bool CreateTaskOperation(string palletid, string locNo);

        /// <summary>
        /// 获取任务信息
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetTaskData(PageResult pageResult);

        /// <summary>
        /// 堆垛机指令重发
        /// </summary>
        int ResendWbsTaskCmd(string Objid);
        /// <summary>
        /// 堆垛机指令结束
        /// </summary>
        bool FinishWbsTaskCmd(string Objid);
        /// <summary>
        /// 堆垛机指令删除
        /// </summary>
        bool DeleteWbsTaskCmd(string Objid);
        /// <summary>
        /// 堆垛机任务删除
        /// </summary>
        bool DeleteWbsTask(string TaskNo);
        /// <summary>
        /// 任务请求
        /// </summary>
        string RequestTaskCmd(string palletNo1, string palletNo2, string sLocNo);
    }
}
