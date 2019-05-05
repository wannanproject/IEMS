using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// WBS_ORDER_LINE [WBS_ORDER_LINE] - 实体类
    /// </summary>
    [Entity(TableName = "WBS_ORDER_LINE", Description = "WBS_ORDER_LINE")]
    public class WbsOrderLine : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "ORDER_LINE_GUID", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string OrderLineGuid { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        [Field(FieldName = "ORDER_NO", Description = "单号",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string OrderNo { get; set; }
        /// <summary>
        /// 单LINE
        /// </summary>
        [Field(FieldName = "LINE_ID", Description = "单LINE",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? LineId { get; set; }
        /// <summary>
        /// 单据行状态  0：等待  1：下达  2：  执行  3： 完毕  4： 取消  
        /// </summary>
        [Field(FieldName = "LINE_STATUS", Description = "单据行状态  0：等待  1：下达  2：  执行  3： 完毕  4： 取消",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? LineStatus { get; set; }
        /// <summary>
        /// 是否已经删除（Y:已删除，N:未删除）
        /// </summary>
        [Field(FieldName = "DELETE_FLAG", Description = "是否已经删除（Y:已删除，N:未删除）",
               DbType = "VARCHAR2(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string DeleteFlag { get; set; }
        /// <summary>
        /// 此单发货顺序
        /// </summary>
        [Field(FieldName = "SORT_ID", Description = "此单发货顺序",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? SortId { get; set; }
        /// <summary>
        /// 【必填项】物料编码
        /// </summary>
        [Field(FieldName = "MATERIAL_NO", Description = "【必填项】物料编码",
               DbType = "VARCHAR2(30)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MaterialNo { get; set; }
        /// <summary>
        /// 【必填项】品级
        /// </summary>
        [Field(FieldName = "PRODUCT_GRADE", Description = "【必填项】品级",
               DbType = "VARCHAR2(64)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ProductGrade { get; set; }
        /// <summary>
        /// 发起站台(库位)
        /// </summary>
        [Field(FieldName = "SLOC_NO", Description = "发起站台(库位)",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string SlocNo { get; set; }
        /// <summary>
        /// 目标站台(库位)
        /// </summary>
        [Field(FieldName = "ELOC_NO", Description = "目标站台(库位)",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ElocNo { get; set; }
        /// <summary>
        /// 单据数量  单据生成时需求数量
        /// </summary>
        [Field(FieldName = "REQUIRE_QTY", Description = "单据数量  单据生成时需求数量",
               DbType = "NUMBER(20,4)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public decimal? RequireQty { get; set; }
        /// <summary>
        /// 已经生成任务的数量
        /// </summary>
        [Field(FieldName = "ACT_QTY", Description = "已经生成任务的数量",
               DbType = "NUMBER(20,4)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? ActQty { get; set; }
        /// <summary>
        /// 实发数量
        /// </summary>
        [Field(FieldName = "SHIP_QTY", Description = "实发数量",
               DbType = "NUMBER(20,4)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? ShipQty { get; set; }
        /// <summary>
        /// 限制-开始时间
        /// </summary>
        [Field(FieldName = "LIMIT_DATE_START", Description = "限制-开始时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? LimitDateStart { get; set; }
        /// <summary>
        /// 限制-结束时间
        /// </summary>
        [Field(FieldName = "LIMIT_DATE_END", Description = "限制-结束时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? LimitDateEnd { get; set; }
        /// <summary>
        /// 限制-库位    0
        /// </summary>
        [Field(FieldName = "LIMIT_BIN_NO", Description = "限制-库位    0",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LimitBinNo { get; set; }
        /// <summary>
        /// 限制-货物GUID   2
        /// </summary>
        [Field(FieldName = "LIMIT_PRODUCT_GUID", Description = "限制-货物GUID   2",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LimitProductGuid { get; set; }
        /// <summary>
        /// 限制-批次号
        /// </summary>
        [Field(FieldName = "LIMIT_BATCH_NO", Description = "限制-批次号",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LimitBatchNo { get; set; }
        /// <summary>
        /// 限制-托盘号   1
        /// </summary>
        [Field(FieldName = "LIMIT_PALLET_ID", Description = "限制-托盘号   1",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LimitPalletId { get; set; }
        /// <summary>
        /// 行项目优先级
        /// </summary>
        [Field(FieldName = "LINE_PRIORITY", Description = "行项目优先级",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? LinePriority { get; set; }
        /// <summary>
        /// 最后生成任务时间
        /// </summary>
        [Field(FieldName = "LAST_TASK_TIME", Description = "最后生成任务时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? LastTaskTime { get; set; }
        /// <summary>
        /// 库存异常信息
        /// </summary>
        [Field(FieldName = "BIN_ERR_DESC", Description = "库存异常信息",
               DbType = "NVARCHAR2(1000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string BinErrDesc { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Field(FieldName = "FMEM", Description = "备注",
               DbType = "NVARCHAR2(1000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Fmem { get; set; }
        /// <summary>
        /// 锁定目标 1：锁定 0：未锁定
        /// </summary>
        [Field(FieldName = "LOCK_END_LOC", Description = "锁定目标 1：锁定 0：未锁定",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? LockEndLoc { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Field(FieldName = "CREATION_DATE", Description = "创建日期",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        [Field(FieldName = "PAREA_NO", Description = "区域编码",
               DbType = "VARCHAR2(50)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PareaNo { get; set; }
        /// <summary>
        /// 限制-绑定单号
        /// </summary>
        [Field(FieldName = "BIND_ORDER_NO", Description = "限制-绑定单号",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string BindOrderNo { get; set; }
        /// <summary>
        /// 出库任务分解 1分解  0:等待
        /// </summary>
        [Field(FieldName = "LINE_TASK_FLAG", Description = "出库任务分解 1分解  0:等待",
               DbType = "NUMBER(2)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? LineTaskFlag { get; set; }
    }
}
