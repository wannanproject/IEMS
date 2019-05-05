using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MSTL.DbAccess;

namespace IEMS.WanLi.DbCI
{
    public interface IOutputService : IDbCIService
    {

        /// <summary>
        /// 获取单据信息
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetBillOutputData(PageResult pageResult);

        /// <summary>
        /// 获取单据信息
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        DataTable GetListOutBillLineData(string OrderNo);
    }
}
