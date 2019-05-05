using System;

using IEMS.Main.AppBiz;
using IEMS.Main.Entity;

public partial class Plugins_Main_Logout :IEMS.Frame.WebUI.Page
{
    #region 属性注入
    private ILoginLogManager sslLoginLogManager = AppBizFactory.CreateInstance<ILoginLogManager>();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        string uri = this.Session["MyReturnUrl"] as string;
        this.sslLoginLogManager.Logout(this.Data.User.UserId);
        if (String.IsNullOrEmpty(uri))
        {
            uri = this.ResolveUrl("~/");
        }
        this.Response.Redirect(uri, true);
    }
}