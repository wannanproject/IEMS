using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 库位类型 [PSB_LOC_TYPE] - 实体类
    /// </summary>
    [Entity(TableName = "PSB_LOC_TYPE", Description = "库位类型")]
    public class PsbLocType : BaseEntity
    {
        /// <summary>
        /// 类型编码
        /// </summary>
        [Field(FieldName = "LOC_TYPE_NO", Description = "类型编码",
               DbType = "VARCHAR2(10)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public string LocTypeNo { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [Field(FieldName = "LOC_TYPE_NAME", Description = "类型名称",
               DbType = "VARCHAR2(50)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LocTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "LOC_TYPE_INDEX", Description = "",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? LocTypeIndex { get; set; }
    }
}
