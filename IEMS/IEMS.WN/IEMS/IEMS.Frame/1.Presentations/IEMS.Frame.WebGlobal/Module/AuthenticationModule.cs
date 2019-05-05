using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;

namespace IEMS.Frame.WebGlobal
{
    /// <summary>
    /// AuthenticationModule 实现类
    /// </summary>
    /// <remarks></remarks>
    public class AuthenticationModule : IHttpModule , System.Web.SessionState.IRequiresSessionState
    {
        private static List<string> NoCheckList = new List<string>();
        private static string RedirectTo = "~/Index.htm";
        private HttpApplication app;
        public void Init(HttpApplication httpApplication)
        {
            app = httpApplication;
            app.PreRequestHandlerExecute += app_PreRequestHandlerExecute;
            //app.BeginRequest += new EventHandler(context_BeginRequest);
            if (NoCheckList.Count == 0)
            {
                AuthenticationXML authentication = new AuthenticationXML();
                NoCheckList = authentication.NoCheckList;
                RedirectTo = authentication.RedirectTo;
            }
        }
        void app_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication Application = (HttpApplication)sender;
            HttpContext context = Application.Context;

            if (GetCurrentPageUrl(context).ToLower().IndexOf(".aspx") > 0)
            {
                if (NoCheck(context))
                {
                    return;
                }
                if (!IsAuthenticationBySession())
                {
                    if (GetCurrentPageUrl(context).ToLower().IndexOf(RedirectTo.ToLower()) < 0)
                    {
                        Application.Response.Redirect(RedirectTo);
                    }
                }
            }
        }

        public void Dispose()
        {
            return;
            throw new NotImplementedException();
        }
        private string GetCurrentPageUrl(HttpContext context)
        {
            string applicationPath = context.Request.ApplicationPath;
            string rawUrl = context.Request.RawUrl;
            rawUrl = rawUrl.Substring(applicationPath.Length);
            int indexOfstring = 0;
            indexOfstring = rawUrl.IndexOf("?");
            if (indexOfstring >= 0)
            {
                rawUrl = rawUrl.Substring(0, indexOfstring);
            }
            indexOfstring = rawUrl.IndexOf("#");
            if (indexOfstring >= 0)
            {
                rawUrl = rawUrl.Substring(0, indexOfstring);
            }
            if (rawUrl.StartsWith("/"))
            {
                rawUrl = rawUrl.Substring(1);
            }
            return rawUrl;
        }
        private bool NoCheck(HttpContext context)
        {
            if (GetCurrentPageUrl(context).ToLower().IndexOf("fastreport") > -1)
            {
                return true;
            }
            foreach (string url in NoCheckList)
            {
                if (GetCurrentPageUrl(context).ToLower().StartsWith(url.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication Application = (HttpApplication)sender;
            HttpContext context = Application.Context;

            if (GetCurrentPageUrl(context).ToLower().IndexOf(".aspx") > 0)
            {
                if (NoCheck(context))
                {
                    return;
                }
                if (!IsAuthenticationByCookie())
                {
                    if (GetCurrentPageUrl(context).ToLower().IndexOf(RedirectTo.ToLower()) < 0)
                    {
                        Application.Response.Redirect("~/" + RedirectTo);
                    }
                }
            }
        }

        protected bool IsAuthenticationByCookie()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = app.Context.Request.Cookies[cookieName];

            if (null == authCookie)
            {
                // 没有验证 Cookie。
                return false;
            }

            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception)
            {
                return false;
            }

            if (null == authTicket)
            {
                // Cookie 无法解密。
                return false;
            }
            return true;
        }

        protected bool IsAuthenticationBySession()
        {
            if (null == app.Context.Session)
            {
                // 没有验证 Cookie。
                return false;
            }
            string userid = GlobalData.USER_DATA_SESSIONNAME;
            if (null == app.Context.Session[userid])
            {
                // 没有验证 Cookie。
                return false;
            }
            return true;
        }

    }

    /// <summary>
    /// AuthenticationXML 实现类
    /// </summary>
    /// <remarks></remarks>
    public class AuthenticationXML
    {
        private readonly string configFilePath = "~/config/Authentication.xml";
        private void FindNode(XmlNodeList Nodes, ref List<string> NoCheckList)
        {
            foreach (XmlNode node in Nodes)
            {
                bool isRead = false;

                if (node.Name.ToLower() == "location".ToLower()
                    && node.ParentNode != null && node.ParentNode.Name.ToLower() == "NoCheckAuthentication".ToLower())
                {
                    XmlAttributeCollection Attributes = node.Attributes;
                    foreach (XmlAttribute Attribute in Attributes)
                    {
                        if (Attribute.Name.ToLower() == "path".ToLower())
                        {
                            NoCheckList.Add(Attribute.Value.Trim());
                        }
                    }
                }
                if (node.Name.ToLower() == "RedirectTo".ToLower()
                    && node.ParentNode != null && node.ParentNode.Name.ToLower() == "NoCheckAuthentication".ToLower())
                {
                    XmlAttributeCollection Attributes = node.Attributes;
                    foreach (XmlAttribute Attribute in Attributes)
                    {
                        if (Attribute.Name.ToLower() == "path".ToLower())
                        {
                            this.RedirectTo = (Attribute.Value.Trim());
                        }
                    }
                }
                if (!isRead)
                {
                    FindNode(node.ChildNodes, ref NoCheckList);
                }
            }
        }
        public AuthenticationXML()
        {
            List<string> Result = new List<string>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(HttpContext.Current.Server.MapPath(this.configFilePath));
            FindNode(xmlDocument.ChildNodes, ref Result);
            NoCheckList = Result;
        }
        public List<string> NoCheckList { get; private set; }
        public string RedirectTo { get; private set; }
    }
}
