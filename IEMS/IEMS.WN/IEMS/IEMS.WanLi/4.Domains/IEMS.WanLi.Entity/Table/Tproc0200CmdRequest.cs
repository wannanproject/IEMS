using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// TPROC_0200_CMD_REQUEST [TPROC_0200_CMD_REQUEST] - 实体类
    /// </summary>
    [Entity(TableName = "TPROC_0200_CMD_REQUEST", Description = "TPROC_0200_CMD_REQUEST")]
    public class Tproc0200CmdRequest : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "OBJID", Description = "",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public long? ObjId { get; set; }
        /// <summary>
        /// 错误号
        /// </summary>
        [Field(FieldName = "ERR_CODE", Description = "错误号",
               DbType = "NUMBER", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public decimal? ErrCode { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        [Field(FieldName = "ERR_DESC", Description = "错误描述",
               DbType = "NVARCHAR2(1000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ErrDesc { get; set; }
        /// <summary>
        /// 执行状态 0：未执行 1：开始执行 2：执行结束
        /// </summary>
        [Field(FieldName = "PROC_STATUS", Description = "执行状态 0：未执行 1：开始执行 2：执行结束",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? ProcStatus { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Field(FieldName = "PROC_CREATE_TIME", Description = "创建时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public DateTime? ProcCreateTime { get; set; }
        /// <summary>
        /// 开始执行时间
        /// </summary>
        [Field(FieldName = "PROC_EXCUTE_TIME", Description = "开始执行时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? ProcExcuteTime { get; set; }
        /// <summary>
        /// 执行结束时间
        /// </summary>
        [Field(FieldName = "PROC_FINISH_TIME", Description = "执行结束时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? ProcFinishTime { get; set; }
        /// <summary>
        /// 任务号
        /// </summary>
        [Field(FieldName = "TASK_NO", Description = "任务号",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? TaskNo { get; set; }
        /// <summary>
        /// 当前站台号
        /// </summary>
        [Field(FieldName = "CURR_LOC_NO", Description = "当前站台号",
               DbType = "VARCHAR2(40)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string CurrLocNo { get; set; }
        /// <summary>
        /// 工装号
        /// </summary>
        [Field(FieldName = "PALLET_NO", Description = "工装号",
               DbType = "VARCHAR2(40)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletNo { get; set; }
        /// <summary>
        /// 任务GUID
        /// </summary>
        [Field(FieldName = "TASK_GUID", Description = "任务GUID",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string TaskGuid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "GLOBAL_GUID", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string GlobalGuid { get; set; }
        /// <summary>
        /// 指令执行路径号
        /// </summary>
        [Field(FieldName = "ROUTE_NO", Description = "指令执行路径号",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string RouteNo { get; set; }
        /// <summary>
        /// 目的站台号
        /// </summary>
        [Field(FieldName = "ELOC_NO", Description = "目的站台号",
               DbType = "VARCHAR2(40)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ElocNo { get; set; }
        /// <summary>
        /// 检验有效性
        /// </summary>
        [Field(FieldName = "PALLET_VALID", Description = "检验有效性",
               DbType = "VARCHAR2(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletValid { get; set; }
    }
}
