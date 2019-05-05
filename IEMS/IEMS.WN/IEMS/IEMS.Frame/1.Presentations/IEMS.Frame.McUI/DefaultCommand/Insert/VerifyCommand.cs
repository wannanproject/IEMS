using System;
using System.Data;
using System.Text;

namespace IEMS.Frame.McUI.DefaultCommand.Insert
{
    /// <summary>
    /// 添加 校验
    /// </summary>
    public class VerifyCommand : BaseCommand
    {
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
            StringBuilder sResult = new StringBuilder();
            string br = "<br>";
            #endregion

            #region 验证必填项
            foreach (ParamField parField in uiHelper.Insert.Panel.ParamFields)
            {
                bool isNull = parField.Nullable;
                string key = parField.FieldName;
                string caption = uiHelper.UiData.GetCaption(parField.Caption, parField.FieldName).Value;
                if (!isNull)
                {
                    object value = null;
                    uiParam.TryGetValue(key, out value);
                    if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        sResult.Append(caption).Append("为空，请添加！").Append(br).ToString();
                    }
                }
            }
            result.sResult = sResult.ToString();
            if (result.sResult != null && !string.IsNullOrWhiteSpace(result.sResult))
            {
                result.iResult = -1;
                result.sResult = result.sResult.Remove(result.sResult.Length - 1, 1);
                return result;
            }
            #endregion

            #region 检验唯一项
            foreach (Unique unique in uiHelper.UiData.Crud.Uniques)
            {
                command.Parameters.Clear();
                StringBuilder sqlstr = new StringBuilder("select count(*) from " + tableName + " where 1=1");
                foreach (string field in unique.FieldList)
                {
                    string key = field.Trim();
                    sResult.Append(uiHelper.UiData.GetCaption(string.Empty, field).Value).Append(" ");
                    string dbkey = prefix + key;
                    object value = null;
                    uiParam.TryGetValue(key, out value);
                    if (value != null)
                    {
                        IDbDataParameter dbparam = command.CreateParameter();
                        dbparam.ParameterName = dbkey;
                        dbparam.Value = value;
                        command.Parameters.Add(dbparam);
                        sqlstr.Append(" and ").Append(key).Append(" = ").Append(dbkey);
                    }
                }
                command.CommandText = sqlstr.ToString();
                command.CommandType = CommandType.Text;
                int obj = Convert.ToInt32(command.ExecuteScalar());
                if (obj > 0)
                {
                    result.iResult = -1;
                    result.sResult = sResult.Append(" 值重复，数据添加失败！").ToString();
                    return result;
                }
            }
            #endregion

            return result;
        }
    }
}
