using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PROC_WMS_UPDATE_SRM_AREA - 存储过程操作接口
    /// </summary>
    public interface IProcWmsUpdateSrmAreaService :IProcedureService
    {
	    /// <summary>
        /// PROC_WMS_UPDATE_SRM_AREA 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ProcWmsUpdateSrmArea ExcuteProcedure(ProcWmsUpdateSrmArea param);
    }

    /// <summary>
    /// PROC_WMS_UPDATE_SRM_AREA - 存储过程操作类
    /// </summary>
    internal class ProcWmsUpdateSrmAreaService : DbCIService, IProcWmsUpdateSrmAreaService
    {
	    /// <summary>
        /// PROC_WMS_UPDATE_SRM_AREA 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ProcWmsUpdateSrmArea ExcuteProcedure(ProcWmsUpdateSrmArea param)
		{
		    var result = this.GetDataSetByStatement("PROC_WMS_UPDATE_SRM_AREA", param);
            param.ProcedureDataSetResult = result;
            return param;
		}
    }
}