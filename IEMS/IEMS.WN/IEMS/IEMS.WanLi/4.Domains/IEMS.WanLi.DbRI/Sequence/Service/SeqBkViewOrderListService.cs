using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.WanLi.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_BK_VIEW_ORDER_LIST - 序列操作接口
    /// </summary>
    public interface ISeqBkViewOrderListService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_BK_VIEW_ORDER_LIST - 序列操作类
    /// </summary>
    internal class SeqBkViewOrderListService : SequenceService, ISeqBkViewOrderListService
    {
        #region 构造方法
        public SeqBkViewOrderListService() : base("SEQ_BK_VIEW_ORDER_LIST") { }
        public SeqBkViewOrderListService(string dbName) : base(dbName, "SEQ_BK_VIEW_ORDER_LIST") { }
        #endregion
    }
}