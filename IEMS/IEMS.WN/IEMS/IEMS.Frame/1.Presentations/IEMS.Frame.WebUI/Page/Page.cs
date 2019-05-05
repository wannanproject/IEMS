using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.Frame.WebUI
{
    /// <summary>
    /// 页面类
    /// </summary>
    public class Page : IEMS.Frame.WebUI.PageUrl
    {
        /// <summary>
        /// 获取页面地址，去掉了?及#后面的信息
        /// </summary>
        /// <returns></returns>
        protected override string GetPageUrl()
        {
            string rawUrl = base.GetPageUrl();
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
            return rawUrl.ToUpper();
        }
    }
}
