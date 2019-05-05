using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 堆垛机叉设置表 [PSB_CRN_FORK] - 实体类
    /// </summary>
    [Entity(TableName = "PSB_CRN_FORK", Description = "堆垛机叉设置表")]
    public class PsbCrnFork : BaseEntity
    {
        /// <summary>
        /// 堆垛机叉编码
        /// </summary>
        [Field(FieldName = "CRN_FORK_NO", Description = "堆垛机叉编码",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public string CrnForkNo { get; set; }
        /// <summary>
        /// 所属堆垛机编码
        /// </summary>
        [Field(FieldName = "CRN_NO", Description = "所属堆垛机编码",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string CrnNo { get; set; }
        /// <summary>
        /// 叉名称
        /// </summary>
        [Field(FieldName = "CRN_FORK_NAME", Description = "叉名称",
               DbType = "VARCHAR2(50)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string CrnForkName { get; set; }
        /// <summary>
        /// 叉序号(需要单独讨论)
        /// </summary>
        [Field(FieldName = "CRN_FORK_IDX", Description = "叉序号(需要单独讨论)",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? CrnForkIdx { get; set; }
        /// <summary>
        /// 叉可用：1：可用 0：禁用
        /// </summary>
        [Field(FieldName = "CRN_FORK_ENABLE", Description = "叉可用：1：可用 0：禁用",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? CrnForkEnable { get; set; }
        /// <summary>
        /// PLC编号
        /// </summary>
        [Field(FieldName = "CRN_FORK_PLC_NO", Description = "PLC编号",
               DbType = "VARCHAR2(50)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string CrnForkPlcNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "OPC_GROUP_NO", Description = "",
               DbType = "VARCHAR2(500)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string OpcGroupNo { get; set; }
    }
}
