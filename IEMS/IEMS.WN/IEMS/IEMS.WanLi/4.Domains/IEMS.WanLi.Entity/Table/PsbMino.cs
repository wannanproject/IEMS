using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PSB_MINO [PSB_MINO] - 实体类
    /// </summary>
    [Entity(TableName = "PSB_MINO", Description = "PSB_MINO")]
    public class PsbMino : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "MINO", Description = "",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public string Mino { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "MIDSC", Description = "",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Midsc { get; set; }
    }
}
