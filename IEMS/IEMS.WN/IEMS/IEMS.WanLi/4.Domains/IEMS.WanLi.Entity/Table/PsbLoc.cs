using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 站台信息维护 [PSB_LOC] - 实体类
    /// </summary>
    [Entity(TableName = "PSB_LOC", Description = "站台信息维护")]
    public class PsbLoc : BaseEntity
    {
        /// <summary>
        /// 站台号码
        /// </summary>
        [Field(FieldName = "LOC_NO", Description = "站台号码",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string LocNo { get; set; }
        /// <summary>
        /// 下传至PLC的编码信息
        /// </summary>
        [Field(FieldName = "LOC_PLC_NO", Description = "下传至PLC的编码信息",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LocPlcNo { get; set; }
        /// <summary>
        /// 站台类型（PSB_LOC_TYPE）
        /// </summary>
        [Field(FieldName = "LOC_TYPE_NO", Description = "站台类型（PSB_LOC_TYPE）",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LocTypeNo { get; set; }
        /// <summary>
        /// 站台名称
        /// </summary>
        [Field(FieldName = "LOC_NAME", Description = "站台名称",
               DbType = "VARCHAR2(200)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LocName { get; set; }
        /// <summary>
        /// 站台顺序号.... PDIN PDOUT
        /// </summary>
        [Field(FieldName = "LOC_INDEX", Description = "站台顺序号.... PDIN PDOUT",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? LocIndex { get; set; }
        /// <summary>
        /// 缓存区大小
        /// </summary>
        [Field(FieldName = "MAX_BUFFER", Description = "缓存区大小",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? MaxBuffer { get; set; }
        /// <summary>
        /// 站台组
        /// </summary>
        [Field(FieldName = "LOC_GROUP_NO", Description = "站台组",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LocGroupNo { get; set; }
        /// <summary>
        /// MES编码
        /// </summary>
        [Field(FieldName = "LOC_MES_NO", Description = "MES编码",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LocMesNo { get; set; }
        /// <summary>
        /// 优先级 20 默认 ，越小优先级越高
        /// </summary>
        [Field(FieldName = "LOC_PRIORITY", Description = "优先级 20 默认 ，越小优先级越高",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? LocPriority { get; set; }
        /// <summary>
        /// 可用状态  1：可用 0:不可用
        /// </summary>
        [Field(FieldName = "LOC_ENABLE", Description = "可用状态  1：可用 0:不可用",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? LocEnable { get; set; }
        /// <summary>
        /// 最后使用时间
        /// </summary>
        [Field(FieldName = "LAST_USED_DATE", Description = "最后使用时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? LastUsedDate { get; set; }
        /// <summary>
        /// 限定通过物料种类 
        /// </summary>
        [Field(FieldName = "LIMIT_PALLET_TYPE", Description = "限定通过物料种类",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LimitPalletType { get; set; }
        /// <summary>
        /// 指令结束标志分类  0:正常结束(WCS控制)   1:发起站台空闲结束(自动)  2:指令完毕结束(自动)
        /// </summary>
        [Field(FieldName = "LIMIT_CMD_FINISH", Description = "指令结束标志分类  0:正常结束(WCS控制)   1:发起站台空闲结束(自动)  2:指令完毕结束(自动)",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? LimitCmdFinish { get; set; }
        /// <summary>
        /// 自动发起工装请求  1：自动发起  0：不发起
        /// </summary>
        [Field(FieldName = "AUTO_PALLET_REQUEST", Description = "自动发起工装请求  1：自动发起  0：不发起",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? AutoPalletRequest { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        [Field(FieldName = "AREA_NO", Description = "区域",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string AreaNo { get; set; }
        /// <summary>
        /// 输送设备号
        /// </summary>
        [Field(FieldName = "TRANSFER_NO", Description = "输送设备号",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string TransferNo { get; set; }
        /// <summary>
        /// 工装类型
        /// </summary>
        [Field(FieldName = "REQUEST_PALLET_TYPE", Description = "工装类型",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string RequestPalletType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "LIMIT_CMD_QTY", Description = "",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? LimitCmdQty { get; set; }
        /// <summary>
        /// 控制权限 0:不控制 1:控制绑定是否正确
        /// </summary>
        [Field(FieldName = "CONTROL_PRIVILEGE", Description = "控制权限 0:不控制 1:控制绑定是否正确",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? ControlPrivilege { get; set; }
        /// <summary>
        /// 指定由WMS还是MES拉动 0:WMS  1:MES
        /// </summary>
        [Field(FieldName = "WMS_MES_LOCK", Description = "指定由WMS还是MES拉动 0:WMS  1:MES",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? WmsMesLock { get; set; }
        /// <summary>
        /// 立库编码
        /// </summary>
        [Field(FieldName = "WH_NO", Description = "立库编码",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string WhNo { get; set; }
        /// <summary>
        /// AGV编号
        /// </summary>
        [Field(FieldName = "LOC_AGV_NO", Description = "AGV编号",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string LocAgvNo { get; set; }
        /// <summary>
        /// 最小列
        /// </summary>
        [Field(FieldName = "MIN_COL", Description = "最小列",
               DbType = "NUMBER", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? MinCol { get; set; }
        /// <summary>
        /// 最大列
        /// </summary>
        [Field(FieldName = "MAX_COL", Description = "最大列",
               DbType = "NUMBER", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public decimal? MaxCol { get; set; }
    }
}
