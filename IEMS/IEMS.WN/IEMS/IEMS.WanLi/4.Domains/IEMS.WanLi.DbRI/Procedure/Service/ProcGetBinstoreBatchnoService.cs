using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PROC_GET_BINSTORE_BATCHNO - 存储过程操作接口
    /// </summary>
    public interface IProcGetBinstoreBatchnoService :IProcedureService
    {
	    /// <summary>
        /// PROC_GET_BINSTORE_BATCHNO 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ProcGetBinstoreBatchno ExcuteProcedure(ProcGetBinstoreBatchno param);
    }

    /// <summary>
    /// PROC_GET_BINSTORE_BATCHNO - 存储过程操作类
    /// </summary>
    internal class ProcGetBinstoreBatchnoService : DbCIService, IProcGetBinstoreBatchnoService
    {
	    /// <summary>
        /// PROC_GET_BINSTORE_BATCHNO 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ProcGetBinstoreBatchno ExcuteProcedure(ProcGetBinstoreBatchno param)
		{
		    var result = this.GetDataSetByStatement("PROC_GET_BINSTORE_BATCHNO", param);
            param.ProcedureDataSetResult = result;
            var idx = 0;
            if (result.Tables.Count > idx + 1)
            {
                param.OCurtable = result.Tables[idx];
            }
            idx++;
            return param;
		}
    }
}