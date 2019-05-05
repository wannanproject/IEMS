using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// WBS_TASK_COMMAND [WBS_TASK_CMD] - 实体类
    /// </summary>
    [Entity(TableName = "WBS_TASK_CMD", Description = "WBS_TASK_COMMAND")]
    public class WbsTaskCmd : BaseEntity
    {
        /// <summary>
        /// 指令ID 序列
        /// </summary>
        [Field(FieldName = "OBJID", Description = "指令ID 序列",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public long? ObjId { get; set; }
        /// <summary>
        /// 任务GUID
        /// </summary>
        [Field(FieldName = "TASK_GUID", Description = "任务GUID",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string TaskGuid { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        [Field(FieldName = "TASK_NO", Description = "任务编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? TaskNo { get; set; }
        /// <summary>
        /// 指令类型
        /// </summary>
        [Field(FieldName = "CMD_TYPE", Description = "指令类型",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string CmdType { get; set; }
        /// <summary>
        /// 源
        /// </summary>
        [Field(FieldName = "SLOC_NO", Description = "源",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string SlocNo { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        [Field(FieldName = "ELOC_NO", Description = "目标",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ElocNo { get; set; }
        /// <summary>
        /// 源
        /// </summary>
        [Field(FieldName = "SLOC_PLC_NO", Description = "源",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string SlocPlcNo { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        [Field(FieldName = "ELOC_PLC_NO", Description = "目标",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ElocPlcNo { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Field(FieldName = "CREATION_DATE", Description = "创建日期",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        [Field(FieldName = "EXCUTE_DATE", Description = "执行时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? ExcuteDate { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        [Field(FieldName = "FINISH_DATE", Description = "执行时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? FinishDate { get; set; }
        /// <summary>
        /// 错误编码
        /// </summary>
        [Field(FieldName = "ERR_NO", Description = "错误编码",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public long? ErrNo { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [Field(FieldName = "ERR_NAME", Description = "错误信息",
               DbType = "VARCHAR2(200)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ErrName { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        [Field(FieldName = "PRODUCT_NO", Description = "物料编号",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ProductNo { get; set; }
        /// <summary>
        /// 输送设备类型
        /// </summary>
        [Field(FieldName = "TRANSFER_TYPE", Description = "输送设备类型",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string TransferType { get; set; }
        /// <summary>
        /// 是否锁定物料输送顺序  0:不锁定  1:锁定
        /// </summary>
        [Field(FieldName = "LOCK_PRODUCT", Description = "是否锁定物料输送顺序  0:不锁定  1:锁定",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? LockProduct { get; set; }
        /// <summary>
        /// 路径编号
        /// </summary>
        [Field(FieldName = "ROUTE_NO", Description = "路径编号",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string RouteNo { get; set; }
        /// <summary>
        /// 输送设备号码
        /// </summary>
        [Field(FieldName = "TRANSFER_NO", Description = "输送设备号码",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string TransferNo { get; set; }
        /// <summary>
        /// 结束状态  201 空出库
        /// </summary>
        [Field(FieldName = "FINISH_STATUS", Description = "结束状态  201 空出库",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string FinishStatus { get; set; }
        /// <summary>
        /// 工装编号
        /// </summary>
        [Field(FieldName = "PALLET_NO", Description = "工装编号",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletNo { get; set; }
        /// <summary>
        /// 订单行项目GUID
        /// </summary>
        [Field(FieldName = "ORDER_LINE_GUID", Description = "订单行项目GUID",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string OrderLineGuid { get; set; }
        /// <summary>
        /// 开始站台类型
        /// </summary>
        [Field(FieldName = "SLOC_TYPE", Description = "开始站台类型",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string SlocType { get; set; }
        /// <summary>
        /// 结束站台类型
        /// </summary>
        [Field(FieldName = "ELOC_TYPE", Description = "结束站台类型",
               DbType = "VARCHAR2(20)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ElocType { get; set; }
        /// <summary>
        /// 指令步骤
        /// </summary>
        [Field(FieldName = "CMD_STEP", Description = "指令步骤",
               DbType = "VARCHAR2(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public string CmdStep { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "FIP_FLAG", Description = "",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? FipFlag { get; set; }
        /// <summary>
        /// 下传时间
        /// </summary>
        [Field(FieldName = "DOWNLOAD_DATE", Description = "下传时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? DownloadDate { get; set; }
        /// <summary>
        /// B道标志
        /// </summary>
        [Field(FieldName = "B_TRANS_FLAG", Description = "B道标志",
               DbType = "NUMBER(1)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? BTransFlag { get; set; }
        /// <summary>
        /// 当前包装单位唯一识别码
        /// </summary>
        [Field(FieldName = "PACKAGE_GUID", Description = "当前包装单位唯一识别码",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PackageGuid { get; set; }
    }
}
