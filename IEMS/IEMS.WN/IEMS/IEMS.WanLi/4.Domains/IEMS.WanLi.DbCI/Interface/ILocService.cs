using MSTL.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.DbCI
{ 
    public interface ILocService : IDbCIService
    {
        /// <summary>
        ///  空笼工位信息
        /// </summary>
        /// <returns></returns>
        PageResult GetLocDataTable(PageResult pageResult);
        /// <summary>
        ///  入库站台信息
        /// </summary>
        /// <returns></returns>
        PageResult GetInputLocDataTable(PageResult pageResult);
        /// <summary>
        /// 获取锁定站台
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetPsbLocLock(PageResult pageResult);
        /// <summary>
        /// 获取站台状态
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetLocStatus(PageResult pageResult);
        /// <summary>
        /// 通过起始站台获取任务
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetTaskBySlocNo(PageResult pageResult);
        /// <summary>
        /// 通过终点站台获取任务
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetTaskByElocNo(PageResult pageResult);
        /// <summary>
        ///  获取工位信息
        /// </summary>
        /// <returns></returns>
        DataTable GetLocable();
    }
}
