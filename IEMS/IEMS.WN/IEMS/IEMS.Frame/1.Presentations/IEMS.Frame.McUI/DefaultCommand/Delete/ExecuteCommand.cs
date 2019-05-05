using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.Frame.McUI.DefaultCommand.Delete
{
    using MSTL.LogAgent;

    /// <summary>
    /// 删除 操作
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

        protected override CommandResult BaseExecute(BaseCommandParam commandParam)
        {
            CommandResult result = new CommandResult();

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
            StringBuilder sqlsb = new StringBuilder();
            #endregion

            #region 操作数据整理
            #region 逻辑删除
            if (!string.IsNullOrWhiteSpace(sysParam.DeleteFlag.Key))
            {
                sqlsb.Append("update " + tableName + " set ");
                sqlsb.Append(sysParam.DeleteFlag.Key).Append(" = 1 ");
                #region 用户信息
                string key = commandParam.UiBiz.UserField;
                object value = null;
                if (!string.IsNullOrWhiteSpace(key))
                {
                    string dbkey = prefix + key;
                    if (value != null)
                    {
                        sqlsb.Append(" , ");
                        sqlsb.Append("").Append(key).Append(" = ").Append(dbkey);
                        IDbDataParameter dbparam = command.CreateParameter();
                        dbparam.ParameterName = dbkey;
                        dbparam.Value = value;
                        command.Parameters.Add(dbparam);
                    }
                }
                #endregion
                #region 时间信息
                key = commandParam.UiBiz.TimeField;
                if (!string.IsNullOrWhiteSpace(uiHelper.Delete.TimeField))
                {
                    value = DateTime.Now;
                    string dbkey = prefix + key;
                    if (value != null)
                    {
                        sqlsb.Append(" , ");
                        sqlsb.Append("").Append(key).Append(" = ").Append(dbkey);
                        IDbDataParameter dbparam = command.CreateParameter();
                        dbparam.ParameterName = dbkey;
                        dbparam.Value = value;
                        command.Parameters.Add(dbparam);
                    }
                }
                #endregion
            }
            #endregion
            #region 物理删除
            else
            {
                sqlsb.Append("delete from " + tableName + " ");
            }
            if (true)
            {
                string key = sysParam.Primarykey.Key;
                object value = sysParam.Primarykey.Value;
                string dbkey = prefix + key;

                sqlsb.Append(" where ");
                sqlsb.Append("").Append(key).Append(" = ").Append(dbkey);
                IDbDataParameter dbparam = command.CreateParameter();
                dbparam.ParameterName = dbkey;
                dbparam.Value = value;
                command.Parameters.Add(dbparam);
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
