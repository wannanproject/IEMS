using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PROC_0100_TASK_REQUEST - 存储过程操作接口
    /// </summary>
    public interface IProc0100TaskRequestService :IProcedureService
    {
	    /// <summary>
        /// PROC_0100_TASK_REQUEST 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Proc0100TaskRequest ExcuteProcedure(Proc0100TaskRequest param);
    }

    /// <summary>
    /// PROC_0100_TASK_REQUEST - 存储过程操作类
    /// </summary>
    internal class Proc0100TaskRequestService : DbCIService, IProc0100TaskRequestService
    {
	    /// <summary>
        /// PROC_0100_TASK_REQUEST 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Proc0100TaskRequest ExcuteProcedure(Proc0100TaskRequest param)
		{
		    var result = this.GetDataSetByStatement("PROC_0100_TASK_REQUEST", param);
            param.ProcedureDataSetResult = result;
            return param;
		}
    }
}