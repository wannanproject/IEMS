using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 工作表 [Z50_WBS_TASK] - 实体类
    /// </summary>
    [Entity(TableName = "Z50_WBS_TASK", Description = "工作表")]
    public class Z50WbsTask : BaseEntity
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Field(FieldName = "TASK_GUID", Description = "序号",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public string TaskGuid { get; set; }
        /// <summary>
        /// 序列  SEQ_Z50_WBS_TASK
        /// </summary>
        [Field(FieldName = "OBJID", Description = "序列  SEQ_Z50_WBS_TASK",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? ObjId { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        [Field(FieldName = "TASK_NO", Description = "任务编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public long? TaskNo { get; set; }
        /// <summary>
        /// 优先级(1: 最优先,  100: 最后)  默认20
        /// </summary>
        [Field(FieldName = "EMERGENCY", Description = "优先级(1: 最优先,  100: 最后)  默认20",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? Emergency { get; set; }
        /// <summary>
        /// 入/出库(I:入库, O:出库 ,T 移栽 S:巷道转移)
        /// </summary>
        [Field(FieldName = "IO_TYPE", Description = "入/出库(I:入库, O:出库 ,T 移栽 S:巷道转移)",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string IoType { get; set; }
        /// <summary>
        /// 输送设备号
        /// </summary>
        [Field(FieldName = "TRANSFER_NO", Description = "输送设备号",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string TransferNo { get; set; }
        /// <summary>
        /// 当前正在执行的指令 起始站台
        /// </summary>
        [Field(FieldName = "SLOC_NO", Description = "当前正在执行的指令 起始站台",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string SlocNo { get; set; }
        /// <summary>
        /// 当前正在执行的指令 结束站台
        /// </summary>
        [Field(FieldName = "ELOC_NO", Description = "当前正在执行的指令 结束站台",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ElocNo { get; set; }
        /// <summary>
        /// 货笼ID
        /// </summary>
        [Field(FieldName = "PALLET_NO", Description = "货笼ID",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletNo { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        [Field(FieldName = "MATERIAL_NO", Description = "物料编号",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string MaterialNo { get; set; }
        /// <summary>
        /// 订单行项目GUID
        /// </summary>
        [Field(FieldName = "ORDER_LINE_GUID", Description = "订单行项目GUID",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string OrderLineGuid { get; set; }
        /// <summary>
        /// 执行顺序
        /// </summary>
        [Field(FieldName = "SORT_ID", Description = "执行顺序",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? SortId { get; set; }
        /// <summary>
        /// 任务创建时间
        /// </summary>
        [Field(FieldName = "CREATION_DATE", Description = "任务创建时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// 任务开始执行时间
        /// </summary>
        [Field(FieldName = "TASK_EXEC_START_DT", Description = "任务开始执行时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? TaskExecStartDt { get; set; }
        /// <summary>
        /// 任务结束时间
        /// </summary>
        [Field(FieldName = "TASK_EXEC_END_DT", Description = "任务结束时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? TaskExecEndDt { get; set; }
        /// <summary>
        /// 最后修改日期
        /// </summary>
        [Field(FieldName = "LAST_UPDATE_DATE", Description = "最后修改日期",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? LastUpdateDate { get; set; }
        /// <summary>
        /// (0:表示无错误 >0: 对应 ERR_CODE，堆垛机、地面线异常码需要唯一)
        /// </summary>
        [Field(FieldName = "FINISH_FLAG", Description = "(0:表示无错误 >0: 对应 ERR_CODE，堆垛机、地面线异常码需要唯一)",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? FinishFlag { get; set; }
        /// <summary>
        /// 错误代码  1： 空出库    2:路径查找失败
        /// </summary>
        [Field(FieldName = "ERR_NO", Description = "错误代码  1： 空出库    2:路径查找失败",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string ErrNo { get; set; }
        /// <summary>
        /// 立库库位信息，如果非立库，可以为空
        /// </summary>
        [Field(FieldName = "BIN_NO", Description = "立库库位信息，如果非立库，可以为空",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string BinNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "USE_QTY", Description = "",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? UseQty { get; set; }
        /// <summary>
        /// 工装类型
        /// </summary>
        [Field(FieldName = "PALLET_TYPE", Description = "工装类型",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletType { get; set; }
        /// <summary>
        /// 0000
        /// </summary>
        [Field(FieldName = "TASK_STEP", Description = "0000",
               DbType = "VARCHAR2(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string TaskStep { get; set; }
        /// <summary>
        /// 结束描述
        /// </summary>
        [Field(FieldName = "FINISH_DESC", Description = "结束描述",
               DbType = "VARCHAR2(500)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string FinishDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "FDESC", Description = "",
               DbType = "VARCHAR2(500)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Fdesc { get; set; }
        /// <summary>
        /// 父区域编码
        /// </summary>
        [Field(FieldName = "PAREA_NO", Description = "父区域编码",
               DbType = "VARCHAR2(50)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PareaNo { get; set; }
        /// <summary>
        /// B道标志
        /// </summary>
        [Field(FieldName = "B_TRANS_FLAG", Description = "B道标志",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? BTransFlag { get; set; }
    }
}
