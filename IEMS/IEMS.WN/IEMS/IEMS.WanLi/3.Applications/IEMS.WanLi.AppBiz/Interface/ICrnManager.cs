using IEMS.WanLi.VoDto;
using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.AppBiz
{
    public interface ICrnManager : IAppBizManager
    {
        DataTable GetCrnDataTable();
        DataTable GetCrnForkErrorStatus();
        void SetCrnInputEnable(string crnNo, int enable);
        void SetCrnOutputEnable(string crnNo, int enable);
        void SetCrnForkEnable(string crnForkNo, int enable);

        string UpdateSrmArea(string CrnNo, string MinCol, string MaxCol);
    }
}
