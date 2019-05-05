using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PROC_WMS_DELETE_BIN - 存储过程操作接口
    /// </summary>
    public interface IProcWmsDeleteBinService :IProcedureService
    {
	    /// <summary>
        /// PROC_WMS_DELETE_BIN 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ProcWmsDeleteBin ExcuteProcedure(ProcWmsDeleteBin param);
    }

    /// <summary>
    /// PROC_WMS_DELETE_BIN - 存储过程操作类
    /// </summary>
    internal class ProcWmsDeleteBinService : DbCIService, IProcWmsDeleteBinService
    {
	    /// <summary>
        /// PROC_WMS_DELETE_BIN 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ProcWmsDeleteBin ExcuteProcedure(ProcWmsDeleteBin param)
		{
		    var result = this.GetDataSetByStatement("PROC_WMS_DELETE_BIN", param);
            param.ProcedureDataSetResult = result;
            return param;
		}
    }
}