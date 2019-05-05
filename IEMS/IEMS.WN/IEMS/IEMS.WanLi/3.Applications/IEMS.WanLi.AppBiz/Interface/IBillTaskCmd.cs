using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.AppBiz
{
    public interface IBillTaskCmd : IAppBizManager
    {
        //订单信息
        PageResult GetBillDataTable(PageResult pageResult);

        //任务信息
        PageResult GetTaskDataTable(PageResult pageResult);

        //指令信息
        PageResult GetCmdDataTable(PageResult pageResult);
    }
}
