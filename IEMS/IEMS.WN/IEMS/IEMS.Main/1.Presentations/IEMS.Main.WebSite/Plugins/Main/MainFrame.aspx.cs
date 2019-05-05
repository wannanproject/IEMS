using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

using Ext.Net;
using IEMS.Main.AppBiz;
using IEMS.Main.Entity;

public partial class Plugins_Main_MainFrame :IEMS.Frame.WebUI.Page
{
    #region 属性注入
    private IPageMenuManager sspPageMenuManager = AppBizFactory.CreateInstance<IPageMenuManager>();
    private IUserManager ssbUserManager = AppBizFactory.CreateInstance<IUserManager>();
    #endregion

    private string getDbVersion()
    {
        var dbVersion = Session["dbVersion"];
        if (dbVersion==null)
        {
            return string.Empty;
        }
        Dictionary<string, string> versions = MSTL.DbAccess.DbVersion.Instance.GetDatasourceVersions();
        if (versions.ContainsKey(dbVersion.ToString()))
        {
            return versions[dbVersion.ToString()];
        }
        return string.Empty;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !X.IsAjaxRequest)
        {
            SetTaskCssColor();
            SsbUser user = ssbUserManager.GetEntityList(new SsbUser() { ObjId = this.Data.User.UserId }).FirstOrDefault();

            this.BorderPanelWest.Title = getDbVersion();
            this.mainTreePanel.Title = "[" + user.UserName + "]";
        }
    }


    public string getloginUser()
    {
        var user = ssbUserManager.GetEntityList(new SsbUser() { ObjId = this.Data.User.UserId }).FirstOrDefault();
        if (user == null)
        {
            return string.Empty;
        }
        return user.WorkBarcode;
    }
    private string addTab(string id, string title, string url, bool closable)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("addTab('").Append(id).Append("','");
        sb.Append(title).Append("','");
        sb.Append(url).Append("',");
        sb.Append(closable.ToString().ToLower()).Append(");");
        return sb.ToString();
    }
    /// <summary>
    /// 添加默认页
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    protected string AddTabDefault()
    {
        StringBuilder sb = new StringBuilder();
        return sb.ToString();
    }

    /// <summary>
    /// Visions this instance.
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    protected string Vision()
    {
        string fileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        fileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
        System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
        return fi.LastWriteTime.ToString("yyyy.MM.dd.HHmmss");
    }

    private Node IniTreeNode(SspPageMenu menu)
    {
        Node result = new Node();
        result.NodeID = menu.ObjId.ToString();
        result.Text = menu.ShowName;
        result.Leaf = false;
        if ((menu.PageUrl != null) && (menu.PageUrl.Length > 0))
        {
            if (menu.PageUrl.ToLower().StartsWith("http://") || menu.PageUrl.ToLower().StartsWith("https://")
                || menu.PageUrl.ToLower().Replace(" ", string.Empty).StartsWith("javascript:"))
            {
                result.Href = menu.PageUrl;
            }
            else if (!string.IsNullOrWhiteSpace(menu.HttpUrl))
            {
                var http = menu.HttpUrl.Trim();

                if ((!http.ToLower().StartsWith("http://")) && (!http.ToLower().StartsWith("https://")))
                {
                    http = "http://" + http;
                }
                if (!http.EndsWith("/"))
                {
                    http = http + "/";
                }
                result.Href = http + menu.PageUrl;
            }
            else
            {
                result.Href = Page.ResolveUrl("~/") + menu.PageUrl;
            }
            result.Leaf = true;
        }
        return result;
    }

    /// <summary>
    /// 初始化一级菜单数据
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="Ext.Net.NodeLoadEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void GetUserPageGroup(object sender, NodeLoadEventArgs e)
    {
        TreePanel trp = (sender as TreeStore).Parent as TreePanel;
        IList<SspPageMenu> lst = this.sspPageMenuManager.GetUserMenuPageList(this.Data.User.UserId, string.Empty);
        foreach (SspPageMenu menu in lst)
        {
            trp.GetRootNode().AppendChild(IniTreeNode(menu));
        }
        trp.GetRootNode().Expand(false);
    }
    /// <summary>
    /// Ajax方式加载二级菜单
    /// </summary>
    /// <param name="storeID">The store ID.</param>
    /// <param name="nodeID">The node ID.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public string NodeLoad(string storeID, string nodeID)
    {
        NodeCollection nodes = new Ext.Net.NodeCollection();
        string parid = sspPageMenuManager.GetEntityList(new SspPageMenu() { ObjId = Convert.ToInt32(nodeID) })[0].MenuLevel;
        IList<SspPageMenu> lst = this.sspPageMenuManager.GetUserMenuPageList(this.Data.User.UserId, parid);
        foreach (SspPageMenu menu in lst)
        {
            nodes.Add(IniTreeNode(menu));
        }
        if (lst.Count == 0)
        {
            Node treeNode = new Node();
            treeNode.NodeID = "无相关菜单=" + DateTime.Now.ToString("yyyyMMddHHmmss");
            treeNode.Text = "无相关菜单";
            treeNode.Leaf = true;
            nodes.Add(treeNode);
        }
        return nodes.ToJson();
    }

    /// <summary>
    /// Called when [timer].
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public string OnTimer()
    {
        int UserID = this.Data.User.UserId;
        this.Data.User.UserId = UserID;
        return DateTime.Now.ToString("HH:mm");
    }
    /// <summary>
    /// 设置皮肤
    /// </summary>
    /// <param name="theme"></param>
    /// <returns></returns>
    [DirectMethod]
    public string GetThemeUrl(string theme)
    {
        Theme temp = (Theme)Enum.Parse(typeof(Theme), theme);

        this.Session["Ext.Net.Theme"] = temp;

        return this.ResourceManager1.GetThemeUrl(temp);
    }


    /// <summary>
    /// 设置待办事项标题的样式颜色
    /// </summary>
    /// <param name="theme"></param>
    /// <returns></returns>
    [DirectMethod]
    public string SetTaskCssColor()
    {
        return "false";
    }

}