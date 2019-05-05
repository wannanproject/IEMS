using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PROC_3001_DATA_BACKUP [PROC_3001_DATA_BACKUP] - 实体类
    /// </summary>
    [Entity(TableName = "PROC_3001_DATA_BACKUP", Description = "PROC_3001_DATA_BACKUP")]
    public class Proc3001DataBackup : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "I_PARAM_OBJID", Description = "", DbType = "NUMBER")]
        public decimal? IParamObjid { get; set; }
        /// <summary>
        /// 存储过程返回 DataSet 数据
        /// </summary>
        public DataSet ProcedureDataSetResult { get; set; }
    }
}
