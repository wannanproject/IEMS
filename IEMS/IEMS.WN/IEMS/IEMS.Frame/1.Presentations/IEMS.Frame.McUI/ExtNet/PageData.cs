using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace IEMS.Frame.McUI.ExtNet
{
    using Ext.Net;
    using System.Collections;

    /// <summary>
    /// 界面数据操作
    /// </summary>
    public class PageData
    {
        private UiHelper uiHelper = null;
        public PageData(UiHelper ui)
        {
            this.uiHelper = ui;
        }

        #region 获取界面数据 GetFieldValue
        private string TrimValue(string value)
        {
            return value.Replace("<br>", string.Empty);
        }
        private string getJsonParamFieldValue(string ss)
        {
            string value = System.Text.RegularExpressions.Regex.Unescape(ss);
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return string.Empty;
                }
                value = TrimValue(value);
                Newtonsoft.Json.Linq.JArray array = (Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(value);
                foreach (Newtonsoft.Json.Linq.JObject obj in array)
                {
                    value = obj["value"].ToString();
                    break;
                }
            }
            catch { }
            return value;
        }
        private object getValue(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            object result = value;
            ParamField field = null;
            if (this.uiHelper.AllParamField.TryGetValue(key, out field))
            {
                #region ParamFieldType.DateBox
                if (field.Type == ParamFieldType.DateBox)
                {
                    DateTime dtResult = new DateTime();
                    if (DateTime.TryParse(value, out dtResult))
                    {
                        return dtResult;
                    }
                    else
                    {
                        return null;
                    }
                }
                #endregion

                #region ParamField.IsMult
                if (field.IsMult)
                {
                    if (field.Type == ParamFieldType.TextBox)
                    {
                        result = result.ToString().Replace("\r\n", "<br />").Replace("\r", "<br />").Replace("\n", "<br />");
                    }
                }
                #endregion
            }
            return result;
        }
        private Hashtable getBeginEndDateBoxParamFieldValue(string key, object value)
        {
            if (value == null)
            {
                return null;
            }
            string ssValue = value.ToString();
            DateTime dateValue = DateTime.Now;
            if (!DateTime.TryParse(ssValue, out dateValue))
            {
                return null;
            }
            string paramKey = string.Empty;
            paramKey = key.Substring(0, key.Length - UiHelper.DATE_STATE_END.Length);
            if (string.IsNullOrWhiteSpace(paramKey))
            {
                return null;
            }
            var result = new Hashtable();
            result["key"] = paramKey;
            result["value"] = dateValue.Date;
            return result;
        }

        private Dictionary<string, object> resetDateTimeBoxParamFieldValue(Dictionary<string, object> value)
        {
            var result = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<string, object> keyvalue in value)
            {
                result.Add(keyvalue.Key, keyvalue.Value);
            }
            foreach (KeyValuePair<string, object> keyvalue in value)
            {
                string key = keyvalue.Key;
                object obj = keyvalue.Value;
                if (obj == null)
                {
                    continue;
                }
                string ssValue = obj.ToString();
                DateTime dateValue = DateTime.Now;
                if (!DateTime.TryParse(ssValue, out dateValue))
                {
                    continue;
                }
                string paramKey = string.Empty;
                if (key.ToLower().EndsWith(UiHelper.DATE_DATETIME_STATE_END.ToLower()))
                {
                    paramKey = key.Substring(0, key.Length - UiHelper.DATE_DATETIME_STATE_END.Length);
                }
                if (key.ToLower().EndsWith(UiHelper.TIME_DATETIME_STATE_END.ToLower()))
                {
                    paramKey = key.Substring(0, key.Length - UiHelper.TIME_DATETIME_STATE_END.Length);
                }
                if (string.IsNullOrWhiteSpace(paramKey))
                {
                    continue;
                }
                if (!result.ContainsKey(paramKey))
                {
                    result.Add(paramKey, DateTime.Now.Date);
                }
                if (key.ToLower().EndsWith(UiHelper.DATE_DATETIME_STATE_END.ToLower()))
                {
                    DateTime paramValue = (DateTime)result[paramKey];
                    paramValue = Convert.ToDateTime(dateValue.ToString("yyyy-MM-dd") + " " + paramValue.ToString("HH:mm:ss"));
                    result[paramKey] = paramValue;
                }
                if (key.ToLower().EndsWith(UiHelper.TIME_DATETIME_STATE_END.ToLower()))
                {
                    DateTime paramValue = (DateTime)result[paramKey];
                    paramValue = Convert.ToDateTime(paramValue.ToString("yyyy-MM-dd") + " " + dateValue.ToString("HH:mm:ss"));
                    result[paramKey] = paramValue;
                }
            }
            return result;
        }

        private Dictionary<string, object> resetBeginEndDateBoxParamFieldValue(Dictionary<string, object> value)
        {
            var result = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<string, object> keyvalue in value)
            {
                result.Add(keyvalue.Key, keyvalue.Value);
            }
            foreach (KeyValuePair<string, object> keyvalue in value)
            {
                string key = keyvalue.Key;
                object obj = keyvalue.Value;
                if (obj == null)
                {
                    continue;
                }
                if ((!key.ToLower().EndsWith(UiHelper.BEGIN_DATE_STATE_END.ToLower()))
                 && (!key.ToLower().EndsWith(UiHelper.END_DATE_STATE_END.ToLower())))
                {
                    continue;
                }
                var kv = getBeginEndDateBoxParamFieldValue(key, obj);
                if (kv == null)
                {
                    continue;
                }
                var paramKey = kv["key"].ToString();
                var paramValue = (DateTime)kv["value"];
                if (!result.ContainsKey(paramKey))
                {
                    result.Add(paramKey, paramValue);
                }
                result[paramKey] = paramValue;
            }
            return result;
        }

        private Dictionary<string, object> resetBeginEndTextBoxParamFieldValue(Dictionary<string, object> value)
        {
            var result = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<string, object> keyvalue in value)
            {
                result.Add(keyvalue.Key, keyvalue.Value);
            }
            foreach (KeyValuePair<string, object> keyvalue in value)
            {
                string key = keyvalue.Key;
                object obj = keyvalue.Value;
                if (obj == null)
                {
                    continue;
                }
                if ((!key.ToLower().EndsWith(UiHelper.BEGIN_TEXT_STATE_END.ToLower()))
                && (!key.ToLower().EndsWith(UiHelper.END_TEXT_STATE_END.ToLower())))
                {
                    continue;
                }
                var paramKey = key.Substring(0, key.Length - UiHelper.TEXT_STATE_END.Length);
                if (!result.ContainsKey(paramKey))
                {
                    result.Add(paramKey, obj.ToString());
                }
                result[paramKey] = obj.ToString();
            }
            return result;
        }

        private Dictionary<string, object> GetParamFieldValue(NameValueCollection form, string paramField_Start)
        {
            Dictionary<string, object> result = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            foreach (string key in form.AllKeys)
            {
                if (key.StartsWith(paramField_Start))
                {
                    string value = form[key];
                    value = TrimValue(value);
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        if (!result.ContainsKey(key))
                        {
                            result.Add(key.Substring(paramField_Start.Length), getValue(key, value));
                        }
                    }
                }
                if (key.StartsWith("_" + paramField_Start) && key.EndsWith("_state"))
                {
                    string value = form[key];
                    value = getJsonParamFieldValue(value);
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        string ssKey = key.Substring(paramField_Start.Length + 1);
                        ssKey = ssKey.Substring(0, ssKey.Length - "_state".Length);
                        if (result.ContainsKey(ssKey))
                        {
                            result[ssKey] = getValue(key, value);
                        }
                        else
                        {
                            result.Add(ssKey, getValue(key, value));
                        }
                    }
                }
            }
            result = resetDateTimeBoxParamFieldValue(result);
            result = resetBeginEndDateBoxParamFieldValue(result);
            result = resetBeginEndTextBoxParamFieldValue(result);
            return result;
        }
        /// <summary>
        /// 获取界面数据 添加
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetInsertParamFieldValue(NameValueCollection form)
        {
            return GetParamFieldValue(form, UiControlNamePrefix.InsertParam);
        }
        /// <summary>
        /// 获取界面数据 修改
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetUpdateParamFieldValue(NameValueCollection form)
        {
            return GetParamFieldValue(form, UiControlNamePrefix.UpdateParam);
        }
        /// <summary>
        /// 获取界面数据 查询
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetSelectParamFieldValue(NameValueCollection form)
        {
            return GetParamFieldValue(form, UiControlNamePrefix.SelectParam);
        }
        /// <summary>
        /// 获取URL数据 查询
        /// </summary>
        /// <param name="paramFieldValue"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetSelectParamFieldValue(Dictionary<string, object> paramFieldValue, NameValueCollection queryString)
        {
            foreach (string name in queryString.AllKeys)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }
                string value = queryString[name];
                bool isExeits = false;
                string key = string.Empty;
                foreach (KeyValuePair<string, object> keyvalue in paramFieldValue)
                {
                    if (keyvalue.Key.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        key = keyvalue.Key;
                        paramFieldValue[key] = value;
                        isExeits = true;
                        break;
                    }
                }
                if (!isExeits)
                {
                    key = name;
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        paramFieldValue.Add(key, value);
                    }
                }
            }
            return paramFieldValue;
        }
        /// <summary>
        /// 更新参数
        /// </summary>
        /// <param name="paramFieldValue"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Dictionary<string, object> UpdateSelectParamFieldValue(Dictionary<string, object> paramFieldValue, string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return paramFieldValue;
            }
            if (paramFieldValue.ContainsKey(key))
            {
                paramFieldValue[key] = value;
            }
            else
            {
                paramFieldValue.Add(key, value);
            }
            return paramFieldValue;
        }
        #endregion

        #region 更新界面信息 RefreshData
        #region IniParamFieldData
        private string getParamFieldDataValue(string key, Dictionary<string, string> values)
        {
            string result = string.Empty;
            if (values == null)
            {
                return result;
            }
            values.TryGetValue(key, out result);
            return result;
        }
        private void RefreshParamFieldData(string paramField_Start, ParamField field, Dictionary<string, string> values)
        {
            StringBuilder js = new StringBuilder();
            string value = getParamFieldDataValue(field.FieldName, values);
            switch (field.Type)
            {
                case ParamFieldType.TextBox:
                    {
                        if (!field.IsMult)
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + ".setValue(\"" + value + "\");");
                        }
                        else
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + ".setValue(\"" + value.Replace("\r", "\\r").Replace("\n", "\\n") + "\");");
                        }
                        break;
                    }
                case ParamFieldType.ComboBox:
                    {
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + ".setValue(\"" + value + "\");");
                        }
                        else if (field.Nullable)
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + ".setValue(\"\");");
                        }
                        break;
                    }
                case ParamFieldType.SearchBox:
                    {
                        js.Append("App.").Append(paramField_Start + field.FieldName + ".setValue(\"" + value + "\");");
                        value = getParamFieldDataValue(field.FieldName + "_TriggerField", values);
                        js.Append("App.").Append(paramField_Start + field.FieldName + "_TriggerField.setValue(\"" + value + "\");");
                        break;
                    }
                case ParamFieldType.DateBox:
                    {
                        DateTime dtValue = DateTime.Now;
                        if (DateTime.TryParse(value, out dtValue))
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + ".setValue(\"" + dtValue.ToString("yyyy-MM-dd") + "\");");
                        }
                        else
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + ".setValue(\"\");");
                        }
                        break;
                    }
                case ParamFieldType.DateTimeBox:
                    {
                        DateTime dtValue = DateTime.Now;
                        if (DateTime.TryParse(value, out dtValue))
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + UiHelper.DATE_DATETIME_STATE_END + ".setValue(\"" + dtValue.ToString("yyyy-MM-dd") + "\");");
                            js.Append("App.").Append(paramField_Start + field.FieldName + UiHelper.TIME_DATETIME_STATE_END + ".setValue(\"" + dtValue.ToString("HH:mm:ss") + "\");");
                        }
                        else
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + UiHelper.DATE_DATETIME_STATE_END + ".setValue(\"\");");
                            js.Append("App.").Append(paramField_Start + field.FieldName + UiHelper.TIME_DATETIME_STATE_END + ".setValue(\"\");");
                        }
                        break;
                    }
                case ParamFieldType.BeginEndDateBox:
                    {
                        var strValue = getParamFieldDataValue(field.FieldName + UiHelper.BEGIN_DATE_STATE_END, values);
                        DateTime dtValue = DateTime.Now;
                        if (DateTime.TryParse(strValue, out dtValue))
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + UiHelper.BEGIN_DATE_STATE_END + ".setValue(\"" + dtValue.ToString("yyyy-MM-dd") + "\");");
                        }
                        else
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + UiHelper.BEGIN_DATE_STATE_END + ".setValue(\"\");");
                        }
                        strValue = getParamFieldDataValue(field.FieldName + UiHelper.END_DATE_STATE_END, values);
                        dtValue = DateTime.Now;
                        if (DateTime.TryParse(strValue, out dtValue))
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + UiHelper.END_DATE_STATE_END + ".setValue(\"" + dtValue.ToString("yyyy-MM-dd") + "\");");
                        }
                        else
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + UiHelper.END_DATE_STATE_END + ".setValue(\"\");");
                        }
                        break;
                    }
                case ParamFieldType.BeginEndTextBox:
                    {
                        var strValue = getParamFieldDataValue(field.FieldName + UiHelper.BEGIN_TEXT_STATE_END, values);
                        if (string.IsNullOrWhiteSpace(strValue))
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + UiHelper.BEGIN_TEXT_STATE_END + ".setValue(\"\");");
                        }
                        else
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + UiHelper.BEGIN_TEXT_STATE_END + ".setValue(\"" + strValue + "\");");
                        }
                        strValue = getParamFieldDataValue(field.FieldName + UiHelper.END_TEXT_STATE_END, values);
                        if (string.IsNullOrWhiteSpace(strValue))
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + UiHelper.END_TEXT_STATE_END + ".setValue(\"\");");
                        }
                        else
                        {
                            js.Append("App.").Append(paramField_Start + field.FieldName + UiHelper.END_TEXT_STATE_END + ".setValue(\"" + strValue + "\");");
                        }
                        break;
                    }
                default:
                    {
                        js.Append("App.").Append(paramField_Start + field.FieldName + ".setValue(\"" + value + "\");");
                        break;
                    }
            }
            X.Js.AddScript(js.ToString());
        }
        #endregion
        /// <summary>
        /// 更新界面信息 添加
        /// </summary>
        /// <param name="values"></param>
        public void RefreshInsertParamData(Dictionary<string, string> values)
        {
            if (values == null)
            {
                var defaultValue = new Dictionary<string, string>();
                foreach (var field in uiHelper.Insert.Panel.ParamFields)
                {
                    if (!string.IsNullOrWhiteSpace(field.DefaultValue))
                    {
                        defaultValue.Add(field.FieldName, field.DefaultValue);
                    }
                }
                if (defaultValue.Count > 0)
                {
                    values = defaultValue;
                }
            }
            foreach (ParamField field in uiHelper.Insert.Panel.ParamFields)
            {
                RefreshParamFieldData(UiControlNamePrefix.InsertParam, field, values);
            }
        }
        /// <summary>
        /// 更新界面信息 修改
        /// </summary>
        /// <param name="values"></param>
        public void RefreshUpdateParamData(Dictionary<string, string> values)
        {
            foreach (ParamField field in uiHelper.Update.Panel.ParamFields)
            {
                RefreshParamFieldData(UiControlNamePrefix.UpdateParam, field, values);
            }
        }
        /// <summary>
        /// 更新界面信息 修改 主键
        /// </summary>
        /// <param name="primaryKeyValue"></param>
        public void RefreshUpdatePrimarykeyData(Dictionary<string, object> primaryKeyValue)
        {
            StringBuilder js = new StringBuilder();
            foreach (KeyValuePair<string, object> primary in primaryKeyValue)
            {
                string key = "_" + UiControlNamePrefix.UpdateParam + primary.Key + "_state";
                if (this.uiHelper.AllParamField.ContainsKey(key))
                {
                    js.Append("App._").Append(UiControlNamePrefix.UpdateParam).Append(primary.Key);
                    js.Append("_state").Append(".setValue(\"" + primary.Value.ToString() + "\");");
                }
            }
            X.Js.AddScript(js.ToString());
        }
        /// <summary>
        /// 更新界面信息 查询
        /// </summary>
        /// <param name="values"></param>
        public void RefreshSelectParamData(Dictionary<string, string> values)
        {
            foreach (ParamField field in uiHelper.Select.ParamPanel.ParamFields)
            {
                RefreshParamFieldData(UiControlNamePrefix.SelectParam, field, values);
            }
        }
        /// <summary>
        /// 更新界面信息 查询 明细
        /// </summary>
        /// <param name="values"></param>
        public void RefreshSelectMainDetailData(Dictionary<string, string> values)
        {
            foreach (ParamField field in uiHelper.Select.MainDetail.ParamFields)
            {
                RefreshParamFieldData(UiControlNamePrefix.SelectMainDetail, field, values);
            }
        }
        #endregion

    }
}
