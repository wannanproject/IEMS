using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IEMS.Frame.WebGlobal
{
    using MSTL.LogAgent;

    public class ErrorLogModule : IHttpModule
    {
        #region 系统日志  log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion
        
        public void Init(HttpApplication context)
        {
            context.Error += new EventHandler(HttpApplication_Error);
        }

        public void Dispose()
        {
            return;
        }

        /// <summary>
        /// 处理出错日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HttpApplication_Error(object sender, EventArgs e)
        {
            HttpApplication ap = sender as HttpApplication;
            Exception ex = ap.Server.GetLastError();
            log.Error(ex);
            if (ex is HttpException)
            {
                HttpException hx = (HttpException)ex;
                if (hx.GetHttpCode() == 404)
                {
                    string page = ap.Request.PhysicalPath;
                    log.ErrorFormat("文件不存在:{0}", ap.Request.Url.AbsoluteUri);
                    return;
                }
            }
            if (ex.InnerException != null) ex = ex.InnerException;
            log.ErrorFormat("{0} @ {1}", ap.Request.Url.AbsoluteUri, ex);
        }
    }
}
