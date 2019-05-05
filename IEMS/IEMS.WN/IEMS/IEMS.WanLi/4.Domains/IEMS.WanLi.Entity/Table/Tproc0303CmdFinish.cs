using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// 存储过程参数 - 任务执行完毕 [TPROC_0303_CMD_FINISH] - 实体类
    /// </summary>
    [Entity(TableName = "TPROC_0303_CMD_FINISH", Description = "存储过程参数 - 任务执行完毕")]
    public class Tproc0303CmdFinish : BaseEntity
    {
        /// <summary>
        /// 主键  编号
        /// </summary>
        [Field(FieldName = "OBJID", Description = "主键  编号",
               DbType = "NUMBER(20)", DefaultValue = "",
               IsPrimaryKey = true, IsIdentity = false, Nullable = false)]
        public long? ObjId { get; set; }
        /// <summary>
        /// 错误编号
        /// </summary>
        [Field(FieldName = "ERR_CODE", Description = "错误编号",
               DbType = "NUMBER", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public decimal? ErrCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [Field(FieldName = "ERR_DESC", Description = "错误信息",
               DbType = "NVARCHAR2(1000)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ErrDesc { get; set; }
        /// <summary>
        /// 0 未执行  1执行中 2：执行完毕
        /// </summary>
        [Field(FieldName = "PROC_STATUS", Description = "0 未执行  1执行中 2：执行完毕",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? ProcStatus { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        [Field(FieldName = "PROC_CREATE_TIME", Description = "启动时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public DateTime? ProcCreateTime { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        [Field(FieldName = "PROC_EXCUTE_TIME", Description = "执行时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? ProcExcuteTime { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        [Field(FieldName = "PROC_FINISH_TIME", Description = "完成时间",
               DbType = "DATE", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public DateTime? ProcFinishTime { get; set; }
        /// <summary>
        /// 【必填项】    ：任务指令号                                                                                    
        /// </summary>
        [Field(FieldName = "TASK_NO", Description = "【必填项】    ：任务指令号",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? TaskNo { get; set; }
        /// <summary>
        /// 【必填项】    ：当前站台
        /// </summary>
        [Field(FieldName = "CURR_LOC_NO", Description = "【必填项】    ：当前站台",
               DbType = "VARCHAR2(40)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string CurrLocNo { get; set; }
        /// <summary>
        /// 【选填项】    ：结束状态    1 ：正常结束   201：空出库 
        /// </summary>
        [Field(FieldName = "FINISH_STATUS", Description = "【选填项】    ：结束状态    1 ：正常结束   201：空出库",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? FinishStatus { get; set; }
        /// <summary>
        /// 【必填项】    ：工装编码
        /// </summary>
        [Field(FieldName = "PALLET_NO", Description = "【必填项】    ：工装编码",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string PalletNo { get; set; }
        /// <summary>
        /// 【系统占用】：起始库位类型
        /// </summary>
        [Field(FieldName = "SLOC_TYPE", Description = "【系统占用】：起始库位类型",
               DbType = "VARCHAR2(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string SlocType { get; set; }
        /// <summary>
        /// 【系统占用】：起始库位
        /// </summary>
        [Field(FieldName = "SLOC_NO", Description = "【系统占用】：起始库位",
               DbType = "VARCHAR2(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string SlocNo { get; set; }
        /// <summary>
        /// 【必填项】：结束库位
        /// </summary>
        [Field(FieldName = "ELOC_NO", Description = "【必填项】：结束库位",
               DbType = "VARCHAR2(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ElocNo { get; set; }
        /// <summary>
        /// 【系统占用】：结束指令GUID
        /// </summary>
        [Field(FieldName = "GLOBAL_GUID", Description = "【系统占用】：结束指令GUID",
               DbType = "NVARCHAR2(200)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string GlobalGuid { get; set; }
        /// <summary>
        /// 【系统占用】：指令OBJID
        /// </summary>
        [Field(FieldName = "CMD_OBJID", Description = "【系统占用】：指令OBJID",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? CmdObjid { get; set; }
        /// <summary>
        /// 【系统占用】：任务号GUID
        /// </summary>
        [Field(FieldName = "TASK_GUID", Description = "【系统占用】：任务号GUID",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string TaskGuid { get; set; }
        /// <summary>
        /// 【选填项】    ：任务结束标志 1：强制结束任务 0：不结束
        /// </summary>
        [Field(FieldName = "TASK_FINISH_FLAG", Description = "【选填项】    ：任务结束标志 1：强制结束任务 0：不结束",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = false)]
        public int? TaskFinishFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "ELOC_TYPE", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string ElocType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "FTYPE", Description = "",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? Ftype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "FTYPE01", Description = "",
               DbType = "VARCHAR2(80)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string Ftype01 { get; set; }
        /// <summary>
        /// 输送设备类型
        /// </summary>
        [Field(FieldName = "TRANSFER_TYPE", Description = "输送设备类型",
               DbType = "VARCHAR2(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public string TransferType { get; set; }
        /// <summary>
        /// 错误标记
        /// </summary>
        [Field(FieldName = "ERR_SIGN", Description = "错误标记",
               DbType = "NUMBER(10)", DefaultValue = "",
               IsPrimaryKey = false, IsIdentity = false, Nullable = true)]
        public int? ErrSign { get; set; }
    }
}
