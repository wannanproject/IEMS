using System.Data;

namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;
    using System.Collections.Generic;

    public interface IUserAllActionManager : IAppBizManager
    {
        IList<ViewUserAllAction> GetEntityList(ViewUserAllAction entity);
    }
}
