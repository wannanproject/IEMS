namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;
    using MSTL.DbAccess;
    using System.Collections.Generic;

    internal class UserActionManager : IUserActionManager
    {

        private ISspUserActionService basicService = TableViewServiceFactory.CreateInstance<ISspUserActionService>();
        public int Delete(SspUserAction entity)
        {
            return this.basicService.Delete(entity);
        }
        public int BatchInsert(List<SspUserAction> lst)
        {
            var result = 0;
            foreach (var item in lst)
            {
                result += this.basicService.Insert(item);
            }
            return result;
        }
    }
}
