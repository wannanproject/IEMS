using IEMS.WanLi.DbRI;
using IEMS.WanLi.DbCI;
using IEMS.WanLi.VoDto;
using IEMS.WanLi.Entity;
using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MSTL.LogAgent;

namespace IEMS.WanLi.AppBiz
{
    internal class LocManager : ILocManager
    {
        #region 系统日志  log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion
        public DataTable GetLocDataTable()
        {
            var service = DbCIServiceFactory.CreateInstance<ILocService>();
            return service.GetLocable();
        }

        public void SetLocEnable(string locNo, int flag)
        {
            try
            {
                var PsbLoc = TableViewServiceFactory.CreateInstance<IPsbLocService>();
                PsbLoc.Update(new PsbLoc { LocEnable = flag }, new PsbLoc { LocNo = locNo });
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}