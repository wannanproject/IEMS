using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 库位状态表 [PSB_BIN_STATUS] - 实体类
    /// </summary>
    [Entity(TableName = "PSB_BIN_STATUS", Description = "库位状态表")]
    public class PsbBinStatus : BaseEntity
    {
        /// <summary>
        /// 库位
        /// </summary>
        [Field(FieldName = "BIN_NO", Description = "库位",
               DbType = "VARCHAR2(8)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public string BinNo { get; set; }
        /// <summary>
        /// 可用状态：0：不可用  1：可用
        /// </summary>
        [Field(FieldName = "USED_FLAG", Description = "可用状态：0：不可用  1：可用",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? UsedFlag { get; set; }
        /// <summary>
        /// 库位大小(H High高, M MIDDLE中, L Low低, :无)
        /// </summary>
        [Field(FieldName = "BIN_SIZE", Description = "库位大小(H High高, M MIDDLE中, L Low低, :无)",
               DbType = "VARCHAR2(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string BinSize { get; set; }
        /// <summary>
        /// 库位状态（_ :空库，$ :库存）
        /// </summary>
        [Field(FieldName = "BIN_STATUS", Description = "库位状态（_ :空库，$ :库存）",
               DbType = "VARCHAR2(2)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string BinStatus { get; set; }
        /// <summary>
        /// 业务状态：  I :预约入库，O :预约出库，T:移库占用, E :空出库位 P :先入库位
        /// </summary>
        [Field(FieldName = "BIN_BIZ_STATUS", Description = "业务状态：  I :预约入库，O :预约出库，T:移库占用, E :空出库位 P :先入库位",
               DbType = "VARCHAR2(2)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string BinBizStatus { get; set; }
        /// <summary>
        /// 权重(被放置时的优先顺序,1最先,数字越大越后)
        /// </summary>
        [Field(FieldName = "BIN_WEIGHT", Description = "权重(被放置时的优先顺序,1最先,数字越大越后)",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? BinWeight { get; set; }
        /// <summary>
        /// 储区(库位类别 A, B, C...)
        /// </summary>
        [Field(FieldName = "BIN_AREA", Description = "储区(库位类别 A, B, C...)",
               DbType = "VARCHAR2(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string BinArea { get; set; }
        /// <summary>
        /// 堆垛机编号
        /// </summary>
        [Field(FieldName = "CRN_NO", Description = "堆垛机编号",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string CrnNo { get; set; }
        /// <summary>
        /// 库位双工位组
        /// </summary>
        [Field(FieldName = "GROUP_LOC_NO", Description = "库位双工位组",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string GroupLocNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "ORDER_LINE_GUID", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string OrderLineGuid { get; set; }
        /// <summary>
        /// 库位对应PLC编码
        /// </summary>
        [Field(FieldName = "BIN_PLC_NO", Description = "库位对应PLC编码",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string BinPlcNo { get; set; }
        /// <summary>
        /// 库位双伸位组
        /// </summary>
        [Field(FieldName = "GROUP_EXTENSION_NO", Description = "库位双伸位组",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string GroupExtensionNo { get; set; }
        /// <summary>
        /// 库区
        /// </summary>
        [Field(FieldName = "WH_NO", Description = "库区",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string WhNo { get; set; }
        /// <summary>
        /// 出库占用标志  订单出库 O1   备货出库 O2  其他出库  O3
        /// </summary>
        [Field(FieldName = "OUT_BIN_BIZ_STATUS", Description = "出库占用标志  订单出库 O1   备货出库 O2  其他出库  O3",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string OutBinBizStatus { get; set; }
        /// <summary>
        /// 库位实际的物理分组编号
        /// </summary>
        [Field(FieldName = "REAL_GROUP_NO", Description = "库位实际的物理分组编号",
               DbType = "NUMBER(8)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? RealGroupNo { get; set; }
        /// <summary>
        /// 列索引
        /// </summary>
        [Field(FieldName = "COLUMN_INDEX", Description = "列索引",
               DbType = "NUMBER(5)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? ColumnIndex { get; set; }
        /// <summary>
        /// 行索引
        /// </summary>
        [Field(FieldName = "ROW_INDEX", Description = "行索引",
               DbType = "NUMBER(5)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? RowIndex { get; set; }
        /// <summary>
        /// 巷道编号：用于赣峰项目确认当前堆垛机巷道
        /// </summary>
        [Field(FieldName = "CHANNEL_NO", Description = "巷道编号：用于赣峰项目确认当前堆垛机巷道",
               DbType = "NUMBER", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? ChannelNo { get; set; }
    }
}
