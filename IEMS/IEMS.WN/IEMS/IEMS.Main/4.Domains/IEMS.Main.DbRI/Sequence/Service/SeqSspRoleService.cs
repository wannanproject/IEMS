using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSP_ROLE - 序列操作接口
    /// </summary>
    public interface ISeqSspRoleService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSP_ROLE - 序列操作类
    /// </summary>
    internal class SeqSspRoleService : SequenceService, ISeqSspRoleService
    {
        #region 构造方法
        public SeqSspRoleService() : base("SEQ_SSP_ROLE") { }
        public SeqSspRoleService(string dbName) : base(dbName, "SEQ_SSP_ROLE") { }
        #endregion
    }
}