using System.Collections.Generic;
using System.Data;

namespace IEMS.Main.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    public interface IUserManager : IAppBizManager
    {
        /// <summary>
        /// 获取权限对应的用户
        /// </summary>
        /// <param name="role"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        IList<SsbUser> GetRoleUserList(SspRole role, SsbUser user);

        DataTable GetAllUserList(object where);

        PageResult GetPageDataByReader(PageResult pageResult);

        DataTable GetDataTableByStatement(string stmtId, object param);

        SsbUser GetByObjId(int ObjId);
        IList<SsbUser> GetEntityList(SsbUser entity);
        int Update(SsbUser update, SsbUser where);
        int Insert(SsbUser entity);
    }
}
