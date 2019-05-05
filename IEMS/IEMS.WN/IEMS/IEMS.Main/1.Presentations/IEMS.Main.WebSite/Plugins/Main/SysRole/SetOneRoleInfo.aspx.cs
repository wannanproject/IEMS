using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

using Ext.Net;
using IEMS.Frame.WebUI.Entity;
using IEMS.Main.AppBiz;
using IEMS.Main.Entity;

public partial class Plugins_Main_SysRole_SetOneRoleInfo :IEMS.Frame.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ :IEMS.Frame.WebUI.___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            设置角色权限 = new PageAction() { ActionId = 1, ActionName = "btnSetRole" };
            复制角色权限 = new PageAction() { ActionId = 2, ActionName = "btnRoleCopy" };
        }
        public PageAction 设置角色权限 { get; private set; } //必须为 public
        public PageAction 复制角色权限 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private IRoleManager sysRoleManager = AppBizFactory.CreateInstance<IRoleManager>();
    private IPageActionManager sysPageActionManager = AppBizFactory.CreateInstance<IPageActionManager>();
    private IRoleActionManager sysRoleActionManager = AppBizFactory.CreateInstance<IRoleActionManager>();

    private IUserManager basUserManager = AppBizFactory.CreateInstance<IUserManager>();
    private IDeptManager basDeptManager = AppBizFactory.CreateInstance<IDeptManager>();
    private IDeptRoleManager sysDeptRoleManager = AppBizFactory.CreateInstance<IDeptRoleManager>();
    private IUserRoleManager sysUserRoleManager = AppBizFactory.CreateInstance<IUserRoleManager>();
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

    #region 初始化权限
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
        if (this._.复制角色权限.Permit > 0)
        {

        }
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
    #endregion

    #region 初始化部门(完整部门，加载慢)
    private Node IniNode(string role, object obj)
    {
        Node n = new Node();
        if (obj is SsbDept)
        {
            int dept_id = Convert.ToInt32((obj as SsbDept).ObjId);
            try
            {
                if (sysDeptRoleManager.GetRowCount(new SspDeptRole() { DeptId = dept_id, RoleId = Convert.ToInt32(role) }) > 0)
                {
                    n.Checked = true;
                }
            }
            catch (Exception)
            {
            }
        }
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
    private void IniDeptTree(string role, Node node, int level, int parent_id)
    {
        node.NodeID = parent_id.ToString();
        IList<SsbDept> lst = basDeptManager.GetEntityList(new SsbDept() { DeptLevel = level + 1, ParentId = parent_id }, "DISPLAY_ID");
        if (lst.Count == 0)
        {
            node.Icon = Icon.BuildingGo;
            node.Leaf = true;
            return;
        }
        node.Icon = Icon.Building;
        node.Leaf = false;
        foreach (SsbDept m in lst)
        {
            Node n = IniNode(role, m);
            IniDeptTree(role, n, (int)m.DeptLevel, Convert.ToInt32(m.ObjId));
            node.Children.Add(n);
        }
    }



    private void IniDeptTree(string role, TreePanel tree)
    {
        tree.GetRootNode().RemoveAll();
        Node node = new Node();
        IList<SsbDept> lst = new List<SsbDept>();
        lst = basDeptManager.GetEntityList(new SsbDept() { DeptLevel = 1, ParentId = 0 }, "DISPLAY_ID");
        foreach (SsbDept m in lst)
        {
            Node n = IniNode(role, m);
            IniDeptTree(role, n, (int)m.DeptLevel, Convert.ToInt32(m.ObjId));
            tree.GetRootNode().AppendChild(n);
        }
    }
    #endregion

    #region 初始化角色
    [DirectMethod]
    public object GridPanelBindRoleData(string action, Dictionary<string, object> extraParams)
    {
        IList<SspRole> lst = sysRoleManager.GetEntityList(new SspRole() { DeleteFlag = 0 }, "OBJID");
        int total = lst.Count;
        return new { data = lst, total };
    }
    #endregion

    #region 初始化部门
    private Dictionary<string, DeptNode> deptInfo = new Dictionary<string, DeptNode>();
    private Dictionary<string, DeptNode> checkedDeptInfo = new Dictionary<string, DeptNode>();
    private class DeptNode
    {
        public DeptNode()
        {
            this.Checked = false;
            this.DeptInfo = new SsbDept();
            this.ParDeptNode = null;
            this.Child = new List<DeptNode>();
        }
        public bool Checked { get; set; }
        public SsbDept DeptInfo { get; set; }
        public DeptNode ParDeptNode { get; set; }
        public List<DeptNode> Child { get; set; }
    }
    private void IniDeptNodeParent(DeptNode node)
    {
        try
        {
            DeptNode pnode = new DeptNode();
            SsbDept dept = basDeptManager.GetEntityList(new SsbDept() { ObjId = node.DeptInfo.ParentId })[0];
            if (deptInfo.TryGetValue(dept.ObjId.ToString(), out pnode))
            {
                node.ParDeptNode = pnode;
                if (!deptInfo.ContainsKey(node.DeptInfo.ObjId.ToString()))
                {
                    deptInfo.Add(node.DeptInfo.ObjId.ToString(), node);
                    pnode.Child.Add(node);
                }
                return;
            }
            else
            {
                pnode = new DeptNode();
                pnode.DeptInfo = dept;
                node.ParDeptNode = pnode;
                if (!deptInfo.ContainsKey(node.DeptInfo.ObjId.ToString()))
                {
                    deptInfo.Add(node.DeptInfo.ObjId.ToString(), node);
                    pnode.Child.Add(node);
                }
                if (checkedDeptInfo.ContainsKey(pnode.DeptInfo.ObjId.ToString()))
                {
                    pnode.Checked = true;
                }
                IniDeptNodeParent(pnode);
                deptInfo.Add(dept.ObjId.ToString(), pnode);
            }
        }
        catch
        {
        }
    }
    private void IniDeptNode(string role)
    {
        IList<SspDeptRole> lst = sysDeptRoleManager.GetEntityList(new SspDeptRole() { RoleId = Convert.ToInt32(role) });
        foreach (SspDeptRole m in lst)
        {
            DeptNode node = new DeptNode();
            SsbDept dept = basDeptManager.GetEntityList(new SsbDept() { ObjId = m.DeptId })[0];
            node.DeptInfo = dept;
            node.Checked = true;
            checkedDeptInfo.Add(dept.ObjId.ToString(), node);
        }
        foreach (DeptNode node in checkedDeptInfo.Values)
        {
            IniDeptNodeParent(node);
        }
    }
    private Node IniNode(object obj)
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
    int i = 0;
    private void IniDeptNode(DeptNode parDeptNode, Node parTreeNode)
    {
        parTreeNode.NodeID = parDeptNode.DeptInfo.ObjId.ToString() + (i++).ToString();
        parTreeNode.Text = parDeptNode.DeptInfo.DeptName;
        if (parDeptNode.Checked)
        {
            parTreeNode.Checked = true;
        }
        if (parDeptNode.Child.Count == 0)
        {
            parTreeNode.Leaf = true;
            return;
        }
        foreach (DeptNode deptNode in parDeptNode.Child)
        {
            Node n = new Node();
            IniDeptNode(deptNode, n);
            parTreeNode.Children.Add(n);
        }
    }
    private void IniDeptNode(TreePanel tree)
    {
        foreach (DeptNode deptNode in deptInfo.Values)
        {
            if (deptNode.ParDeptNode == null)
            {
                Node n = new Node();
                n.Expanded = true;
                IniDeptNode(deptNode, n);
                tree.GetRootNode().AppendChild(n);
            }
        }
    }
    private void IniDeptNode(string role, TreePanel tree)
    {
        tree.GetRootNode().RemoveAll();
        if (string.IsNullOrWhiteSpace(role))
        {
            return;
        }
        IniDeptNode(role);
        IniDeptNode(tree);
    }
    #endregion

    #region 初始化人员
    [DirectMethod]
    public object GridPanelBindUserData(string action, Dictionary<string, object> extraParams)
    {
        string roleid = hiddenRoleID.Text;
        IniDeptNode(roleid, TreePanel2);
        if (string.IsNullOrWhiteSpace(roleid))
        {
            return null;
        }
        SspRole role = new SspRole();
        role.ObjId = int.Parse(roleid);
        SsbUser user = new SsbUser();
        IList<SsbUser> lst = basUserManager.GetRoleUserList(role, user);
        int total = lst.Count;
        return new { data = lst, total };
    }
    #endregion

    #region 权限设置
    [DirectMethod]
    public string ResetRoleAction(string roles, string actions)
    {
        if (this._.设置角色权限.Permit == 0)
        {
            return "您没有进行设置角色权限的权限！";
        }
        string Result = string.Empty;
        List<SspRoleAction> lst = new List<SspRoleAction>();
        string[] roleList = roles.Split('|');
        string[] actionList = actions.Split('|');
        foreach (string role in roleList)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                continue;
            }
            sysRoleActionManager.Delete(new SspRoleAction() { RoleId = Convert.ToInt32(role) });
            foreach (string action in actionList)
            {
                if (string.IsNullOrWhiteSpace(action))
                {
                    continue;
                }
                int irole = 0;
                if (!int.TryParse(role, out irole))
                {
                    continue;
                }
                int iaction = 0;
                if (!int.TryParse(action, out iaction))
                {
                    continue;
                }
                SspRoleAction m = new SspRoleAction();
                m.RoleId = irole;
                m.ActionId = iaction;
                lst.Add(m);
            }
        }
        sysRoleActionManager.BatchInsert(lst);
        return Result;
    }
    #endregion

    #region 权限显示赋值
    [DirectMethod]
    public string GetActionInfo(string role)
    {
        StringBuilder Result = new StringBuilder("|");
        IList<SspRoleAction> lst = sysRoleActionManager.GetEntityList(new SspRoleAction() { RoleId = Convert.ToInt32(role) });
        foreach (SspRoleAction m in lst)
        {
            Result.Append(m.ActionId).Append("|");
        }
        return Result.ToString();
    }
    #endregion

    #region 权限拷贝
    /// <summary>
    /// 拷贝角色权限
    /// </summary>
    /// <param name="sourceRoleID">The source role ID.</param>
    /// <param name="targetRoleID">The target role ID.</param>
    /// <remarks></remarks>
    [DirectMethod]
    public void SetRoleActionByOther(string sourceRoleID, string targetRoleID)
    {
        if (this._.复制角色权限.Permit == 0)
        {
            X.Msg.Alert("提示", "您没有进行复制角色权限的权限！").Show();
            return;
        }
        if (sourceRoleID == targetRoleID)
        {
            return;
        }
        sysRoleActionManager.Delete(new SspRoleAction() { RoleId = Convert.ToInt32(targetRoleID) });
        sysRoleActionManager.CopyForm(sourceRoleID, targetRoleID);
    }
    #endregion

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string RoleId, string UserId)
    {
        if (string.IsNullOrWhiteSpace(RoleId))
        {
            return "请选择角色信息！";
        }
        if (string.IsNullOrWhiteSpace(UserId))
        {
            return "请选择用户信息！";
        }
        var uid = Convert.ToInt32(UserId);
        var rid = Convert.ToInt32(RoleId);
        SspUserRole m = new SspUserRole();
        m.UserId = uid;
        m.RoleId = rid;
        sysUserRoleManager.Delete(m);
        return "删除成功！";
    }
}