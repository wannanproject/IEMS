using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PROC_GET_BINSTORE - 存储过程操作接口
    /// </summary>
    public interface IProcGetBinstoreService :IProcedureService
    {
	    /// <summary>
        /// PROC_GET_BINSTORE 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ProcGetBinstore ExcuteProcedure(ProcGetBinstore param);
    }

    /// <summary>
    /// PROC_GET_BINSTORE - 存储过程操作类
    /// </summary>
    internal class ProcGetBinstoreService : DbCIService, IProcGetBinstoreService
    {
	    /// <summary>
        /// PROC_GET_BINSTORE 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ProcGetBinstore ExcuteProcedure(ProcGetBinstore param)
		{
		    var result = this.GetDataSetByStatement("PROC_GET_BINSTORE", param);
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