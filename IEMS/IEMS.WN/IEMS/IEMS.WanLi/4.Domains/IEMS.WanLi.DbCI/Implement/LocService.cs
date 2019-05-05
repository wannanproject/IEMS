using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.DbCI
{
    internal class LocService : DbCIService,ILocService
    {
        public PageResult GetLocDataTable(PageResult pageResult)
        {
            string stmtId = "GetLocDataTable";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }

        public PageResult GetInputLocDataTable(PageResult pageResult)
        {
            string stmtId = "GetInputLocDataTable";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }
        public PageResult GetPsbLocLock(PageResult pageResult)
        {
            string stmtId = "GetPsbLocLock";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }
        /// <summary>
        /// 获取站台状态数据
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public PageResult GetLocStatus(PageResult pageResult)
        {
            string stmtId = "GetLocStatus";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }
        /// <summary>
        /// 通过起始站台获取任务
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public PageResult GetTaskBySlocNo(PageResult pageResult)
        {
            string stmtId = "GetTaskBySlocNo";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }
        /// <summary>
        /// 通过终点站台获取任务
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public PageResult GetTaskByElocNo(PageResult pageResult)
        {
            string stmtId = "GetTaskByElocNo";
            pageResult.StatementId = stmtId;
            return this.GetPageDataByReader(pageResult);
        }

        public DataTable GetLocable()
        {
            var dTable = this.GetDataTableByStatement("GetLocTable", null);
            return dTable;
        }
    }
}
