using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Frame.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSL_WEB_LOG - 序列操作接口
    /// </summary>
    public interface ISeqSslWebLogService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSL_WEB_LOG - 序列操作类
    /// </summary>
    internal class SeqSslWebLogService : SequenceService, ISeqSslWebLogService
    {
        #region 构造方法
        public SeqSslWebLogService() : base("SEQ_SSL_WEB_LOG") { }
        public SeqSslWebLogService(string dbName) : base(dbName, "SEQ_SSL_WEB_LOG") { }
        #endregion
    }
}