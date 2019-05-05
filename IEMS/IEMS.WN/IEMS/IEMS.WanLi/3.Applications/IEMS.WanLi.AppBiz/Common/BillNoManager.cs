using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEMS.WanLi.DbRI;
using MSTL.DbAccess;

namespace IEMS.WanLi.AppBiz.Common
{
    internal class BillNoManager
    {
        /// <summary>
        /// 获取单据标示符
        /// </summary>
        /// <param name="ReceiptType"></param>
        /// <returns></returns>
        private string getBillTypeNo(int ReceiptType)
        {
            var result = string.Empty;
            switch (ReceiptType)
            {
                case 1:
                    result = "I";
                    break;
                case 2:
                    result = "O";
                    break;
                case 3:
                    result = "P";
                    break;
                default:
                    result = string.Empty;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 获取单据编号
        /// </summary>
        /// <param name="ReceiptType"></param>
        /// <returns></returns>
        public string GetNewBillNo(int ReceiptType)
        {
            var result = new StringBuilder();
            result.Append(DateTime.Now.ToString("yyyyMMdd"));
            //result.Append(getBillTypeNo(ReceiptType));
            var outHeaderService = TableViewServiceFactory.CreateInstance<IWbsOrderService>();
            var seqservice = SequenceServiceFactory.CreateInstance<ISeqWbsOrderService>();
            var seq = seqservice.NEXTVAL;
            result.Append(seq.ToString("D4"));
            return result.ToString();
        }
    }
}
