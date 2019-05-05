using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.Frame.WebUI.Db
{
    using MSTL.LogAgent;
    using IEMS.Frame.Entity;
    using IEMS.Frame.AppBiz;
    using IEMS.Frame.WebUI.Entity;

    /// <summary>
    /// 页面数据库操作类
    /// </summary>
    public class DbPage
    {
        #region 系统日志  log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion

        #region 权限
        /// <summary>
        /// 将当前页面的菜单类保存到数据库
        /// </summary>
        /// <param name="page"></param>
        public void AddPageAction(PageAction pageAction)
        {
            var manager = AppBizFactory.CreateInstance<IPageActionManager>();
            object max = manager.GetMaxObjId(null);
            int iMax = 0;
            if (max != null)
            {
                int.TryParse(max.ToString(), out iMax);
            }
            iMax++;
            var sspPageAction = new SspPageAction();
            sspPageAction.ObjId = iMax;
            sspPageAction.PageMenuId = pageAction.PageMenu.ObjId;
            sspPageAction.ActionId = pageAction.ActionId;
            sspPageAction.ActionName = pageAction.ActionName;
            sspPageAction.ShowName = pageAction.ShowName;
            sspPageAction.DeleteFlag = 0;
            manager.Insert(sspPageAction);
        }
        /// <summary>
        /// 将当前页面的菜单类保存到数据库
        /// </summary>
        /// <param name="page"></param>
        public void AddPageMenu(PageMenu page)
        {
            var manager = AppBizFactory.CreateInstance<IPageMenuManager>();
            object max = manager.GetMaxObjId(null);
            int iMax = 0;
            if (max != null)
            {
                int.TryParse(max.ToString(), out iMax);
            }
            iMax++;
            var sspPageMenu = new SspPageMenu();
            sspPageMenu.ObjId = iMax;
            sspPageMenu.MenuLevel = "00" + iMax.ToString();
            sspPageMenu.IsShow = 1;
            sspPageMenu.SeqIndex = 1;
            sspPageMenu.PageUrl = page.PageUrl;
            sspPageMenu.ShowName = page.ShowName;
            sspPageMenu.DeleteFlag = 1;
            manager.Insert(sspPageMenu);
        }
        /// <summary>
        /// 获取当前页面对应的菜单类
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public PageMenu GetPageMenu(string url)
        {
            var manager = AppBizFactory.CreateInstance<IPageMenuManager>();
            var lst = manager.GetEntityList(new SspPageMenu() { PageUrl = url });
            var sspPageMenu = lst.FirstOrDefault();
            PageMenu result = null;
            if (sspPageMenu != null)
            {
                result = new PageMenu();
                result.Permit = 0;
                result.NeedPermit = 1;
                result.ObjId = (int)sspPageMenu.ObjId;
                result.PageUrl = sspPageMenu.PageUrl;
                result.ShowName = sspPageMenu.ShowName;
                if (sspPageMenu.DeleteFlag == 1)
                {
                    result.NeedPermit = 0;
                    result.Permit = 1;
                }
            }
            return result;
        }
        private IList<PageAction> SspToPageAction(IList<SspPageAction> lst, PageMenu pageMenu)
        {
            var result = new List<PageAction>();
            foreach (SspPageAction m in lst)
            {
                var a = new PageAction();
                a.PageMenu = pageMenu;
                a.ActionId = (int)m.ActionId;
                a.ActionName = "," + m.ActionName + ",";
                a.ShowName = m.ShowName;
                result.Add(a);
            }
            return result;
        }
        /// <summary>
        /// 获取当前页面数据库对应的权限信息
        /// </summary>
        /// <returns></returns>
        public IList<PageAction> GetPageActionList(PageMenu pageMenu)
        {
            var m = new SspPageAction();
            m.PageMenuId = pageMenu.ObjId;
            var manager = AppBizFactory.CreateInstance<IPageActionManager>();
            var lst = manager.GetEntityList(m);
            return SspToPageAction(lst, pageMenu);
        }
        /// <summary>
        /// 获取当前用户在当前页面数据库对应的权限信息
        /// </summary>
        /// <returns></returns>
        public IList<PageAction> GetUserPageActionList(PageMenu pageMenu, UserData user)
        {
            var manager = AppBizFactory.CreateInstance<IPageActionManager>();
            var lst = manager.GetUserPageActionList(pageMenu.ObjId, user.UserId);
            return SspToPageAction(lst, pageMenu);
        }
        #endregion

        #region Web日志
        /// <summary>
        /// 获取当前页面操作方法的ID
        /// </summary>
        /// <param name="sysPageMethod"></param>
        /// <returns></returns>
        private int getPageMethodId(SspPageMethod sspPageMethod)
        {
            var manager = AppBizFactory.CreateInstance<IPageMethodManager>();
            var method = new SspPageMethod();
            method.PageId = sspPageMethod.PageId;
            method.MethodName = sspPageMethod.MethodName;
            IList<SspPageMethod> lst = manager.GetEntityList(method);
            if (lst.Count > 0)
            {
                return Convert.ToInt32(lst[0].ObjId);
            }
            return 0;
        }
        /// <summary>
        /// 获取当前页面操作方法的ID
        /// </summary>
        /// <param name="sysPageMethod"></param>
        /// <returns></returns>
        private int AppendPageMethod(SspPageMethod sspPageMethod)
        {
            var manager = AppBizFactory.CreateInstance<IPageMethodManager>();
            int result = getPageMethodId(sspPageMethod);
            if (result <= 0)
            {
                sspPageMethod.SeqIndex = sspPageMethod.ObjId;
                sspPageMethod.ShowName = "未知操作";
                manager.Insert(sspPageMethod);
                result = getPageMethodId(sspPageMethod);
            }
            return result;
        }
        /// <summary>
        /// 保存日志信息
        /// </summary>
        /// <param name="method"></param>
        /// <param name="weblog"></param>
        public void AppendWebLog(WebLog weblog)
        {
            var sslWebLog = new SslWebLog();
            sslWebLog.PageId = weblog.Method.PageMenu.ObjId;
            sslWebLog.UserId = weblog.UserId;
            sslWebLog.UserIp = weblog.UserIP;
            sslWebLog.MethodResult = weblog.MethodResult;
            sslWebLog.PageRequest = weblog.PageRequest;
            sslWebLog.Remark = weblog.Remark;

            var sysPageMethod = new SspPageMethod();
            sysPageMethod.PageId = weblog.Method.PageMenu.ObjId;
            sysPageMethod.MethodName = weblog.Method.MethodName;

            var manager = AppBizFactory.CreateInstance<IWebLogManager>();
            sslWebLog.MethodId = AppendPageMethod(sysPageMethod);
            manager.Insert(sslWebLog);
        }

        #endregion
    }

}
