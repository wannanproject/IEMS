using MSTL.DbAccess;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.DbCI
{
    public interface IAgvTaskService : IDbCIService
    {
       PageResult GetAgvTaskData(PageResult pageResult);
        
        DataTable GetAgvTaskDt();

    }
}
