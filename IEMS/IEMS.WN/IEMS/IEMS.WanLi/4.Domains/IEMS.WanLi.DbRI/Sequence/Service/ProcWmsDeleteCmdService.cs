using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PROC_WMS_DELETE_CMD - 存储过程操作接口
    /// </summary>
    public interface IProcWmsDeleteCmdService :IProcedureService
    {
	    /// <summary>
        /// PROC_WMS_DELETE_CMD 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ProcWmsDeleteCmd ExcuteProcedure(ProcWmsDeleteCmd param);
    }

    /// <summary>
    /// PROC_WMS_DELETE_CMD - 存储过程操作类
    /// </summary>
    internal class ProcWmsDeleteCmdService : DbCIService, IProcWmsDeleteCmdService
    {
	    /// <summary>
        /// PROC_WMS_DELETE_CMD 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ProcWmsDeleteCmd ExcuteProcedure(ProcWmsDeleteCmd param)
		{
		    var result = this.GetDataSetByStatement("PROC_WMS_DELETE_CMD", param);
            param.ProcedureDataSetResult = result;
            return param;
		}
    }
}