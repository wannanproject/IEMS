using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace IEMS.WanLi.DbCI
{
    internal class MaterialService : DbCIService, IMaterialService
    {
        public PageResult GetMaterialData(PageResult pageResult)
        {
            string stmtId = "GetMaterialData";
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
