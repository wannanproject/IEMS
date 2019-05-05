using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.WebUI.Entity
{
    /// <summary>
    /// 页面权限操作
    /// </summary>
    public class PageAction
    {
        /// <summary>
        /// 页面Menu
        /// </summary>
        public PageMenu PageMenu { get; set; }
        /// <summary>
        /// 权限页面内Id
        /// </summary>
        public int ActionId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 权限连接
        /// </summary>
        public string ActionUrl { get; set; }
        /// <summary>
        /// 权限显示名称
        /// </summary>
        public string ShowName { get; set; }
        /// <summary>
        /// 是否有此权限
        /// </summary>
        public int Permit { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string IcoName { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
    }
}
