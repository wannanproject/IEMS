using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PROC_0100_TASK_REQUEST [PROC_0100_TASK_REQUEST] - 实体类
    /// </summary>
    [Entity(TableName = "PROC_0100_TASK_REQUEST", Description = "PROC_0100_TASK_REQUEST")]
    public class Proc0100TaskRequest : BaseEntity
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
