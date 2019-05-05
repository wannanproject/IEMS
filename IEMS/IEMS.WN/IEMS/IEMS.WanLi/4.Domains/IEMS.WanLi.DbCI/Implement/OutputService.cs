using IEMS.WanLi.DbCI;
using MSTL.DbAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.DbCI
{
    internal class OutputService:DbCIService ,IOutputService
    {

        /// <summary>
        /// 获取单据信息
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public PageResult GetBillOutputData(PageResult pageResult)
        {
            pageResult.StatementId = "GetBillOutputData";
            return base.GetPageDataByReader(pageResult);
        }
        /// <summary>
        /// 获取单据行信息
        /// </summary>
        /// <param name="WhNo"></param>
        /// <param name="ListNo"></param>
        /// <returns></returns>
        public DataTable GetListOutBillLineData(string OrderNo)
        {
            var pageResult = new PageResult();
            pageResult.PageIndex = 0;
            pageResult.PageSize = 0;
            pageResult.StatementId = "GetListOutBillLineData";
            var param = new Hashtable(2);
            param["OrderNo"] = OrderNo;
            pageResult.ParameterObject = param;
            pageResult.OrderString = "TL.LINE_ID";
            return base.GetPageDataByReader(pageResult).ResultDataSet.Tables[0];
        }
    }
}
