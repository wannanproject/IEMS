namespace IEMS.Main.DbCI
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;

    public interface IDeptService : IDbCIService
    {
        SsbDept GetByObjId(int ObjId);
    }
}
