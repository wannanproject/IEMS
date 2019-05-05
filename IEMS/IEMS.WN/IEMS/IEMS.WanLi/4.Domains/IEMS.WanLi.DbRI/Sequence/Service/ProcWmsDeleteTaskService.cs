using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PROC_WMS_DELETE_TASK - 存储过程操作接口
    /// </summary>
    public interface IProcWmsDeleteTaskService :IProcedureService
    {
	    /// <summary>
        /// PROC_WMS_DELETE_TASK 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ProcWmsDeleteTask ExcuteProcedure(ProcWmsDeleteTask param);
    }

    /// <summary>
    /// PROC_WMS_DELETE_TASK - 存储过程操作类
    /// </summary>
    internal class ProcWmsDeleteTaskService : DbCIService, IProcWmsDeleteTaskService
    {
	    /// <summary>
        /// PROC_WMS_DELETE_TASK 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ProcWmsDeleteTask ExcuteProcedure(ProcWmsDeleteTask param)
		{
		    var result = this.GetDataSetByStatement("PROC_WMS_DELETE_TASK", param);
            param.ProcedureDataSetResult = result;
            return param;
		}
    }
}