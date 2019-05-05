using IEMS.Frame.WebGlobal;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Profile;

namespace IEMS.Frame.WebGlobal
{
    using MSTL.LogAgent;

    [CompilerGlobalScope]
    public class global_asax : HttpApplication
    {
        #region 系统日志 log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion

        private static bool __initialized;
        protected DefaultProfile Profile
        {
            get
            {
                return (DefaultProfile)base.Context.Profile;
            }
        }
        private void Application_Start(object sender, EventArgs e)
        {
            log.Debug("Application_Start");
            new PluginManager().SetPRIVATE_BINPATH("Plugins");
            log.Debug("SetPRIVATE_BINPATH");
            new Routes().Registe();
            log.Debug("new Routes().Registe();");
        }
        private void Application_End(object sender, EventArgs e)
        {
        }
        private void Application_Error(object sender, EventArgs e)
        {
        }
        private void Session_Start(object sender, EventArgs e)
        {
        }
        private void Session_End(object sender, EventArgs e)
        {
        }
        [DebuggerNonUserCode]
        public global_asax()
        {
            if (!global_asax.__initialized)
            {
                global_asax.__initialized = true;
            }
        }
    }
}
