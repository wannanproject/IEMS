using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PSB_PALLET [PSB_PALLET] - 实体类
    /// </summary>
    [Entity(TableName = "PSB_PALLET", Description = "PSB_PALLET")]
    public class PsbPallet : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PALLET_NO", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public string PalletNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "RFID_NO", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string RfidNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PAINT_NO", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string PaintNo { get; set; }
        /// <summary>
        /// A,B,C,D,E
        /// </summary>
        [Field(FieldName = "PALLET_TYPE", Description = "A,B,C,D,E",
               DbType = "VARCHAR2(240)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PALLET_NAME", Description = "",
               DbType = "NVARCHAR2(480)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string PalletName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "USED_FLAG", Description = "",
               DbType = "CHAR(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string UsedFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PALLET_WEIGHT", Description = "",
               DbType = "NUMBER(12,4)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? PalletWeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PRODUCT_GUID", Description = "",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ProductGuid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "LAST_PRODUCT_GUID", Description = "",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LastProductGuid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "BINDING_TIME", Description = "",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? BindingTime { get; set; }
        /// <summary>
        /// 1:有载 0：空载
        /// </summary>
        [Field(FieldName = "LOAD_STATUS", Description = "1:有载 0：空载",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? LoadStatus { get; set; }
        /// <summary>
        /// 1:正常 0：异常
        /// </summary>
        [Field(FieldName = "ERROR_STATUS", Description = "1:正常 0：异常",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? ErrorStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "FMEM", Description = "",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Fmem { get; set; }
        /// <summary>
        /// 产品号
        /// </summary>
        [Field(FieldName = "PRODUCT_NO", Description = "产品号",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ProductNo { get; set; }
        /// <summary>
        /// 检验结果
        /// </summary>
        [Field(FieldName = "CHECK_RESULT", Description = "检验结果",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? CheckResult { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "BATCH_NO", Description = "",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string BatchNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PORT_NO", Description = "",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PortNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PLAN_NAME", Description = "",
               DbType = "VARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PlanName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "REAL_WEIGHT", Description = "",
               DbType = "NUMBER(12,4)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? RealWeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "ERR_WEIGHT", Description = "",
               DbType = "NUMBER(12,4)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? ErrWeight { get; set; }
    }
}
