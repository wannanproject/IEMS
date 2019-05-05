using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// VIEW_PALLET [VIEW_PALLET] - 实体类
    /// </summary>
    [Entity(TableName = "VIEW_PALLET", Description = "VIEW_PALLET")]
    public class ViewPallet : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PALLET_NO", Description = "",
               DbType = "NVARCHAR2(240)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "RFID_NO", Description = "",
               DbType = "NVARCHAR2(240)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string RfidNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PAINT_CODE", Description = "",
               DbType = "NVARCHAR2(240)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string PaintCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PALLET_TYPE", Description = "",
               DbType = "NVARCHAR2(240)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PALLET_NAME", Description = "",
               DbType = "NVARCHAR2(240)", DefaultValue = "",
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
               DbType = "CHAR", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletWeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "PRODUCT_GUID", Description = "",
               DbType = "NVARCHAR2(240)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ProductGuid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "LAST_PRODUCT_GUID", Description = "",
               DbType = "NVARCHAR2(240)", DefaultValue = "",
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
        /// 
        /// </summary>
        [Field(FieldName = "LOAD_STATUS", Description = "",
               DbType = "NUMBER", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? LoadStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "ERROR_STATUS", Description = "",
               DbType = "CHAR(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ErrorStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "CHECKOUT_STATUS", Description = "",
               DbType = "CHAR(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string CheckoutStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "DEQUIP_CODE", Description = "",
               DbType = "NVARCHAR2(240)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string DequipCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "DLOG_NO", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string DlogNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "SLMS_FLAG", Description = "",
               DbType = "CHAR(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string SlmsFlag { get; set; }
    }
}
