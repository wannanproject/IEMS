using IEMS.WanLi.DbCI;
using MSTL.DbAccess;
using MSTL.LogAgent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IEMS.WanLi.Entity;
using IEMS.WanLi.DbRI;

namespace IEMS.WanLi.AppBiz
{
    internal class LocAutoManager : ILocAutoManager
    {
        #region 系统日志  log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion
        public PageResult GetLocDataTable(PageResult pageResult)
        {
            var service = DbCIServiceFactory.CreateInstance<ILocService>();
            return service.GetLocDataTable(pageResult);
        }

        /// <summary>
        ///  查询站台号
        /// </summary>
        /// <returns></returns>
        public PsbLoc GetPalletType(string locNo)
        {
            var tdate = TableViewServiceFactory.CreateInstance<IPsbLocService>();
            var where = new PsbLoc() { LocNo = locNo };
            return tdate.GetEntityList(where, "LOC_NO").FirstOrDefault();
        }

        public void SetLocAutoEnable(string locNo, int enable)
        {
            var service = TableViewServiceFactory.CreateInstance<IPsbLocService>();
            service.Update(new PsbLoc() { AutoPalletRequest = enable }, new PsbLoc() { LocNo = locNo });
        }

    }
}
