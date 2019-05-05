using System;
using System.Text;

using Ext.Net;
using IEMS.Main.AppBiz;
using IEMS.Main.Entity;

public partial class Plugins_Main_SysUser_MyUser :IEMS.Frame.WebUI.Page
{
    #region 属性注入
    private IUserManager ssbUserManager = AppBizFactory.CreateInstance<IUserManager>();
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            int userid = this.Data.User.UserId;
            SsbUser user = ssbUserManager.GetEntityList(new SsbUser() { ObjId = userid })[0];
            txtUserCode.Text = user.WorkBarcode;
            txtUserName.Text = user.UserName;
            txtUserRealName.Text = user.RealName;
        }
    }
    #endregion
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int userid = this.Data.User.UserId;
        SsbUser upuser = ssbUserManager.GetEntityList(new SsbUser() { ObjId = userid })[0];
        if (txtUserNewPassword1.Text.Trim().Length > 0)
        {
            if (txtUserNewPassword1.Text.Trim().Length == 0)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "错误提示",
                    Message = "修改密码必须输入原密码！",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR
                }
                           );
                return;
            }
            if (txtUserNewPassword1.Text.Trim() != txtUserNewPassword2.Text.Trim())
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "错误提示",
                    Message = "修改密码必须输入原密码！",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR
                }
                           );
                return;
            }
            SsbUser user = ssbUserManager.GetEntityList(new SsbUser() { ObjId = userid })[0];
            var dencrypt = AppBizFactory.CreateInstance<IMcPassword>();
            string spassword = dencrypt.DecryptString(user.UserPwd.Trim(), string.Empty, Encoding.ASCII);
            if (spassword != txtUserPassword.Text)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "错误提示",
                    Message = "原密码不正确！",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR
                });
                return;
            }
            var encrypt = AppBizFactory.CreateInstance<IMcPassword>();
            upuser.UserPwd = encrypt.EncryptString(txtUserNewPassword1.Text.Trim(), string.Empty, Encoding.ASCII);
        }
        upuser.RealName = txtUserRealName.Text == "" ? txtUserName.Text : txtUserRealName.Text;
        ssbUserManager.Update(upuser, new SsbUser() { ObjId = userid });
        txtUserPassword.Text = string.Empty;
        txtUserNewPassword1.Text = string.Empty;
        txtUserNewPassword2.Text = string.Empty;
        X.Msg.Show(new MessageBoxConfig
        {
            Title = "成功提示",
            Message = "用户信息修改成功！",
            Buttons = MessageBox.Button.OK,
            Icon = MessageBox.Icon.INFO
        });
    }
}