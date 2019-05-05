using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 存储过程参数 - 数据日志备份 [TPROC_DATA_BACKUP] - 实体类
    /// </summary>
    [Entity(TableName = "TPROC_DATA_BACKUP", Description = "存储过程参数 - 数据日志备份")]
    public class TprocDataBackup : BaseEntity
    {
        /// <summary>
        /// 主键  编号
        /// </summary>
        [Field(FieldName = "OBJID", Description = "主键  编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public long? ObjId { get; set; }
        /// <summary>
        /// 错误编号
        /// </summary>
        [Field(FieldName = "ERR_CODE", Description = "错误编号",
               DbType = "NUMBER", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public decimal? ErrCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [Field(FieldName = "ERR_DESC", Description = "错误信息",
               DbType = "NVARCHAR2(1500)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ErrDesc { get; set; }
        /// <summary>
        /// 0 为执行  1已执行
        /// </summary>
        [Field(FieldName = "PROC_STATUS", Description = "0 为执行  1已执行",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? ProcStatus { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        [Field(FieldName = "PROC_CREATE_TIME", Description = "启动时间",
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
        /// 完成时间
        /// </summary>
        [Field(FieldName = "PROC_FINISH_TIME", Description = "完成时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? ProcFinishTime { get; set; }
        /// <summary>
        /// 备份类型
        /// </summary>
        [Field(FieldName = "DATA_TYPE", Description = "备份类型",
               DbType = "VARCHAR2(200)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string DataType { get; set; }
        /// <summary>
        /// C:创建  R:查询 U:更新 D:删除
        /// </summary>
        [Field(FieldName = "CRUD_TYPE", Description = "C:创建  R:查询 U:更新 D:删除",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string CrudType { get; set; }
        /// <summary>
        /// 备份系统
        /// </summary>
        [Field(FieldName = "BACKUP_SYS", Description = "备份系统",
               DbType = "VARCHAR2(200)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string BackupSys { get; set; }
        /// <summary>
        /// 备份系统 OBJID
        /// </summary>
        [Field(FieldName = "BACKUP_SYS_OBJID", Description = "备份系统 OBJID",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? BackupSysObjid { get; set; }
        /// <summary>
        /// 备份函数
        /// </summary>
        [Field(FieldName = "BACKUP_METHOD", Description = "备份函数",
               DbType = "VARCHAR2(400)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string BackupMethod { get; set; }
        /// <summary>
        /// 备份位置
        /// </summary>
        [Field(FieldName = "BACKUP_POINT", Description = "备份位置",
               DbType = "NVARCHAR2(3600)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string BackupPoint { get; set; }
        /// <summary>
        /// 参数1
        /// </summary>
        [Field(FieldName = "PARAM_01", Description = "参数1",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Param01 { get; set; }
        /// <summary>
        /// 参数2
        /// </summary>
        [Field(FieldName = "PARAM_02", Description = "参数2",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Param02 { get; set; }
        /// <summary>
        /// 参数3
        /// </summary>
        [Field(FieldName = "PARAM_03", Description = "参数3",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Param03 { get; set; }
        /// <summary>
        /// 参数4
        /// </summary>
        [Field(FieldName = "PARAM_04", Description = "参数4",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Param04 { get; set; }
        /// <summary>
        /// 参数5
        /// </summary>
        [Field(FieldName = "PARAM_05", Description = "参数5",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Param05 { get; set; }
        /// <summary>
        /// 参数6
        /// </summary>
        [Field(FieldName = "PARAM_06", Description = "参数6",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Param06 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "GLOBAL_GUID", Description = "",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string GlobalGuid { get; set; }
    }
}
