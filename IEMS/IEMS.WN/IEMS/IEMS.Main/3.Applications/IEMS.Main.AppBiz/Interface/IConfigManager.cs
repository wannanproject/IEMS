using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEMS.Main.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    public interface IConfigManager: IAppBizManager
    {
        List<SysConfig> GetSysConfig();
    }
}
