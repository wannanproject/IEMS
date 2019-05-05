using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Main.Entity
{
    using MSTL.DbAccess;

    /// <summary>
    /// PROC_PAGE_MENU_USER [PROC_PAGE_MENU_USER] - 实体类
    /// </summary>
    [Entity(TableName = "PROC_PAGE_MENU_USER", Description = "PROC_PAGE_MENU_USER")]
    public class ProcPageMenuUser : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "USERID", Description = "", DbType = "NUMBER")]
        public decimal? Userid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "MENULEVEL", Description = "", DbType = "VARCHAR2")]
        public string Menulevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field(FieldName = "CURTABLE", Description = "", DbType = "REF CURSOR")]
        public DataTable Curtable { get; set; }
        /// <summary>
        /// 存储过程返回 DataSet 数据
        /// </summary>
        public DataSet ProcedureDataSetResult { get; set; }
    }
}
