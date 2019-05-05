using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 堆垛机叉状态表 [PEM_CRN_FORK_STATUS] - 实体类
    /// </summary>
    [Entity(TableName = "PEM_CRN_FORK_STATUS", Description = "堆垛机叉状态表")]
    public class PemCrnForkStatus : BaseEntity
    {
        /// <summary>
        /// 堆垛机编码
        /// </summary>
        [Field(FieldName = "CRN_NO", Description = "堆垛机编码",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public string CrnNo { get; set; }
        /// <summary>
        /// 堆垛机叉编码
        /// </summary>
        [Field(FieldName = "CRN_FORK_NO", Description = "堆垛机叉编码",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public string CrnForkNo { get; set; }
        /// <summary>
        /// 上一次执行任务类型 (I:入库, O:出库)
        /// </summary>
        [Field(FieldName = "LAST_TASK_TYPE", Description = "上一次执行任务类型 (I:入库, O:出库)",
               DbType = "VARCHAR2(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string LastTaskType { get; set; }
        /// <summary>
        /// 入/出库(I:入库, O:出库)
        /// </summary>
        [Field(FieldName = "TASK_TYPE", Description = "入/出库(I:入库, O:出库)",
               DbType = "VARCHAR2(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string TaskType { get; set; }
        /// <summary>
        /// 任务号
        /// </summary>
        [Field(FieldName = "TASK_NO", Description = "任务号",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? TaskNo { get; set; }
        /// <summary>
        /// 源仓位
        /// </summary>
        [Field(FieldName = "FROM_BIN", Description = "源仓位",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string FromBin { get; set; }
        /// <summary>
        /// 目的仓位
        /// </summary>
        [Field(FieldName = "TO_BIN", Description = "目的仓位",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ToBin { get; set; }
        /// <summary>
        /// 堆垛机叉目前执行步骤/0,无任务；1，取放货；2，移动至取货位；3，取货；4，移动至放货位；5，放货
        /// </summary>
        [Field(FieldName = "STEP", Description = "堆垛机叉目前执行步骤/0,无任务；1，取放货；2，移动至取货位；3，取货；4，移动至放货位；5，放货",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? Step { get; set; }
        /// <summary>
        /// 堆垛机叉状态 0:无载 1:有载 2:完成 3:置物异常
        /// </summary>
        [Field(FieldName = "LOADING", Description = "堆垛机叉状态 0:无载 1:有载 2:完成 3:置物异常",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? Loading { get; set; }
        /// <summary>
        /// 目前重下命令次数
        /// </summary>
        [Field(FieldName = "RETRY_COUNT", Description = "目前重下命令次数",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? RetryCount { get; set; }
        /// <summary>
        /// 堆垛机模式（1-自动；2-半自动；3-手动；4-应急点动）
        /// </summary>
        [Field(FieldName = "CRN_MODE", Description = "堆垛机模式（1-自动；2-半自动；3-手动；4-应急点动）",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? CrnMode { get; set; }
        /// <summary>
        /// 状态 （0 空闲 1 执行中 2 完成  3-N 异常）
        /// </summary>
        [Field(FieldName = "STATUS", Description = "状态 （0 空闲 1 执行中 2 完成  3-N 异常）",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? Status { get; set; }
        /// <summary>
        /// 当前水平位置
        /// </summary>
        [Field(FieldName = "ACT_POS_X", Description = "当前水平位置",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? ActPosX { get; set; }
        /// <summary>
        /// 当前垂直位置
        /// </summary>
        [Field(FieldName = "ACT_POS_Y", Description = "当前垂直位置",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? ActPosY { get; set; }
        /// <summary>
        /// 叉当前位置，编码器数值
        /// </summary>
        [Field(FieldName = "ACT_POS_Z", Description = "叉当前位置，编码器数值",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? ActPosZ { get; set; }
        /// <summary>
        /// 当前行走速度
        /// </summary>
        [Field(FieldName = "ACT_SPEED_X", Description = "当前行走速度",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? ActSpeedX { get; set; }
        /// <summary>
        /// 当前升降速度
        /// </summary>
        [Field(FieldName = "ACT_SPEED_Y", Description = "当前升降速度",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? ActSpeedY { get; set; }
        /// <summary>
        /// 叉当前速度
        /// </summary>
        [Field(FieldName = "ACT_SPEED_Z", Description = "叉当前速度",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? ActSpeedZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "RECORD_TIME", Description = "",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? RecordTime { get; set; }
        /// <summary>
        /// 错误编号
        /// </summary>
        [Field(FieldName = "FAULT_NO", Description = "错误编号",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? FaultNo { get; set; }
        /// <summary>
        /// 先入品标志  0:无 / 1：出现先入品/  2：人工确认
        /// </summary>
        [Field(FieldName = "FIP_FLAG", Description = "先入品标志  0:无 / 1：出现先入品/  2：人工确认",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? FipFlag { get; set; }
        /// <summary>
        /// 先入品发生时间
        /// </summary>
        [Field(FieldName = "FIP_DATE", Description = "先入品发生时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? FipDate { get; set; }
        /// <summary>
        /// 先入品处理结果
        /// </summary>
        [Field(FieldName = "FIP_HANDLE_RESULT", Description = "先入品处理结果",
               DbType = "NVARCHAR2(100)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string FipHandleResult { get; set; }
        /// <summary>
        /// 先入品错误代码     --68  往货架放货时检测到货架有货     --125 往深库位放货时浅库位有货物阻挡     --126 取深库位货物时浅库位有货物阻挡
        /// </summary>
        [Field(FieldName = "FIP_FAULT_NO", Description = "先入品错误代码     --68  往货架放货时检测到货架有货     --125 往深库位放货时浅库位有货物阻挡     --126 取深库位货物时浅库位有货物阻挡",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? FipFaultNo { get; set; }
    }
}
