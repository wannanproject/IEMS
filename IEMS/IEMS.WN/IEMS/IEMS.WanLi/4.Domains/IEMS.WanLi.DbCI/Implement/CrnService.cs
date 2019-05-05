using IEMS.WanLi.VoDto;
using MSTL.DbAccess;
using MSTL.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.DbCI
{
    internal class CrnService : DbCIService, ICrnService
    {
        public DataTable GetCrnDataTable()
        {
            var dTable = this.GetDataTableByStatement("GetCrnDataTable", null);
            return dTable;
        }

        public DataTable GetCrnForkErrorStatus()
        {
            var dTable = this.GetDataTableByStatement("GetCrnForkErrorStatus", null);
            return dTable;
        }
    }
}
