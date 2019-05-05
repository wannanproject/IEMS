using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using IEMS.Main.AppBiz;
using IEMS.Main.Entity;

public partial class Plugins_Main_Login :IEMS.Frame.WebUI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.InitDbVersion();           //初始化数据版本(帐套)
        }
    }

    #region 初始化数据版本(帐套)
    /// <summary>
    /// 初始化数据版本
    /// </summary>
    /// <remarks></remarks>
    private void InitDbVersion()
    {
        Dictionary<string, string> versions = MSTL.DbAccess.DbVersion.Instance.GetDatasourceVersions();
        // Mesnac.DbAccess.DbVersion.Instance.GetDatasourceVersions();
        foreach (KeyValuePair<string, string> version in versions)
        {
            ListItem item = new ListItem();
            item.Value = version.Key;
            item.Text = version.Value;
            this.ddlDbVersion.Items.Add(item);
        }
        if (this.ddlDbVersion.Items.Count <= 1)
        {
            this.trDbVersion.Visible = false;
        }
    }
    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ILoginLogManager loginLogManager = AppBizFactory.CreateInstance<ILoginLogManager>();
        #region 获取使用的数据版本(帐套)
        string version = this.ddlDbVersion.SelectedItem.Value;
        //保存当前用户使用的数据版本（帐套）
        Session["dbVersion"] = version;
        #endregion
        SsbUser currUser = new SsbUser();
        currUser.UserName = this.txtUserName.Text.Trim();
        currUser.UserPwd = this.txtPassword.Text.Trim();
        currUser = loginLogManager.Login(currUser);
        if (currUser != null)
        {
            //保存当前用户使用的数据版本（帐套）
            this.Data.User.UserId = (int)currUser.ObjId;
            Session["MyReturnUrl"] = this.Request.Url.AbsolutePath;  //保存起始登录页的访问路径
            this.AppendWebLog("用户登陆成功", "UserId=" + this.Data.User.UserId);
            Response.Redirect("MainFrame.aspx");
        }
        else
        {
            this.cv.IsValid = false;
        }
    }
}