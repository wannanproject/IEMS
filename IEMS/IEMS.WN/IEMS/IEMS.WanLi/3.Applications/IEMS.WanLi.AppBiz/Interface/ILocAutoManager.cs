using IEMS.WanLi.Entity;
using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.AppBiz
{
    public interface ILocAutoManager : IAppBizManager
    {
        PageResult GetLocDataTable(PageResult pageResult);

        /// <summary>
        ///  查询工装类型
        /// </summary>
        /// <returns></returns>
        PsbLoc GetPalletType(string locNo);

        void SetLocAutoEnable(string locNo, int enable);

    }
}
