using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Main.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 系统基础资料-人员信息 [SSB_USER] - 实体类
    /// </summary>
    [Entity(TableName = "SSB_USER", Description = "系统基础资料-人员信息")]
    public class SsbUser : BaseEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Field(FieldName = "OBJID", Description = "用户编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public long? ObjId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [Field(FieldName = "USER_NAME", Description = "用户名称",
               DbType = "NVARCHAR2(450)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [Field(FieldName = "USER_PWD", Description = "用户密码",
               DbType = "NVARCHAR2(900)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string UserPwd { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [Field(FieldName = "REAL_NAME", Description = "真实姓名",
               DbType = "NVARCHAR2(450)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string RealName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Field(FieldName = "SEX", Description = "性别",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? Sex { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [Field(FieldName = "TELEPHONE", Description = "电话",
               DbType = "NVARCHAR2(450)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Telephone { get; set; }
        /// <summary>
        /// 用户工号
        /// </summary>
        [Field(FieldName = "WORK_BARCODE", Description = "用户工号",
               DbType = "NVARCHAR2(180)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string WorkBarcode { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        [Field(FieldName = "DEPT_ID", Description = "所属部门",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? DeptId { get; set; }
        /// <summary>
        /// 所属岗位
        /// </summary>
        [Field(FieldName = "WORK_ID", Description = "所属岗位",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? WorkId { get; set; }
        /// <summary>
        /// 所属班次
        /// </summary>
        [Field(FieldName = "SHIFT_ID", Description = "所属班次",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? ShiftId { get; set; }
        /// <summary>
        /// 所属班组
        /// </summary>
        [Field(FieldName = "CLASS_ID", Description = "所属班组",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? ClassId { get; set; }
        /// <summary>
        /// 所属车间
        /// </summary>
        [Field(FieldName = "WORKSHOP_ID", Description = "所属车间",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? WorkshopId { get; set; }
        /// <summary>
        /// 删除标志
        /// </summary>
        [Field(FieldName = "DELETE_FLAG", Description = "删除标志",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? DeleteFlag { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Field(FieldName = "REMARK", Description = "备注",
               DbType = "NVARCHAR2(900)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Remark { get; set; }
        /// <summary>
        /// 普通员工
        /// </summary>
        [Field(FieldName = "IS_EMPLOYEE", Description = "普通员工",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? IsEmployee { get; set; }
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
        /// 用户名称，用户手持等登录
        /// </summary>
        [Field(FieldName = "USER_NO", Description = "用户名称，用户手持等登录",
               DbType = "NVARCHAR2(450)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string UserNo { get; set; }
    }
}
