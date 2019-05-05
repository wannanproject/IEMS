using System.Collections.Generic;
using System.Data;
namespace IEMS.Main.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;
    using IEMS.Main.DbCI;
    internal class PageMenuManager: IPageMenuManager
    {
        private IPageMenuService businessService = DbCIServiceFactory.CreateInstance<IPageMenuService>();
        private ISspPageMenuService basicService = TableViewServiceFactory.CreateInstance<ISspPageMenuService>();

        public SspPageMenu GetByObjId(int ObjId)
        {
            return this.businessService.GetByObjId(ObjId);
        }

        /// <summary>
        /// 获取首层主菜单列表方法
        /// </summary> 
        /// <param name="menuLevelLike">菜单级别Like值</param>
        /// <param name="menuLevelLength">菜单级别长度</param>
        /// <returns></returns>
        public IList<SspPageMenu> GetMainPageMenuList(string menuLevelLike, int menuLevelLength)
        {
            return this.businessService.GetMainPageMenuList(menuLevelLike, menuLevelLength);
        }

        /// <summary>
        /// 获取用户操作的菜单列表
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="parid">菜单级别</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public IList<SspPageMenu> GetUserMenuPageList(int userid, string parid)
        {
            return this.businessService.GetUserMenuPageList(userid, parid);
        }

        public PageResult GetPageDataByReader(PageResult pageResult)
        {
            return this.businessService.GetPageDataByReader(pageResult);
        }

        public int Update(SspPageMenu update, SspPageMenu where)
        {
           return this.basicService.Update(update, where);
        }
        public IList<SspPageMenu> GetEntityList(SspPageMenu entity)
        {
            return this.basicService.GetEntityList(entity);
        }
        public int Insert(SspPageMenu entity)
        {
            return this.basicService.Insert(entity);
        }
        
    }
}
