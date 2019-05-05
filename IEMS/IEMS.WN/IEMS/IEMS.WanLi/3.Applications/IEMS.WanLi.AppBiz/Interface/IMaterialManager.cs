using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSTL.DbAccess;
using IEMS.WanLi.Entity;
using System.Data;

namespace IEMS.WanLi.AppBiz
{
    public interface IMaterialManager: IAppBizManager
    {
        /// <summary>
        /// 获取物料信息
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetMaterialData(PageResult pageResult);
        
        /// <summary>
        /// 删除任务信息
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        int DeleteAgvTask(string taskNo);
        /// <summary>
        /// 仅删除任务
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        int DeleteAgvTaskOnly(string taskNo);

        /// <summary>
        /// 添加任务信息
        /// </summary>
        /// <param name="palletId"></param>
        /// <param name="sLocPlcNo"></param>
        /// <param name="eLocPlcNo"></param>
        /// <param name="sLocNo"></param>
        /// <param name="eLocNo"></param>
        /// <returns></returns>
        int AddMaterialInfo(string materialNo, string materialName);

        /// <summary>
        ///  查询站台号
        /// </summary>
        /// <returns></returns>
        PsbLoc GetLocNo(string LocName);
        /// <summary>
        /// 查询AGV任务信息
        /// </summary>
        /// <returns></returns>
        DataTable GetAgvTaskDataTable();

    }
}
