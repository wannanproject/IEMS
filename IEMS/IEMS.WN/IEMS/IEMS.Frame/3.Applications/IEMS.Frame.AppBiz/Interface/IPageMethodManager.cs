using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.AppBiz
{
    using IEMS.Frame.Entity;

    public interface IPageMethodManager : IAppBizManager
    {
        IList<SspPageMethod> GetEntityList(SspPageMethod entity);
        int Insert(SspPageMethod entity);
    }
}