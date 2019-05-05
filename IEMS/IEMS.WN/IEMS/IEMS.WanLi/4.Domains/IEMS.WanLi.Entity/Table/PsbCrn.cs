using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 堆垛机维护 [PSB_CRN] - 实体类
    /// </summary>
    [Entity(TableName = "PSB_CRN", Description = "堆垛机维护")]
    public class PsbCrn : BaseEntity
    {
        /// <summary>
        /// 堆垛机编号
        /// </summary>
        [Field(FieldName = "CRN_NO", Description = "堆垛机编号",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public string CrnNo { get; set; }
        /// <summary>
        /// 堆垛机名称
        /// </summary>
        [Field(FieldName = "CRN_NAME", Description = "堆垛机名称",
               DbType = "VARCHAR2(50)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string CrnName { get; set; }
        /// <summary>
        /// 堆垛机叉叉数
        /// </summary>
        [Field(FieldName = "CRN_FORK_COUNT", Description = "堆垛机叉叉数",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? CrnForkCount { get; set; }
        /// <summary>
        /// 出库任务上限
        /// </summary>
        [Field(FieldName = "MAX_OUT_TASK_COUNT", Description = "出库任务上限",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? MaxOutTaskCount { get; set; }
        /// <summary>
        /// 入库任务上限
        /// </summary>
        [Field(FieldName = "MAX_IN_TASK_COUNT", Description = "入库任务上限",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? MaxInTaskCount { get; set; }
        /// <summary>
        /// 堆垛机状态 1：正常  0:禁用
        /// </summary>
        [Field(FieldName = "CRN_STATUS", Description = "堆垛机状态 1：正常  0:禁用",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? CrnStatus { get; set; }
        /// <summary>
        /// 入库可用 1：可用  0:禁用
        /// </summary>
        [Field(FieldName = "CRN_IN_ENABLE", Description = "入库可用 1：可用  0:禁用",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? CrnInEnable { get; set; }
        /// <summary>
        /// 出库可用 1：可用  0:禁用
        /// </summary>
        [Field(FieldName = "CRN_OUT_ENABLE", Description = "出库可用 1：可用  0:禁用",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? CrnOutEnable { get; set; }
        /// <summary>
        /// 限定任务总数
        /// </summary>
        [Field(FieldName = "LIMIT_TASK_SIZE", Description = "限定任务总数",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? LimitTaskSize { get; set; }
        /// <summary>
        /// 巷道类型 1：单伸   2：双伸
        /// </summary>
        [Field(FieldName = "EXTENSION_SIZE", Description = "巷道类型 1：单伸   2：双伸",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? ExtensionSize { get; set; }
        /// <summary>
        /// 列数
        /// </summary>
        [Field(FieldName = "COL_SIZE", Description = "列数",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? ColSize { get; set; }
        /// <summary>
        /// 层高
        /// </summary>
        [Field(FieldName = "LAYER_SIZE", Description = "层高",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? LayerSize { get; set; }
        /// <summary>
        /// 立库区域
        /// </summary>
        [Field(FieldName = "WH_NO", Description = "立库区域",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string WhNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "OPC_GROUP_NO", Description = "",
               DbType = "VARCHAR2(500)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string OpcGroupNo { get; set; }
    }
}
