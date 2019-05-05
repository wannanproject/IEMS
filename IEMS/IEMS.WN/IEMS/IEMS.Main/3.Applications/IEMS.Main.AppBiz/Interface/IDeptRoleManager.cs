using System.Collections.Generic;
namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;

    public interface IDeptRoleManager : IAppBizManager
    {
        IList<SspDeptRole> GetEntityList(SspDeptRole entity);
        int Delete(SspDeptRole entity);
        int BatchInsert(List<SspDeptRole> lst);
        int GetRowCount(SspDeptRole entity);
    }
}
