
namespace IEMS.WanLi.DbCI
{
    using MSTL.DbAccess;
    using System.Data;

    public interface ICrnService : IDbCIService
    {
        DataTable GetCrnDataTable();
        /// <summary>
        /// ��ȡ�Ѷ�����쳣״̬
        /// </summary>
        /// <returns></returns>
        DataTable GetCrnForkErrorStatus();
    }
}
