using System.Collections.Generic;

namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;

    public interface IRoleManager : IAppBizManager
    {
        IList<SspRole> GetEntityList(SspRole entity);
        IList<SspRole> GetEntityList(SspRole entity,string orderby);
    }
}
