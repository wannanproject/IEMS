using IEMS.Frame.WebGlobal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace IEMS.Frame.WebUI
{
    /// <summary>
    /// 用户数据
    /// </summary>
    public class UserData
    {
        private int userid = -99;
        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        private int getUserId()
        {
            int result = userid;
            if (result < 0)
            {
                object obj = HttpContext.Current.Session[GlobalData.USER_DATA_SESSIONNAME];
                if ((obj != null) && (!string.IsNullOrWhiteSpace(obj.ToString())))
                {
                    int tmp = 0;
                    if (int.TryParse(obj.ToString(), out tmp))
                    {
                        result = tmp;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 设置用户Id
        /// </summary>
        /// <returns></returns>
        private void setUserId(int id)
        {
            this.userid = id;
            string ss = id.ToString();
            FormsAuthenticationTicket tk = new FormsAuthenticationTicket(1,
                    ss,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(20),
                    false,
                    ss,
                    FormsAuthentication.FormsCookiePath
                    );
            string key = FormsAuthentication.Encrypt(tk); //得到加密后的身份验证票字串 
            HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, key);
            HttpContext.Current.Response.Cookies.Add(ck);
            HttpContext.Current.Session[GlobalData.USER_DATA_SESSIONNAME] = ss;
        }
        /// <summary>
        /// 用户Id信息
        /// </summary>
        public int UserId { get { return getUserId(); } set { setUserId(value); } }
    }

    /// <summary>
    /// 前台数据
    /// 主要解决是否使用框架的问题
    /// </summary>
    public class HtmlData
    {
        public HtmlData()
        {
            this.GlobalPath = "GlobalPath";
            this.LoaclPath = "LoaclPath";
        }
        /// <summary>
        /// 框架路径-用户前台
        /// </summary>
        public string GlobalPath { get; private set; }
        /// <summary>
        /// 当前插件路径-用户前台
        /// </summary>
        public string LoaclPath { get; private set; }
    }
    /// <summary>
    /// 页面信息
    /// </summary>
    public class PageData
    {
        public PageData()
        {
            this.User = new UserData();
            this.Html = new HtmlData();
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserData User { get; private set; }
        /// <summary>
        /// 前台数据
        /// </summary>
        public HtmlData Html { get; private set; }
    }
}
