using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Frame.Entity;
    using IEMS.Frame.DbRI;
    using IEMS.Frame.DbCI;

    internal class WebLogManager: IWebLogManager
    {
        private ISslWebLogService basicService = TableViewServiceFactory.CreateInstance<ISslWebLogService>();
     
        public int Insert(SslWebLog entity)
        {
            return this.basicService.Insert(entity);
        }
    }
}
