using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;
    using IEMS.Main.DbCI;
    using MSTL.DbAccess;

    internal class PageActionManager : IPageActionManager
    {
        /// <summary>
        /// 获取所有的页面操作信息
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet GetAllPageMenuAction()
        {
            var service = ProcedureServiceFactory.CreateInstance<IProcPageMenuActionAllService>();
            var param = new ProcPageMenuActionAll();
            return  service.ExcuteProcedure(param).ProcedureDataSetResult;
        }
     


    }
}
