using System;
using System.Collections.Generic;
using System.Data;

namespace IEMS.Frame.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Frame.Entity;
    using IEMS.Frame.DbCI;
    using IEMS.Frame.DbRI;

    internal class PageActionManager: IPageActionManager
    {
        private IPageActionService DbCIService = DbCIServiceFactory.CreateInstance<IPageActionService>();
        private ISspPageActionService basicService = TableViewServiceFactory.CreateInstance<ISspPageActionService>();

        /// <summary>
        /// 获取ObjId最大值
        /// </summary>
        /// <returns></returns>
        public object GetMaxObjId(SspPageAction entity)
        {
            return this.DbCIService.GetMaxObjId(entity);
        }

        /// <summary>
        /// 获取当前页面用户的操作信息
        /// </summary>
        /// <param name="menuId">The menuId.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public IList<SspPageAction> GetUserPageActionList(int menuId, int userId)
        {
            return this.DbCIService.GetUserPageActionList(menuId, userId);
        }


        /// <summary>
        /// 获取当权限用户的操作信息
        /// </summary>
        /// <param name="pageActionId">The pageActionId.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataTable GetUserPageAction(int pageActionId, int? userId)
        {
            string stmtId = "GetUserPageAction";
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("PageActionId", pageActionId);
            where.Add("UserId", userId);
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("where", where);
            return this.DbCIService.GetDataTableByStatement(stmtId, param);
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="pageAction">The pageActionId.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int Insert(SspPageAction entity)
        {
            return basicService.Insert(entity);
        }


        public IList<SspPageAction> GetEntityList(SspPageAction entity)
        {
            return basicService.GetEntityList(entity);
        }
    }
}
