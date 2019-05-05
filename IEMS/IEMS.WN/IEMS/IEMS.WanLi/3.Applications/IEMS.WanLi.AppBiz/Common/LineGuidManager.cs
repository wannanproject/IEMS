using IEMS.WanLi.DbRI;
using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.AppBiz.Common
{
    internal class LineGuidManager
    {
        /// <summary>
        /// 获取单据行Guid编号（添加界面）
        /// </summary>
        /// <param name="ReceiptType"></param>
        /// <returns></returns>
        public string GetNewLineGuid(int ReceiptType)
        {
            var result = new StringBuilder();
            result.Append(DateTime.Now.ToString("yyyyMMdd"));
            //result.Append(getBillTypeNo(ReceiptType));
            var seqservice = SequenceServiceFactory.CreateInstance<ISeqWbsOrderLineService>();
            var seq = seqservice.NEXTVAL;
            result.Append(seq.ToString("D4"));
            return result.ToString();
        }
        /// <summary>
        /// 获取单据行Guid编号（修改界面）
        /// </summary>
        /// <param name="ReceiptType"></param>
        /// <returns></returns>
        public string GetLineGuidNew(int ReceiptType)
        {
            var result = new StringBuilder();
            result.Append(DateTime.Now.ToString("yyyyMMdd"));
            //result.Append(getBillTypeNo(ReceiptType));
            var seqservice = SequenceServiceFactory.CreateInstance<ISeqBkViewOrderListService>();
            var seq = seqservice.NEXTVAL;
            result.Append(seq.ToString("D4"));
            return result.ToString();
        }
    }
}
