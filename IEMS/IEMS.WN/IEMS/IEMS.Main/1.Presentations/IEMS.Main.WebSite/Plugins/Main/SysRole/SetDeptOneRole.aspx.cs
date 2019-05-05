using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Ext.Net;
using IEMS.Frame.WebUI.Entity;
using IEMS.Main.AppBiz;
using IEMS.Main.Entity;

public partial class Plugins_Main_SysRole_SetDeptOneRole :IEMS.Frame.WebUI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ :IEMS.Frame.WebUI.___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            设置部门角色 = new PageAction() { ActionId = 1, ActionName = "btnSetRole" };
        }
        public PageAction 设置部门角色 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private IDeptManager basDeptManager = AppBizFactory.CreateInstance<IDeptManager>();
    private IRoleManager sysRoleManager = AppBizFactory.CreateInstance<IRoleManager>();
    private IDeptRoleManager sysDeptRoleManager = AppBizFactory.CreateInstance<IDeptRoleManager>();
    #endregion

    /// <summary>
    /// Gets the request.
    /// yuany   @ 2015-1-20 11:17:19
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    /// <remarks></remarks>
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
        IList<SspDeptRole> lst = sysDeptRoleManager.GetEntityList(new SspDeptRole() { RoleId = Convert.ToInt32(GetRequest("RoleID")) });
        StringBuilder sb = new StringBuilder(",");
        foreach (SspDeptRole m in lst)
        {
            sb.Append(m.DeptId).Append(",");
        }
        hiddenHaveRole.Text = sb.ToString();
    }
    private void CheckNode(Node node)
    {
        if (hiddenHaveRole.Text != null && hiddenHaveRole.Text.Contains("," + node.NodeID + ","))
        {
            node.Checked = true;
        }
    }
    private Node IniNode(object obj)
    {
        Node n = new Node();
        n.Checked = false;
        n.Icon = Icon.Building;
        PropertyInfo[] fields = obj.GetType().GetProperties();
        foreach (PropertyInfo f in fields)
        {
            ConfigItem c = new ConfigItem();
            object value = f.GetValue(obj, null);
            c.Name = f.Name.ToString();
            if (f.Name.ToString().ToLower() == "DeleteFlag".ToLower())
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
            Node n = IniNode(m);
            n.NodeID = m.ObjId.ToString();
            CheckNode(n);
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
                Node n = IniNode(m);
                n.NodeID = m.ObjId.ToString();
                CheckNode(n);
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


    [DirectMethod]
    public string ResetRoleAction(string depts, string roles)
    {
        if (this._.设置部门角色.Permit == 0)
        {
            return "您没有进行设置部门角色的权限！";
        }
        string Result = string.Empty;
        List<SspDeptRole> lst = new List<SspDeptRole>();
        string[] deptList = depts.Split('|');

        int irole = 0;
        if (!int.TryParse(GetRequest("RoleID"), out irole))
        {
            return "请选择角色";
        }
        string[] roleList = roles.Split('|');
        foreach (string dept in deptList)
        {
            if (string.IsNullOrWhiteSpace(dept))
            {
                continue;
            }
            sysDeptRoleManager.Delete(new SspDeptRole() { RoleId = irole, DeptId = Convert.ToInt32(dept) });
        }
        foreach (string dept in roleList)
        {
            if (string.IsNullOrWhiteSpace(dept))
            {
                continue;
            }
            SspDeptRole m = new SspDeptRole();
            m.DeptId = Convert.ToInt32(dept);
            m.RoleId = irole;
            lst.Add(m);
        }
        sysDeptRoleManager.BatchInsert(lst);
        return Result;
    }
}