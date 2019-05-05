using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PROC_0303_CMD_FINISH - 存储过程操作接口
    /// </summary>
    public interface IProc0303CmdFinishService :IProcedureService
    {
	    /// <summary>
        /// PROC_0303_CMD_FINISH 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Proc0303CmdFinish ExcuteProcedure(Proc0303CmdFinish param);
    }

    /// <summary>
    /// PROC_0303_CMD_FINISH - 存储过程操作类
    /// </summary>
    internal class Proc0303CmdFinishService : DbCIService, IProc0303CmdFinishService
    {
	    /// <summary>
        /// PROC_0303_CMD_FINISH 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Proc0303CmdFinish ExcuteProcedure(Proc0303CmdFinish param)
		{
		    var result = this.GetDataSetByStatement("PROC_0303_CMD_FINISH", param);
            param.ProcedureDataSetResult = result;
            return param;
		}
    }
}