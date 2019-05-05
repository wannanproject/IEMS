using System;
using System.Data;
using System.Web.UI.HtmlControls;

namespace IEMS.Frame.McUI.ExtNet
{
    using Ext.Net;
    using IEMS.Frame.AppBiz;
    using System.Collections.Generic;

    /// <summary>
    /// 界面初始化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageIni
    {
        private UiHelper uiHelper = null;
        private PageData pageData = null;
        private IMcUIManager manager = null;
        public PageIni(UiHelper ui, IMcUIManager m)
        {
            this.uiHelper = ui;
            this.manager = m;
            this.pageData = new PageData(ui);
        }

        #region 初始化参数控件 IniParamField
        private void IniAllParamField(string fieldName, ParamField field)
        {
            var dic = uiHelper.AllParamField;
            if (dic.ContainsKey(fieldName))
            {
                return;
            }
            dic.Add(fieldName, field);
        }
        /// <summary>
        /// 初始化TestBox控件  TextField
        /// </summary>
        /// <param name="paramField_Start"></param>
        /// <param name="field"></param>
        /// <param name="isReadonly"></param>
        /// <returns></returns>
        private Field IniParamTextBox(string paramField_Start, ParamField field, bool isReadonly)
        {
            if (field.Type != ParamFieldType.TextBox)
            {
                return null;
            }
            TextFieldBase result = null;
            if (!field.IsMult)
            {
                result = new TextField();
            }
            else
            {
                // 多行文本框
                result = new TextArea();
            }

            if (!string.IsNullOrWhiteSpace(field.DefaultValue))
            {
                result.Text = field.DefaultValue;
            }

            return result;
        }
        /// <summary>
        /// 初始化ComboBox控件  ComboBox
        /// </summary>
        /// <param name="paramField_Start"></param>
        /// <param name="field"></param>
        /// <param name="isReadonly"></param>
        /// <returns></returns>
        private Field IniParamComboBox(string paramField_Start, ParamField field, bool isReadonly)
        {
            if (field.Type != ParamFieldType.ComboBox)
            {
                return null;
            }
            ComboBox result = new ComboBox();
            result.Editable = false;
            DataTable dt = manager.GetComboBoxData(uiHelper.Name, field.FieldName);
            // .GetDataTableByStatement("GetComboBoxData@Select@" + uiHelper.Name + "@" + field.FieldName, null);
            foreach (DataRow row in dt.Rows)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = row["ssValue"].ToString();
                item.Value = row["ssKey"].ToString();
                result.Items.Add(item);
            }
            if ((!field.Nullable) && (result.Items.Count > 0))
            {
                result.Value = result.Items[0].Value;
            }
            if (field.Nullable)
            {
                result.Triggers.Add(new FieldTrigger() { Icon = TriggerIcon.Clear, HideTrigger = false });
                result.Listeners.TriggerClick.Handler = "this.clearValue();";
            }
            return result;
        }
        /// <summary>
        /// 初始化SearchBox控件  TriggerField
        /// </summary>
        /// <param name="paramField_Start"></param>
        /// <param name="field"></param>
        /// <param name="isReadonly"></param>
        /// <returns></returns>
        private Field IniParamSearchBox(string paramField_Start, ParamField field, bool isReadonly)
        {
            if (field.Type != ParamFieldType.SearchBox)
            {
                return null;
            }
            TriggerField result = new TriggerField();
            result.ID = paramField_Start + field.FieldName + "_TriggerField";
            result.Name = paramField_Start + field.FieldName + "_TriggerField";
            result.Editable = false;
            result.Triggers.Add(new FieldTrigger() { Icon = TriggerIcon.Clear, HideTrigger = true });
            result.Triggers.Add(new FieldTrigger() { Icon = TriggerIcon.Search, HideTrigger = false });
            result.Listeners.TriggerClick.Fn = "get_" + field.FieldName + "_TriggerField";

            if (field.Nullable)
            {
                result.Triggers[0].HideTrigger = false;
            }

            return result;
        }
        /// <summary>
        /// 初始化Hidden控件  Hidden
        /// </summary>
        /// <param name="paramField_Start"></param>
        /// <param name="field"></param>
        /// <param name="isReadonly"></param>
        /// <returns></returns>
        private Field IniParamHidden(string paramField_Start, ParamField field, bool isReadonly)
        {
            if ((field.Type != ParamFieldType.SearchBox) && (field.IsShow))
            {
                return null;
            }
            Hidden result = new Hidden();
            if (!string.IsNullOrWhiteSpace(field.DefaultValue))
            {
                result.Text = field.DefaultValue;
            }
            return result;
        }
        /// <summary>
        /// 初始化NumberBox控件  NumberField
        /// </summary>
        /// <param name="paramField_Start"></param>
        /// <param name="field"></param>
        /// <param name="isReadonly"></param>
        /// <returns></returns>
        private Field IniParamNumberBox(string paramField_Start, ParamField field, bool isReadonly)
        {
            if (field.Type != ParamFieldType.NumberBox)
            {
                return null;
            }
            NumberField result = new NumberField();
            if (!string.IsNullOrWhiteSpace(field.DefaultValue))
            {
                int _int = 0;
                if (int.TryParse(field.DefaultValue, out _int))
                {
                    result.Text = _int.ToString();
                }
            }
            return result;
        }

