using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.AppBiz
{
    using IEMS.Frame.Entity;

    public interface IPageMenuManager : IAppBizManager
    {
        /// <summary>
        /// 获取ObjId最大值
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        object GetMaxObjId(SspPageMenu entity);
        int Insert(SspPageMenu entity);
        IList<SspPageMenu> GetEntityList(SspPageMenu entity);
    }
}