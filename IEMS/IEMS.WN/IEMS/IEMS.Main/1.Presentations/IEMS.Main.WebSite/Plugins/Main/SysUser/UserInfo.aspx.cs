using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Ext.Net;
using MSTL.DbAccess;
using IEMS.Frame.WebUI.Entity;
using IEMS.Main.AppBiz;
using IEMS.Main.Entity;

public partial class Plugins_Main_SysUser_UserInfo :IEMS.Frame.WebUI.Page
{
    #region 业务处理类
    protected IUserManager userManager = AppBizFactory.CreateInstance<IUserManager>();
    protected IDeptManager deptManager = AppBizFactory.CreateInstance<IDeptManager>();
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ :IEMS.Frame.WebUI.___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            添加 = new PageAction() { ActionId = 1, ActionName = "btnAdd" };
            删除 = new PageAction() { ActionId = 2, ActionName = "Delete" };
            修改 = new PageAction() { ActionId = 3, ActionName = "Edit" };
            查询 = new PageAction() { ActionId = 4, ActionName = "btnSearch" };
            历史查询 = new PageAction() { ActionId = 5, ActionName = "btnHistorySearch" };
            恢复 = new PageAction() { ActionId = 6, ActionName = "Recover" };
            导出 = new PageAction() { ActionId = 7, ActionName = "btnExport" };
            初始化密码 = new PageAction() { ActionId = 8, ActionName = "InitPwd" };
            禁止登录 = new PageAction() { ActionId = 9, ActionName = "DeletePwd" };
        }
        public PageAction 添加 { get; private set; } //必须为 public
        public PageAction 删除 { get; private set; } //必须为 public
        public PageAction 修改 { get; private set; } //必须为 public
        public PageAction 查询 { get; private set; } //必须为 public
        public PageAction 历史查询 { get; private set; } //必须为 public
        public PageAction 恢复 { get; private set; } //必须为 public
        public PageAction 导出 { get; private set; } //必须为 public
        public PageAction 初始化密码 { get; private set; } //必须为 public
        public PageAction 禁止登录 { get; private set; } //必须为 public
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
        if (this.IsPostBack || X.IsAjaxRequest)
        {
            return;
        }

        #region 下拉列表 - 性别
        Ext.Net.ListItem item = new Ext.Net.ListItem("男", "1");
        add_sex.Items.Add(item);
        modify_sex.Items.Add(item);

        item = new Ext.Net.ListItem("女", "2");
        add_sex.Items.Add(item);
        modify_sex.Items.Add(item);

        if (add_sex.Items.Count > 0)
        {
            add_sex.Value = add_sex.Items[0].Value;
            modify_sex.Value = modify_sex.Items[0].Value;
        }
        #endregion

        #region 下拉列表 - 班组
            item = new Ext.Net.ListItem("甲", "1");
            add_class.Items.Add(item);
            modify_class.Items.Add(item);

        item = new Ext.Net.ListItem("乙", "2");
        add_class.Items.Add(item);
        modify_class.Items.Add(item);

        item = new Ext.Net.ListItem("丙", "3");
        add_class.Items.Add(item);
        modify_class.Items.Add(item);

        if (add_class.Items.Count > 0)
        {
            add_class.Value = add_class.Items[0].Value;
            modify_class.Value = modify_class.Items[0].Value;
        }
        #endregion

        #region 下拉列表 - 班次
        item = new Ext.Net.ListItem("早", "1");
        add_shift.Items.Add(item);
        modify_shift.Items.Add(item);

        item = new Ext.Net.ListItem("中", "2");
        add_shift.Items.Add(item);
        modify_shift.Items.Add(item);

        item = new Ext.Net.ListItem("夜", "3");
        add_shift.Items.Add(item);
        modify_shift.Items.Add(item);

        if (add_shift.Items.Count > 0)
        {
            add_shift.Value = add_shift.Items[0].Value;
            modify_shift.Value = modify_shift.Items[0].Value;
        }
        #endregion
    }
    #endregion

    #region 分页相关方法
    /// <summary>
    /// 数据分页查询
    /// </summary>
    /// <param name="pageResult"></param>
    /// <returns></returns>
    private PageResult GridPanelBindData(PageResult pageResult)
    {
        #region 初始化 pageParams
        var pageParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(txt_work_barcode.Text))
        {
            pageParams.Add("WORK_BARCODE", txt_work_barcode.Text.TrimEnd().TrimStart());
        }
        if (!string.IsNullOrEmpty(txt_user_name.Text.TrimEnd().TrimStart()))
        {
            pageParams.Add("USER_NAME", txt_user_name.Text.TrimEnd().TrimStart());
        }
        if (!string.IsNullOrEmpty(hidden_txt_dept.Text))
        {
            pageParams.Add("DEPT_ID", hidden_txt_dept.Text);
        }
        if (!string.IsNullOrEmpty(hidden_delete_flag.Text))
        {
            pageParams.Add("DELETE_FLAG", hidden_delete_flag.Text);
        }
        #endregion

        #region 设置查询
        pageResult.ParameterObject = pageParams;
        pageResult.StatementId = "GetAllUserList";
        pageResult.OrderString = "T1.OBJID ASC";
        #endregion

        return userManager.GetPageDataByReader(pageResult);
    }
    /// <summary>
    /// 用户界面分页
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
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
        this.ExcelFileDown(data, "用户信息");
        #endregion
    }
    #endregion

    #region 添加
    /// <summary>
    /// 点击添加按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        add_user_code.Text = string.Empty;
        add_user_name.Text = string.Empty;
        add_real_name.Text = string.Empty;
        add_telNum.Text = string.Empty;
        add_dept.Text = string.Empty;
        add_work.Text = string.Empty;
        add_class.Text = string.Empty;
        add_shift.Text = string.Empty;
        add_workshop.Text = string.Empty;
        add_remark.Text = string.Empty;
        hidden_add_dept.Text = string.Empty;
        hidden_add_work.Text = string.Empty;
        hidden_add_workshop.Text = string.Empty;
        this.winAdd.Show();
    }
    /// <summary>
    /// 数据库中是否存在相同字段
    /// </summary>
    /// <param name="objid"></param>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private bool userIsExist(int? objid, string field, string value)
    {
        Hashtable param = new Hashtable(1);
        var dic = new Dictionary<string, string>();
        dic.Add(field, value);
        param["where"] = dic;
        var userLst = userManager.GetDataTableByStatement("GetPageDataByReader", param);
        if (objid == null)
        {
            return userLst.Rows.Count > 0;
        }
        foreach (DataRow user in userLst.Rows)
        {
            if (user["objid"].ToString() != objid.ToString())
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(add_user_name.Text))
            {
                X.Msg.Alert("提示", "用户名不能为空！").Show();
                return;
            }
            if (string.IsNullOrWhiteSpace(add_user_code.Text))
            {
                X.Msg.Alert("提示", "用户工号不能为空！").Show();
                return;
            }
            if (userIsExist(null, "UserName", add_user_name.Text))
            {
                X.Msg.Alert("提示", "此用户名称已被使用！").Show();
                return;
            }
            if (userIsExist(null, "WorkBarcode", add_user_code.Text))
            {
                X.Msg.Alert("提示", "此用户工号已被使用！").Show();
                return;
            }
            string ss = string.Empty;
            var user = new SsbUser();
            user.WorkBarcode = add_user_code.Text;
            user.UserName = add_user_name.Text;
            user.RealName = add_real_name.Text;
            user.Sex = Convert.ToInt32(add_sex.SelectedItem.Value);
            user.Telephone = add_telNum.Text;
            ss = hidden_add_dept.Text;
            if (!string.IsNullOrWhiteSpace(ss))
            {
                user.DeptId = Convert.ToInt32(ss);
            }
            ss = hidden_add_work.Text;
            if (!string.IsNullOrWhiteSpace(ss))
            {
                user.WorkId = Convert.ToInt32(ss);
            }
            ss = hidden_add_workshop.Text;
            if (!string.IsNullOrWhiteSpace(ss))
            {
                user.WorkshopId = Convert.ToInt32(ss);
            }
            ss = add_class.Text;
            if (!string.IsNullOrWhiteSpace(ss))
            {
                user.ClassId = Convert.ToInt32(add_class.SelectedItem.Value);// Convert.ToInt32(hidden_shift_id.Text);
            }
            ss = add_shift.Text;
            if (!string.IsNullOrWhiteSpace(ss))
            {
                user.ShiftId = Convert.ToInt32(add_shift.SelectedItem.Value);// Convert.ToInt32(hidden_shift_id.Text);
            }
            user.Remark = add_remark.Text;
            user.DeleteFlag = 0;
            userManager.Insert(user);
            this.AppendWebLog("用户信息添加", "用户名" + user.UserName);
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            X.Msg.Alert("操作", "保存成功").Show();
        }
        catch (Exception ex)
        {
            X.Msg.Alert("操作", "保存失败：" + ex).Show();
        }
    }
    /// <summary>
    /// 点击取消按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
        this.winModify.Close();
    }
    #endregion

    #region 修改
    /// <summary>
    /// 点击修改激发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string obj_id)
    {
        #region 清空界面
        modify_objid.Text = string.Empty;
        modify_user_code.Text = string.Empty;
        modify_user_name.Text = string.Empty;
        modify_real_name.Text = string.Empty;
        modify_telNum.Text = string.Empty;
        modify_dept.Text = string.Empty;
        modify_work.Text = string.Empty;
        modify_class.Text = string.Empty;
        modify_shift.Text = string.Empty;
        modify_workshop.Text = string.Empty;
        modify_remark.Text = string.Empty;
        hidden_modify_dept.Text = string.Empty;
        hidden_modify_work.Text = string.Empty;
        hidden_modify_workshop.Text = string.Empty;
        #endregion

        #region 获取信息
        SsbUser user = userManager.GetByObjId(Convert.ToInt32(obj_id));
        #endregion

        #region 设置界面
        modify_objid.Value = user.ObjId;
        modify_user_name.Value = user.UserName;
        modify_real_name.Value = user.RealName;
        modify_sex.Select(user.Sex.ToString());
        modify_telNum.Value = user.Telephone;
        modify_user_code.Value = user.WorkBarcode;
        hidden_modify_dept.Value = user.DeptId;
        if (user.DeptId != null)
        {
            modify_dept.Value = deptManager.GetEntityList(new SsbDept() { ObjId = user.DeptId })[0].DeptName;
        }
        if (user.ClassId != null)
        {
            modify_class.Value = user.ClassId.ToString();
        }
        if (user.ShiftId != null)
        {
            modify_shift.Value = user.ShiftId.ToString();
        }
        modify_remark.Value = user.Remark;
        this.winModify.Show();
        #endregion
    }
    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(modify_objid.Text))
            {
                X.Msg.Alert("提示", "编号不能为空！").Show();
                return;
            }
            int objid = Convert.ToInt32(modify_objid.Text);
            if (string.IsNullOrWhiteSpace(modify_user_name.Text))
            {
                X.Msg.Alert("提示", "用户名不能为空！").Show();
                return;
            }
            if (string.IsNullOrWhiteSpace(modify_user_code.Text))
            {
                X.Msg.Alert("提示", "用户工号不能为空！").Show();
                return;
            }
            if (userIsExist(objid, "UserName", modify_user_name.Text))
            {
                X.Msg.Alert("提示", "此用户名称已被使用！").Show();
                return;
            }
            if (userIsExist(objid, "WorkBarcode", modify_user_code.Text))
            {
                X.Msg.Alert("提示", "此用户工号已被使用！").Show();
                return;
            }
            string ss = string.Empty;
            var user = new SsbUser();
            user.WorkBarcode = modify_user_code.Text;
            user.UserName = modify_user_name.Text;
            user.RealName = modify_real_name.Text;
            user.Sex = Convert.ToInt32(modify_sex.SelectedItem.Value);
            user.Telephone = modify_telNum.Text;
            ss = hidden_modify_dept.Text;
            if (!string.IsNullOrWhiteSpace(ss))
            {
                user.DeptId = Convert.ToInt32(ss);
            }
            ss = hidden_modify_work.Text;
            if (!string.IsNullOrWhiteSpace(ss))
            {
                user.WorkId = Convert.ToInt32(ss);
            }
            ss = hidden_modify_workshop.Text;
            if (!string.IsNullOrWhiteSpace(ss))
            {
                user.WorkshopId = Convert.ToInt32(ss);
            }
            ss = modify_class.Text;
            if (!string.IsNullOrWhiteSpace(ss))
            {
                user.ClassId = Convert.ToInt32(modify_class.SelectedItem.Value);// Convert.ToInt32(hidden_shift_id.Text);
            }
            else
            {
                user.ClassId = null;
            }
            ss = modify_shift.Text;
            if (!string.IsNullOrWhiteSpace(ss))
            {
                user.ShiftId = Convert.ToInt32(modify_shift.SelectedItem.Value);// Convert.ToInt32(hidden_shift_id.Text);
            }
            else
            {
                user.ShiftId = null;
            }
            user.Remark = modify_remark.Text;
            userManager.Update(user, new SsbUser() { ObjId = objid });
            this.AppendWebLog("用户信息修改", "ObjId=" + objid);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            X.Msg.Alert("操作", "保存成功").Show();
        }
        catch (Exception ex)
        {
            X.Msg.Alert("操作", "保存失败：" + ex).Show();
        }
    }
    #endregion

    #region 删除
    /// <summary>
    /// 点击恢复激发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_recover(string obj_id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(obj_id))
            {
                return "编号不能为空！";
            }
            int objid = Convert.ToInt32(obj_id);
            if (objid == 1)
            {
                return "超级管理员不支持此操作！";
            }
            userManager.Update(new SsbUser() { DeleteFlag = 0 }, new SsbUser() { ObjId = objid });
            this.AppendWebLog("用户恢复", "ObjId=" + objid);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            return "恢复成功！";
        }
        catch (Exception ex)
        {
            return "恢复失败！";
        }
    }

    /// <summary>
    /// 点击删除触发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string obj_id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(obj_id))
            {
                return "编号不能为空！";
            }
            int objid = Convert.ToInt32(obj_id);
            if (objid == 1)
            {
                return "超级管理员不支持此操作！";
            }
            userManager.Update(new SsbUser() { DeleteFlag = 1 }, new SsbUser() { ObjId = objid });
            this.AppendWebLog("用户删除", "ObjId=" + objid);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            return "删除成功！";
        }
        catch (Exception ex)
        {
            return "删除失败！";
        }
    }
    #endregion

    #region 密码
    /// <summary>
    /// 删除用户密码，禁止登陆
    /// </summary>
    /// <param name="obj_id"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_deletepwd(string obj_id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(obj_id))
            {
                return "编号不能为空！";
            }
            int objid = Convert.ToInt32(obj_id);
            if (objid == 1)
            {
                return "超级管理员不支持此操作！";
            }
            userManager.Update(new SsbUser() { UserPwd = string.Empty }, new SsbUser() { ObjId = objid });
            this.AppendWebLog("用户禁止登陆", "ObjId=" + objid);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            return "禁止登陆成功！";
        }
        catch (Exception ex)
        {
            return "禁止登陆失败！";
        }
    }

    /// <summary>
    /// 初始化用户密码，允许登陆
    /// </summary>
    /// <param name="obj_id"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_initpwd(string obj_id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(obj_id))
            {
                return "编号不能为空！";
            }
            int objid = Convert.ToInt32(obj_id);
            var encrypt = AppBizFactory.CreateInstance<IMcPassword>();
            string pwd = encrypt.EncryptString("123", string.Empty, Encoding.ASCII);
            userManager.Update(new SsbUser() { UserPwd = pwd }, new SsbUser() { ObjId = objid });
            this.AppendWebLog("用户初始化", "ObjId=" + objid);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            return "初始化登陆成功！";
        }
        catch (Exception ex)
        {
            return "初始化登陆失败！";
        }
    }
    #endregion
}