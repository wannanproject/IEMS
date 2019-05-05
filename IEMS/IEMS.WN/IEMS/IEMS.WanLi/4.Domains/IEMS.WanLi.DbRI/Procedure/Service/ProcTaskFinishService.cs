using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PROC_TASK_FINISH - 存储过程操作接口
    /// </summary>
    public interface IProcTaskFinishService :IProcedureService
    {
	    /// <summary>
        /// PROC_TASK_FINISH 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ProcTaskFinish ExcuteProcedure(ProcTaskFinish param);
    }

    /// <summary>
    /// PROC_TASK_FINISH - 存储过程操作类
    /// </summary>
    internal class ProcTaskFinishService : DbCIService, IProcTaskFinishService
    {
	    /// <summary>
        /// PROC_TASK_FINISH 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ProcTaskFinish ExcuteProcedure(ProcTaskFinish param)
		{
		    var result = this.GetDataSetByStatement("PROC_TASK_FINISH", param);
            param.ProcedureDataSetResult = result;
            return param;
		}
    }
}