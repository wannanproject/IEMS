namespace IEMS.Main.DbCI
{
    using MSTL.DbAccess;

    public interface IRoleActionService : IDbCIService
    {
        /// <summary>
        /// 角色操作权限拷贝
        /// </summary>
        /// <param name="sourceRoleID">The source role ID.</param>
        /// <param name="targetRoleID">The target role ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int CopyForm(string sourceRoleID, string targetRoleID);
    }
}
