using System.Data;

namespace IEMS.Frame.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Frame.Entity;

    public interface IMcUIManager : IAppBizManager
    {
        DbHelper DbHelper { get; }

        DataTable GetComboBoxData(string uiHelperName, string fieldName);

        PageResult GetPageDataByReader(PageResult pageResult);

        DataTable GetSelectAndUpdateByUiName(string uiName, object param);
    }
}