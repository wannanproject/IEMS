using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 订单请求参数表 [TPROC_0100_TASK_REQUEST] - 实体类
    /// </summary>
    [Entity(TableName = "TPROC_0100_TASK_REQUEST", Description = "订单请求参数表")]
    public class Tproc0100TaskRequest : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "OBJID", Description = "",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public long? ObjId { get; set; }
        /// <summary>
        /// 【返回值】：异常编码
        /// </summary>
        [Field(FieldName = "ERR_CODE", Description = "【返回值】：异常编码",
               DbType = "NUMBER", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public decimal? ErrCode { get; set; }
        /// <summary>
        /// 【返回值】：异常描述  
        /// </summary>
        [Field(FieldName = "ERR_DESC", Description = "【返回值】：异常描述",
               DbType = "NVARCHAR2(1000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ErrDesc { get; set; }
        /// <summary>
        /// 【返回值】：执行结果  0:未执行 1：执行中  2：执行结束    
        /// </summary>
        [Field(FieldName = "PROC_STATUS", Description = "【返回值】：执行结果  0:未执行 1：执行中  2：执行结束",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? ProcStatus { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Field(FieldName = "PROC_CREATE_TIME", Description = "创建日期",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public DateTime? ProcCreateTime { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        [Field(FieldName = "PROC_EXCUTE_TIME", Description = "执行时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? ProcExcuteTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Field(FieldName = "PROC_FINISH_TIME", Description = "结束时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? ProcFinishTime { get; set; }
        /// <summary>
        /// 【必填项】：订单类型                  
        /// </summary>
        [Field(FieldName = "ORDER_TYPE_NO", Description = "【必填项】：订单类型",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string OrderTypeNo { get; set; }
        /// <summary>
        /// 【必填项】：起始站台(库位)    
        /// </summary>
        [Field(FieldName = "SLOC_NO", Description = "【必填项】：起始站台(库位)",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string SlocNo { get; set; }
        /// <summary>
        /// 【必填项】：目标站台(库位)             
        /// </summary>
        [Field(FieldName = "ELOC_NO", Description = "【必填项】：目标站台(库位)",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ElocNo { get; set; }
        /// <summary>
        /// 【必填项】：来源类型  1：手持   2: WMS  3：WCS  
        /// </summary>
        [Field(FieldName = "SOURCE_TYPE", Description = "【必填项】：来源类型  1：手持   2: WMS  3：WCS",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? SourceType { get; set; }
        /// <summary>
        /// 【可选项】：订单数量 
        /// </summary>
        [Field(FieldName = "REQUIRE_QTY", Description = "【可选项】：订单数量",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? RequireQty { get; set; }
        /// <summary>
        /// 【可选项】：锁定目标      1；锁定  0:不锁定  
        /// </summary>
        [Field(FieldName = "LOCK_END_LOC", Description = "【可选项】：锁定目标      1；锁定  0:不锁定",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? LockEndLoc { get; set; }
        /// <summary>
        /// 【返回值】：唯一GUID
        /// </summary>
        [Field(FieldName = "GLOBAL_GUID", Description = "【返回值】：唯一GUID",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string GlobalGuid { get; set; }
        /// <summary>
        /// 【返回值】：目标位置类型
        /// </summary>
        [Field(FieldName = "ELOC_TYPE", Description = "【返回值】：目标位置类型",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ElocType { get; set; }
        /// <summary>
        /// 【返回值】：目标位置区域
        /// </summary>
        [Field(FieldName = "ELOC_AREA", Description = "【返回值】：目标位置区域",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ElocArea { get; set; }
        /// <summary>
        /// 【返回值】：路径
        /// </summary>
        [Field(FieldName = "ROUTE_NO", Description = "【返回值】：路径",
               DbType = "VARCHAR2(200)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string RouteNo { get; set; }
        /// <summary>
        /// 【返回值】：发起站台类型
        /// </summary>
        [Field(FieldName = "SLOC_TYPE", Description = "【返回值】：发起站台类型",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string SlocType { get; set; }
        /// <summary>
        /// 【返回值】：发起站台区域
        /// </summary>
        [Field(FieldName = "SLOC_AREA", Description = "【返回值】：发起站台区域",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string SlocArea { get; set; }
        /// <summary>
        /// 【返回值】：订单优先级
        /// </summary>
        [Field(FieldName = "ORDER_PRIORITY", Description = "【返回值】：订单优先级",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? OrderPriority { get; set; }
        /// <summary>
        /// 【返回值】：工装类型
        /// </summary>
        [Field(FieldName = "PALLET_TYPE", Description = "【返回值】：工装类型",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletType { get; set; }
        /// <summary>
        /// 【返回值】：订单类型
        /// </summary>
        [Field(FieldName = "ORDER_TYPE_MODULE", Description = "【返回值】：订单类型",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string OrderTypeModule { get; set; }
        /// <summary>
        /// 入库物料包唯一GUID
        /// </summary>
        [Field(FieldName = "PACKAGE_GUID", Description = "入库物料包唯一GUID",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PackageGuid { get; set; }
        /// <summary>
        /// 生成任务编号
        /// </summary>
        [Field(FieldName = "TASK_GUID", Description = "生成任务编号",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string TaskGuid { get; set; }
        /// <summary>
        /// 【必填项】：工装编码1
        /// </summary>
        [Field(FieldName = "PALLET_NO1", Description = "【必填项】：工装编码1",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletNo1 { get; set; }
        /// <summary>
        /// 【必填项】：工装编码2
        /// </summary>
        [Field(FieldName = "PALLET_NO2", Description = "【必填项】：工装编码2",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletNo2 { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        [Field(FieldName = "TASK_NO", Description = "任务编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? TaskNo { get; set; }
    }
}
