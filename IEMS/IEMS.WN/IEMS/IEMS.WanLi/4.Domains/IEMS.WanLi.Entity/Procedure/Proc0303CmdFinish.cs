using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PROC_0303_CMD_FINISH [PROC_0303_CMD_FINISH] - 实体类
    /// </summary>
    [Entity(TableName = "PROC_0303_CMD_FINISH", Description = "PROC_0303_CMD_FINISH")]
    public class Proc0303CmdFinish : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "I_PARAM_OBJID", Description = "", DbType = "NUMBER")]
        public decimal? IParamObjid { get; set; }
        /// <summary>
        /// 存储过程返回 DataSet 数据
        /// </summary>
        public DataSet ProcedureDataSetResult { get; set; }
    }
}
