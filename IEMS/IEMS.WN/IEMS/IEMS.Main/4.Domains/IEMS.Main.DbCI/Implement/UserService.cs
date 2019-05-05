using System.Collections.Generic;
using System.Data;

namespace IEMS.Main.DbCI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    internal class UserService : DbCIService, IUserService
    {
        #region 构造函数
        public UserService() : base() { }
        public UserService(string dbName) : base(dbName) { }
        #endregion

        /// <summary>
        /// 获取权限对应的用户
        /// </summary>
        /// <param name="role"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public IList<SsbUser> GetRoleUserList(SspRole role, SsbUser user)
        {
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("ObjId", role.ObjId.ToString());
            where.Add("UserName", user.UserName);
            where.Add("RealName", user.RealName);
            where.Add("RoleName", role.RoleName);
            return this.GetEntityByStatement<SsbUser>("GetRoleUserList", where);
        }

        public DataTable GetAllUserList(object where)
        {
            return this.GetDataTableByStatement("GetAllUserList", where);
        }
    }
}