        private DateTime? getDefaultDateTime(string defaultValue)
        {
            var result = DateTime.Now;
            var defaultAddDays = 0;
            var defaultDateTime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(defaultValue))
            {
                if (int.TryParse(defaultValue, out defaultAddDays))
                {
                    result = result.AddDays(defaultAddDays);
                }
                else if (DateTime.TryParse(defaultValue, out defaultDateTime))
                {
                    result = defaultDateTime;
                }
            }
            if (Math.Abs((result - DateTime.Now).TotalDays) < 10000)
            {
                return result;
            }
            return null;
        }
        /// <summary>
        /// 初始化DateField控件  DateField
        /// </summary>
        /// <param name="paramField_Start"></param>
        /// <param name="field"></param>
        /// <param name="isReadonly"></param>
        /// <returns></returns>
        private Field IniParamDateField(string paramField_Start, ParamField field, bool isReadonly)
        {
            if (field.Type != ParamFieldType.DateBox)
            {
                return null;
            }
            DateField result = new DateField();
            var date = getDefaultDateTime(field.DefaultValue);
            if (date != null)
            {
                result.Text = ((DateTime)date).ToString("yyyy-MM-dd");
            }
            result.Format = "yyyy-MM-dd";
            if (isReadonly || field.ReadOnly)
            {
                result.ReadOnly = true;
            }
            return result;
        }
        /// <summary>
        /// 初始化控件基本信息
        /// </summary>
        /// <param name="field"></param>
        /// <param name="paramField_Start"></param>
        /// <param name="param"></param>
        /// <param name="container"></param>
        /// <param name="isReadonly"></param>
        private void IniField(Field field, string paramField_Start, ParamField param, Container container, bool isReadonly)
        {
            if (field == null)
            {
                return;
            }
            string fieldName = paramField_Start + param.FieldName;
            Caption caption = uiHelper.UiData.GetCaption(param.Caption, param.FieldName);
            if (string.IsNullOrWhiteSpace(field.ID))
            {
                field.ID = fieldName;
            }
            if (string.IsNullOrWhiteSpace(field.Name))
            {
                field.Name = fieldName;
            }
            this.IniAllParamField(fieldName, param);
            field.FieldLabel = caption.Value;
            field.LabelWidth = 80;
            field.LabelAlign = LabelAlign.Right;
            if (isReadonly || param.ReadOnly)
            {
                field.ReadOnly = true;
            }
            container.Add(field);
        }
        /// <summary>
        /// 初始化 DateTimeBox 控件  FieldContainer=DateField+TimeField
        /// </summary>
        /// <param name="paramField_Start"></param>
        /// <param name="field"></param>
        /// <param name="container"></param>
        /// <param name="isReadonly"></param>
        private void IniParamDateTimeBox(string paramField_Start, ParamField field, Container container, bool isReadonly)
        {
            if (field.Type != ParamFieldType.DateTimeBox)
            {
                return;
            }
            Caption caption = uiHelper.UiData.GetCaption(field.Caption, field.FieldName);
            FieldContainer result = new FieldContainer();
            string fieldName = paramField_Start + field.FieldName;
            this.IniAllParamField(fieldName, field);
            result.ID = fieldName;
            result.FieldLabel = caption.Value;
            result.LabelWidth = 80;
            result.LabelAlign = LabelAlign.Right;
            result.Layout = LayoutType.HBox.ToString();
            result.Flex = 1;
            var defaultValue = field.DefaultValue;
            if (string.IsNullOrWhiteSpace(defaultValue))
            {
                defaultValue = DateTime.Now.ToString("yyyy-MM-dd");
            }
            DateTime? date = DateTime.Now;
            var values = defaultValue.Split('|');
            if (values.Length > 0)
            {
                date = getDefaultDateTime(values[0]);
            }
            string dtDate = string.Empty;
            if (date != null)
            {
                dtDate = ((DateTime)date).ToString("yyyy-MM-dd");
            }
            string dtTime = string.Empty;
            if (values.Length > 1)
            {
                dtTime = values[1];
            }
            if (string.IsNullOrWhiteSpace(dtTime))
            {
                dtTime = "08:00:00";
            }
            DateField dateField = new DateField();
            dateField.ID = result.ID + UiHelper.DATE_DATETIME_STATE_END;
            dateField.Format = "yyyy-MM-dd";
            if (!string.IsNullOrWhiteSpace(dtDate))
            {
                dateField.Text = dtDate;
            }
            dateField.Flex = 1;
            TimeField timeField = new TimeField();
            timeField.ID = result.ID + UiHelper.TIME_DATETIME_STATE_END;
            if (!string.IsNullOrWhiteSpace(dtDate))
            {
                timeField.Text = dtTime;
            }
            timeField.Increment = 30;
            timeField.Format = "HH:mm:ss";
            timeField.Flex = 1;
            if (isReadonly || field.ReadOnly)
            {
                dateField.ReadOnly = true;
                timeField.ReadOnly = true;
            }
            result.Items.Add(dateField);
            result.Items.Add(timeField);
            container.Add(result);
        }

        /// <summary>
        /// 初始化 BeginEndDateBox 控件  FieldContainer=DateField+DateField
        /// </summary>
        /// <param name="paramField_Start"></param>
        /// <param name="field"></param>
        /// <param name="container"></param>
        /// <param name="isReadonly"></param>
        private void IniParamBeginEndDateBox(string paramField_Start, ParamField field, Container container, bool isReadonly)
        {
            if (field.Type != ParamFieldType.BeginEndDateBox)
            {
                return;
            }
            Caption caption = uiHelper.UiData.GetCaption(field.Caption, field.FieldName);
            FieldContainer result = new FieldContainer();
            string fieldName = paramField_Start + field.FieldName;
            this.IniAllParamField(fieldName, field);
            result.ID = fieldName;
            result.FieldLabel = caption.Value;
            result.LabelWidth = 80;
            result.LabelAlign = LabelAlign.Right;
            result.Layout = LayoutType.HBox.ToString();
            result.Flex = 1;
            var defaultValue = field.DefaultValue;
            if (string.IsNullOrWhiteSpace(defaultValue))
            {
                defaultValue = string.Empty;
            }
            var values = defaultValue.Split('|');
            var dateFields = new List<DateField>();
            dateFields.Add(new DateField() { ID = result.ID + UiHelper.BEGIN_DATE_STATE_END, FieldLabel = "起" });
            dateFields.Add(new DateField() { ID = result.ID + UiHelper.END_DATE_STATE_END, FieldLabel = "止" });
            for (int i = 0; i < dateFields.Count; i++)
            {
                var dateField = dateFields[i];
                dateField.LabelWidth = 20;
                dateField.LabelSeparator = string.Empty;
                dateField.LabelAlign = LabelAlign.Right;
                dateField.Format = "yyyy-MM-dd";
                dateField.Flex = 1;
                DateTime? dateFieldValue = DateTime.Now;
                if (values.Length > i)
                {
                    dateFieldValue = getDefaultDateTime(values[i]);
                }
                if (dateFieldValue != null)
                {
                    dateField.Text = ((DateTime)dateFieldValue).ToString(dateField.Format);
                }
                if (isReadonly || field.ReadOnly)
                {
                    dateField.ReadOnly = true;
                }
                result.Items.Add(dateField);
            }
            container.Add(result);
        }

        /// <summary>
        /// 初始化 BeginEndTextBox 控件  FieldContainer=TextField+TextField
        /// </summary>
        /// <param name="paramField_Start"></param>
        /// <param name="field"></param>
        /// <param name="container"></param>
        /// <param name="isReadonly"></param>
        private void IniParamBeginEndTextBox(string paramField_Start, ParamField field, Container container, bool isReadonly)
        {
            if (field.Type != ParamFieldType.BeginEndTextBox)
            {
                return;
            }
            Caption caption = uiHelper.UiData.GetCaption(field.Caption, field.FieldName);
            FieldContainer result = new FieldContainer();
            string fieldName = paramField_Start + field.FieldName;
            this.IniAllParamField(fieldName, field);
            result.ID = fieldName;
            result.FieldLabel = caption.Value;
            result.LabelWidth = 80;
            result.LabelAlign = LabelAlign.Right;
            result.Layout = LayoutType.HBox.ToString();
            result.Flex = 1;
            var defaultValue = field.DefaultValue;
            if (string.IsNullOrWhiteSpace(defaultValue))
            {
                defaultValue = string.Empty;
            }
            var values = defaultValue.Split('|');
            var textFields = new List<TextField>();
            textFields.Add(new TextField() { ID = result.ID + UiHelper.BEGIN_TEXT_STATE_END, FieldLabel = "起" });
            textFields.Add(new TextField() { ID = result.ID + UiHelper.END_TEXT_STATE_END, FieldLabel = "止" });
            for (int i = 0; i < textFields.Count; i++)
            {
                var textField = textFields[i];
                textField.LabelWidth = 16;
                textField.LabelSeparator = string.Empty;
                textField.LabelAlign = LabelAlign.Right;
                textField.Flex = 1;
                if ((values.Length > i) && (!string.IsNullOrWhiteSpace(values[i])))
                {
                    textField.Text = values[i];
                }
                if (isReadonly || field.ReadOnly)
                {
                    textField.ReadOnly = true;
                }
                result.Items.Add(textField);
            }
            container.Add(result);
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="field"></param>
        /// <param name="container"></param>
        /// <param name="isReadonly"></param>
        private void IniParamField(string prefix, ParamField field, Container container, bool isReadonly)
        {
            if (!field.IsShow)
            {
                IniField(IniParamHidden(prefix, field, isReadonly),
                     prefix, field, container, isReadonly);
                return;
            }
            IniField(IniParamTextBox(prefix, field, isReadonly),
                prefix, field, container, isReadonly);
            IniField(IniParamComboBox(prefix, field, isReadonly),
                prefix, field, container, isReadonly);
            IniField(IniParamSearchBox(prefix, field, isReadonly),
                prefix, field, container, isReadonly);
            IniField(IniParamHidden(prefix, field, isReadonly),
                prefix, field, container, isReadonly);
            IniField(IniParamNumberBox(prefix, field, isReadonly),
                prefix, field, container, isReadonly);
            IniField(IniParamDateField(prefix, field, isReadonly),
                prefix, field, container, isReadonly);
            IniParamDateTimeBox(prefix, field, container, isReadonly);
            IniParamBeginEndDateBox(prefix, field, container, isReadonly);
            IniParamBeginEndTextBox(prefix, field, container, isReadonly);
        }
        private void IniParamField(string prefix, ParamField field, Container container)
        {
            IniParamField(prefix, field, container, false);
        }
        #endregion

        #region 初始化网格控件 IniGridColumn
        /// <summary>
        /// 构建网格列  DateColumn
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private DateColumn IniGridDateColumn(GridColumn column)
        {
            if (column.ColumnType != GridColumnType.Date)
            {
                return null;
            }
            DateColumn cell = new DateColumn();
            if (string.IsNullOrWhiteSpace(column.ColumnFormt))
            {
                cell.Format = "yyyy-MM-dd HH:mm:ss";
            }
            else
            {
                cell.Format = column.ColumnFormt;
            }
            return cell;
        }
        /// <summary>
        /// 构建网格列  DateColumn
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private CellCommandColumn IniGridColumn(string prefix, GridColumn column)
        {
            Caption caption = uiHelper.UiData.GetCaption(column.Caption, column.ColumnName);
            CellCommandColumn result = null;
            result = IniGridDateColumn(column);
            if (result == null)
            {
                result = new Column();
            }
            result.ID = prefix + column.ColumnName;
            result.DataIndex = column.ColumnName;
            if (column.Locked)
            {
                result.Locked = column.Locked;
            }
            if (column.Flex > 0)
            {
                result.Flex = column.Flex;
            }
            else
            {
                if (column.Width > 0)
                {
                    result.Width = column.Width;
                }
                else
                {
                    result.Flex = 1;
                }
            }
            result.Text = caption.Value;
            return result;
        }
        #endregion

        #region 初始化页面 IniWebPage
        /// <summary>
        /// 初始化页面
        /// </summary>
        /// <param name="web"></param>
        public void IniWebPage(System.Web.UI.Page web)
        {
            web.Title = uiHelper.WebPage.Title;
            for (int i = 0; i < uiHelper.WebPage.JavaScripts.Count; i++)
            {
                string js = uiHelper.WebPage.JavaScripts[i].FileUrl;
                HtmlGenericControl jsscr = new HtmlGenericControl("script");
                jsscr.Attributes.Add("type", "text/javascript");
                jsscr.Attributes.Add("src", web.ResolveClientUrl(js + "?v=" + DateTime.Now.ToString("HHmmss")));
                web.Header.Controls.Add(jsscr);
            }
        }
        #endregion

        #region 初始化添加 IniInsert
        /// <summary>
        /// 初始化添加Window
        /// </summary>
        /// <param name="win"></param>
        private void IniInsertWindow(Window win)
        {
            win.Hidden = true;
            int i = uiHelper.Insert.Panel.Height;
            win.Height = i > 0 ? i : 300;
            i = uiHelper.Insert.Panel.Width;
            win.Width = i > 0 ? i : 300;
            win.Title = uiHelper.WebPage.Title + "--添加";
        }
        /// <summary>
        /// 初始化添加项
        /// </summary>
        /// <param name="panel"></param>
        private void IniInsertPanel(Container panel)
        {
            string prefix = UiControlNamePrefix.InsertParam + "c_";
            int paramFieldsCount = uiHelper.Insert.Panel.ParamFields.Count;
            int columncount = uiHelper.Insert.Panel.ColumnCount;
            if (columncount == 0) { columncount = 1; }
            int rowcount = paramFieldsCount;
            double dColumnWidth = 1.0 / columncount;
            rowcount = rowcount / columncount + 1;
            for (int i = 0; i < columncount; i++)
            {
                Container container = new Container();
                container.ID = prefix + i.ToString();
                container.Layout = "FormLayout";
                container.Padding = 5;
                container.ColumnWidth = dColumnWidth;
                for (int j = 0; j < rowcount; j++)
                {
                    int idx = j * columncount + i;
                    if (idx >= paramFieldsCount)
                    {
                        continue;
                    }
                    ParamField field = uiHelper.Insert.Panel.ParamFields[idx];
                    IniParamField(UiControlNamePrefix.InsertParam, field, container);
                }
                panel.Add(container);
            }
        }
        /// <summary>
        /// 初始化添加
        /// </summary>
        /// <param name="win"></param>
        /// <param name="panel"></param>
        public void IniInsert(Window win, Container panel)
        {
            IniInsertWindow(win);
            IniInsertPanel(panel);
            this.pageData.RefreshInsertParamData(null);
        }
        #endregion

        #region 初始化修改 IniUpdate
        /// <summary>
        /// 初始化修改项中的主键信息
        /// </summary>
        /// <returns></returns>
        private Hidden IniUpdatePrimarykey()
        {
            string fieldName = "_" + UiControlNamePrefix.UpdateParam
                + uiHelper.UiData.Crud.Primarykey.FieldName + "_state";
            Hidden hidden = new Hidden();
            IniAllParamField(fieldName, new ParamField());
            hidden.ID = fieldName;
            hidden.Name = fieldName;
            hidden.LabelAlign = LabelAlign.Right;
            return hidden;
        }
        /// <summary>
        /// 初始化修改Window
        /// </summary>
        /// <param name="win"></param>
        private void IniUpdateWindow(Window win)
        {
            win.Hidden = true;
            int i = uiHelper.Update.Panel.Height;
            win.Height = i > 0 ? i : 300;
            i = uiHelper.Update.Panel.Width;
            win.Width = i > 0 ? i : 300;
            win.Title = uiHelper.WebPage.Title + "--修改";
        }
        /// <summary>
        /// 初始化修改项
        /// </summary>
        /// <param name="panel"></param>
        private void IniUpdatePanel(Container panel)
        {
            string prefix = UiControlNamePrefix.UpdateParam + "c_";
            bool hasPrimarykey = false;
            foreach (var param in uiHelper.Update.Panel.ParamFields)
            {
                if (param.FieldName.Equals(uiHelper.UiData.Crud.Primarykey.FieldName, StringComparison.CurrentCultureIgnoreCase))
                {
                    hasPrimarykey = true;
                    break;
                }
            }
            if (!hasPrimarykey)
            {
                panel.Add(IniUpdatePrimarykey());
            }
            int paramFieldsCount = uiHelper.Update.Panel.ParamFields.Count;
            int columncount = uiHelper.Update.Panel.ColumnCount;
            if (columncount == 0) { columncount = 1; }
            int rowcount = paramFieldsCount;
            double dColumnWidth = 1.0 / columncount;
            rowcount = rowcount / columncount + 1;
            for (int i = 0; i < columncount; i++)
            {
                Container container = new Container();
                container.ID = prefix + i.ToString();
                container.Layout = "FormLayout";
                container.Padding = 5;
                container.ColumnWidth = dColumnWidth;
                for (int j = 0; j < rowcount; j++)
                {
                    int idx = j * columncount + i;
                    if (idx >= paramFieldsCount)
                    {
                        continue;
                    }
                    ParamField field = uiHelper.Update.Panel.ParamFields[idx];
                    IniParamField(UiControlNamePrefix.UpdateParam, field, container);
                }
                panel.Add(container);
            }
        }
        /// <summary>
        /// 初始化修改
        /// </summary>
        /// <param name="win"></param>
        /// <param name="panel"></param>
        public void IniUpdate(Window win, Container panel)
        {
            IniUpdateWindow(win);
            IniUpdatePanel(panel);
            this.pageData.RefreshUpdateParamData(null);
        }
        #endregion

        #region 初始化查询  IniSelect
        /// <summary>
        /// 初始化查询项
        /// </summary>
        /// <param name="panel"></param>
        private void IniSeleteParamPanel(Container panel)
        {
            string prefix = UiControlNamePrefix.SelectParam + "c_";
            int paramFieldsCount = 3;
            if (uiHelper.Select.ParamPanel.ColumnCount > 0)
            {
                paramFieldsCount = uiHelper.Select.ParamPanel.ColumnCount;
            }
            else
            {
                if (uiHelper.UiType == UiType.SearchBox)
                {
                    paramFieldsCount = 1;
                }
            }
            double dColumnWidth = 1.0 / paramFieldsCount;
            int count = uiHelper.Select.ParamPanel.ParamFields.Count;
            int rowscount = count / paramFieldsCount + 1;
            for (int iCol = 0; iCol < paramFieldsCount; iCol++)
            {
                Container container = new Container();
                container.ID = prefix + iCol.ToString();
                container.Layout = "FormLayout";
                container.Padding = 5;
                container.ColumnWidth = dColumnWidth;
                for (int iRow = 0; iRow < rowscount; iRow++)
                {
                    int i = iRow * paramFieldsCount + iCol;
                    if (i >= uiHelper.Select.ParamPanel.ParamFields.Count)
                    {
                        continue;
                    }
                    ParamField field = uiHelper.Select.ParamPanel.ParamFields[i];
                    IniParamField(UiControlNamePrefix.SelectParam, field, container);
                }
                if (container.Items.Count > 0)
                {
                    panel.Add(container);
                }
            }
        }
        /// <summary>
        /// 初始化查询
        /// </summary>
        /// <param name="panel"></param>
        public void IniSelectMainPanel(Container panel)
        {
            IniSeleteParamPanel(panel);
            //this.pageData.RefreshSelectParamData(null);
        }
        #endregion

        #region 初始化查询结果
        #region MainGrid
        /// <summary>
        /// 初始化自动增加的隐藏项（主键项或删除标识项）
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="key"></param>
        /// <param name="havecount"></param>
        /// <param name="grid"></param>
        private void IniAddGridColumn(string prefix, string key, int havecount, Ext.Net.GridPanel grid)
        {
            Model model = grid.GetStore().ModelInstance;
            if (!string.IsNullOrWhiteSpace(key))
            {
                bool isExist = false;
                foreach (GridColumn column in uiHelper.Select.MainGrid.Columns)
                {
                    if (key.Equals(column.ColumnName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        column.ColumnName = key;
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    GridColumn column = new GridColumn();
                    column.Caption = key;
                    column.ColumnName = key;
                    column.IsShow = false;
                    ModelField mField = new ModelField();
                    mField.Name = column.ColumnName;
                    model.Fields.Add(mField);
                    if (column.IsShow)
                    {
                        CellCommandColumn cell = IniGridColumn(prefix, column);
                        grid.ColumnModel.Columns.Insert(grid.ColumnModel.Columns.Count - havecount, cell);
                    }
                }
            }
        }
        /// <summary>
        /// 初始化查询主界面项
        /// </summary>
        /// <param name="grid"></param>
        public void IniSelectMainGrid(Ext.Net.GridPanel grid)
        {
            #region 分页每页数量
            var pagesize = uiHelper.Select.MainGrid.PageSize;
            if (pagesize <= 0)
            {
                pagesize = 15;
            }
            grid.GetStore().PageSize = pagesize;
            #endregion

            string prefix = UiControlNamePrefix.SelectMainGrid;
            Model model = grid.GetStore().ModelInstance;
            #region 网格条数信息
            int havecount = grid.ColumnModel.Columns.Count;
            RowNumbererColumn numbercol = new RowNumbererColumn();
            numbercol.Width = 40;
            grid.ColumnModel.Columns.Insert(grid.ColumnModel.Columns.Count - havecount, numbercol);
            #endregion

            #region 添加特殊字段
            if (uiHelper.UiData.Crud.Primarykey != null
                && !string.IsNullOrWhiteSpace(uiHelper.UiData.Crud.Primarykey.FieldName))
            {
                IniAddGridColumn(prefix, uiHelper.UiData.Crud.Primarykey.FieldName, havecount, grid);
            }
            IniAddGridColumn(prefix, uiHelper.UiData.Crud.DeleteFlag, havecount, grid);
            #endregion

            #region 添加设置字段
            foreach (GridColumn column in uiHelper.Select.MainGrid.Columns)
            {
                ModelField mField = new ModelField();
                mField.Name = column.ColumnName;
                if (column.ColumnType == GridColumnType.Date)
                {
                    mField.Type = ModelFieldType.Date;
                }
                model.Fields.Add(mField);
                if (column.IsShow)
                {
                    CellCommandColumn cell = IniGridColumn(prefix, column);
                    grid.ColumnModel.Columns.Insert(grid.ColumnModel.Columns.Count - havecount, cell);
                }
            }
            #endregion

            #region 更新显示按钮
            if (uiHelper.Update.Panel.ParamFields.Count > 0)
            {
                return;
            }
            int commandcount = 0;
            if (uiHelper.Select.MainDetail.ParamFields.Count > 0)
            {
                commandcount++;
            }
            if (uiHelper.Select.DetailGrid.Columns.Count > 0)
            {
                commandcount++;
            }
            if (commandcount == 0)
            {
                return;
            }
            CommandColumn commandColumn = new CommandColumn();
            commandColumn.ID = "commandColumnShowDetail";
            commandColumn.Width = commandcount * 86;
            commandColumn.Text = "查看";
            commandColumn.Align = Alignment.Center;
            int _command = commandColumn.Commands.Count;
            if (uiHelper.Select.MainDetail.ParamFields.Count > 0)
            {
                GridCommand command = new GridCommand();
                command.Icon = Icon.TableColumn;
                command.Text = "详细信息";
                command.CommandName = "ShowFieldsInfo";
                commandColumn.Commands.Add(command);
                commandColumn.Listeners.Command.Fn = "showdetailcommandcolumn_click";
            }
            if (commandColumn.Commands.Count > _command)
            {
                commandColumn.Commands.Add(new CommandSeparator());
                _command = commandColumn.Commands.Count;
            }
            if (uiHelper.Select.DetailGrid.Columns.Count > 0)
            {
                GridCommand command = new GridCommand();
                command.Icon = Icon.TableColumn;
                command.Text = "明细信息";
                command.CommandName = "ShowDetail";
                commandColumn.Commands.Add(command);
                commandColumn.Listeners.Command.Fn = "showdetailcommandcolumn_click";
            }
            grid.ColumnModel.Columns.Add(commandColumn);
            #endregion
        }
        #endregion

        #region 详细信息 MainDetail
        /// <summary>
        /// 初始化查询 主详细信息
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="model"></param>
        public void IniSelectMainDetailPanel(Ext.Net.Panel panel)
        {
            int width = uiHelper.Select.MainDetail.Width;
            panel.Width = width > 0 ? width : 360;
            panel.Visible = uiHelper.Select.MainDetail.ParamFields.Count > 0;
            panel.Title = uiHelper.WebPage.Title + "--详细信息";
        }
        /// <summary>
        /// 初始化查询 主详细信息
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="model"></param>
        public void IniSelectMainDetailContainer(Container panel)
        {
            #region 初始化显示字段
            string prefix = UiControlNamePrefix.SelectMainDetail;
            int columncount = 1;
            if (uiHelper.Select.MainDetail.ColumnCount > 0)
            {
                columncount = uiHelper.Select.MainDetail.ColumnCount;
            }
            double dColumnWidth = 1.0 / columncount;
            int count = uiHelper.Select.MainDetail.ParamFields.Count;
            int rowscount = count / columncount + 1;
            for (int iCol = 0; iCol < columncount; iCol++)
            {
                Container container = new Container();
                container.ID = prefix + "Container_" + iCol.ToString();
                container.Layout = "FormLayout";
                container.Padding = 5;
                container.ColumnWidth = dColumnWidth;
                for (int iRow = 0; iRow < rowscount; iRow++)
                {
                    int i = iRow * columncount + iCol;
                    if (i >= uiHelper.Select.MainDetail.ParamFields.Count)
                    {
                        continue;
                    }
                    ParamField field = uiHelper.Select.MainDetail.ParamFields[i];
                    IniParamField(prefix, field, container, true);
                }
                if (container.Items.Count > 0)
                {
                    panel.Add(container);
                }
            }
            #endregion
        }
        #endregion

        #region 汇总信息 SummaryGrid
        /// <summary>
        /// 初始化查询 查询汇总
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="model"></param>
        public void IniSelectSummaryGrid(Ext.Net.GridPanel grid)
        {
        }
        #endregion

        #region 明细信息 DetailGrid
        /// <summary>
        /// 初始化查询 明细列表
        /// </summary>
        /// <param name="grid"></param>
        public void IniSelectDetailGrid(Ext.Net.GridPanel grid)
        {
            #region 分页每页数量
            var pagesize = uiHelper.Select.DetailGrid.PageSize;
            if (pagesize <= 0)
            {
                pagesize = 15;
            }
            grid.GetStore().PageSize = pagesize;
            #endregion
            string prefix = UiControlNamePrefix.SelectDetailGrid;
            Model model = grid.GetStore().ModelInstance;
            #region 网格条数信息
            int havecount = grid.ColumnModel.Columns.Count;
            RowNumbererColumn numbercol = new RowNumbererColumn();
            numbercol.Width = 40;
            grid.ColumnModel.Columns.Insert(grid.ColumnModel.Columns.Count - havecount, numbercol);
            #endregion


            #region 添加设置字段
            foreach (GridColumn column in uiHelper.Select.DetailGrid.Columns)
            {
                ModelField mField = new ModelField();
                mField.Name = column.ColumnName;
                if (column.ColumnType == GridColumnType.Date)
                {
                    mField.Type = ModelFieldType.Date;
                }
                model.Fields.Add(mField);
                if (column.IsShow)
                {
                    CellCommandColumn cell = IniGridColumn(prefix, column);
                    grid.ColumnModel.Columns.Insert(grid.ColumnModel.Columns.Count - havecount, cell);
                }
            }
            #endregion

        }
        /// <summary>
        /// 初始化查询 明细列表
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="model"></param>
        public void IniSelectDetailWindow(Ext.Net.Window win)
        {
            int n = uiHelper.Select.DetailGrid.Width;
            win.Width = n > 0 ? n : 420;
            n = uiHelper.Select.DetailGrid.Height;
            win.Height = n > 0 ? n : 300;
            win.Title = uiHelper.WebPage.Title + "--明细信息";
        }
        #endregion

        #endregion
    }
}
