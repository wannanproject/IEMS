using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PROC_WMS_DELETE_CMD [PROC_WMS_DELETE_CMD] - 实体类
    /// </summary>
    [Entity(TableName = "PROC_WMS_DELETE_CMD", Description = "PROC_WMS_DELETE_CMD")]
    public class ProcWmsDeleteCmd : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "I_OBJID", Description = "", DbType = "NUMBER")]
        public decimal? IObjid { get; set; }
        /// <summary>
        /// 存储过程返回 DataSet 数据
        /// </summary>
        public DataSet ProcedureDataSetResult { get; set; }
    }
}
