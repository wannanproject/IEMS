using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.WebUI.Entity
{

    /// <summary>
    /// 页面项
    /// </summary>
    public class PageMenu
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public int ObjId { get; set; }
        /// <summary>
        /// 菜单级别
        /// </summary>
        public string MenuLevel { get; set; }
        /// <summary>
        /// 页面Url
        /// </summary>
        public string PageUrl { get; set; }
        /// <summary>
        /// 页面名称
        /// </summary>
        public string ShowName { get; set; }
        /// <summary>
        /// 需要权限管理
        /// </summary>
        public int NeedPermit { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string IcoName { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 页面权限
        /// </summary>
        public int Permit { get; set; }
    }
}
