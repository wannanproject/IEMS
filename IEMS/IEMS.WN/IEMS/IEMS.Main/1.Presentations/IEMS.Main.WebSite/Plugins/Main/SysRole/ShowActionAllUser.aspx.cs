using System;
using System.Collections.Generic;
using System.Data;

using Ext.Net;
using IEMS.Frame.WebUI;
using IEMS.Frame.WebUI.Entity;
using IEMS.Main.AppBiz;
using IEMS.Main.Entity;

public partial class Plugins_Main_SysRole_ShowActionAllUser :IEMS.Frame.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new PageAction() { ActionId = 1 };
        }
        public PageAction 查看 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private IDeptManager basDeptManager = AppBizFactory.CreateInstance<IDeptManager>();
    private IPageActionManager sysPageActionManager = AppBizFactory.CreateInstance<IPageActionManager>();

    private IUserManager basUserManager = AppBizFactory.CreateInstance<IUserManager>();
    private IUserAllActionManager sysUserAllActionManager = AppBizFactory.CreateInstance<IUserAllActionManager>();
    #endregion

    #region 常量定义
    readonly string pageidNodeIDStarWith = "pageid=";
    readonly string actionidNodeIDStarWith = "actionid=";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack || X.IsAjaxRequest)
        {
            return;
        }
        IniActionTree(TreePanel1);
    }
    private Node IniNodeByTable(DataRow row, DataTable menuTable, DataTable actionTable)
    {
        Node n = new Node();
        n.Icon = Icon.Building;
        string[] ss = new string[] { "OBJID", "SHOW_NAME" };
        foreach (string s in ss)
        {
            ConfigItem c = new ConfigItem();
            c.Name = s;
            object value = row[c.Name];
            c.Value = value == null ? string.Empty : value.ToString();
            c.Mode = ParameterMode.Value;
            n.CustomAttributes.Add(c);
        }
        return n;
    }
    private List<DataRow> MenuPageSelect(DataTable menu, string menulevel)
    {
        List<DataRow> Result = new List<DataRow>();
        foreach (DataRow row in menu.Rows)
        {
            string thisMid = row["MENU_LEVEL"].ToString();
            if ((thisMid.Length == menulevel.Length + 2) && (thisMid.StartsWith(menulevel)))
            {
                Result.Add(row);
            }
        }
        return Result;
    }
    private List<DataRow> PageActionSelect(DataTable action, string pageid)
    {
        List<DataRow> Result = new List<DataRow>();
        foreach (DataRow row in action.Rows)
        {
            if (row["PAGE_MENU_ID"].ToString() == pageid)
            {
                Result.Add(row);
            }
        }
        return Result;
    }
    private void IniActionTree(Node node, DataRow parRow, DataTable menuTable, DataTable actionTable)
    {
        List<DataRow> MenuRows = MenuPageSelect(menuTable, parRow["MENU_LEVEL"].ToString());
        List<DataRow> ActionRows = PageActionSelect(actionTable, parRow["OBJID"].ToString());
        foreach (DataRow row in MenuRows)
        {
            Node n = IniNodeByTable(row, menuTable, actionTable);
            n.NodeID = pageidNodeIDStarWith + row["OBJID"];
            n.Leaf = false;
            IniActionTree(n, row, menuTable, actionTable);
            node.Children.Add(n);
        }
        foreach (DataRow row in ActionRows)
        {
            Node n = IniNodeByTable(row, menuTable, actionTable);
            n.NodeID = actionidNodeIDStarWith + row["OBJID"];
            n.Leaf = true;
            node.Children.Add(n);
        }
    }
    private void IniActionTree(TreePanel tree)
    {
        DataSet menuaction = sysPageActionManager.GetAllPageMenuAction();
        if (menuaction.Tables.Count < 2)
        {
            return;
        }
        DataTable menuTable = menuaction.Tables[0];
        DataTable actionTable = menuaction.Tables[1];
        foreach (DataRow row in menuTable.Rows)
        {
            if (row["MENU_LEVEL"].ToString().Length == 2)
            {
                Node n = IniNodeByTable(row, menuTable, actionTable);
                n.NodeID = pageidNodeIDStarWith + row["OBJID"];
                n.Leaf = false;
                IniActionTree(n, row, menuTable, actionTable);
                tree.GetRootNode().AppendChild(n);
            }
        }
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        string useraction = hiddenAcitonId.Text;
        string user_name = txt_user_name.Text;
        if (string.IsNullOrWhiteSpace(useraction))
        {
            return new { data = new List<SsbUser>(), total = 0 };
        }
        IList<ViewUserAllAction> lstAction = sysUserAllActionManager.GetEntityList(new ViewUserAllAction(){ActionId = Convert.ToInt32(useraction) });
        IList<SsbUser> lst = new List<SsbUser>();
        foreach (ViewUserAllAction m in lstAction)
        {
            try
            {   IList<SsbUser> userList = new List<SsbUser>();
                if (string.IsNullOrWhiteSpace(user_name))
                {
                    userList = basUserManager.GetEntityList(new SsbUser() { ObjId = (long)m.UserId});
                }
                else
                {
                    userList = basUserManager.GetEntityList(new SsbUser() { ObjId = (long)m.UserId, UserName = user_name });
                }
                if (userList.Count > 0)
                {
                    SsbUser u = userList[0];
                    string ss = m.M.ToString();
                    ss = ss.Replace("0", "");
                    ss = ss.Replace("1", "用户定义;");
                    ss = ss.Replace("2", "用户角色;");
                    ss = ss.Replace("3", "部门权限;");
                    ss = ss.Replace("4", "部门角色;");
                    u.Remark = ss;
                    lst.Add(u);
                }
            }
            catch { }
        }
        return new { data = lst, total = lst.Count };
    }


    [DirectMethod]
    public object GetActionInfo(string user)
    {
        IList<ViewUserAllAction> lst = sysUserAllActionManager.GetEntityList(new ViewUserAllAction() { UserId = Convert.ToInt32(user) });
        return lst;
    }

    #region 点击查询按钮操作
    /// <summary>
    /// 点击查询按钮筛选出用户信息
    /// yuany 2013年7月15日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [DirectMethod]
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string deptCode = hiddenAcitonId.Value.ToString();
        store.Reload();
    }
    #endregion
}