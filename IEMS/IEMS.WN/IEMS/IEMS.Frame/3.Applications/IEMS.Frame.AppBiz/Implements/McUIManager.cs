using System.Data;

namespace IEMS.Frame.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Frame.Entity;
    using IEMS.Frame.DbCI;

    internal class McUIManager :IMcUIManager
    {
        #region 属性注入与构造方法
        private IMcUIService DbCIService = DbCIServiceFactory.CreateInstance<IMcUIService>();
        #endregion

        public McUIManager()
        {
            DbCIService = DbCIServiceFactory.CreateInstance<IMcUIService>();
        }
        public McUIManager(string dbName)
        {
            DbCIService = DbCIServiceFactory.CreateInstance<IMcUIService>(dbName);
        }

        public DbHelper DbHelper
        {
            get
            {
                return this.DbCIService.DbHelper;
            }
        }

        public DataTable GetComboBoxData(string uiHelperName, string fieldName)
        {
            return this.DbCIService.GetComboBoxData(uiHelperName, fieldName);
        }

        public PageResult GetPageDataByReader(PageResult pageResult)
        {
            return this.DbCIService.GetPageDataByReader(pageResult);
        }

        public DataTable GetSelectAndUpdateByUiName(string uiName, object param)
        {
            return this.DbCIService.GetSelectAndUpdateByUiName(uiName, param);
        }
    }
}
