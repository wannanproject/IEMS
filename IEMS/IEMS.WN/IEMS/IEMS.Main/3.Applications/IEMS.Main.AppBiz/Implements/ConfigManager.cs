using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEMS.Main.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;
    using IEMS.Main.DbCI;
    public class ConfigManager : IConfigManager
    {
        private ISsbConfigService basicService = TableViewServiceFactory.CreateInstance<ISsbConfigService>();
        // private ISsbConfigService basicService =busi Factory.CreateInstance<ISsbConfigService>();

        public List<SysConfig> GetSysConfig()
        {
            var result = new List<SysConfig>();
            var lst = basicService.GetEntityList(null);
            foreach (var p in lst)
            {
                var c = new SysConfig();
                c.SsKey = p.ConfigKey;
                c.SsVaule = p.ConfigValue;
                c.Remark = "";
                result.Add(c);
            }
            return result;
        }
    }
    public class SysConfig
    {
        /// <summary>
        /// 标示
        /// </summary>
        public string SsKey { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string SsVaule { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
    }
}
