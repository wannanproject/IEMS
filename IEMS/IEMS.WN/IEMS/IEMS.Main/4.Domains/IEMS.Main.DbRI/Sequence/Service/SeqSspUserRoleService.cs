using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSP_USER_ROLE - 序列操作接口
    /// </summary>
    public interface ISeqSspUserRoleService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSP_USER_ROLE - 序列操作类
    /// </summary>
    internal class SeqSspUserRoleService : SequenceService, ISeqSspUserRoleService
    {
        #region 构造方法
        public SeqSspUserRoleService() : base("SEQ_SSP_USER_ROLE") { }
        public SeqSspUserRoleService(string dbName) : base(dbName, "SEQ_SSP_USER_ROLE") { }
        #endregion
    }
}