using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 物料信息表 [PSB_MATERIAL] - 实体类
    /// </summary>
    [Entity(TableName = "PSB_MATERIAL", Description = "物料信息表")]
    public class PsbMaterial : BaseEntity
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        [Field(FieldName = "MATER_NO", Description = "物料编码",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public string MaterNo { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Field(FieldName = "MATER_NAME", Description = "物料名称",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MaterName { get; set; }
        /// <summary>
        /// 物料编号展示
        /// </summary>
        [Field(FieldName = "MATER_CODE", Description = "物料编号展示",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MaterCode { get; set; }
        /// <summary>
        /// 大类
        /// </summary>
        [Field(FieldName = "MATER_MKIND", Description = "大类",
               DbType = "VARCHAR2(2)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MaterMkind { get; set; }
        /// <summary>
        /// 物料分类
        /// </summary>
        [Field(FieldName = "MATER_TYPE", Description = "物料分类",
               DbType = "VARCHAR2(4)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MaterType { get; set; }
        /// <summary>
        /// 最短时间（时间单位：小时）
        /// </summary>
        [Field(FieldName = "MIN_TIME", Description = "最短时间（时间单位：小时）",
               DbType = "NUMBER(10,2)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? MinTime { get; set; }
        /// <summary>
        /// 最长时间（时间单位：小时）
        /// </summary>
        [Field(FieldName = "MAX_TIME", Description = "最长时间（时间单位：小时）",
               DbType = "NUMBER(10,2)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? MaxTime { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        [Field(FieldName = "MAIN_UNIT", Description = "计量单位",
               DbType = "VARCHAR2(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MainUnit { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [Field(FieldName = "MATER_SPEC", Description = "规格",
               DbType = "VARCHAR2(200)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MaterSpec { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Field(FieldName = "MATER_DESC", Description = "描述",
               DbType = "VARCHAR2(200)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MaterDesc { get; set; }
        /// <summary>
        /// 同步时间
        /// </summary>
        [Field(FieldName = "CREATION_TIME", Description = "同步时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? CreationTime { get; set; }
        /// <summary>
        /// 大类名称
        /// </summary>
        [Field(FieldName = "MKIND_NAME", Description = "大类名称",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MkindName { get; set; }
        /// <summary>
        /// 小类名称
        /// </summary>
        [Field(FieldName = "IKIND_NAME", Description = "小类名称",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string IkindName { get; set; }
        /// <summary>
        /// 入库允许误差
        /// </summary>
        [Field(FieldName = "ERR_WEIGHT", Description = "入库允许误差",
               DbType = "NUMBER(10,3)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? ErrWeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "SEQ_INDEX", Description = "",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? SeqIndex { get; set; }
        /// <summary>
        /// 删除标记 1：删除 0：正常
        /// </summary>
        [Field(FieldName = "DEL_FLAG", Description = "删除标记 1：删除 0：正常",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? DelFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "MES_NO", Description = "",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MesNo { get; set; }
    }
}
