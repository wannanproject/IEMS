using System.Data;

namespace IEMS.Frame.DbCI
{
    using MSTL.DbAccess;

    internal class McUIService : DbCIService, IMcUIService
    {
        #region 构造方法
        public McUIService() : base() { }
        public McUIService(string dbName) : base(dbName) { }
        #endregion

        public DataTable GetComboBoxData(string uiHelperName, string fieldName)
        {
            return this.GetDataTableByStatement("GetComboBoxData@Select@"
                                                    + uiHelperName
                                                    + "@" + fieldName, null);
        }

        public DataTable GetSelectAndUpdateByUiName(string uiName, object param)
        {
            string statementId = "Select+Update@" + uiName;
            return this.GetDataTableByStatement(statementId, param);
        }
    }
}
