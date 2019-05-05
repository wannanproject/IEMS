using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PSB_GRADE [PSB_GRADE] - 实体类
    /// </summary>
    [Entity(TableName = "PSB_GRADE", Description = "PSB_GRADE")]
    public class PsbGrade : BaseEntity
    {
        /// <summary>
        /// 等级编码
        /// </summary>
        [Field(FieldName = "GRADE_NO", Description = "等级编码",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public string GradeNo { get; set; }
        /// <summary>
        /// 等级描述
        /// </summary>
        [Field(FieldName = "GRADE_DESC", Description = "等级描述",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string GradeDesc { get; set; }
    }
}
