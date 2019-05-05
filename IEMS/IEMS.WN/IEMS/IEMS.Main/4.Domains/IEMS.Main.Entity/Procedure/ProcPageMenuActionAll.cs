using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Main.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PROC_PAGE_MENU_ACTION_ALL [PROC_PAGE_MENU_ACTION_ALL] - 实体类
    /// </summary>
    [Entity(TableName = "PROC_PAGE_MENU_ACTION_ALL", Description = "PROC_PAGE_MENU_ACTION_ALL")]
    public class ProcPageMenuActionAll : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "CURTABLE1", Description = "", DbType = "REF CURSOR")]
        public DataTable Curtable1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "CURTABLE2", Description = "", DbType = "REF CURSOR")]
        public DataTable Curtable2 { get; set; }
        /// <summary>
        /// 存储过程返回 DataSet 数据
        /// </summary>
        public DataSet ProcedureDataSetResult { get; set; }
    }
}
