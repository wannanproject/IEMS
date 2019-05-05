using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.Frame.McUI.ExtNet
{
    static class UiControlNamePrefix
    {
        /// <summary>
        /// 默认增加前缀 添加字段
        /// </summary>
        public static string InsertParam { get { return "ui_i_"; } }
        /// <summary>
        /// 默认增加前缀 更新字段
        /// </summary>
        public static string UpdateParam { get { return "ui_u_"; } }
        /// <summary>
        /// 默认增加前缀 查询条件
        /// </summary>
        public static string SelectParam { get { return "ui_s_"; } }
        /// <summary>
        /// 默认增加前缀 查询结果 主明细信息
        /// </summary>
        public static string SelectMainGrid { get { return "ui_s_mg_"; } }
        /// <summary>
        /// 默认增加前缀 查询结果 主详细信息
        /// </summary>
        public static string SelectMainDetail { get { return "ui_s_md_"; } }
        /// <summary>
        /// 默认增加前缀 查询结果 汇总信息
        /// </summary>
        public static string SelectSummaryGrid { get { return "ui_s_sg_"; } }
        /// <summary>
        /// 默认增加前缀 查询结果 明细信息
        /// </summary>
        public static string SelectDetailGrid { get { return "ui_s_dg_"; } }
    }
}
