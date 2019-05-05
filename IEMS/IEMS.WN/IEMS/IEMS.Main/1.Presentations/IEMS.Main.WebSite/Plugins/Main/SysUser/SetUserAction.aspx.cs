using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

using Ext.Net;
using IEMS.Frame.WebUI.Entity;
using IEMS.Main.AppBiz;
using IEMS.Main.Entity;

public partial class Manager_System_SetRoleAction_SetUserAction :IEMS.Frame.WebUI.Page
{
    #region 业务处理类
    private IDeptManager deptManager = AppBizFactory.CreateInstance<IDeptManager>();
    private IPageActionManager pageActionManager = AppBizFactory.CreateInstance<IPageActionManager>();
    private IUserAllActionManager userAllActionManager = AppBizFactory.CreateInstance<IUserAllActionManager>();
    private IUserActionManager userActionManager = AppBizFactory.CreateInstance<IUserActionManager>();
    private IUserManager userManager = AppBizFactory.CreateInstance<IUserManager>();
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ :IEMS.Frame.WebUI.___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            设置用户权限 = new PageAction() { ActionId = 1, ActionName = "btnSetRole" };
        }
        public PageAction 设置用户权限 { get; private set; } //必须为 public
    }
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
        this.buildTree(TreePanel1.Root);
        this.IniActionTree(TreePanel2);
    }
    private Node IniNodeByProperty(object obj)
    {
        Node n = new Node();
        n.Icon = Icon.Building;
        PropertyInfo[] fields = obj.GetType().GetProperties();
        foreach (PropertyInfo f in fields)
        {
            ConfigItem c = new ConfigItem();
            object value = f.GetValue(obj, null);
            c.Name = f.Name.ToString();
            if (f.Name.ToString().ToLower() == "DELETE_FLAG".ToLower())
            {
                if (value == null)
                {
                    c.Value = "无";
                }
                else
                {
                    c.Value = value.ToString() == "1" ? "停用" : "正常";
                }
            }
            else
            {
                c.Value = value == null ? string.Empty : value.ToString();
            }
            c.Mode = ParameterMode.Value;
            n.CustomAttributes.Add(c);
        }
        return n;
    }

    #region 部门树显示
    /// <summary>
    /// 嵌套查询部门树
    /// </summary>
    /// <param name="noderoot"></param>
    /// <param name="table"></param>
    private void buildTree(Node noderoot, IList<SsbDept> deptList)
    {
        foreach (SsbDept dept in deptList)
        {
            Ext.Net.Node node = new Ext.Net.Node();
            node.NodeID = dept.ObjId.ToString();
            node.Text = dept.DeptName;
            node.CustomAttributes.Add(new ConfigItem("OBJID", dept.ObjId.ToString(), ParameterMode.Value));
            node.CustomAttributes.Add(new ConfigItem("DELETE_FLAG", dept.DeleteFlag.ToString(), ParameterMode.Value));
            node.CustomAttributes.Add(new ConfigItem("DEPT_NAME", dept.DeptName, ParameterMode.Value));

            IList<SsbDept> subDeptList = deptManager.GetEntityList(new SsbDept() { ParentId = dept.ObjId });
            node.Leaf = subDeptList.Count == 0;
            buildTree(node, subDeptList);
            noderoot.Children.Add(node);
        }
    }

    /// <summary>
    /// 获取部门树
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    private Ext.Net.NodeCollection buildTree(Ext.Net.NodeCollection nodes)
    {
        if (nodes == null)
        {
            nodes = new Ext.Net.NodeCollection();
        }
        Ext.Net.Node root = new Ext.Net.Node();
        root.Text = "Root";
        nodes.Add(root);
        IList<SsbDept> subDeptList = deptManager.GetEntityList(new SsbDept() { ParentId = 0 });
        buildTree(root, subDeptList);
        root.Leaf = root.Children.Count == 0;
        if (!root.Leaf)
        {
            foreach (var node in root.Children)
            {
                if (!node.Leaf)
                {
                    node.Expanded = true;
                }
            }
        }
        return nodes;
    }

    /// <summary>
    /// 刷新部门树
    /// </summary>
    /// <returns></returns>
    [DirectMethod]
    public string RefreshDept()
    {
        Ext.Net.NodeCollection nodes = this.buildTree(null);

        return nodes.ToJson();
    }
    #endregion
    private Node IniNodeByTable(DataRow row, DataTable menuTable, DataTable actionTable)
    {
        Node n = new Node();
        n.Checked = false;
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
        DataSet menuaction = pageActionManager.GetAllPageMenuAction();
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
                n.NodeID = pageidNodeIDStarWith + row["ObjID"];
                n.Leaf = false;
                IniActionTree(n, row, menuTable, actionTable);
                tree.GetRootNode().AppendChild(n);
            }
        }
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        IList<SsbUser> lst = new List<SsbUser>();
        string deptcode = hiddenDeptCode.Text;
        if (string.IsNullOrWhiteSpace(deptcode))
        {
            if (!string.IsNullOrWhiteSpace(txtUserName.Text) && !string.IsNullOrWhiteSpace(txtWorkBarcode.Text))
            {
                lst = userManager.GetEntityList(new SsbUser() { UserName = txtUserName.Text, WorkBarcode = txtWorkBarcode.Text, DeleteFlag = 0 });
            }
            else if (!string.IsNullOrWhiteSpace(txtUserName.Text) && string.IsNullOrWhiteSpace(txtWorkBarcode.Text))
            {
                lst = userManager.GetEntityList(new SsbUser() { UserName = txtUserName.Text, DeleteFlag = 0 });
            }
            else if (string.IsNullOrWhiteSpace(txtUserName.Text) && !string.IsNullOrWhiteSpace(txtWorkBarcode.Text))
            {
                lst = userManager.GetEntityList(new SsbUser() { WorkBarcode = txtWorkBarcode.Text, DeleteFlag = 0 });
            }
        }
        else
        {
            lst = userManager.GetEntityList(new SsbUser() { DeptId = Convert.ToInt32(deptcode), DeleteFlag = 0 });
        }
        return new { data = lst, total = lst.Count };
    }

    [DirectMethod]
    public string ResetRoleAction(string users, string actions)
    {
        if (this._.设置用户权限.Permit == 0)
        {
            return "您没有进行设置用户权限的权限！";
        }
        string Result = string.Empty;
        List<SspUserAction> lst = new List<SspUserAction>();
        string[] userList = users.Split('|');
        string[] actionList = actions.Split('|');
        foreach (string user in userList)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                continue;
            }
            int userid = Convert.ToInt32(user);
            userActionManager.Delete(new SspUserAction() { UserId = userid });
            foreach (string action in actionList)
            {
                if (string.IsNullOrWhiteSpace(action))
                {
                    continue;
                }
                int iaction = 0;
                if (!int.TryParse(action, out iaction))
                {
                    continue;
                }
                SspUserAction m = new SspUserAction();
                m.UserId = userid;
                m.ActionId = iaction;
                lst.Add(m);
            }
        }
        userActionManager.BatchInsert(lst);
        return Result;
    }



    [DirectMethod]
    public object GetActionInfo(string user)
    {
        if (string.IsNullOrWhiteSpace(user))
        {
            return string.Empty;
        }
        int userid = Convert.ToInt32(user);
        var lst = userAllActionManager.GetEntityList(new ViewUserAllAction() { UserId = userid });
        return lst;
    }
}