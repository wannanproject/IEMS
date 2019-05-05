using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PROC_WMS_DELETE_BIN [PROC_WMS_DELETE_BIN] - 实体类
    /// </summary>
    [Entity(TableName = "PROC_WMS_DELETE_BIN", Description = "PROC_WMS_DELETE_BIN")]
    public class ProcWmsDeleteBin : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "I_BIN_NO", Description = "", DbType = "NUMBER")]
        public decimal? IBinNo { get; set; }
        /// <summary>
        /// 存储过程返回 DataSet 数据
        /// </summary>
        public DataSet ProcedureDataSetResult { get; set; }
    }
}
