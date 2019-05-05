using IEMS.WanLi.VoDto;
using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.AppBiz
{
    public interface ILocManager : IAppBizManager
    {
        DataTable GetLocDataTable();

        void SetLocEnable(string locNo, int flag);
    }
}
