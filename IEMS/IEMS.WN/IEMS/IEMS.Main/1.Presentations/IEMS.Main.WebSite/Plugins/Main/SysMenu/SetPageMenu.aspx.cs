using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

using Ext.Net;
using MSTL.DbAccess;
using IEMS.Frame.WebUI.Entity;
using IEMS.Main.AppBiz;
using IEMS.Main.Entity;
using IEMS.Main.DbRI;

public partial class Plugins_Main_SysMenu_SetPageMenu :IEMS.Frame.WebUI.Page
{
    #region 业务处理类
    protected IPageMenuManager pageMenuManager =AppBizFactory.CreateInstance<IPageMenuManager>();
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
            InitTreeMenuPage();

            modify_is_show.Items.Add(new Ext.Net.ListItem("是", 1));
            modify_is_show.Items.Add(new Ext.Net.ListItem("否", 0));

            add_is_show.Items.Add(new Ext.Net.ListItem("是", 1));
            add_is_show.Items.Add(new Ext.Net.ListItem("否", 0));
            add_is_show.Value = 1;
        }
    }
    #region 查询显示左侧菜单列表 树
    [DirectMethod]
    public string treePanelMenuPageLoad(string pageid)
    {
        SspPageMenu mainPageMenu = pageMenuManager.GetByObjId(Convert.ToInt32(pageid));
        IList<SspPageMenu> pageMenuList = pageMenuManager.GetMainPageMenuList(mainPageMenu.MenuLevel, mainPageMenu.MenuLevel.Length + 2);
        NodeCollection nodes = new Ext.Net.NodeCollection();
        if (pageMenuList.Count > 0)
        {
            foreach (SspPageMenu pageMenu in pageMenuList)
            {
                if (pageMenuManager.GetMainPageMenuList(pageMenu.MenuLevel, pageMenu.MenuLevel.Length + 2).Count > 0)
                {
                    Node node = new Node();
                    node.NodeID = pageMenu.ObjId.ToString();
                    node.Text = pageMenu.ShowName;
                    node.Icon = Icon.FolderLink;
                    node.Leaf = false;
                    nodes.Add(node);
                }
                else
                {
                    Node node = new Node();
                    node.NodeID = pageMenu.ObjId.ToString();
                    node.Text = pageMenu.ShowName;
                    node.Icon = Icon.FolderLink;
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
    private void InitTreeMenuPage()
    {
        treeDept.GetRootNode().RemoveAll();
        IList<SspPageMenu> pageMenuList = pageMenuManager.GetMainPageMenuList("", 2);
        foreach (SspPageMenu pageMenu in pageMenuList)
        {

            Node node = new Node();
            node.NodeID = pageMenu.ObjId.ToString();
            node.Text = pageMenu.ShowName;
            node.Icon = Icon.FolderLink;
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
        if (!string.IsNullOrEmpty(txt_obj_id.Text))
        {
            pageParams.Add("OBJID", txt_obj_id.Text);
        }
        if (!string.IsNullOrEmpty(txt_show_name.Text))
        {
            pageParams.Add("SHOW_NAME", txt_show_name.Text);
        }
        if (!string.IsNullOrEmpty(hidden_obj_id.Text))
        {
            SspPageMenu pageMenu = pageMenuManager.GetByObjId(Convert.ToInt32(hidden_obj_id.Text));
            pageParams.Add("MENU_LEVEL", pageMenu.MenuLevel);
        }
        if (!string.IsNullOrEmpty(txt_remark.Text))
        {
            pageParams.Add("REMARK", txt_remark.Text);
        }
        if (!string.IsNullOrEmpty(hidden_delete_flag.Text))
        {
            pageParams.Add("DELETE_FLAG", hidden_delete_flag.Text);
        }
        #endregion

        #region 设置查询
        pageResult.ParameterObject = pageParams;
        pageResult.StatementId = "GetAllPageMenuList";
        pageResult.OrderString = "T1.MENU_LEVEL ASC";
        #endregion

        return pageMenuManager.GetPageDataByReader(pageResult);
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
        if (hidden_obj_id.Value == "")
        {
            new MessageBox().Alert("操作", "请选择左侧菜单节点！").Show();
            return;
        }
        add_menu_level.Text = pageMenuManager.GetByObjId(Convert.ToInt32(hidden_obj_id.Text)).MenuLevel;
        add_show_name.Text = "";
        add_page_url.Text = "";
        add_ico_name.Text = "";
        add_seq_idx.Text = "";
        add_remark.Text = "";
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
            SspPageMenu pageMenu = pageMenuManager.GetByObjId(Convert.ToInt32(obj_id));
            int objid = Convert.ToInt32(obj_id);
            pageMenuManager.Update(new SspPageMenu() { DeleteFlag = 0 }, new SspPageMenu() { ObjId = objid });
            this.AppendWebLog("菜单信息恢复", "菜单编码：" + objid);
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
    public void commandcolumn_direct_edit(string objid)
    {
        SspPageMenu pageMenu = pageMenuManager.GetByObjId(Convert.ToInt32(objid));
        modify_obj_id.Value = pageMenu.ObjId;
        modify_menu_level.Value = pageMenu.MenuLevel;
        modify_show_name.Value = pageMenu.ShowName;
        modify_page_url.Value = pageMenu.PageUrl;
        modify_ico_name.Value = pageMenu.IcoName;
        modify_is_show.Value = pageMenu.IsShow;
        modify_seq_idx.Value = pageMenu.SeqIndex;
        modify_remark.Value = pageMenu.Remark;
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
            //判断是否可以删除，有子菜单禁止删除
            SspPageMenu pageMenu = pageMenuManager.GetByObjId(Convert.ToInt32(obj_id));
            if (pageMenu.MenuLevel.Length == 2)
            {
                return "删除失败：顶级菜单禁止删除";
            }

            IList<SspPageMenu> pageMenuList = pageMenuManager.GetMainPageMenuList(pageMenu.MenuLevel, pageMenu.MenuLevel.Length + 2);
            if (pageMenuList.Count > 0)
            {
                return "删除失败：有子菜单挂在此菜单下，禁止删除";
            }

            pageMenu.DeleteFlag = 1;
            int objid = Convert.ToInt32(obj_id);
            pageMenuManager.Update(new SspPageMenu() { DeleteFlag = 1 }, new SspPageMenu() { ObjId = objid });
            this.AppendWebLog("菜单信息删除", "菜单编码：" + objid);
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
    private int? getInt(string ss)
    {
        if (string.IsNullOrWhiteSpace(ss))
        {
            return null;
        }
        int result = 0;
        if (int.TryParse(ss, out result))
        {
            return result;
        }
        return null;
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
            IList<SspPageMenu> pageMenuList = pageMenuManager.GetEntityList(new SspPageMenu() { MenuLevel = add_menu_level.Text.TrimStart().TrimEnd() });
            if (pageMenuList.Count > 0)
            {
                X.Msg.Alert("提示", "此菜单层级已经被使用！").Show();
                return;
            }
            if (add_menu_level.Text.Length % 2 == 1)
            {
                X.Msg.Alert("提示", "菜单层级必须为偶数！").Show();
                return;
            }
            if (add_menu_level.Text.Length >= 2)
            {

                IList<SspPageMenu> pmList = pageMenuManager.GetEntityList(new SspPageMenu() { MenuLevel = add_menu_level.Text.Substring(0, add_menu_level.Text.Length - 2) });
                if (pmList.Count == 0)
                {
                    X.Msg.Alert("提示", "定义的菜单层级必须有上级菜单！").Show();
                    return;
                }
            }
            SspPageMenu pageMenu = new SspPageMenu();
            var seqService = SequenceServiceFactory.CreateInstance<ISeqSspPageMenuService>();
            pageMenu.ObjId = seqService.NEXTVAL;
            pageMenu.MenuLevel = add_menu_level.Text;
            pageMenu.ShowName = add_show_name.Text;
            pageMenu.PageUrl = add_page_url.Text.Trim().ToUpper();
            pageMenu.DeleteFlag = 0;
            pageMenu.IcoName = add_ico_name.Text;
            pageMenu.IsShow = Convert.ToInt32(add_is_show.SelectedItem.Value.ToString());
            pageMenu.SeqIndex = getInt(add_seq_idx.Text);
            pageMenu.RecordUserId = this.Data.User.UserId;
            pageMenu.RecordTime = DateTime.Now;
            pageMenu.Remark = add_remark.Text;
            pageMenuManager.Insert(pageMenu);
            pageMenuList = pageMenuManager.GetEntityList(new SspPageMenu() { MenuLevel = pageMenu.MenuLevel });
            this.AppendWebLog("菜单信息添加", "菜单编码：" + pageMenuList[0].ObjId);
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

            if (modify_menu_level.Text.Length % 2 == 1)
            {
                X.Msg.Alert("提示", "菜单层级必须为偶数！").Show();
                return;
            }
            if (modify_menu_level.Text.Length >= 2)
            {
                string parentMenuLevel = modify_menu_level.Text.Substring(0, modify_menu_level.Text.Length - 2);
                IList<SspPageMenu> pmList = pageMenuManager.GetEntityList(new SspPageMenu() { MenuLevel = parentMenuLevel });
                if (pmList.Count == 0)
                {
                    X.Msg.Alert("提示", "定义的菜单层级必须有上级菜单！").Show();
                    return;
                }
            }
            //修改重复校验
            SspPageMenu pageMenu = pageMenuManager.GetByObjId(Convert.ToInt32(modify_obj_id.Text));
            pageMenu.MenuLevel = modify_menu_level.Text;
            pageMenu.ShowName = modify_show_name.Text;
            pageMenu.PageUrl = modify_page_url.Text.Trim().ToUpper();
            pageMenu.IcoName = modify_ico_name.Text;
            pageMenu.IsShow = Convert.ToInt32(modify_is_show.Text);
            pageMenu.RecordUserId = this.Data.User.UserId;
            pageMenu.RecordTime = DateTime.Now;
            pageMenu.SeqIndex = getInt(modify_seq_idx.Text);
            pageMenu.Remark = modify_remark.Text;
            pageMenuManager.Update(pageMenu, new SspPageMenu() { ObjId = Convert.ToInt32(modify_obj_id.Text) });
            this.AppendWebLog("系统菜单修改", "菜单编码：" + pageMenu.ObjId);
            this.winModify.Close();
            pageToolBar.DoRefresh();
            //左侧树刷新
            treeDept.GetNodeById(hidden_obj_id.Text).Reload();
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
        //IList<SspPageMenu> childDepList = pageMenuManager.GetEntityList(new SspPageMenu() { ParentId = Convert.ToInt32(String.IsNullOrEmpty(hidden_parent_num.Text) ? "1" : hidden_parent_num.Text), DeleteFlag = 0 });
        //if (childDepList.Count == 1)
        //{
        //    treeDept.GetNodeById(String.IsNullOrEmpty(hidden_parent_num.Text) ? "1" : hidden_parent_num.Text).ParentNode().Reload();
        //}
        //else
        //{
        //    treeDept.GetNodeById(String.IsNullOrEmpty(hidden_parent_num.Text) ? "1" : hidden_parent_num.Text).Reload();
        //}
    }
    #endregion

    #region 校验方法
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