using System.Collections.Generic;
using System.Data;

namespace IEMS.Main.DbCI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    public interface IUserService : IDbCIService
    {
        /// <summary>
        /// 获取权限对应的用户
        /// </summary>
        /// <param name="role"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        IList<SsbUser> GetRoleUserList(SspRole role, SsbUser user);

        DataTable GetAllUserList(object where);
    }
}
