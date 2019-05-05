namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;
    using IEMS.Main.DbCI;
    using MSTL.DbAccess;
    using System.Collections.Generic;

    internal class RoleActionManager: IRoleActionManager
    {

        private IRoleActionService businessService = DbCIServiceFactory.CreateInstance<IRoleActionService>();
        private ISspRoleActionService basicService = TableViewServiceFactory.CreateInstance<ISspRoleActionService>();
        /// <summary>
        /// 角色操作权限拷贝
        /// </summary>
        /// <param name="sourceRoleID">The source role ID.</param>
        /// <param name="targetRoleID">The target role ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CopyForm(string sourceRoleID, string targetRoleID)
        {
            return this.businessService.CopyForm(sourceRoleID, targetRoleID);
        }

        public int Delete(SspRoleAction entity)
        {
            return this.basicService.Delete(entity);
        }
        public int BatchInsert(List<SspRoleAction> lst)
        {
            var result = 0;
            foreach(var item in lst)
            {
                result += this.basicService.Insert(item);
            }
            return result;
        }
        public IList<SspRoleAction> GetEntityList(SspRoleAction entity)
        {
            return this.basicService.GetEntityList(entity);
        }
    }
}
