using System.Data;

namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;

    public interface IPageActionManager : IAppBizManager
    {
        /// <summary>
        /// 获取所有的页面操作信息
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        DataSet GetAllPageMenuAction();

      
    }
}
