using System;
using System.Collections.Generic;
using System.Data;

namespace IEMS.Frame.AppBiz
{
    using IEMS.Frame.Entity;

    public interface IPageActionManager : IAppBizManager
    {
        /// <summary>
        /// 获取ObjId最大值
        /// </summary>
        /// <returns></returns>
        object GetMaxObjId(SspPageAction entity);

        /// <summary>
        /// 获取当前页面用户的操作信息
        /// </summary>
        /// <param name="menuId">The menuId.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        IList<SspPageAction> GetUserPageActionList(int menuId, int userId);


        /// <summary>
        /// 获取当权限用户的操作信息
        /// </summary>
        /// <param name="pageActionId">The pageActionId.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        DataTable GetUserPageAction(int pageActionId, int? userId);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">The pageActionId.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int Insert(SspPageAction entity);

        IList<SspPageAction> GetEntityList(SspPageAction entity);
    }
}