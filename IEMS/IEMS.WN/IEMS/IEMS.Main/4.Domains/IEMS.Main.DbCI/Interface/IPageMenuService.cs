using System.Collections.Generic;

namespace IEMS.Main.DbCI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    public interface IPageMenuService : IDbCIService
    {
        SspPageMenu GetByObjId(int ObjId);

        /// <summary>
        /// 获取首层主菜单列表方法
        /// </summary> 
        /// <param name="menuLevelLike">菜单级别Like值</param>
        /// <param name="menuLevelLength">菜单级别长度</param>
        /// <returns></returns>
        IList<SspPageMenu> GetMainPageMenuList(string menuLevelLike, int menuLevelLength);

        /// <summary>
        /// 获取用户操作的菜单列表
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="parid">菜单级别</param>
        /// <returns></returns>
        /// <remarks></remarks>
        IList<SspPageMenu> GetUserMenuPageList(int userid, string parid);
    }
}
