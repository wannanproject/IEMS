using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.Frame.WebUI
{
    /// <summary>
    /// 页面基类 完整Url进行权限验证
    /// </summary>
    public class PageUrl :IEMS.Frame.WebUI.PageRole
    {
        /// <summary>
        /// 获取页面地址，没有去掉了?及#后面的信息
        /// </summary>
        /// <returns></returns>
        protected override string GetPageUrl()
        {
            return base.GetPageUrl();
        }
    }
}
