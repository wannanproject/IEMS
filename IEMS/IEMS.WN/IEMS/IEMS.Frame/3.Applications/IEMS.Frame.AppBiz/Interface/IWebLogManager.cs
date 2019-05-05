using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.AppBiz
{
    using IEMS.Frame.Entity;

    public interface IWebLogManager : IAppBizManager
    {
        int Insert(SslWebLog entity);
    }
}