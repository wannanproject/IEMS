using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    /// <summary>
    /// PROC_PAGE_MENU_ACTION_ALL - 存储过程操作接口
    /// </summary>
    public interface IProcPageMenuActionAllService :IProcedureService
    {
	    /// <summary>
        /// PROC_PAGE_MENU_ACTION_ALL 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ProcPageMenuActionAll ExcuteProcedure(ProcPageMenuActionAll param);
    }

    /// <summary>
    /// PROC_PAGE_MENU_ACTION_ALL - 存储过程操作类
    /// </summary>
    internal class ProcPageMenuActionAllService : DbCIService, IProcPageMenuActionAllService
    {
	    /// <summary>
        /// PROC_PAGE_MENU_ACTION_ALL 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ProcPageMenuActionAll ExcuteProcedure(ProcPageMenuActionAll param)
		{
		    var result = this.GetDataSetByStatement("PROC_PAGE_MENU_ACTION_ALL", param);
            param.ProcedureDataSetResult = result;
            var idx = 0;
            if (result.Tables.Count > idx + 1)
            {
                param.Curtable1 = result.Tables[idx];
            }
            idx++;
            if (result.Tables.Count > idx + 1)
            {
                param.Curtable2 = result.Tables[idx];
            }
            idx++;
            return param;
		}
    }
}