using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Routing;

namespace IEMS.Frame.WebGlobal
{
    using MSTL.LogAgent;

    /// <summary>
    /// 插件初始化
    /// </summary>
    public class PluginManager
    {
        #region 系统日志 log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion

        /// <summary>
        /// 获取插件列表，只获取存在Bin文件的路径
        /// </summary>
        /// <returns></returns>
        private string[] getPlugins(string pluginDir)
        {
            List<string> result = new List<string>();
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + pluginDir);
            if (dir.Exists)
            {
                foreach (var p in dir.GetDirectories())
                {
                    var dirPlugin = new System.IO.DirectoryInfo(p.FullName + "\\bin");
                    if (dirPlugin.Exists)
                    {
                        var binName = string.Empty;
                        if (!string.IsNullOrWhiteSpace(pluginDir))
                        {
                            binName = pluginDir + "\\";
                        }
                        binName += p.Name;
                        result.Add(binName);
                    }
                }
            }
            return result.ToArray();
        }
        /// <summary>
        /// 重定义 专用程序集
        /// </summary>
        public void SetPRIVATE_BINPATH(string pluginDir)
        {
            string[] plugins = getPlugins(pluginDir);
            StringBuilder sb = new StringBuilder();
            sb.Append("bin;");
            foreach (string p in plugins)
            {
                sb.Append(p).Append(@"\bin;");
            }
            string APP_CONFIG_FILE = sb.ToString();
            log.Debug("PRIVATE_BINPATH=" + APP_CONFIG_FILE);
            //通过反射进行路径更新，直接采用公开接口无法实现
            AppDomain.CurrentDomain.SetData("PRIVATE_BINPATH", APP_CONFIG_FILE);
            AppDomain.CurrentDomain.SetData("BINPATH_PROBE_ONLY", APP_CONFIG_FILE);
            var m = typeof(AppDomainSetup).GetMethod("UpdateContextProperty",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var funsion = typeof(AppDomain).GetMethod("GetFusionContext",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            m.Invoke(null, new object[] { funsion.Invoke(AppDomain.CurrentDomain, null), 
                                      "PRIVATE_BINPATH", APP_CONFIG_FILE });
        }
    }
}
