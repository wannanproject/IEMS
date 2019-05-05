using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Frame.McUI.DefaultCommand.Insert
{
    using MSTL.LogAgent;

    /// <summary>
    /// 添加 操作
    /// </summary>
    public class ExecuteCommand : BaseCommand
    {
        #region 系统日志  log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion
        /// <summary>
        /// SQL日志
        /// </summary>
        /// <param name="command"></param>
        private void IDbCommandLog(IDbCommand command)
        {
            StringBuilder paraLog = new StringBuilder();
            paraLog.AppendLine(this.GetType().FullName);
            paraLog.AppendLine(command.CommandText).AppendLine("参数：");
            foreach (var para in command.Parameters)
            {
                if (para == null)
                {
                    continue;
                }
                if (para is IDbDataParameter)
                {
                    var dbPara = para as IDbDataParameter;
                    paraLog.Append(dbPara.ParameterName).Append("=").Append(dbPara.Value).AppendLine(string.Empty);
                }
                else
                {
                    paraLog.Append("NotIDbDataParameter").Append("=").Append(para.ToString()).AppendLine(string.Empty);
                }
            }
            log.Debug(paraLog.ToString());
        }

        private List<string> sqlKeys = new List<string>();
        private bool keyNotInSql(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }
            key = key.ToUpper();
            foreach (string sqlKey in sqlKeys)
            {
                if (key.Equals(sqlKey, StringComparison.CurrentCultureIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }
        private void addDataParameter(StringBuilder sqlfield, StringBuilder sqlvalue,
                                       IDbCommand command, string key, string prefix, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }
            key = key.ToUpper();
            if (value == null)
            {
                return;
            }
            string dbkey = string.Empty;
            if (string.IsNullOrWhiteSpace(prefix))
            {
                dbkey = value.ToString() + ".nextval";
            }
            else
            {
                dbkey = prefix + key;
                IDbDataParameter dbparam = command.CreateParameter();
                dbparam.ParameterName = dbkey;
                dbparam.Value = value;
                command.Parameters.Add(dbparam);
            }
            sqlfield.Append("").Append(key).Append(",");
            sqlvalue.Append(dbkey).Append(",");
            sqlKeys.Add(key);
        }
        protected override CommandResult BaseExecute(BaseCommandParam commandParam)
        {
            CommandResult result = new CommandResult();
            sqlKeys = new List<string>();
            #region 系统数据
            var biz = commandParam.UiBiz;
            var uiHelper = commandParam.UiHelper;
            var uiParam = commandParam.UiParam;
            var sysParam = commandParam.SysParam;
            var conn = commandParam.DbConnection;
            var prefix = commandParam.ParameterPrefix;
            #endregion

            #region 初始化操作
            string tableName = uiHelper.UiData.Crud.TableName;
            IDbCommand command = conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.Parameters.Clear();
            StringBuilder sqlfield = new StringBuilder("INSERT INTO  " + tableName + " (");
            StringBuilder sqlvalue = new StringBuilder(") VALUES (");
            #endregion

            #region 数据整理
            #region 操作数据整理
            foreach (ParamField field in uiHelper.Insert.Panel.ParamFields)
            {
                string key = field.FieldName.Trim();
                string datasource = string.Empty;
                if (!string.IsNullOrWhiteSpace(field.Datasource))
                {
                    datasource = field.Datasource.Trim();
                }
                object value = null;
                if (!uiParam.TryGetValue(key, out value))
                {
                    uiParam.TryGetValue(datasource, out value);
                }
                addDataParameter(sqlfield, sqlvalue, command, key, prefix, value);
            }
            #endregion

            #region 序列号字段
            foreach (SeqField field in uiHelper.UiData.Crud.SeqFields)
            {
                string key = field.FieldName;
                string value = field.SeqName;
                if ((!string.IsNullOrWhiteSpace(key)) && (!string.IsNullOrWhiteSpace(value)))
                {
                    addDataParameter(sqlfield, sqlvalue, command, key, string.Empty, value);
                }
            }
            #endregion

            #region 主键
            if (!uiHelper.UiData.Crud.Primarykey.Identity)
            {
                string key = uiHelper.UiData.Crud.Primarykey.FieldName;
                if (!uiParam.ContainsKey(key) && !sqlKeys.Contains(key)) // 判断是否已在使用序列中添加 qusf 20150130
                {
                    string sqlstr = "select nvl(max(" + key + "),0) from " + tableName + "";
                    command.CommandText = sqlstr;
                    object value = command.ExecuteScalar();
                    if (value == null || value.ToString() == string.Empty)
                    {
                        value = 0;
                    }
                    value = Convert.ToInt64(value.ToString()) + 1;
                    addDataParameter(sqlfield, sqlvalue, command, key, prefix, value);
                }
            }
            #endregion

            #region 用户信息
            if (keyNotInSql(commandParam.UiBiz.UserField))
            {
                string key = commandParam.UiBiz.UserField;
                object value = sysParam.UserId.Value;
                addDataParameter(sqlfield, sqlvalue, command, key, prefix, value);
            }
            #endregion
            #region 时间信息
            if (keyNotInSql(commandParam.UiBiz.TimeField))
            {
                string key = commandParam.UiBiz.TimeField;
                object value = DateTime.Now;
                string dbkey = prefix + key;
                addDataParameter(sqlfield, sqlvalue, command, key, prefix, value);
            }
            #endregion
            #region 删除标示
            if (keyNotInSql(sysParam.DeleteFlag.Key))
            {
                string key = sysParam.DeleteFlag.Key;
                object value = sysParam.DeleteFlag.Value;
                addDataParameter(sqlfield, sqlvalue, command, key, prefix, value);
            }
            #endregion

            #region DataValues
            foreach (var dvalue in commandParam.UiBiz.DataValues)
            {
                string key = dvalue.FieldName;
                object value = null;
                if ((keyNotInSql(dvalue.FieldName))
                    && commandParam.UiParam.TryGetValue(key, out value))
                {
                    addDataParameter(sqlfield, sqlvalue, command, key, prefix, value);
                }
            }
            #endregion
            sqlfield = sqlfield.Remove(sqlfield.Length - 1, 1);
            sqlvalue = sqlvalue.Remove(sqlvalue.Length - 1, 1);
            sqlvalue = sqlvalue.Append(")");
            #endregion

            #region 执行操作
            command.CommandText = sqlfield.ToString() + sqlvalue.ToString();
            command.CommandType = CommandType.Text;
            IDbCommandLog(command);
            int obj = Convert.ToInt32(command.ExecuteScalar());
            if (obj > 0)
            {
                result.iResult = -1;
                return result;
            }
            #endregion

            return result;
        }
    }
}
