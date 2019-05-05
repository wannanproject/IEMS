using System.Collections.Generic;
using System.Data;
using System.Linq;



namespace IEMS.Main.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;
    using IEMS.Main.DbCI;

    internal class UserManager : IUserManager
    {
        private IUserService businessService = DbCIServiceFactory.CreateInstance<IUserService>();
        private ISsbUserService basicService = TableViewServiceFactory.CreateInstance<ISsbUserService>();

        /// <summary>
        /// 获取权限对应的用户
        /// </summary>
        /// <param name="role"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public IList<SsbUser> GetRoleUserList(SspRole role, SsbUser user)
        {
            return this.businessService.GetRoleUserList(role, user);
        }

        public DataTable GetAllUserList(object where)
        {
            return this.businessService.GetAllUserList(where);
        }

        public PageResult GetPageDataByReader(PageResult pageResult)
        {
            return this.businessService.GetPageDataByReader(pageResult);
        }

        public DataTable GetDataTableByStatement(string stmtId, object param)
        {
            return this.businessService.GetDataTableByStatement(stmtId, param);
        }

        public SsbUser GetByObjId(int ObjId)
        {
            var result = this.basicService.GetEntityList(
                new SsbUser() { ObjId = ObjId }
                ).FirstOrDefault();
            return result;
        }

        public IList<SsbUser> GetEntityList(SsbUser entity)
        {
            return this.basicService.GetEntityList(entity);
        }
        public int Update(SsbUser update, SsbUser where)
        {
            return this.basicService.Update(update, where);
        }

        public int Insert(SsbUser entity)
        {
            return this.basicService.Insert(entity);
        }
    }
}
