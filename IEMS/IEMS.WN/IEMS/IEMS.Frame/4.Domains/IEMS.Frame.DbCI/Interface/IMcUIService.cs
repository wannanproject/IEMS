using System.Data;

namespace IEMS.Frame.DbCI
{
    using MSTL.DbAccess;

    public interface IMcUIService : IDbCIService
    {
        DataTable GetComboBoxData(string uiHelperName, string fieldName);

        DataTable GetSelectAndUpdateByUiName(string uiName, object param);
    }
}
