using System;
using System.Collections.Generic;
using System.Data;

namespace IEMS.Frame.McUI
{
    using Ext.Net;
    using IEMS.Frame.McUI.ExtNet;
    using MSTL.DbAccess;
    using IEMS.Frame.AppBiz;

    public abstract class Page : IEMS.Frame.WebUI.Page
    {
        #region 属性
        private IMcUIManager getIMcUIManager()
        {
            string pluginName = this.ui.PluginName;
            var dbMapper = DbVersion.Instance.GetDbMapperByPluginName(pluginName);
            return AppBizFactory.CreateInstance<IMcUIManager>(dbMapper.DbName);
        }
        /// <summary>
        /// 业务执行类
        /// </summary>
        protected IMcUIManager manager
        {
            get
            {
                return getIMcUIManager();
            }
        }
        /// <summary>
        /// 配置界面类
        /// </summary>
        protected UiHelper ui
        {
            get
            {
                string id = Page.RouteData.Values["id"] as string;//接收路由参数
                string type = Page.RouteData.Values["UiType"] as string;//接收路由参数
                var result = UiHelper.GetInstance(id, type);
                return result;
            }
        }
        /// <summary>
        /// 界面数据操作
        /// </summary>
        protected PageData pdata
        {
            get
            {
                return new PageData(ui);
            }
        }
        #endregion

        #region 页面初始化
        /// <summary>
        /// 页面load方法  在Page_Load被调用
        /// </summary>
        protected abstract void McUI_Load();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 页面首次执行
            if ((IsPostBack || X.IsAjaxRequest))
            {
                return;
            }
            #endregion

            #region 页面初始化
            if (ui == null)
            {
                return;
            }
            var pageini = new PageIni(ui, manager);
            pageini.IniWebPage(this);
            McUI_Load();
            #endregion
        }
        #endregion

        #region 执行扩展
        /// <summary>
        /// 获取配置的执行接口参数
        /// </summary>
        /// <param name="biz"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected CommandParam getCommandParam(UiBiz biz, Dictionary<string, object> param)
        {
            CommandParam result = new CommandParam();
            result.DbHelper = manager.DbHelper;
            result.UiBiz = biz;
            if (param != null)
            {
                result.UiParam = param;
            }
            result.McUIManager = manager;
            result.SysParam.UserId = new ParamKeyValue(biz.UserField, this.Data.User.UserId);
            return result;
        }
        /// <summary>
        /// 执行配置的执行接口
        /// </summary>
        /// <param name="commandParam"></param>
        /// <returns></returns>
        protected CommandResult ExecuteCommands(CommandParam commandParam)
        {
            var biz = commandParam.UiBiz;
            var uiHelper = commandParam.UiHelper;
            #region 参数格式化接口
            CommandResult result = new CommandResult();
            foreach (Command command in biz.TrimCommands)
            {
                if (command != null && command.Instance != null)
                {
                    result = command.Execute(commandParam);
                    if (result.isBreak)
                    {
                        X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = result.sResult, Icon = MessageBox.Icon.WARNING });
                        return result;
                    }
                }
            }
            #endregion
            #region 参数校验接口
            foreach (Command command in biz.VerifyCommands)
            {
                if (command != null && command.Instance != null)
                {
                    result = command.Execute(commandParam);
                    if (result.isBreak)
                    {
                        X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = result.sResult, Icon = MessageBox.Icon.WARNING });
                        return result;
                    }
                }
            }
            #endregion
            #region 执行接口
            if (true)
            {
                Command command = biz.ExcuteCommand;
                {
                    if (command != null && command.Instance != null)
                    {
                        result = command.Execute(commandParam);
                        if (result.isBreak)
                        {
                            if (!string.IsNullOrWhiteSpace(result.sResult))
                            {
                                X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = result.sResult, Icon = MessageBox.Icon.WARNING });
                            }
                            else if (result.Exception != null)
                            {
                                X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = result.Exception.ToString(), Icon = MessageBox.Icon.WARNING });
                            }
                            return result;
                        }
                    }
                }
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 执行  执行接口命令
        /// </summary>
        /// <param name="commandParam"></param>
        /// <returns></returns>
        protected CommandResult ExecuteCommand(CommandParam commandParam)
        {
            var biz = commandParam.UiBiz;
            CommandResult result = new CommandResult();
            if (true)
            {
                Command command = biz.ExcuteCommand;
                {
                    if (command != null && command.Instance != null)
                    {
                        result = command.Execute(commandParam);
                        if (result.isBreak)
                        {
                            X.MessageBox.Show(new MessageBoxConfig() { Title = "提示", Message = result.sResult, Icon = MessageBox.Icon.WARNING });
                            return result;
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #region 数据处理
        /// <summary>
        /// 更新查询表结构中的字段名
        /// 避免Ext.Net区分大小写问题
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected DataTable updateDataColumnName(DataTable dt)
        {
            foreach (DataColumn dc in dt.Columns)
            {
                if (ui.UiData.Crud.Primarykey != null
                    && !string.IsNullOrWhiteSpace(ui.UiData.Crud.Primarykey.FieldName)
                  && (dc.ColumnName.Equals(ui.UiData.Crud.Primarykey.FieldName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    dc.ColumnName = ui.UiData.Crud.Primarykey.FieldName;
                }
                if (dc.ColumnName.Equals(ui.UiData.Crud.DeleteFlag, StringComparison.CurrentCultureIgnoreCase))
                {
                    dc.ColumnName = ui.UiData.Crud.DeleteFlag;
                }
            }
            return dt;
        }
        /// <summary>
        /// 转化表中第一行数据到Dic中
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected Dictionary<string, string> DataTableToDic(DataTable dt)
        {
            dt = updateDataColumnName(dt);
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (System.Data.DataColumn column in dt.Columns)
                {
                    result.Add(column.ColumnName, dt.Rows[0][column.ColumnName].ToString());
                }
            }
            return result;
        }

        protected Dictionary<string, object> getDetailParam(string ss)
        {
            Dictionary<string, object> result = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            if (string.IsNullOrWhiteSpace(ss))
            {
                return result;
            }
            Newtonsoft.Json.JsonReader reader = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(ss));
            while (reader.Read())
            {
                if (reader.ValueType == null || reader.Value == null)
                {
                    continue;
                }
                if (reader.TokenType != Newtonsoft.Json.JsonToken.PropertyName)
                {
                    result.Add(reader.Path, reader.Value);
                }
            }
            return result;
        }
        #endregion
    }
}
