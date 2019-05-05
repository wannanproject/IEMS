using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// Z30_LOC_0301_CMD_MONITOR [Z30_LOC_0301_CMD_MONITOR] - 实体类
    /// </summary>
    [Entity(TableName = "Z30_LOC_0301_CMD_MONITOR", Description = "Z30_LOC_0301_CMD_MONITOR")]
    public class Z30Loc0301CmdMonitor : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "LOC_NO", Description = "",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LocNo { get; set; }
        /// <summary>
        /// 0 无请求，1 有请求 ，2 有请求且已生成指令但尚未下传PLC
        /// </summary>
        [Field(FieldName = "REQUEST_FLAG", Description = "0 无请求，1 有请求 ，2 有请求且已生成指令但尚未下传PLC",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? RequestFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "REQUEST_DATE", Description = "",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? RequestDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "TASK_NO", Description = "",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? TaskNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "TASK_GUID", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string TaskGuid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "RETRY_COUNT", Description = "",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public long? RetryCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "CMD_OBJID", Description = "",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? CmdObjid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "ERR_CODE", Description = "",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public long? ErrCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "ERR_DESC", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ErrDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PALLET_NO", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PRODUCT_GUID", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ProductGuid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "LOADED_STATUS", Description = "",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? LoadedStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PACKAGE_GUID", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PackageGuid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PALLET_VALID", Description = "",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletValid { get; set; }
    }
}
