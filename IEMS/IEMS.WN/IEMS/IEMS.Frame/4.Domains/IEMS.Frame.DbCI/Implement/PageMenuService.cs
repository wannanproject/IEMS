using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IEMS.Frame.DbCI
{
    using MSTL.DbAccess;
    using IEMS.Frame.Entity;
    using IEMS.Frame.DbRI;

    internal class PageMenuService : DbCIService, IPageMenuService
    {
        private ISspPageMenuService basicService;

        #region 构造方法

        public PageMenuService() : base() {
            basicService = TableViewServiceFactory.CreateInstance<ISspPageMenuService>();
        }

        public PageMenuService(string dbName) : base(dbName) { }

        #endregion

        public SspPageMenu GetByObjId(int ObjId)
        {
            var result = this.basicService.GetEntityList(new SspPageMenu() { ObjId = (long)ObjId }).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 获取ObjId最大值
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public object GetMaxObjId(SspPageMenu entity)
        {
            return this.basicService.GetMaxValue("OBJID", null);
        }
    }
}
