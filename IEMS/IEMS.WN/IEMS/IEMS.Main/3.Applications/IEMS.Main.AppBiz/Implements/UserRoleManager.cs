namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;
    using MSTL.DbAccess;
    using System.Collections.Generic;

    internal class UserRoleManager : IUserRoleManager
    {
        private ISspUserRoleService basicService = TableViewServiceFactory.CreateInstance<ISspUserRoleService>();
        public int Delete(SspUserRole entity)
        {
            return this.basicService.Delete(entity);
        }
        public IList<SspUserRole> GetEntityList(SspUserRole entity)
        {
            return this.basicService.GetEntityList(entity);
        }
        public int BatchInsert(List<SspUserRole> lst)
        {
            var result = 0;
            foreach (var item in lst)
            {
                result += this.basicService.Insert(item);
            }
            return result;
        }
        public int Insert(SspUserRole entity)
        {
            return this.basicService.Insert(entity);
        }
    }
}
