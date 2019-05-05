using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PROC_TASK_FINISH [PROC_TASK_FINISH] - 实体类
    /// </summary>
    [Entity(TableName = "PROC_TASK_FINISH", Description = "PROC_TASK_FINISH")]
    public class ProcTaskFinish : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "IN_TASK_NO", Description = "", DbType = "NUMBER")]
        public decimal? InTaskNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "IN_FINISH_DESC", Description = "", DbType = "VARCHAR2")]
        public string InFinishDesc { get; set; }
        /// <summary>
        /// 存储过程返回 DataSet 数据
        /// </summary>
        public DataSet ProcedureDataSetResult { get; set; }
    }
}
