using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 订单监控 [Z30_LOC_0102_ORDER_MONITOR] - 实体类
    /// </summary>
    [Entity(TableName = "Z30_LOC_0102_ORDER_MONITOR", Description = "订单监控")]
    public class Z30Loc0102OrderMonitor : BaseEntity
    {
        /// <summary>
        /// 站台号码
        /// </summary>
        [Field(FieldName = "LOC_NO", Description = "站台号码",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LocNo { get; set; }
        /// <summary>
        /// 工装编码
        /// </summary>
        [Field(FieldName = "PALLET_NO", Description = "工装编码",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletNo { get; set; }
        /// <summary>
        /// 请求标志 1：有请求  0：无请求
        /// </summary>
        [Field(FieldName = "REQUEST_FLAG", Description = "请求标志 1：有请求  0：无请求",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? RequestFlag { get; set; }
        /// <summary>
        /// 物料GUID
        /// </summary>
        [Field(FieldName = "PRODUCT_GUID", Description = "物料GUID",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ProductGuid { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Field(FieldName = "PRODUCT_NO", Description = "物料编码",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ProductNo { get; set; }
        /// <summary>
        /// 1:有载  0：空载
        /// </summary>
        [Field(FieldName = "LOADED_STATUS", Description = "1:有载  0：空载",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? LoadedStatus { get; set; }
        /// <summary>
        /// 请求日期
        /// </summary>
        [Field(FieldName = "REQUEST_DATE", Description = "请求日期",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? RequestDate { get; set; }
        /// <summary>
        /// 订单号码
        /// </summary>
        [Field(FieldName = "ORDER_NO", Description = "订单号码",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string OrderNo { get; set; }
        /// <summary>
        /// 重试次数
        /// </summary>
        [Field(FieldName = "RETRY_COUNT", Description = "重试次数",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public long? RetryCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PALLET_VALID", Description = "",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletValid { get; set; }
        /// <summary>
        /// 错误编码
        /// </summary>
        [Field(FieldName = "ERR_CODE", Description = "错误编码",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? ErrCode { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        [Field(FieldName = "ERR_DESC", Description = "错误描述",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ErrDesc { get; set; }
        /// <summary>
        /// 发生业务累计数量
        /// </summary>
        [Field(FieldName = "BIZ_COUNT", Description = "发生业务累计数量",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? BizCount { get; set; }
        /// <summary>
        /// 生成的包号
        /// </summary>
        [Field(FieldName = "PACKAGE_GUID", Description = "生成的包号",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PackageGuid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "ORDER_REQUEST_OBJID", Description = "",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? OrderRequestObjid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "ORDER_LINE_GUID", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string OrderLineGuid { get; set; }
    }
}
