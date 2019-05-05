using System.Collections.Generic;

namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;
    using MSTL.DbAccess;

    internal class DeptRoleManager : IDeptRoleManager
    {
        private ISspDeptRoleService basicService = TableViewServiceFactory.CreateInstance<ISspDeptRoleService>();
        public IList<SspDeptRole> GetEntityList(SspDeptRole entity)
        {
            return this.basicService.GetEntityList(entity);
        }
        public int Delete(SspDeptRole entity)
        {
            return this.basicService.Delete(entity);
        }
        public int BatchInsert(List<SspDeptRole> lst)
        {
            int result = 0;
            foreach (var item in lst)
            {
                result += this.basicService.Insert(item);
            }
            return result;
        }
        public int GetRowCount(SspDeptRole entity)
        {
            return this.basicService.GetRowCount(entity);
        }
    }
}
