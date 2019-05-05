using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using MSTL.DbAccess;
using MSTL.LogAgent;
using IEMS.WanLi.Entity;
using IEMS.WanLi.DbRI;
using IEMS.WanLi.DbCI;
using IEMS.WanLi.AppBiz.Common;

namespace IEMS.WanLi.AppBiz
{
    internal class MaterialManager : IMaterialManager
    {
        #region 系统日志  log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion

        public int AddMaterialInfo(string materialNo, string materialName)
        {
            try
            {
                //var seqservice = SequenceServiceFactory.CreateInstance<ISeqWbsTaskCmdService>();
                //var seqservice2 = SequenceServiceFactory.CreateInstance<ISeqWbsTaskService>();
                var task = new PsbMaterial();
                task.MaterNo = materialNo;
                task.MaterName = materialName;
                
                var service = TableViewServiceFactory.CreateInstance<IPsbMaterialService>();
                return service.Insert(task);
            }
            catch (Exception ex)
            {
                log.Error("插入数据", ex);
                return 0;
            }
        }

        public int DeleteAgvTask(string taskNo)
        {
            var task = new WbsTaskCmd();
            task.TaskNo = Convert.ToInt64(taskNo);
            var service = TableViewServiceFactory.CreateInstance<IWbsTaskCmdService>();
            return service.Delete(task);
        }

        public int DeleteAgvTaskOnly(string taskNo)
        {
            var task = new WbsTask();
            task.TaskNo = Convert.ToInt64(taskNo);
            var service = TableViewServiceFactory.CreateInstance<IWbsTaskService>();
            return service.Delete(task);
        }

        public PageResult GetMaterialData(PageResult pageResult)
        {
            var service = DbCIServiceFactory.CreateInstance<IMaterialService>();
            return service.GetMaterialData(pageResult);
        }

        public DataTable GetAgvTaskDataTable()
        {
            var service = DbCIServiceFactory.CreateInstance<IAgvTaskService>();
            return service.GetAgvTaskDt();
        }

        /// <summary>
        ///  查询站台号
        /// </summary>
        /// <returns></returns>
        public PsbLoc GetLocNo(string LocName)
        {
            var tdate = TableViewServiceFactory.CreateInstance<IPsbLocService>();
            var where = new PsbLoc() { LocNo = LocName };
            return tdate.GetEntityList(where).FirstOrDefault();
        }
    }
}
