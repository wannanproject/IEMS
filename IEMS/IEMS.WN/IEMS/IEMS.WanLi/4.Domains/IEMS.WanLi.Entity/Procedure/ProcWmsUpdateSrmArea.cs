using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PROC_WMS_UPDATE_SRM_AREA [PROC_WMS_UPDATE_SRM_AREA] - 实体类
    /// </summary>
    [Entity(TableName = "PROC_WMS_UPDATE_SRM_AREA", Description = "PROC_WMS_UPDATE_SRM_AREA")]
    public class ProcWmsUpdateSrmArea : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "I_CRN_NO", Description = "", DbType = "VARCHAR2")]
        public string ICrnNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "I_MIN_COL", Description = "", DbType = "NUMBER")]
        public decimal? IMinCol { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "I_MAX_COL", Description = "", DbType = "NUMBER")]
        public decimal? IMaxCol { get; set; }
        /// <summary>
        /// 存储过程返回 DataSet 数据
        /// </summary>
        public DataSet ProcedureDataSetResult { get; set; }
    }
}
