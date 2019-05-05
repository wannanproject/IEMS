using IEMS.Main.AppBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.Main.AppBiz
{
    public interface IMcPassword : IAppBizManager
    {
        string EncryptString(string src, string key, Encoding encoding);
        string DecryptString(string src, string key, Encoding encoding);
    }
}
