namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;
    using System.Collections.Generic;

    public interface IRoleActionManager : IAppBizManager
    {
        /// <summary>
        /// 角色操作权限拷贝
        /// </summary>
        /// <param name="sourceRoleID">The source role ID.</param>
        /// <param name="targetRoleID">The target role ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int CopyForm(string sourceRoleID, string targetRoleID);
        int Delete(SspRoleAction entity);
        int BatchInsert(List<SspRoleAction> lst);
        IList<SspRoleAction> GetEntityList(SspRoleAction entity);
    }
}
