using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;
    using IEMS.WanLi.Entity;

    /// <summary>
    /// PROC_3001_DATA_BACKUP - 存储过程操作接口
    /// </summary>
    public interface IProc3001DataBackupService :IProcedureService
    {
	    /// <summary>
        /// PROC_3001_DATA_BACKUP 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Proc3001DataBackup ExcuteProcedure(Proc3001DataBackup param);
    }

    /// <summary>
    /// PROC_3001_DATA_BACKUP - 存储过程操作类
    /// </summary>
    internal class Proc3001DataBackupService : DbCIService, IProc3001DataBackupService
    {
	    /// <summary>
        /// PROC_3001_DATA_BACKUP 执行存储过程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Proc3001DataBackup ExcuteProcedure(Proc3001DataBackup param)
		{
		    var result = this.GetDataSetByStatement("PROC_3001_DATA_BACKUP", param);
            param.ProcedureDataSetResult = result;
            return param;
		}
    }
}