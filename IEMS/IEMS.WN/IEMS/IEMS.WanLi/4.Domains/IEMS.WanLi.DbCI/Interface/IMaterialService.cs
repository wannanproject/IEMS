using MSTL.DbAccess;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.DbCI
{
    public interface IMaterialService : IDbCIService
    {
       PageResult GetMaterialData(PageResult pageResult);
        
        DataTable GetAgvTaskDt();

    }
}
