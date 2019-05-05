using System;
using System.Collections.Generic;
using System.Text;

namespace IEMS.Main.DbRI
{
    using MSTL.DbAccess;

    /// <summary>
    /// SEQ_SSP_DEPT_ROLE - 序列操作接口
    /// </summary>
    public interface ISeqSspDeptRoleService :ISequenceService
    {
    }

    /// <summary>
    /// SEQ_SSP_DEPT_ROLE - 序列操作类
    /// </summary>
    internal class SeqSspDeptRoleService : SequenceService, ISeqSspDeptRoleService
    {
        #region 构造方法
        public SeqSspDeptRoleService() : base("SEQ_SSP_DEPT_ROLE") { }
        public SeqSspDeptRoleService(string dbName) : base(dbName, "SEQ_SSP_DEPT_ROLE") { }
        #endregion
    }
}