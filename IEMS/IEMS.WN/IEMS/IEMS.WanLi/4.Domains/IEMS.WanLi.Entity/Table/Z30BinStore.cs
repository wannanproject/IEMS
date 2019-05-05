using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 库存统计 [Z30_BIN_STORE] - 实体类
    /// </summary>
    [Entity(TableName = "Z30_BIN_STORE", Description = "库存统计")]
    public class Z30BinStore : BaseEntity
    {
        /// <summary>
        /// 存货编码
        /// </summary>
        [Field(FieldName = "PRODUCT_ID", Description = "存货编码",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ProductId { get; set; }
        /// <summary>
        /// MI号
        /// </summary>
        [Field(FieldName = "MINO", Description = "MI号",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Mino { get; set; }
        /// <summary>
        /// 分选档位
        /// </summary>
        [Field(FieldName = "GRADE", Description = "分选档位",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Grade { get; set; }
        /// <summary>
        /// 最早分选时间
        /// </summary>
        [Field(FieldName = "MIN_DATE", Description = "最早分选时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? MinDate { get; set; }
        /// <summary>
        /// 最晚分选时间
        /// </summary>
        [Field(FieldName = "MAX_DATE", Description = "最晚分选时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? MaxDate { get; set; }
        /// <summary>
        /// 电芯数量
        /// </summary>
        [Field(FieldName = "AQTY", Description = "电芯数量",
               DbType = "NUMBER", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? Aqty { get; set; }
        /// <summary>
        /// 所在托盘数
        /// </summary>
        [Field(FieldName = "PQTY", Description = "所在托盘数",
               DbType = "NUMBER", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? Pqty { get; set; }
    }
}
