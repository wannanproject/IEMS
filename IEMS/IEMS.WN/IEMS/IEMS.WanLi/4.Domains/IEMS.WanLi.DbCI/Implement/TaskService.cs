using MSTL.DbAccess;
using System;
using System.Collections.Generic;
namespace IEMS.WanLi.DbCI
{
    internal class TaskService : DbCIService, ITaskService
    {
        public PageResult GetWbsTaskData(PageResult pageResult)
        {
            string stmtId = "GetWbsTaskData";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }
        public bool ExecPROC_0304_CMD_STEP_ADJUST(string locNo,string palletId)
        {
            try
            {
                Dictionary<string, object> para = new Dictionary<string, object>();
                para.Add("ILocNo", locNo);
                para.Add("IPalletNo", palletId);
                this.GetDataTableByStatement("pack_3010_system_operation", para);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }           
        }
        public PageResult GetTaskData(PageResult pageResult)
        {
            string stmtId = "GetTaskData";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }
    }
}
