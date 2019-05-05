using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PROC_WMS_DELETE_TASK [PROC_WMS_DELETE_TASK] - 实体类
    /// </summary>
    [Entity(TableName = "PROC_WMS_DELETE_TASK", Description = "PROC_WMS_DELETE_TASK")]
    public class ProcWmsDeleteTask : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "I_TASK_NO", Description = "", DbType = "NUMBER")]
        public decimal? ITaskNo { get; set; }
        /// <summary>
        /// 存储过程返回 DataSet 数据
        /// </summary>
        public DataSet ProcedureDataSetResult { get; set; }
    }
}
