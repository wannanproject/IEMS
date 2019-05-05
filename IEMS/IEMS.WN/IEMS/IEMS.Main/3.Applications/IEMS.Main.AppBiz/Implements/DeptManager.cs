using System.Collections.Generic;

namespace IEMS.Main.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;
    using IEMS.Main.DbCI;

    internal class DeptManager : IDeptManager
    {
        private IDeptService businessService = DbCIServiceFactory.CreateInstance<IDeptService>();
        private ISsbDeptService basicService = TableViewServiceFactory.CreateInstance<ISsbDeptService>();

        public PageResult GetPageDataByReader(PageResult pageResult)
        {
            return this.businessService.GetPageDataByReader(pageResult);
        }

        public SsbDept GetByObjId(int ObjId)
        {
            return this.businessService.GetByObjId(ObjId);
        }
        public IList<SsbDept> GetEntityList(SsbDept entity)
        {
            return this.basicService.GetEntityList(entity);
        }
        public IList<SsbDept> GetEntityList(SsbDept entity, string orderby)
        {
            return this.basicService.GetEntityList(entity,orderby);
        }
        public int Update(SsbDept update, SsbDept where)
        {
            return this.basicService.Update(update, where);
        }
        public int Insert(SsbDept entity)
        {
            return this.basicService.Insert(entity);
        }

        public int GetRowCount(SsbDept entity)
        {
            
            return this.basicService.GetRowCount(entity);
        }
    }
}
