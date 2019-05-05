using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Main.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 系统权限管理-部门角色表 [SSP_DEPT_ROLE] - 实体类
    /// </summary>
    [Entity(TableName = "SSP_DEPT_ROLE", Description = "系统权限管理-部门角色表")]
    public class SspDeptRole : BaseEntity
    {
        /// <summary>
        /// 自增主键序列
        /// </summary>
        [Field(FieldName = "OBJID", Description = "自增主键序列",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public long? ObjId { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [Field(FieldName = "DEPT_ID", Description = "部门编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? DeptId { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        [Field(FieldName = "ROLE_ID", Description = "角色编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? RoleId { get; set; }
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
        /// 外部接口编号
        /// </summary>
        [Field(FieldName = "FOREIGN_NO", Description = "外部接口编号",
               DbType = "NVARCHAR2(900)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ForeignNo { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        [Field(FieldName = "SEQ_INDEX", Description = "排序列",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public long? SeqIndex { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Field(FieldName = "REMARK", Description = "备注",
               DbType = "NVARCHAR2(1800)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Remark { get; set; }
        /// <summary>
        /// 删除标志
        /// </summary>
        [Field(FieldName = "DELETE_FLAG", Description = "删除标志",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? DeleteFlag { get; set; }
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
    }
}
