using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Frame.McUI.DefaultCommand.Update
{
    using MSTL.LogAgent;

    /// <summary>
    /// 修改 操作
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
        private void addDataParameter(StringBuilder sqlsb,
                                       IDbCommand command, string key, string prefix, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }
            key = key.ToUpper();
            if (value == null)
            {
                value = DBNull.Value;
            }
            string dbkey = prefix + key;
            IDbDataParameter dbparam = command.CreateParameter();
            dbparam.ParameterName = dbkey;
            dbparam.Value = value;
            command.Parameters.Add(dbparam);
            sqlsb.Append(" ").Append(key).Append(" = ").Append(dbkey).Append(",");
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
            command.Parameters.Clear();
            StringBuilder sqlsb = new StringBuilder("update " + tableName + " set ");
            #endregion

            #region 操作数据整理
            if (keyNotInSql(sysParam.DeleteFlag.Key))
            {
                string key = sysParam.DeleteFlag.Key;
                object value = sysParam.DeleteFlag.Value;
                addDataParameter(sqlsb, command, key, prefix, value);
            }
            else
            {
                foreach (ParamField field in uiHelper.Update.Panel.ParamFields)
                {

                    string datasource = string.Empty;
                    if (!string.IsNullOrWhiteSpace(field.Datasource))
                    {
                        datasource = field.Datasource.Trim();
                    }
                    string key = field.FieldName.Trim();
                    string dbkey = prefix + key;
                    object value = null;
                    if (!uiParam.TryGetValue(key, out value))
                    {
                        uiParam.TryGetValue(datasource, out value);
                    }
                    addDataParameter(sqlsb, command, key, prefix, value);
                }
            }

            #region 用户信息
            if (keyNotInSql(commandParam.UiBiz.UserField))
            {
                string key = commandParam.UiBiz.UserField;
                object value = sysParam.UserId.Value;
                addDataParameter(sqlsb, command, key, prefix, value);
            }
            #endregion

            #region 时间信息
            if (keyNotInSql(commandParam.UiBiz.TimeField))
            {
                string key = commandParam.UiBiz.TimeField;
                object value = DateTime.Now;
                addDataParameter(sqlsb, command, key, prefix, value);
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
                    addDataParameter(sqlsb, command, key, prefix, value);
                }
            }
            #endregion

            #region where Primarykey=@value
            if (sqlsb.ToString().Trim().EndsWith(","))
            {
                string sqlstr = sqlsb.ToString().Trim();
                sqlsb.Clear();
                sqlsb.Append(sqlstr.Remove(sqlstr.Length - 1));
            }
            else
            {
                result.sResult = "没有需要修改的信息";
                result.iResult = -1;
                return result;
            }
            if (string.IsNullOrWhiteSpace(sysParam.Primarykey.Key)
                || sysParam.Primarykey.Value == null
                || string.IsNullOrWhiteSpace(sysParam.Primarykey.Value.ToString()))
            {
                result.sResult = "没有主键信息";
                result.iResult = -1;
                return result;
            }
            else
            {
                string key = sysParam.Primarykey.Key.Trim();
                string dbkey = prefix + key;
                object value = sysParam.Primarykey.Value;
                IDbDataParameter dbparam = command.CreateParameter();
                dbparam.ParameterName = dbkey;
                dbparam.Value = value;
                command.Parameters.Add(dbparam);
                sqlsb.Append(" where ").Append("").Append(key).Append("");
                sqlsb.Append(" = ").Append(dbkey);
            }
            #endregion
            #endregion

            #region 执行操作
            command.CommandText = sqlsb.ToString();
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
