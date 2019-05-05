using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Frame.Entity;
    using IEMS.Frame.DbRI;
    using IEMS.Frame.DbCI;

    internal class PageMenuManager: IPageMenuManager
    {
        private IPageMenuService DbCIService = DbCIServiceFactory.CreateInstance<IPageMenuService>();
        private ISspPageMenuService basicService = TableViewServiceFactory.CreateInstance<ISspPageMenuService>();

        /// <summary>
        /// 获取ObjId最大值
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public object GetMaxObjId(SspPageMenu entity)
        {
            return this.DbCIService.GetMaxObjId(entity);
        }

        public int Insert(SspPageMenu entity)
        {
            return this.basicService.Insert(entity);
        }
        public IList<SspPageMenu> GetEntityList(SspPageMenu entity)
        {
            return this.basicService.GetEntityList(entity);
        }
    }
}
