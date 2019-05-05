namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;
    using System.Collections.Generic;
    using MSTL.DbAccess;

    internal class RoleManager:IRoleManager
    {
        private ISspRoleService basicService = TableViewServiceFactory.CreateInstance<ISspRoleService>();
        public  IList<SspRole> GetEntityList(SspRole entity)
        {
            return this.basicService.GetEntityList(entity);
        }
        public IList<SspRole> GetEntityList(SspRole entity, string orderby)
        {
            return this.basicService.GetEntityList(entity, orderby);
        }
    }
}
