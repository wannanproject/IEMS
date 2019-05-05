using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ext.Net;
using IEMS.Frame.WebUI.Db;
using IEMS.Frame.WebUI.Entity;
using MSTL.LogAgent;

namespace IEMS.Frame.WebUI
{
    /// <summary>
    /// Page 日志类
    /// </summary>
    public class PageLog : IEMS.Frame.WebUI.BasePage
    {
        #region 系统日志  log
        public ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion
        
        public PageLog() : base()
        {
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="Remark"></param>
        /// <param name="MethodResult"></param>
        /// <param name="MethodIndex"></param>
        public void AppendWebLog(string Remark, string MethodResult, int MethodIndex)
        {
            WebLog webLog = new WebLog();
            webLog.UserId = this.Data.User.UserId;
            webLog.PageRequest = this.Request.Form.ToString();
            webLog.Remark = Remark;
            webLog.MethodResult = MethodResult;
            webLog.UserIP = this.Request.UserHostAddress;
            if (webLog.UserIP == "::1")
            {
                webLog.UserIP = "127.0.0.1";
            }
            PageMethod pageMethod = new PageMethod();
            MethodBase stackMethod = new StackTrace().GetFrame(MethodIndex).GetMethod();
            pageMethod.MethodName = stackMethod.ToString();
            pageMethod.PageMenu = this.PageMenu;
            webLog.Method = pageMethod;
            new DbPage().AppendWebLog(webLog);
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="Remark">The remark.</param>
        /// <param name="MethodResult">The method result.</param>
        public void AppendWebLog(string Remark, string MethodResult)
        {
            AppendWebLog(Remark, MethodResult, 2);
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="MethodResult">The method result.</param>
        public void AppendWebLog(string MethodResult)
        {
            AppendWebLog(string.Empty, MethodResult, 2);
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        public void AppendWebLog()
        {
            AppendWebLog(string.Empty, string.Empty, 2);
        }
    }
}