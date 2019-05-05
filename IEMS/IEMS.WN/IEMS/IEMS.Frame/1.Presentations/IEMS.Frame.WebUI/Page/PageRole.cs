using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ext.Net;
using IEMS.Frame.WebUI.Db;
using IEMS.Frame.WebUI.Entity;

namespace IEMS.Frame.WebUI
{
    /// <summary>
    /// Page 权限类
    /// </summary>
    public class PageRole : IEMS.Frame.WebUI.PageLog
    {
        #region 接口设置信息
        /// <summary>
        /// 当前页面无权限进行的处理
        /// </summary>
        private void NoPermit()
        {
            string urlNoPermit = "~/NoPermit.aspx";
            Response.Redirect(urlNoPermit);
        }
        /// <summary>
        /// 初始化当前页面时，增加的脚本信息
        /// </summary>
        private void IniClientPageScript()
        {
            this.ClientScript.RegisterClientScriptInclude("documentOnkeydown", ResolveClientUrl("~/resources/js/onkeydown.js"));
        }

        #endregion

        #region 类成员信息
        public PageRole() : base()
        {
        }

        /// <summary>
        /// 通过页面初始化设定不进行用户权限管理
        /// </summary>
        public bool NoCheckPagePermit { get; set; }
        /// <summary>
        /// 当前页面需要进行权限校验的操作项
        /// </summary>
        private IList<PageAction> PageDbActionList = null;
        /// <summary>
        /// 当前页面用户包含的操作项
        /// </summary>
        private IList<PageAction> UserPageDbActionList = null;
        /// <summary>
        /// 页面数据库操作类
        /// </summary>
        private DbPage dbPage = new DbPage();
        #endregion

        #region 控件权限处理
        #region 设置权限
        /// <summary>
        /// 根据权限设置控件属性
        /// </summary>
        /// <param name="webcontrol"></param>
        /// <param name="can"></param>
        private void setControlAction(WebControl webcontrol, bool can)
        {
            if (can)
            {
                return;
            }
            //webcontrol.Visible = can;
            if (webcontrol is Ext.Net.ButtonBase)
            {
                (webcontrol as Ext.Net.ButtonBase).Disabled = !can;
                (webcontrol as Ext.Net.ButtonBase).ToolTip = (webcontrol as Ext.Net.ButtonBase).ToolTip + "【无此权限】";
            }
            else if (webcontrol is Ext.Net.Field)
            {
                (webcontrol as Ext.Net.Field).ReadOnly = !can;
            }
            else if (webcontrol is Ext.Net.ActionColumn)
            {
                foreach (ActionItem item in (webcontrol as Ext.Net.ActionColumn).Items)
                {
                    item.Disabled = !can;
                }
            }
            else if (webcontrol is Ext.Net.AbstractComponent)
            {
                (webcontrol as Ext.Net.AbstractComponent).Disabled = !can;
            }
            else
            {
                webcontrol.Enabled = can;
            }
        }
        /// <summary>
        /// 根据权限设置控件属性
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="can"></param>
        private void setControlAction(Ext.Net.GridCommand cmd, bool can)
        {
            if (can)
            {
                return;
            }
            cmd.Disabled = !can;
            if (!can)
            {
                cmd.ToolTip.Text = "【无此权限】";
            }
        }
        /// <summary>
        /// 根据权限设置控件属性
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="can"></param>
        private void setControlAction(Ext.Net.MenuCommand cmd, bool can)
        {
            cmd.Disabled = !can;
        }
        #endregion

