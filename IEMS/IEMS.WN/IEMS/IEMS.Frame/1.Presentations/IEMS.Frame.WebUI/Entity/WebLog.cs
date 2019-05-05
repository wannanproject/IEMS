using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.WebUI.Entity
{
    public class WebLog
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string UserIP { get; set; }
        /// <summary>
        /// 函数
        /// </summary>
        public PageMethod Method { get; set; }
        /// <summary>
        /// 函数执行结果
        /// </summary>
        public string MethodResult{ get; set; }
        /// <summary>
        /// 页面提交数据
        /// </summary>
        public string PageRequest{ get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark{ get; set; }
    }
}
