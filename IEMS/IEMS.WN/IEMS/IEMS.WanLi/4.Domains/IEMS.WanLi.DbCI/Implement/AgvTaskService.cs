using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace IEMS.WanLi.DbCI
{
    internal class AgvTaskService : DbCIService, IAgvTaskService
    {
        public PageResult GetAgvTaskData(PageResult pageResult)
        {
            string stmtId = "GetAgvTaskData";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }

        public DataTable GetAgvTaskDt()
        {
            var dTable = this.GetDataTableByStatement("GetAgvTaskTable", null);
            return dTable;
        }
    }
}
