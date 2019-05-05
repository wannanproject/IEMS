using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

using Ext.Net;
using IEMS.Frame.WebUI.Entity;
using IEMS.Main.AppBiz;
using IEMS.Main.Entity;

public partial class Plugins_Main_SysRole_SetUserOneRole :IEMS.Frame.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ :IEMS.Frame.WebUI.___   //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            设置用户角色 = new PageAction() { ActionId = 1, ActionName = "btnSetRole" };
        }
        public PageAction 设置用户角色 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private IDeptManager basDeptManager = AppBizFactory.CreateInstance<IDeptManager>();
    private IPageActionManager sysPageActionManager = AppBizFactory.CreateInstance<IPageActionManager>();

    private IRoleManager sysRoleManager = AppBizFactory.CreateInstance<IRoleManager>();
    private IUserManager basUserManager = AppBizFactory.CreateInstance<IUserManager>();
    private IUserRoleManager sysUserRoleManager = AppBizFactory.CreateInstance<IUserRoleManager>();
    #endregion

    #region 常量定义
    readonly string pageidNodeIDStarWith = "pageid=";
    readonly string actionidNodeIDStarWith = "actionid=";
    #endregion

    private string GetRequest(string key)
    {
        if (this.Request[key] != null)
        {
            return this.Request[key].ToString();
        }
        return string.Empty;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack || X.IsAjaxRequest)
        {
            return;
        }
        SspRole role = sysRoleManager.GetEntityList(new SspRole() { ObjId = Convert.ToInt32(GetRequest("RoleID")) })[0];
        TreePanel1.Title = "部门信息[" + role.RoleName + "]";
        IList<SspUserRole> lst = sysUserRoleManager.GetEntityList(new SspUserRole() { RoleId = Convert.ToInt32(GetRequest("RoleID")) });
        StringBuilder sb = new StringBuilder(",");
        foreach (SspUserRole m in lst)
        {
            sb.Append(m.UserId).Append(",");
        }
        hiddenHaveRole.Text = sb.ToString();
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
    private void IniDeptTree(Node node, int leavel, string parCode)
    {
        IList<SsbDept> lst = basDeptManager.GetEntityList(new SsbDept() { DeptLevel = leavel + 1, ParentId = Convert.ToInt32(parCode) }, "DISPLAY_ID");
        if (lst.Count == 0)
        {
            node.Icon = Icon.BuildingGo;
            node.Leaf = true;
            return;
        }
        foreach (SsbDept m in lst)
        {
            Node n = IniNodeByProperty(m);
            n.NodeID = m.ObjId.ToString();
            if (basDeptManager.GetRowCount(new SsbDept() { DeptLevel = leavel + 2, ParentId = m.ObjId }) == 0)
            {
                n.Icon = Icon.BuildingGo;
                n.Leaf = true;
            }
            else
            {
                n.Icon = Icon.Building;
                n.Leaf = false;
            }
            node.Children.Add(n);
        }
    }
    [DirectMethod]
    public Node IniDeptTree(string ss)
    {
        Node node = new Node();
        IList<SsbDept> lst = new List<SsbDept>();
        if (ss.ToLower() == "root".ToLower())
        {
            lst = basDeptManager.GetEntityList(new SsbDept() { DeptLevel = 1, ParentId = 0 }, "DISPLAY_ID");
            foreach (SsbDept m in lst)
            {
                Node n = IniNodeByProperty(m);
                n.NodeID = m.ObjId.ToString();
                n.Icon = Icon.Building;
                n.Leaf = basDeptManager.GetRowCount(new SsbDept() { ParentId = m.ObjId }) == 0;
                node.Children.Add(n);
            }
        }
        else
        {
            lst = basDeptManager.GetEntityList(new SsbDept() { ObjId = Convert.ToInt32(ss) });
            foreach (SsbDept m in lst)
            {
                IniDeptTree(node, (int)m.DeptLevel, m.ObjId.ToString());
            }
        }
        return node;//.Children.ToJson();
    }

    private object GridPanelBindData_userinfo(string userName, string userCode)
    {
        var dic = new Dictionary<string, string>();
        dic.Add("USER_NAME", userName);
        dic.Add("WORK_BARCODE", userCode);

        var where = new Dictionary<string, object>();
        where.Add("where", dic);
        var dTable = basUserManager.GetAllUserList(where); // .GetDataTableByStatement("GetAllUserList", where);
        foreach (DataRow row in dTable.Rows)
        {
            var objid = row["OBJID"].ToString();
            if (hiddenHaveRole.Text.Contains("," + objid + ","))
            {
                row["IS_EMPLOYEE"] = 1;
            }
            else
            {
                row["IS_EMPLOYEE"] = 2;
            }
        }
        return new { data = dTable, total = dTable.Rows.Count };
    }
    private object GridPanelBindData_dept(string deptCode)
    {
        if (string.IsNullOrWhiteSpace(deptCode))
        {
            return new { data = new List<SsbUser>(), total = 0 };
        }
        var dic = new Dictionary<string, string>();
        dic.Add("DEPT_ID", deptCode);
        var where = new Dictionary<string, object>();
        where.Add("where", dic);
        var dTable = basUserManager.GetAllUserList(where); // .GetDataTableByStatement("GetAllUserList", where);
        foreach (DataRow row in dTable.Rows)
        {
            var objid = row["OBJID"].ToString();
            if (hiddenHaveRole.Text.Contains("," + objid + ","))
            {
                row["IS_EMPLOYEE"] = 1;
            }
            else
            {
                row["IS_EMPLOYEE"] = 2;
            }
        }
        return new { data = dTable, total = dTable.Rows.Count };
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        string userName = txtUserName.Text.Trim();
        string userCode = txtWorkBarcode.Text.Trim();
        if ((!string.IsNullOrWhiteSpace(userName)) || (!string.IsNullOrWhiteSpace(userCode)))
        {
            return GridPanelBindData_userinfo(userName, userCode);
        }
        else
        {
            string deptCode = hiddenDeptCode.Text.Trim();
            return GridPanelBindData_dept(deptCode);
        }
    }

    [DirectMethod]
    public object GridPanelBindDataRole(string action, Dictionary<string, object> extraParams)
    {
        IList<SspRole> lst = sysRoleManager.GetEntityList(new SspRole() { DeleteFlag = 0 }, "OBJID");
        int total = lst.Count;
        return new { data = lst, total };
    }

    [DirectMethod]
    public string ResetRoleAction(string users, string roles)
    {
        if (this._.设置用户角色.Permit == 0)
        {
            return "您没有进行设置用户角色的权限！";
        }
        string Result = string.Empty;
        List<SspUserRole> lst = new List<SspUserRole>();
        string[] userList = users.Split('|');
        string[] roleList = GetRequest("RoleID").Split('|');
        foreach (string role in roleList)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                continue;
            }
            int irole = 0;
            if (!int.TryParse(role, out irole))
            {
                continue;
            }
            foreach (string user in userList)
            {
                if (string.IsNullOrWhiteSpace(user))
                {
                    continue;
                }
                var uid = Convert.ToInt32(user);
                SspUserRole m = new SspUserRole();
                m.UserId = uid;
                m.RoleId = irole;
                sysUserRoleManager.Delete(m);
                sysUserRoleManager.Insert(m);
            }
        }
        sysUserRoleManager.BatchInsert(lst);
        return Result;
    }
}