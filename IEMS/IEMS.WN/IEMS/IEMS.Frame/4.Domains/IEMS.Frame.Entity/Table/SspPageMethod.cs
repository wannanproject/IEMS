using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Frame.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// SSP_PAGE_METHOD [SSP_PAGE_METHOD] - 实体类
    /// </summary>
    [Entity(TableName = "SSP_PAGE_METHOD", Description = "SSP_PAGE_METHOD")]
    public class SspPageMethod : BaseEntity
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
        [Field(FieldName = "PAGE_ID", Description = "页面ID",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? PageId { get; set; }
        /// <summary>
        /// 函数名称
        /// </summary>
        [Field(FieldName = "METHOD_NAME", Description = "函数名称",
               DbType = "NVARCHAR2(4000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MethodName { get; set; }
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
        /// 排序字段
        /// </summary>
        [Field(FieldName = "SEQ_INDEX", Description = "排序字段",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? SeqIndex { get; set; }
        /// <summary>
        /// 备注标识
        /// </summary>
        [Field(FieldName = "BAKUP_FLAG", Description = "备注标识",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? BakupFlag { get; set; }
        /// <summary>
        /// 备注时间
        /// </summary>
        [Field(FieldName = "BAKUP_TIME", Description = "备注时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? BakupTime { get; set; }
    }
}
