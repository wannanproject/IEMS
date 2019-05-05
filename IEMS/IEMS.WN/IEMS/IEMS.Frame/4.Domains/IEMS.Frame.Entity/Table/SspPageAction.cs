using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Frame.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 系统权限管理-系统权限 [SSP_PAGE_ACTION] - 实体类
    /// </summary>
    [Entity(TableName = "SSP_PAGE_ACTION", Description = "系统权限管理-系统权限")]
    public class SspPageAction : BaseEntity
    {
        /// <summary>
        /// 自增主键编号
        /// </summary>
        [Field(FieldName = "OBJID", Description = "自增主键编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public long? ObjId { get; set; }
        /// <summary>
        /// 页面ID
        /// </summary>
        [Field(FieldName = "PAGE_MENU_ID", Description = "页面ID",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? PageMenuId { get; set; }
        /// <summary>
        /// 功能ID
        /// </summary>
        [Field(FieldName = "ACTION_ID", Description = "功能ID",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? ActionId { get; set; }
        /// <summary>
        /// 功能操作名
        /// </summary>
        [Field(FieldName = "ACTION_NAME", Description = "功能操作名",
               DbType = "NVARCHAR2(450)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ActionName { get; set; }
        /// <summary>
        /// 功能页面
        /// </summary>
        [Field(FieldName = "ACTION_URL", Description = "功能页面",
               DbType = "NVARCHAR2(450)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ActionUrl { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [Field(FieldName = "SHOW_NAME", Description = "显示名称",
               DbType = "NVARCHAR2(450)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ShowName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Field(FieldName = "REMARK", Description = "备注",
               DbType = "NVARCHAR2(900)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Remark { get; set; }
        /// <summary>
        /// 标题图标
        /// </summary>
        [Field(FieldName = "ICO_NAME", Description = "标题图标",
               DbType = "NVARCHAR2(450)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string IcoName { get; set; }
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
        /// 外部接口编号
        /// </summary>
        [Field(FieldName = "FOREIGN_NO", Description = "外部接口编号",
               DbType = "NVARCHAR2(900)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ForeignNo { get; set; }
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
