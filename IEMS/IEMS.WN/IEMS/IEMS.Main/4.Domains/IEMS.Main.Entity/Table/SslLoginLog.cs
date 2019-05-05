using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Main.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// SSL_LOGIN_LOG [SSL_LOGIN_LOG] - 实体类
    /// </summary>
    [Entity(TableName = "SSL_LOGIN_LOG", Description = "SSL_LOGIN_LOG")]
    public class SslLoginLog : BaseEntity
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
        /// 登录时间
        /// </summary>
        [Field(FieldName = "LOGIN_TIME", Description = "登录时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? LoginTime { get; set; }
        /// <summary>
        /// 登录ip地址
        /// </summary>
        [Field(FieldName = "LOGIN_IP", Description = "登录ip地址",
               DbType = "NVARCHAR2(4000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LoginIp { get; set; }
        /// <summary>
        /// 退出时间
        /// </summary>
        [Field(FieldName = "LOGOUT_TIME", Description = "退出时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? LogoutTime { get; set; }
        /// <summary>
        /// 退出ip地址
        /// </summary>
        [Field(FieldName = "LOGOUT_IP", Description = "退出ip地址",
               DbType = "NVARCHAR2(4000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LogoutIp { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Field(FieldName = "REMARK", Description = "备注",
               DbType = "NVARCHAR2(4000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Remark { get; set; }
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
