using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSL_LOGIN_LOG - 序列操作接口
    /// </summary>
    public interface ISeqSslLoginLogService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSL_LOGIN_LOG - 序列操作类
    /// </summary>
    internal class SeqSslLoginLogService : SequenceService, ISeqSslLoginLogService
    {
        #region 构造方法
        public SeqSslLoginLogService() : base("SEQ_SSL_LOGIN_LOG") { }
        public SeqSslLoginLogService(string dbName) : base(dbName, "SEQ_SSL_LOGIN_LOG") { }
        #endregion
    }
}