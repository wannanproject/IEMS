using System;
using System.Data;

namespace IEMS.Frame.McUI.DefaultCommand
{
    using MSTL.LogAgent;

    /// <summary>
    /// 操作基类
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        #region 系统日志 log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion

        /// <summary>
        /// 操作参数
        /// </summary>
        public class BaseCommandParam : CommandParam
        {
            public BaseCommandParam(CommandParam param)
            {
                this.DbHelper = param.DbHelper;
                this.UiBiz = param.UiBiz;
                this.UiParam = param.UiParam;
                this.SysParam = param.SysParam;
            }
            public IDbConnection DbConnection { get; set; }
            public string ParameterPrefix { get; set; }
        }
        /// <summary>
        /// 基本执行操作
        /// </summary>
        /// <param name="commandParam"></param>
        /// <returns></returns>
        protected abstract CommandResult BaseExecute(BaseCommandParam commandParam);

        private void IniUiParam(CommandParam commandParam)
        {
            var dic = commandParam.UiParam;
            string key = string.Empty;
            object value = null;
            #region DataValues
            foreach (var dvalue in commandParam.UiBiz.DataValues)
            {
                key = dvalue.FieldName;
                if (dic.ContainsKey(key))
                {
                    continue;
                }
                value = dvalue.DefaultValue;
                if (dic.ContainsKey(dvalue.Datasource))
                {
                    object source = null;
                    if (dic.TryGetValue(dvalue.Datasource, out source))
                    {
                        value = source;
                    }
                }
                if (value != null)
                {
                    dic.Add(key, value);
                }
            }
            #endregion

            #region 用户信息
            key = commandParam.UiBiz.UserField;
            if ((!string.IsNullOrWhiteSpace(key))
                && (!dic.ContainsKey(key)))
            {
                value = commandParam.SysParam.UserId.Value;
                if (value != null)
                {
                    dic.Add(key, value);
                }
            }
            #endregion
            #region 时间信息
            key = commandParam.UiBiz.TimeField;
            if ((!string.IsNullOrWhiteSpace(key))
                && (!dic.ContainsKey(key)))
            {
                value = DateTime.Now;
                    dic.Add(key, value);
            }
            #endregion
        }
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="commandParam"></param>
        /// <returns></returns>
        public CommandResult Execute(CommandParam commandParam)
        {
            var dbHelper = commandParam.DbHelper;
            IniUiParam(commandParam);
            BaseCommandParam baseCommandParam = new BaseCommandParam(commandParam);
            CommandResult result = new CommandResult();
            MyBatis.DataMapper.Session.ISession session = null;
            IDbConnection conn = null;
            try
            {
                session = dbHelper.SessionFactory.OpenSession();
                session.OpenConnection();
                conn = dbHelper.CurrentSession.Connection;
                baseCommandParam.DbConnection = conn;
                baseCommandParam.ParameterPrefix = dbHelper.SessionFactory.DataSource.DbProvider.ParameterPrefix;
                result = BaseExecute(baseCommandParam);
            }
            catch (Exception ex)
            {
                log.Error("执行" + this.GetType().FullName + "错误=" + ex.ToString());
                result.iResult = -1;
                result.Exception = ex;
                return result;
            }
            finally
            {
                conn.Close();
                session.Close();
                session.Dispose();
            }
            return result;
        }

    }
}
