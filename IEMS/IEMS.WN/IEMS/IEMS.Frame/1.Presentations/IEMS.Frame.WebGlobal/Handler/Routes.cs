using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace IEMS.Frame.WebGlobal
{

    /// <summary>
    /// 路由器
    /// </summary>
    public class Routes
    {
        public void Registe()
        {
            var routes = System.Web.Routing.RouteTable.Routes;
            //参数含义:
            //第一个参数：路由名称--随便自己起
            //第二个参数：路由规则
            //第三个参数：该路由规则交给哪一个页面来处理
            string[] routeList = new string[] { "Crud", "SearchBox", "Report", "ReportBill" };
            foreach (string routeName in routeList)
            {
                routes.MapPageRoute(
                    "MesAutoPage-" + routeName + "-MapPageRoute",
                    "McUI/" + routeName + "/{id}.aspx",
                    "~/McUI/" + routeName + ".aspx",
                    false, new RouteValueDictionary { { "UiType", routeName } }
                    );
            }
        }
    }
}
