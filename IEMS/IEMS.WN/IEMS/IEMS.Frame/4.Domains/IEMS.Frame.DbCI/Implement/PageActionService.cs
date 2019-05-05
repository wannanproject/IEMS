using System;
using System.Collections.Generic;
using System.Data;

namespace IEMS.Frame.DbCI
{
    using MSTL.DbAccess;
    using IEMS.Frame.Entity;
    using IEMS.Frame.DbRI;

    internal class PageActionService : DbCIService, IPageActionService
    {
        private ISspPageActionService basicService;

        #region 构造方法

        public PageActionService() : base() {
            basicService = TableViewServiceFactory.CreateInstance<ISspPageActionService>();
        }

        public PageActionService(string dbName) : base(dbName) { }

        #endregion


        #region 数据转化
        private string ToString(DataRow row, string field)
        {
            if (row[field] == null || row[field] == DBNull.Value)
            {
                return null;
            }
            return row[field].ToString();
        }

        private int? ToInt(DataRow row, string field)
        {
            if (row[field] == null || row[field] == DBNull.Value)
            {
                return null;
            }
            int result = 0;
            if (int.TryParse(row[field].ToString(), out result))
            {
                return result;
            }
            return null;
        }
        private DateTime? ToDateTime(DataRow row, string field)
        {
            if (row[field] == null || row[field] == DBNull.Value)
            {
                return null;
            }
            DateTime result = DateTime.Now;
            if (DateTime.TryParse(row[field].ToString(), out result))
            {
                return result;
            }
            return null;
        }
        #endregion

        /// <summary>
        /// 获取ObjId最大值
        /// </summary>
        /// <returns></returns>
        public object GetMaxObjId(SspPageAction entity)
        {
            return basicService.GetMaxValue("OBJID", entity);
        }

        /// <summary>
        /// 获取当前页面用户的操作信息
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public IList<SspPageAction> GetUserPageActionList(int menuId, int userId)
        {
            string stmtId = "GetUserPageActionList";
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("PageMenuId", menuId);
            where.Add("UserId", userId);
            DataTable dt= this.GetDataTableByStatement(stmtId, where);
            IList<SspPageAction> resultList = new List<SspPageAction>();
            foreach (DataRow row in dt.Rows)
            {
                var p = new SspPageAction();
                p.ObjId = ToInt(row,"OBJID");
                p.PageMenuId = ToInt(row,"PAGE_MENU_ID");
                p.ActionId = ToInt(row,"ACTION_ID");
                p.ActionName = ToString(row, "ACTION_NAME");
                p.ActionUrl = ToString(row, "ACTION_URL");
                p.ShowName = ToString(row, "SHOW_NAME");
                p.Remark = ToString(row, "REMARK");
                p.IcoName = ToString(row, "ICO_NAME");
                p.RecordUserId = ToInt(row, "RECORD_USER_ID");
                p.RecordTime = ToDateTime(row, "RECORD_TIME");
                p.SeqIndex = ToInt(row, "SEQ_INDEX");
                p.DeleteFlag = ToInt(row, "DELETE_FLAG");
                p.BakupFlag = ToInt(row, "BAKUP_FLAG");
                p.BakupTime = ToDateTime(row, "BAKUP_TIME");
                resultList.Add(p);
            }
            return resultList;
        }

        /// <summary>
        /// 获取当权限用户的操作信息
        /// </summary>
        /// <param name="pageActionId">The URL.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataTable GetUserPageAction(int pageActionId, int? userId)
        {
            string stmtId = "GetUserPageAction";
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("PageActionId", pageActionId);
            where.Add("UserId", userId);
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("where", where);
            return this.GetDataTableByStatement(stmtId, param);
        }

    }
}
