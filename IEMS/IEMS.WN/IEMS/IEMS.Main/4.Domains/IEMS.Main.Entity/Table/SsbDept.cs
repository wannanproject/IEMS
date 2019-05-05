using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Main.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 系统基础资料-部门信息 [SSB_DEPT] - 实体类
    /// </summary>
    [Entity(TableName = "SSB_DEPT", Description = "系统基础资料-部门信息")]
    public class SsbDept : BaseEntity
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        [Field(FieldName = "OBJID", Description = "部门编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public long? ObjId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Field(FieldName = "DEPT_NAME", Description = "部门名称",
               DbType = "NVARCHAR2(900)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string DeptName { get; set; }
        /// <summary>
        /// 部门级别
        /// </summary>
        [Field(FieldName = "DEPT_LEVEL", Description = "部门级别",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? DeptLevel { get; set; }
        /// <summary>
        /// 上级部门编号
        /// </summary>
        [Field(FieldName = "PARENT_ID", Description = "上级部门编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? ParentId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Field(FieldName = "REMARK", Description = "备注",
               DbType = "NVARCHAR2(900)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Remark { get; set; }
        /// <summary>
        /// 显示序号
        /// </summary>
        [Field(FieldName = "DISPLAY_ID", Description = "显示序号",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? DisplayId { get; set; }
        /// <summary>
        /// 删除标志
        /// </summary>
        [Field(FieldName = "DELETE_FLAG", Description = "删除标志",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? DeleteFlag { get; set; }
        /// <summary>
        /// 备份标识
        /// </summary>
        [Field(FieldName = "BAKUP_FLAG", Description = "备份标识",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? BakupFlag { get; set; }
        /// <summary>
        /// 备份时间
        /// </summary>
        [Field(FieldName = "BAKUP_TIME", Description = "备份时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? BakupTime { get; set; }
        /// <summary>
        /// 记录人
        /// </summary>
        [Field(FieldName = "RECORD_USER_ID", Description = "记录人",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public long? RecordUserId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [Field(FieldName = "RECORD_TIME", Description = "记录时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public DateTime? RecordTime { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        [Field(FieldName = "SEQ_INDEX", Description = "排序列",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public long? SeqIndex { get; set; }
        /// <summary>
        /// 备份标志
        /// </summary>
        [Field(FieldName = "BACKUP_FLAG", Description = "备份标志",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? BackupFlag { get; set; }
        /// <summary>
        /// 备份时间
        /// </summary>
        [Field(FieldName = "BACKUP_TIME", Description = "备份时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public DateTime? BackupTime { get; set; }
        /// <summary>
        /// 外部接口编号
        /// </summary>
        [Field(FieldName = "FOREIGN_NO", Description = "外部接口编号",
               DbType = "NVARCHAR2(1800)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ForeignNo { get; set; }
    }
}
