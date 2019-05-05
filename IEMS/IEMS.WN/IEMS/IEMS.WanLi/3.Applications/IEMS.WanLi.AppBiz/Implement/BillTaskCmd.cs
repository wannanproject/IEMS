using IEMS.WanLi.DbCI;
using MSTL.DbAccess;
using MSTL.LogAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.AppBiz
{
    internal class BillTaskCmd : IBillTaskCmd
    {
        #region 系统日志  log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion
        public PageResult GetBillDataTable(PageResult pageResult)
        {
            var service = DbCIServiceFactory.CreateInstance<IBillTaskCmdService>();
            return service.GetBillDataTable(pageResult);
        }

        public PageResult GetTaskDataTable(PageResult pageResult)
        {
            var service = DbCIServiceFactory.CreateInstance<IBillTaskCmdService>();
            return service.GetTaskDataTable(pageResult);
        }

        public PageResult GetCmdDataTable(PageResult pageResult)
        {
            var service = DbCIServiceFactory.CreateInstance<IBillTaskCmdService>();
            return service.GetCmdDataTable(pageResult);
        }
    }
}
