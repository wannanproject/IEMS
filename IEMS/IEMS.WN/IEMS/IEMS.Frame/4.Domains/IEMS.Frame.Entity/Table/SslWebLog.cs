using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Frame.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 增、删、改必须记录。 [SSL_WEB_LOG] - 实体类
    /// </summary>
    [Entity(TableName = "SSL_WEB_LOG", Description = "增、删、改必须记录。")]
    public class SslWebLog : BaseEntity
    {
        /// <summary>
        /// 自增主键编号
        /// </summary>
        [Field(FieldName = "OBJID", Description = "自增主键编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public long? ObjId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        [Field(FieldName = "USER_ID", Description = "用户编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? UserId { get; set; }
        /// <summary>
        /// 页面编号
        /// </summary>
        [Field(FieldName = "PAGE_ID", Description = "页面编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? PageId { get; set; }
        /// <summary>
        /// 函数名称
        /// </summary>
        [Field(FieldName = "METHOD_ID", Description = "函数名称",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? MethodId { get; set; }
        /// <summary>
        /// 用户ip地址
        /// </summary>
        [Field(FieldName = "USER_IP", Description = "用户ip地址",
               DbType = "NVARCHAR2(450)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string UserIp { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [Field(FieldName = "SHOW_NAME", Description = "显示名称",
               DbType = "NVARCHAR2(4000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ShowName { get; set; }
        /// <summary>
        /// 函数执行结果
        /// </summary>
        [Field(FieldName = "METHOD_RESULT", Description = "函数执行结果",
               DbType = "NVARCHAR2(4000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MethodResult { get; set; }
        /// <summary>
        /// 页面提交数据
        /// </summary>
        [Field(FieldName = "PAGE_REQUEST", Description = "页面提交数据",
               DbType = "NVARCHAR2(4000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PageRequest { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Field(FieldName = "REMARK", Description = "备注",
               DbType = "NVARCHAR2(4000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Remark { get; set; }
        /// <summary>
        /// 记录人编号
        /// </summary>
        [Field(FieldName = "RECORD_USER_ID", Description = "记录人编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? RecordUserId { get; set; }
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
    }
}
