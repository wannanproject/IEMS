using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.DbCI
{
    using MSTL.DbAccess;
    using IEMS.Frame.Entity;

    public interface IPageMenuService : IDbCIService
    {
        SspPageMenu GetByObjId(int ObjId);

        /// <summary>
        /// 获取ObjId最大值
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        object GetMaxObjId(SspPageMenu entity);
    }
}
