using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PROC_GET_BINSTORE_BATCHNO [PROC_GET_BINSTORE_BATCHNO] - 实体类
    /// </summary>
    [Entity(TableName = "PROC_GET_BINSTORE_BATCHNO", Description = "PROC_GET_BINSTORE_BATCHNO")]
    public class ProcGetBinstoreBatchno : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "I_BTIME", Description = "", DbType = "DATE")]
        public DateTime? IBtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "I_EDATE", Description = "", DbType = "DATE")]
        public DateTime? IEdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "I_BATCH_NO", Description = "", DbType = "VARCHAR2")]
        public string IBatchNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "O_CURTABLE", Description = "", DbType = "REF CURSOR")]
        public DataTable OCurtable { get; set; }
        /// <summary>
        /// 存储过程返回 DataSet 数据
        /// </summary>
        public DataSet ProcedureDataSetResult { get; set; }
    }
}
