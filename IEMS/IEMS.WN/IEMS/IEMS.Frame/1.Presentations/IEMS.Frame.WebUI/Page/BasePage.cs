using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ext.Net;
using IEMS.Frame.WebUI.Db;
using IEMS.Frame.WebUI.Entity;
using System.IO;
using System.Web;
using System.Text;
using System.Data;
using MSTL.ExcelData;

namespace IEMS.Frame.WebUI
{
    /// <summary>
    /// Page 权限类
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
            this.Data = new PageData();
            this.PageMenu = new PageMenu();
        }
        /// <summary>
        /// 页面数据
        /// </summary>
        public PageData Data { get; private set; }
        /// <summary>
        /// 当前页面
        /// </summary>
        public PageMenu PageMenu { get; set; }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="ms">The ms.</param>
        /// <param name="filename">The filename.</param>
        /// <remarks></remarks>
        public void FileDown(MemoryStream ms, string filename)
        {
            Page page = (Page)HttpContext.Current.Handler;
            Byte[] content = ms.ToArray();
            page.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            page.Response.Cache.SetNoStore();
            page.Response.Clear();
            page.Response.ClearHeaders();
            page.Response.ClearContent();
            page.Response.Buffer = true;
            page.Response.ContentType = "application/octet-stream";
            page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename + ".xls", Encoding.UTF8));
            page.Response.AppendHeader("Content-Length", content.Length.ToString());
            page.Response.BinaryWrite(content);
            page.Response.Flush();
            page.Response.End();
        }
        /// <summary>
        /// Excel文件下载
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="filename">The filename.</param>
        /// <remarks></remarks>
        public void ExcelFileDown(DataTable dt, string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                filename = DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            Stream ms = new MemoryStream();
            new DataToFile().ToExcel(dt, ref ms);
            FileDown((MemoryStream)ms, filename);
            ms.Close();
            ms.Dispose();
        }
    }
}