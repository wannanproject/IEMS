using System.Data;

namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;
    using MSTL.DbAccess;
    using System.Collections.Generic;

    internal class UserAllActionManager: IUserAllActionManager
    {
        private IViewUserAllActionService basicService = TableViewServiceFactory.CreateInstance<IViewUserAllActionService>();
        public IList<ViewUserAllAction> GetEntityList(ViewUserAllAction entity)
        {
            return this.basicService.GetEntityList(entity);
        }
    }
}
