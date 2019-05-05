using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IEMS.WanLi.AppBiz;
using MSTL.DbAccess;
using IEMS.WanLi.Entity;

namespace IEMS.WanLi.AppBiz
{
    public interface IOutputBillManager : IAppBizManager
    {
        /// <summary>
        /// 获取单据信息(查询界面)
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        PageResult GetBillOutputData(PageResult pageResult);

        /// <summary>
        /// 删除单据信息（查询界面）
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        string DeleteBillData(string OrderNo);

        /// <summary>
        /// 获取出库单唯一单据号（添加界面）
        /// </summary>
        /// <returns></returns>
        string GetNewBillNo();

        /// <summary>
        ///  查询出库站台信息
        /// </summary>
        /// <returns></returns>
        PsbLoc[] GetEquipData(string LOCTypeNo);

        /// <summary>
        ///  查询品级
        /// </summary>
        /// <returns></returns>
        PsbGrade[] GetGradeTable(PsbGrade GradeString);
        /// <summary>
        ///  查询MI
        /// </summary>
        /// <returns></returns>
        PsbMino[] GetMinoTable(PsbMino MinoString);

        /// <summary>
        /// 添加单据信息
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        WbsOrder SaveOutputBill(WbsOrder bill, WbsOrderLine[] body);

        /// <summary>
        /// 获取出库单行Guid（添加界面）
        /// </summary>
        /// <returns></returns>
        string GetNewLineGuid();

        /// <summary>
        /// 获取单据主信息(修改界面)
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        WbsOrder GetWbsOrder(string OrderNo);

        /// <summary>
        /// 获取单据信息(修改界面)
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        DataTable GetListOutBillData(string OrderNo);

        /// <summary>
        /// 获取单据行信息(修改界面)
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        WbsOrderLine GetWbsOrderLine(string OrderNo);

        /// <summary>
        /// 更新单据信息(修改界面)
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        WbsOrder UpdateOutputBill(WbsOrder bill, WbsOrderLine[] body);

       
    }


}