        #region 获取用户操作控件的权限
        /// <summary>
        /// 对单个操作项进行权限校验
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        private bool checkControlActionPermit(string actionName)
        {
            #region 获取本页权限
            if (PageDbActionList == null || PageDbActionList.Count == 0)
            {
                return true;
            }
            if (UserPageDbActionList == null || UserPageDbActionList.Count == 0)
            {
                return false;
            }
            #endregion
            bool result = true;
            #region 是否需要对此项进行权限校验
            foreach (PageAction action in PageDbActionList)
            {
                if (action.ActionName.ToUpper().Contains("," + actionName.ToUpper() + ","))
                {
                    result = false;
                    break;
                }
            }
            #endregion
            #region 用户是否有操作此项的权限
            if (!result)
            {
                foreach (PageAction action in UserPageDbActionList)
                {
                    if (action.ActionName.ToUpper().Contains("," + actionName.ToUpper() + ","))
                    {
                        result = true;
                        break;
                    }
                }
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 系统控件进行权限校验
        /// </summary>
        /// <param name="webcontrol"></param>
        /// <returns></returns>
        private bool checkControlActionPermit(WebControl webcontrol)
        {
            string id = webcontrol.ID;
            return checkControlActionPermit(id);
        }
        /// <summary>
        /// 系统控件进行权限校验
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private bool checkControlActionPermit(Ext.Net.GridCommand cmd)
        {
            string id = cmd.CommandName;
            return checkControlActionPermit(id);
        }
        /// <summary>
        /// 系统控件进行权限校验
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private bool checkControlActionPermit(Ext.Net.MenuCommand cmd)
        {
            string id = cmd.CommandName;
            return checkControlActionPermit(id);
        }
        #endregion

        #region 设置控件
        /// <summary>
        /// 设置Ext中菜单
        /// </summary>
        /// <param name="menus"></param>
        private void setCommandColumn(Ext.Net.CommandMenu menus)
        {
            foreach (Ext.Net.MenuCommand menu in menus.Items)
            {
                bool check = checkControlActionPermit(menu);
                if (!check)
                {
                    setControlAction(menu, check);
                }
                else
                {
                    setCommandColumn(menu.Menu);
                }
            }
        }
        /// <summary>
        /// 设置Ext中GridCommand
        /// </summary>
        /// <param name="cmds"></param>
        private void setCommandColumn(Ext.Net.CommandColumn cmds)
        {
            foreach (object cmd in cmds.Commands)
            {
                if (cmd is Ext.Net.GridCommand)
                {
                    bool check = checkControlActionPermit(cmd as Ext.Net.GridCommand);
                    if (!check)
                    {
                        setControlAction(cmd as Ext.Net.GridCommand, check);
                    }
                    else
                    {
                        setCommandColumn((cmd as Ext.Net.GridCommand).Menu);
                    }
                }
            }
        }


        /// <summary>
        /// 系统控件进行权限校验
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private bool checkControlActionPermit(Ext.Net.ActionItem cmd)
        {
            string id = cmd.Handler;
            return checkControlActionPermit(id);
        }
        /// <summary>
        /// 根据权限设置控件属性
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="can"></param>
        private void setControlAction(Ext.Net.ActionItem cmd, bool can)
        {
            if (can)
            {
                return;
            }
            cmd.Disabled = !can;
            if (!can)
            {
                cmd.Tooltip = "【无此权限】";
            }
        }
        /// <summary>
        /// 设置Ext中GridCommand
        /// </summary>
        /// <param name="cmds"></param>
        private void setActionColumn(Ext.Net.ActionColumn cmds)
        {
            foreach (object cmd in cmds.Items)
            {
                if (cmd is Ext.Net.ActionItem)
                {
                    bool check = checkControlActionPermit(cmd as Ext.Net.ActionItem);
                    if (!check)
                    {
                        setControlAction(cmd as Ext.Net.ActionItem, check);
                    }
                }
            }
        }


        /// <summary>
        /// 递归设置控件
        /// </summary>
        /// <param name="webcontrol"></param>
        private void setControls(WebControl webcontrol)
        {
            bool check = checkControlActionPermit(webcontrol);
            if (!check)
            {
                setControlAction(webcontrol, false);
            }
            else
            {
                foreach (object o in webcontrol.Controls)
                {
                    if (o is WebControl)
                    {
                        WebControl _webcontrol = o as WebControl;
                        setControls(_webcontrol);
                    }
                }
                if (webcontrol is Ext.Net.ActionColumn)
                {
                    setActionColumn(webcontrol as Ext.Net.ActionColumn);
                }
                if (webcontrol is Ext.Net.CommandColumn)
                {
                    setCommandColumn(webcontrol as Ext.Net.CommandColumn);
                }
            }
        }
        #endregion
        /// <summary>
        /// 设置页面中的控件
        /// </summary>
        /// <param name="page"></param>
        private void setPageControls(System.Web.UI.Page page)
        {
            foreach (object o in page.Controls)
            {
                if (o is HtmlForm)
                {
                    HtmlForm htmlform = o as HtmlForm;
                    foreach (object _o in htmlform.Controls)
                    {
                        if (_o is WebControl)
                        {
                            setControls(_o as WebControl);
                        }
                    }
                }
            }
        }
        #endregion

        #region 页面权限处理
        /// <summary>
        /// 判断页面权限
        /// </summary>
        /// <returns></returns>
        private bool checkPagePermit()
        {
            if (this.PageDbActionList.Count > 0
                && this.UserPageDbActionList.Count == 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 页面初始化
        #region 页面权限设置项
        /// <summary>
        /// 是否进行权限判断
        /// </summary>
        /// <returns></returns>
        private bool doNotCheckPermit()
        {
            if (this.PageMenu.NeedPermit == 0)
            {
                return true;
            }
            if (this.NoCheckPagePermit)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region 页面数据初始化
        /// <summary>
        /// 通过反射获取当前页面中的权限设置项
        /// </summary>
        /// <returns></returns>
        private ___ getPageActionListByReflection()
        {
            ___ result = null;
            Type type = this.GetType();
            #region 属性
            PropertyInfo[] piList = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (PropertyInfo pi in piList)
            {
                if (pi.PropertyType == typeof(___))
                {
                    return (___)pi.GetValue(this, null);
                }
                if (pi.PropertyType.IsSubclassOf(typeof(___)))
                {
                    return (___)pi.GetValue(this, null);
                }
            }
            #endregion
            #region 字段
            FieldInfo[] miList = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo pi in miList)
            {
                if (pi.FieldType == typeof(___))
                {

                    return (___)pi.GetValue(this);
                }
                if (pi.FieldType.IsSubclassOf(typeof(___)))
                {
                    return (___)pi.GetValue(this);
                }
            }
            #endregion
            return result;
        }
        /// <summary>
        /// 页面权限项初始化
        /// </summary>
        private void PageActionIni()
        {
            this.PageDbActionList = dbPage.GetPageActionList(this.PageMenu);
            this.UserPageDbActionList = dbPage.GetUserPageActionList(this.PageMenu, this.Data.User);
            ___ action = getPageActionListByReflection();
            if (action != null)
            {
                action.PageMenu = this.PageMenu;
                action.User = this.Data.User;
                action.DbActionList = this.PageDbActionList;
                action.DbUserActionList = this.UserPageDbActionList;
                action.IniPageAction();
                action.UserBindAction();
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        private void PageIni()
        {
            PageActionIni();
            if (this.IsPostBack || X.IsAjaxRequest)
            {
                return;
            }
            IniClientPageScript();
            if (doNotCheckPermit())
            {
                return;
            }
            int userid = this.Data.User.UserId;
            if (userid < 0)
            {
                return;
            }
            if (checkPagePermit())
            {
                setPageControls(this);
            }
            else
            {
                NoPermit();
            }
        }
        #endregion

        #region PageMenu
        /// <summary>
        /// 获取网页的url，去除和Ext相关的内容   _dc=
        /// </summary>
        /// <returns></returns>
        protected virtual string GetPageUrl()
        {
            string applicationPath = this.Request.ApplicationPath;
            string rawUrl = this.Request.RawUrl;
            rawUrl = rawUrl.Substring(applicationPath.Length);
            if (rawUrl.ToLower().Contains("_dc="))
            {
                int istart = rawUrl.IndexOf("_dc=");
                int iend = rawUrl.LastIndexOf("&");
                if (iend <= istart)
                {
                    iend = rawUrl.Length;
                }
                rawUrl = rawUrl.Substring(0, istart).Trim() + rawUrl.Substring(iend).Trim();
                while (true)
                {
                    if ((rawUrl.EndsWith("?")) || (rawUrl.EndsWith("&")))
                    {
                        rawUrl = rawUrl.Substring(0, rawUrl.Length - 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (rawUrl.StartsWith("/"))
            {
                rawUrl = rawUrl.Substring(1);
            }
            return rawUrl.ToUpper();
        }
        /// <summary>
        /// 获取页面类型信息
        /// </summary>
        /// <returns></returns>
        private PageMenu getPageMenu()
        {
            var dbPage = new DbPage();
            PageMenu result = null;
            string url = this.GetPageUrl();
            result = dbPage.GetPageMenu(url);
            if (result == null)
            {
                PageMenu m = new PageMenu();
                m.ShowName = string.IsNullOrWhiteSpace(this.Title) ? "请设置名称" : this.Title;
                m.PageUrl = url;
                dbPage.AddPageMenu(m);
                return getPageMenu();
            }
            else
            {
                return result;
            }
        }
        #endregion
        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);
                this.PageMenu = getPageMenu();
                PageIni();
            }
            catch { }
        }
        #endregion

    }
}