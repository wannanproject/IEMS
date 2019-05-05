using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PROC_WMS_FINISH_CMD - 存储过程操作接口
    /// </summary>
    public interface IProcWmsFinishCmdService :IProcedureService
    {
	    /// <summary>
        /// PROC_WMS_FINISH_CMD 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ProcWmsFinishCmd ExcuteProcedure(ProcWmsFinishCmd param);
    }

    /// <summary>
    /// PROC_WMS_FINISH_CMD - 存储过程操作类
    /// </summary>
    internal class ProcWmsFinishCmdService : DbCIService, IProcWmsFinishCmdService
    {
	    /// <summary>
        /// PROC_WMS_FINISH_CMD 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ProcWmsFinishCmd ExcuteProcedure(ProcWmsFinishCmd param)
		{
		    var result = this.GetDataSetByStatement("PROC_WMS_FINISH_CMD", param);
            param.ProcedureDataSetResult = result;
            return param;
		}
    }
}