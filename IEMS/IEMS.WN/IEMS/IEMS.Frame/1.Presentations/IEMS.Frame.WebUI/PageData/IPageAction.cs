using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using IEMS.Frame.WebUI.Entity;
using IEMS.Frame.WebUI.Db;

namespace IEMS.Frame.WebUI
{
    /// <summary>
    /// 页面权限定义基类
    /// </summary>
    public class ___
    {
        #region 初始化属性
        public PageMenu PageMenu { get; set; }
        public UserData User { get; set; }
        public IList<PageAction> DbActionList { get; set; }
        public IList<PageAction> DbUserActionList { get; set; }
        #endregion
        /// <summary>
        /// 页面数据库操作类
        /// </summary>
        private DbPage dbPage = new DbPage();
        /// <summary>
        /// 反射页面中的权限
        /// </summary>
        /// <returns>List{SysPageAction}.</returns>
        private List<PageAction> Reflection()
        {
            List<PageAction> lst = new List<PageAction>();
            Type type = this.GetType();
            #region 属性
            PropertyInfo[] piList = type.GetProperties();
            foreach (PropertyInfo pi in piList)
            {
                if (pi.PropertyType == typeof(PageAction))
                {
                    PageAction m = (PageAction)pi.GetValue(this, null);
                    if (string.IsNullOrWhiteSpace(m.ShowName))
                    {
                        m.ShowName = pi.Name;
                    }
                    m.Permit = 0;
                    lst.Add(m);
                }
            }
            #endregion
            #region 字段
            FieldInfo[] miList = type.GetFields();
            foreach (FieldInfo pi in miList)
            {
                if (pi.FieldType == typeof(PageAction))
                {
                    PageAction m = (PageAction)pi.GetValue(this);
                    if (string.IsNullOrWhiteSpace(m.ShowName))
                    {
                        m.ShowName = pi.Name;
                    }
                    m.Permit = 0;
                    lst.Add(m);
                }
            }
            #endregion
            #region 去除重复的ActionId
            List<PageAction> Result = new List<PageAction>();
            for (int i = lst.Count - 1; i >= 0; i--)
            {
                bool isExist = false;
                PageAction m = lst[i];
                foreach (PageAction a in Result)
                {
                    if (m.ActionId == a.ActionId)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    Result.Add(m);
                }
            }
            #endregion
            return Result;
        }

        /// <summary>
        /// 判断数据库中是否已经存在权限
        /// </summary>
        /// <param name="pageAction"></param>
        /// <returns></returns>
        private bool dbExistPageAction(PageAction pageAction)
        {
            bool result = false;
            var dbActionList = this.DbActionList;
            foreach (PageAction dbAction in dbActionList)
            {
                if (pageAction.ActionId == dbAction.ActionId)
                {
                    result = true;
                    if (!string.IsNullOrWhiteSpace(pageAction.ShowName))
                    {
                        dbAction.ShowName = pageAction.ShowName;
                    }
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 初始化当前页面的权限信息
        /// </summary>
        /// <param name="pageMenu">The page.</param>
        public void IniPageAction()
        {
            var pageActionList = Reflection();
            var pageMenu = this.PageMenu;
            foreach (PageAction pageAction in pageActionList)
            {
                pageAction.PageMenu = pageMenu;
                pageAction.Permit = 0;
            }
            foreach (PageAction pageAction in pageActionList)
            {
                if (!dbExistPageAction(pageAction))
                {
                    dbPage.AddPageAction(pageAction);
                }
            }
            if (pageMenu.NeedPermit== 0)
            {
                foreach (PageAction m in pageActionList)
                {
                    m.Permit = 1;
                }
            }
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="pageAction"></param>
        /// <returns></returns>
        private bool CheckPageAction(PageAction pageAction)
        {
            var dbUserActionList = this.DbUserActionList;
            foreach (PageAction dbAction in dbUserActionList)
            {
                if (pageAction.ActionId == dbAction.ActionId)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 用户权限信息绑定
        /// </summary>
        public void UserBindAction()
        {
            var pageActionList = Reflection();
            var pageMenu = this.PageMenu;
            if (pageMenu.NeedPermit == 0)
            {
                foreach (PageAction pageAction in pageActionList)
                {
                    pageAction.Permit = 1;
                }
                return;
            }
            var dbUserActionList = this.DbUserActionList;
            foreach (PageAction pageAction in pageActionList)
            {
                if (pageAction.Permit == 0)
                {
                    if (CheckPageAction(pageAction))
                    {
                        pageAction.Permit = 1;
                    }
                }
            }
        }
    }
}
