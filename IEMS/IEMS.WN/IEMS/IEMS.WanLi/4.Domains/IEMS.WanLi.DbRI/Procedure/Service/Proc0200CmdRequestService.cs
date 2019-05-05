using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PROC_0200_CMD_REQUEST - 存储过程操作接口
    /// </summary>
    public interface IProc0200CmdRequestService :IProcedureService
    {
	    /// <summary>
        /// PROC_0200_CMD_REQUEST 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Proc0200CmdRequest ExcuteProcedure(Proc0200CmdRequest param);
    }

    /// <summary>
    /// PROC_0200_CMD_REQUEST - 存储过程操作类
    /// </summary>
    internal class Proc0200CmdRequestService : DbCIService, IProc0200CmdRequestService
    {
	    /// <summary>
        /// PROC_0200_CMD_REQUEST 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Proc0200CmdRequest ExcuteProcedure(Proc0200CmdRequest param)
		{
		    var result = this.GetDataSetByStatement("PROC_0200_CMD_REQUEST", param);
            param.ProcedureDataSetResult = result;
            return param;
		}
    }
}