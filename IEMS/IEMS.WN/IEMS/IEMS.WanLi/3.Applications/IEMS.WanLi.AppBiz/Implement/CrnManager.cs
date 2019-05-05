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
    internal class CrnManager : ICrnManager
    {
        #region 系统日志  log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion
        public DataTable GetCrnDataTable()
        {
            var service = DbCIServiceFactory.CreateInstance<ICrnService>();
            return service.GetCrnDataTable();
        }
        public DataTable GetCrnForkErrorStatus()
        {
            var service = DbCIServiceFactory.CreateInstance<ICrnService>();
            return service.GetCrnForkErrorStatus();
        }
        public void SetCrnInputEnable(string crnNo,int enable)
        {
            try
            {
                var service = TableViewServiceFactory.CreateInstance<IPsbCrnService>();
                service.Update(new PsbCrn() { CrnInEnable = enable }, new PsbCrn() { CrnNo = crnNo });
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        public void SetCrnOutputEnable(string crnNo, int enable)
        {
            try
            {
                var service = TableViewServiceFactory.CreateInstance<IPsbCrnService>();
                service.Update(new PsbCrn() { CrnOutEnable = enable }, new PsbCrn() { CrnNo = crnNo });
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public void SetCrnForkEnable(string crnForkNo, int enable)
        {
            try
            {
                var service = TableViewServiceFactory.CreateInstance<IPsbCrnForkService>();
                service.Update(new PsbCrnFork() { CrnForkEnable = enable }, new PsbCrnFork() { CrnForkNo = crnForkNo });
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public string UpdateSrmArea(string CrnNo, string MinCol, string MaxCol)
        {
            try
            {
                var service = ProcedureServiceFactory.CreateInstance<IProcWmsUpdateSrmAreaService>();
                service.ExcuteProcedure(new ProcWmsUpdateSrmArea()
                {
                    ICrnNo = CrnNo,
                    IMinCol = int.Parse(MinCol),
                    IMaxCol = int.Parse(MaxCol)
                });
                return string.Empty;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}