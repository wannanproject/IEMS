using System.Collections.Generic;
using System.Linq;
namespace IEMS.Main.DbCI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;

    internal class PageMenuService : DbCIService, IPageMenuService
    {
        private ISspPageMenuService basicService;
        #region 构造方法
        public PageMenuService() : base()
        {
            basicService = TableViewServiceFactory.CreateInstance<ISspPageMenuService>();
        }
        public PageMenuService(string dbName) : base(dbName)
        {
            basicService = TableViewServiceFactory.CreateInstance<ISspPageMenuService>(dbName);
        }
        #endregion

        public SspPageMenu GetByObjId(int ObjId)
        {
            var result = this.basicService.GetEntityList(
                new SspPageMenu() { ObjId = (long)ObjId }
                ).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 获取首层主菜单列表方法
        /// </summary> 
        /// <param name="menuLevelLike">菜单级别Like值</param>
        /// <param name="menuLevelLength">菜单级别长度</param>
        /// <returns></returns>
        public IList<SspPageMenu> GetMainPageMenuList(string menuLevelLike, int menuLevelLength)
        {
            string stmtId = "GetMainPageMenuList";
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("MenuLevelLike", menuLevelLike);
            where.Add("MenuLevelLength", menuLevelLength);
            IList<SspPageMenu> resultList = this.GetEntityByStatement<SspPageMenu>(stmtId, where);
            return resultList;
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
            string stmtId = "GetUserMenuPageList";
            System.Collections.Hashtable parameters = new System.Collections.Hashtable();
            parameters.Add("UserID", userid);
            parameters.Add("MenuLevel", parid);
            IList<SspPageMenu> resultList = this.GetEntityByStatement<SspPageMenu>(stmtId, parameters);
            return resultList;
        }

    }
}
