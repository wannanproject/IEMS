using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.DbCI
{
    public interface IBillTaskCmdService : IDbCIService
    {
        /// <summary>
        /// 获取订单主表数据
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetBillOrderDataTable(PageResult pageResult);

        /// <summary>
        /// 获取订单主表数据
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetBillOrderLineDataTable(PageResult pageResult);
        /// <summary>
        /// 获取订单任务数据
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetBillOrderTaskDataTable(PageResult pageResult);
        /// <summary>
        /// 获取订单发货队列数据
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetBillOrderWaittingListDataTable(PageResult pageResult);
        /// <summary>
        /// 获取订单发货异常队列数据
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetBillOrderErrorListDataTable(PageResult pageResult);


        PageResult GetBillDataTable(PageResult pageResult);

        PageResult GetTaskDataTable(PageResult pageResult);

        PageResult GetCmdDataTable(PageResult pageResult);
    }
}
