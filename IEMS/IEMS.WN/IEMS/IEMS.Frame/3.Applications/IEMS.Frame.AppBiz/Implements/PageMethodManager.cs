using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Frame.Entity;
    using IEMS.Frame.DbRI;
    using IEMS.Frame.DbCI;

    internal class PageMethodManager: IPageMethodManager
    {
        private ISspPageMethodService basicService = TableViewServiceFactory.CreateInstance<ISspPageMethodService>();
        public IList<SspPageMethod> GetEntityList(SspPageMethod entity)
        {
            return this.basicService.GetEntityList(entity);
        }
        public int Insert(SspPageMethod entity)
        {
            return this.basicService.Insert(entity);
        }
    }
}
