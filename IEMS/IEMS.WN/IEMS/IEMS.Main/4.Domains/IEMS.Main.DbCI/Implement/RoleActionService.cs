using System.Collections.Generic;

namespace IEMS.Main.DbCI
{
    using MSTL.DbAccess;

    internal class RoleActionService : DbCIService, IRoleActionService
    {
        #region 构造方法
        public RoleActionService() : base() { }
        public RoleActionService(string dbName) : base(dbName) { }
        #endregion

        /// <summary>
        /// 角色操作权限拷贝
        /// </summary>
        /// <param name="sourceRoleID">The source role ID.</param>
        /// <param name="targetRoleID">The target role ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CopyForm(string sourceRoleID, string targetRoleID)
        {
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("SourceRoleID", sourceRoleID);
            where.Add("TargetRoleID", targetRoleID);
            this.InsertByStatement("RolePowerInsertCopy", where);
            return 0;
        }
    }
}
