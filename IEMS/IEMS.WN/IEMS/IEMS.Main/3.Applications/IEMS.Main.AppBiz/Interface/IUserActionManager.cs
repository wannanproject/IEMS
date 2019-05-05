namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;
    using System.Collections.Generic;

    public interface IUserActionManager : IAppBizManager
    {
        int Delete(SspUserAction entity);
        int BatchInsert(List<SspUserAction> lst);
    }
}
