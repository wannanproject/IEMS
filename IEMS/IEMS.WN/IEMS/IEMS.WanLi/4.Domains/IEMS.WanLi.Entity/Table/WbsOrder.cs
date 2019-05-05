using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 订单主表 [WBS_ORDER] - 实体类
    /// </summary>
    [Entity(TableName = "WBS_ORDER", Description = "订单主表")]
    public class WbsOrder : BaseEntity
    {
        /// <summary>
        /// 单号
        /// </summary>
        [Field(FieldName = "ORDER_NO", Description = "单号",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public string OrderNo { get; set; }
        /// <summary>
        /// 单据状态 (0: 未执行, 1: 执行中, /2: 已完成, /3: 已取消   /4 : 订单关闭)
        /// </summary>
        [Field(FieldName = "ORDER_STATUS", Description = "单据状态 (0: 未执行, 1: 执行中, /2: 已完成, /3: 已取消   /4 : 订单关闭)",
               DbType = "NUMBER(2)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? OrderStatus { get; set; }
        /// <summary>
        /// 单据类型
        /// </summary>
        [Field(FieldName = "ORDER_TYPE_NO", Description = "单据类型",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string OrderTypeNo { get; set; }
        /// <summary>
        /// 单据类型模式
        /// </summary>
        [Field(FieldName = "ORDER_TYPE_MODULE", Description = "单据类型模式",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string OrderTypeModule { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Field(FieldName = "CREATION_DATE", Description = "创建日期",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [Field(FieldName = "CREATED_BY", Description = "创建者",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string CreatedBy { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Field(FieldName = "FMEM", Description = "备注",
               DbType = "VARCHAR2(255)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Fmem { get; set; }
        /// <summary>
        /// 优先级    数字越小优先级越高
        /// </summary>
        [Field(FieldName = "ORDER_PRIORITY", Description = "优先级    数字越小优先级越高",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? OrderPriority { get; set; }
        /// <summary>
        /// 0: WMS 生成  1:WCS生成  2: interface导入 
        /// </summary>
        [Field(FieldName = "SOURCE_TYPE", Description = "0: WMS 生成  1:WCS生成  2: interface导入",
               DbType = "NUMBER(2)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? SourceType { get; set; }
        /// <summary>
        /// 请求单号码
        /// </summary>
        [Field(FieldName = "REQUEST_OBJID", Description = "请求单号码",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? RequestObjid { get; set; }
        /// <summary>
        /// 目标站台
        /// </summary>
        [Field(FieldName = "ELOC_NO", Description = "目标站台",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ElocNo { get; set; }
    }
}
