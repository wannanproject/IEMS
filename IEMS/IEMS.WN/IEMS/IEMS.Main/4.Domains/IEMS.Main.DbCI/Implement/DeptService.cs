using System.Linq;
namespace IEMS.Main.DbCI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;

    internal class DeptService : DbCIService, IDeptService
    {
        private ISsbDeptService basicService;

        #region 构造方法
        public DeptService() : base() {
            basicService = TableViewServiceFactory.CreateInstance<ISsbDeptService>();
        }
        public DeptService(string dbName) : base(dbName) {
            basicService = TableViewServiceFactory.CreateInstance<ISsbDeptService>(dbName);
        }
        #endregion

        public SsbDept GetByObjId(int ObjId)
        {
            var result = this.basicService.GetEntityList(
                new SsbDept() { ObjId = (long)ObjId }
                ).FirstOrDefault();
            return result;
        }
    }
}
