using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

using Ext.Net;
using MSTL.DbAccess;
using IEMS.Frame.WebUI.Entity;
using IEMS.Main.AppBiz;
using IEMS.Main.Entity;

public partial class Plugins_Main_BaseInfo_DeptInfo : IEMS.Frame.WebUI.Page
{
    #region 业务处理类
    protected IDeptManager deptManager = AppBizFactory.CreateInstance<IDeptManager>();
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ :IEMS.Frame.WebUI.___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            添加 = new PageAction() { ActionId = 1, ActionName = "btn_add" };
            删除 = new PageAction() { ActionId = 2, ActionName = "Delete" };
            修改 = new PageAction() { ActionId = 3, ActionName = "Edit" };
            查询 = new PageAction() { ActionId = 4, ActionName = "btn_search" };
            历史查询 = new PageAction() { ActionId = 5, ActionName = "btn_history_search" };
            导出 = new PageAction() { ActionId = 6, ActionName = "btnExport" };
        }
        public PageAction 添加 { get; private set; } //必须为 public
        public PageAction 删除 { get; private set; } //必须为 public
        public PageAction 修改 { get; private set; } //必须为 public
        public PageAction 查询 { get; private set; } //必须为 public
        public PageAction 历史查询 { get; private set; } //必须为 public
        public PageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    #region 初始化方法
    /// <summary>
    /// 页面初始化方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitTreeDept();
        }
    }
    #region 查询显示左侧部门列表 树
    [DirectMethod]
    public string treePanelDeptLoad(string pageid)
    {
        IList<SsbDept> deptList = deptManager.GetEntityList(new SsbDept() { DeleteFlag = 0, ParentId = Convert.ToInt32(pageid) },"OBJID ASC");
        NodeCollection nodes = new Ext.Net.NodeCollection();
        if (deptList.Count > 0)
        {
            foreach (SsbDept dept in deptList)
            {
                if (deptManager.GetEntityList(new SsbDept() { ParentId = dept.ObjId, DeleteFlag = 0 }).Count > 0)
                {
                    Node node = new Node();
                    node.NodeID = dept.ObjId.ToString();
                    node.Text = dept.DeptName;
                    node.Icon = Icon.Building;
                    node.Leaf = false;
                    nodes.Add(node);
                }
                else
                {
                    Node node = new Node();
                    node.NodeID = dept.ObjId.ToString();
                    node.Text = dept.DeptName;
                    node.Icon = Icon.BuildingGo;
                    node.Leaf = true;
                    nodes.Add(node);
                }
            }
        }
        return nodes.ToJson();
    }
    /// <summary>
    /// 初始化部门列表树
    /// </summary>
    private void InitTreeDept()
    {
        treeDept.GetRootNode().RemoveAll();
        IList<SsbDept> deptList = deptManager.GetEntityList(new SsbDept() { DeleteFlag = 0, ParentId = 0 });
        foreach (SsbDept dept in deptList)
        {

            Node node = new Node();
            node.NodeID = dept.ObjId.ToString();
            node.Text = dept.DeptName;
            node.Icon = Icon.Building;
            treeDept.GetRootNode().AppendChild(node);
        }
    }
    #endregion
    #endregion

    #region 分页相关方法
    private PageResult GridPanelBindData(PageResult pageResult)
    {
        #region 初始化 pageParams
        var pageParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(txt_dep_code.Text))
        {
            pageParams.Add("ObjId", txt_dep_code.Text.TrimEnd().TrimStart());
        }
        if (!string.IsNullOrEmpty(txt_dep_name.Text.TrimEnd().TrimStart()))
        {
            pageParams.Add("DeptName", txt_dep_name.Text.TrimEnd().TrimStart());
        }
        if (!string.IsNullOrEmpty(hidden_parent_num.Text))
        {
            pageParams.Add("ParentId", hidden_parent_num.Text);
        }
        if (!string.IsNullOrEmpty(txt_remark.Text))
        {
            pageParams.Add("Remark", txt_remark.Text);
        }
        if (!string.IsNullOrEmpty(hidden_delete_flag.Text))
        {
            pageParams.Add("DeleteFlag", hidden_delete_flag.Text);
        }
        #endregion

        #region 设置查询
        pageResult.ParameterObject = pageParams;
        pageResult.StatementId = "GetAllDeptInfoListOrder";
        pageResult.OrderString = "OBJID ASC";
        #endregion

        return deptManager.GetPageDataByReader(pageResult);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        #region 初始化 pageResult
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        var pageResult = new PageResult();
        pageResult.PageIndex = prms.Page;
        pageResult.PageSize = prms.Limit;
        #endregion

        #region 输出表格数据
        pageResult = GridPanelBindData(pageResult);
        var data = pageResult.ResultDataSet.Tables[0];
        var total = pageResult.RecordCount;
        return new { data, total };
        #endregion
    }

    #endregion

    #region 导出
    /// <summary>
    /// 导出调用方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        #region 初始化 pageResult
        var pageResult = new PageResult();
        pageResult.PageIndex = 0;
        pageResult.PageSize = 0;
        #endregion

        #region pageResult 导出为Excel
        pageResult = GridPanelBindData(pageResult);
       var data = pageResult.ResultDataSet.Tables[0];
        for (int i = 0; i < data.Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = data.Columns[i];
            foreach (ColumnBase cb in this.pnlList.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
                {
                    dc.ColumnName = cb.Text;
                    isshow = true;
                    break;
                }
            }
            if (!isshow)
            {
                data.Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        this.ExcelFileDown(data, "部门信息");
        #endregion
    }
    #endregion

    #region 增删改查按钮事件

    /// <summary>
    /// 点击添加按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (hidden_parent_num.Value == "" || hidden_parent_num.Value.ToString() == "0")
        {
            new MessageBox().Alert("操作", "请选择左侧部门节点！").Show();
            return;
        }
        add_dep_name.Text = "";
        hidden_dept_name.Text = "";
        add_parent_num.Text = deptManager.GetByObjId(Convert.ToInt32(hidden_parent_num.Value == "" ? 0 : hidden_parent_num.Value)).DeptName;
        add_display_id.Text = "";
        add_remark.Text = "";
        btnAddSave.Disable(true);
        this.winAdd.Show();
    }

    /// <summary>
    /// 点击恢复激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_recover(string obj_id)
    {
        try
        {
            SsbDept dep = deptManager.GetByObjId(Convert.ToInt32(obj_id));
            IList<SsbDept> parentDepList = deptManager.GetEntityList(new SsbDept() { ObjId = dep.ParentId, DeleteFlag = 1 });
            if (parentDepList.Count > 0)
            {
                return "恢复失败：请先恢复父级部门[" + parentDepList[0].DeptName + "]";
            }
            int objid = Convert.ToInt32(obj_id);
            deptManager.Update(new SsbDept() { DeleteFlag = 0 }, new SsbDept() { ObjId = objid });

            this.AppendWebLog("部门信息恢复", "部门编码：" + objid);
            pageToolBar.DoRefresh();

            //左侧树刷新 
            ReloadLeftDeptTree();
        }
        catch (Exception e)
        {
            return "恢复失败：" + e;
        }
        return "恢复成功";
    }

    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string dep_num)
    {
        SsbDept dep = deptManager.GetByObjId(Convert.ToInt32(dep_num));
        modify_obj_id.Value = dep.ObjId;
        modify_dep_name.Value = dep.DeptName;
        hidden_dept_name.Value = dep.DeptName;
        modify_dep_level.Value = dep.DeptLevel + "级";
        //-----------------------加载父级部门下拉框数据
        modify_parent_num.Value = dep.ParentId == 0 ? "" : deptManager.GetByObjId(Convert.ToInt32(dep.ParentId)).DeptName;
        hidden_parent_num.Value = dep.ParentId;

        modify_display_id.Value = dep.DisplayId;
        modify_remark.Value = dep.Remark;
        this.winModify.Show();
    }

    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string obj_id)
    {
        try
        {
            //判断是否可以删除，有子菜单禁止删除，部门中心禁止删除
            SsbDept dep = deptManager.GetByObjId(Convert.ToInt32(obj_id));
            if (1.Equals(obj_id))
            {
                return "删除失败：顶级部门禁止删除";
            }
            IList<SsbDept> depList = deptManager.GetEntityList(new SsbDept() { ParentId = dep.ObjId , DeleteFlag = 0});
            if (depList.Count > 0)
            {
                return "删除失败：有子部门挂在此部门下，禁止删除";
            }

            dep.DeleteFlag = 1;
            int objid = Convert.ToInt32(obj_id);
            deptManager.Update(new SsbDept() { DeleteFlag = 1 }, new SsbDept() { ObjId = objid });
            this.AppendWebLog("部门信息删除", "部门编码：" +  objid);
            pageToolBar.DoRefresh();

            //左侧树刷新 
            ReloadLeftDeptTree();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
        this.winModify.Close();
    }

    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            //添加校验重复
            IList<SsbDept> depList = deptManager.GetEntityList(new SsbDept() { DeptName = add_dep_name.Text.TrimStart().TrimEnd()}); 
            if (depList.Count > 0)
            {
                X.Msg.Alert("提示", "此部门名称已被使用！").Show();
                return;
            }
            SsbDept dep = new SsbDept();
            dep.DeptName = add_dep_name.Text;
            dep.DeptLevel = Convert.ToInt32(deptManager.GetByObjId(Convert.ToInt32(hidden_parent_num.Value)).DeptLevel + 1);
            dep.ParentId = Convert.ToInt32(hidden_parent_num.Value);
            dep.DeleteFlag = 0;
            if (!string.Empty.Equals(add_display_id.Text))
            {
                dep.DisplayId = Convert.ToInt32(add_display_id.Text);
            }
            dep.Remark = add_remark.Text;
            deptManager.Insert(dep);
            IList<SsbDept> deptList = deptManager.GetEntityList(new SsbDept() { DeptName = dep.DeptName});
            this.AppendWebLog("部门信息添加", "部门编码：" + deptList[0].ObjId);
            this.winAdd.Close();
            pageToolBar.DoRefresh();

            //左侧树刷新 
            ReloadLeftDeptTree();
            new MessageBox().Alert("操作", "保存成功").Show();
        }
        catch (Exception ex)
        {
            new MessageBox().Alert("操作", "保存失败").Show();
        }
    }

    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            //修改重复校验
            IList<SsbDept> workList = deptManager.GetEntityList(new SsbDept() { DeptName = modify_dep_name.Text.TrimStart().TrimEnd()});
            if (workList.Count > 0)
            {
                if (workList[0].DeptName != hidden_dept_name.Text)
                {
                    X.Msg.Alert("提示", "此部门名称已被使用！").Show();
                    return;
                }
            }
            SsbDept dep = deptManager.GetByObjId(Convert.ToInt32(modify_obj_id.Text));
            dep.DeptName = modify_dep_name.Text;
            dep.ParentId = Convert.ToInt32(hidden_parent_num.Value);
            dep.DeleteFlag = 0;
            if (!string.Empty.Equals(modify_display_id.Text))
            {
                dep.DisplayId = Convert.ToInt32(modify_display_id.Text);
            }
            dep.Remark = modify_remark.Text;
            deptManager.Update(dep, new SsbDept() { ObjId = Convert.ToInt32(modify_obj_id.Text) });
            this.AppendWebLog("部门信息修改", "部门编码：" + dep.ObjId);
            this.winModify.Close();
            pageToolBar.DoRefresh();
            //左侧树刷新
            treeDept.GetNodeById(hidden_parent_num.Text).Reload();
            X.Msg.Alert("操作", "更新成功");
            X.Msg.Show();
        }
        catch (Exception ex)
        {
            X.Msg.Alert("操作", "更新失败：" + ex);
            X.Msg.Show();
        }
    }


    /// <summary>
    /// 左侧树刷新方法
    /// </summary>
    private void ReloadLeftDeptTree()
    {
        IList<SsbDept> childDepList = deptManager.GetEntityList(new SsbDept() { ParentId = Convert.ToInt32(String.IsNullOrEmpty(hidden_parent_num.Text) ? "1" : hidden_parent_num.Text), DeleteFlag = 0 });
        if (childDepList.Count == 1)
        {
            treeDept.GetNodeById(String.IsNullOrEmpty(hidden_parent_num.Text) ? "1" : hidden_parent_num.Text).ParentNode().Reload();
        }
        else
        {
            treeDept.GetNodeById(String.IsNullOrEmpty(hidden_parent_num.Text) ? "1" : hidden_parent_num.Text).Reload();
        }
    }
    #endregion
    
    #region 校验方法
    /// <summary>
    /// 检查用户名是否重复
    /// yuany
    /// 2013年1月31日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckDeptName(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string deptname = field.Text;
        IList<SsbDept> deptList = deptManager.GetEntityList(new SsbDept() { DeptName = deptname });
        if (deptList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (deptList[0].DeptName.ToString() == hidden_dept_name.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此部门名称已被使用！";
            }
        }
    }

    protected void CheckFieldInt(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        Regex regex = new Regex(@"^[\d]+$");
        if (regex.IsMatch(field.Text) || field.Text == "")
        {
            e.Success = true;
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此填入项必须为正整数!";
        }
    }
    #endregion
}