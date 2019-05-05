using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Main.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 系统基础资料-配置信息 [SSB_CONFIG] - 实体类
    /// </summary>
    [Entity(TableName = "SSB_CONFIG", Description = "系统基础资料-配置信息")]
    public class SsbConfig : BaseEntity
    {
        /// <summary>
        /// 班组编号
        /// </summary>
        [Field(FieldName = "OBJID", Description = "班组编号",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public int? ObjId { get; set; }
        /// <summary>
        /// 配置项名称
        /// </summary>
        [Field(FieldName = "CONFIG_KEY", Description = "配置项名称",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ConfigKey { get; set; }
        /// <summary>
        /// 配置项值
        /// </summary>
        [Field(FieldName = "CONFIG_VALUE", Description = "配置项值",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ConfigValue { get; set; }
        /// <summary>
        /// 删除标志
        /// </summary>
        [Field(FieldName = "DELETE_FLAG", Description = "删除标志",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? DeleteFlag { get; set; }
        /// <summary>
        /// 记录人编号
        /// </summary>
        [Field(FieldName = "RECORD_USER_ID", Description = "记录人编号",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? RecordUserId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [Field(FieldName = "RECORD_TIME", Description = "记录时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? RecordTime { get; set; }
        /// <summary>
        /// 备份标识
        /// </summary>
        [Field(FieldName = "BAKUP_FLAG", Description = "备份标识",
               DbType = "NUMBER(10)", DefaultValue = "",
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
        /// 备注
        /// </summary>
        [Field(FieldName = "REMARK", Description = "备注",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Remark { get; set; }
    }
}
