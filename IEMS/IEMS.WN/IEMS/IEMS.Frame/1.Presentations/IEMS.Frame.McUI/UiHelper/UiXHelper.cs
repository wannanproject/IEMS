using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MSTL.XmlOrm;


namespace IEMS.Frame.McUI
{
    /// <summary>
    /// 配置解析类
    /// </summary>
    internal class UiXHelper
    {
        #region UiData
        private List<Caption> getCaptions(XReader xReader)
        {
            var reader = new XReader() { XmlNode = xReader.XmlNode, NodeName = "Caption" };
            var xmlHelper = new XHelper();
            var result = xmlHelper.TMapper<Caption>(reader);
            return result.ToList<Caption>();
        }
        private Primarykey getPrimarykey(XReader xReader)
        {
            var reader = new XReader() { XmlNode = xReader.XmlNode,  NodeName = "Primarykey"};
            var xmlHelper = new XHelper();
            var result = xmlHelper.TMapper<Primarykey>(reader);
            return result.FirstOrDefault();
        }
        private string getDeleteFlag(XReader xReader)
        {
            var reader = new XReader() { XmlNode = xReader.XmlNode, NodeName = "DeleteFlag" };
            var xmlHelper = new XHelper();
            var result = xmlHelper.TMapper<string>(reader);
            return result.FirstOrDefault();
        }
        private List<Unique> getUniques(XReader xReader)
        {
            var reader = new XReader() { XmlNode = xReader.XmlNode, NodeName = "Unique" };
            var xmlHelper = new XHelper();
            var result = xmlHelper.TMapper<Unique>(reader);
            return result.ToList<Unique>();
        }
        private List<SeqField> getSeqFields(XReader xReader)
        {
            var reader = new XReader() { XmlNode = xReader.XmlNode, NodeName = "SeqField" };
            var xmlHelper = new XHelper();
            var result = xmlHelper.TMapper<SeqField>(reader);
            return result.ToList<SeqField>();
        }

        private Crud getCrud(XmlNode node)
        {
            var reader = new XReader() { XmlNode = node, NodeName = "Crud" };
            var xmlHelper = new XHelper();
            var list = xmlHelper.TRead<Crud>(reader);
            var result = new Crud();
            var first = list.FirstOrDefault();
            if (first != null && first.Instance != null)
            {
                result = first.Instance;
            }
            result.Primarykey = getPrimarykey(reader);
            string ss = getDeleteFlag(reader);
            if (!string.IsNullOrWhiteSpace(ss))
            {
                result.DeleteFlag = ss;
            }
            result.Uniques = getUniques(reader);
            result.SeqFields = getSeqFields(reader);
            return result;
        }
        private UiData getUiData(XmlNode node)
        {
            var reader = new XReader() { XmlNode = node };
            var result = new UiData();
            List<Caption> lst = getCaptions(reader);
            result.AddRangeCaption(lst);
            result.Crud = getCrud(node);
            return result;
        }
        #endregion

        #region WebPage
        private List<JavaScript> getJavaScripts(XmlNode node)
        {
            var reader = new XReader() { XmlNode = node, NodeName = "JavaScript" };
            var xmlHelper = new XHelper();
            var result = xmlHelper.TMapper<JavaScript>(reader);
            return result.ToList<JavaScript>();
        }
        private WebPage getWebPage(XmlNode node)
        {
            var reader = new XReader() { XmlNode = node, NodeName = "WebPage" };
            var result = new WebPage();
            var xmlHelper = new XHelper();
            var instance = xmlHelper.TRead<WebPage>(reader).FirstOrDefault();
            if (instance == null)
            {
                return result;
            }
            result = instance.Instance;
            result.JavaScripts.AddRange(getJavaScripts(instance.XRow.Node));
            reader = new XReader() { XmlNode = reader.XmlNode };
            result.PageLoadCommands = getCommands(reader, "IniCommands\\Command");
            return result;
        }
        #endregion

        #region UiBiz
        #region ParamPanel
        private List<ParamField> getParamFields(XReader xReader)
        {
            var reader = new XReader() { XmlNode = xReader.XmlNode, NodeName = "ParamField" };
            var xmlHelper = new XHelper();
            var result = xmlHelper.TMapper<ParamField>(reader);
            return result.ToList<ParamField>();
        }
        private ParamPanel getParamPanel(XReader xReader,string nodeName)
        {
            var result = new ParamPanel();
            var reader = new XReader() { XmlNode = xReader.XmlNode, NodeName = nodeName };
            var xmlHelper = new XHelper();
            var target = xmlHelper.TRead<ParamPanel>(reader).FirstOrDefault();
            if (target == null)
            {
                return result;
            }
            var instance = target.Instance;
            if (instance == null)
            {
                return result;
            }
            result = instance;
            result.ParamFields = getParamFields(new XReader() { XmlNode = target.XRow.Node });
            return result;
        }
        #endregion
        #region GridPanel
        private List<GridColumn> getGridColumns(XReader xReader)
        {
            var reader = new XReader() { XmlNode = xReader.XmlNode, NodeName = "GridColumn" };
            var xmlHelper = new XHelper();
            return xmlHelper.TMapper<GridColumn>(reader).ToList<GridColumn>();
        }
        private Total getTotalColumns(XReader xReader)
        {
            var reader = new XReader() { XmlNode = xReader.XmlNode, NodeName = "TotalColumns" };
            var xmlHelper = new XHelper();
            var result = xmlHelper.TMapper<Total>(reader).ToList<Total>().FirstOrDefault();
            if (result == null)
            {
                result = new Total();
            }
            return result;
        }
        private GridPanel getGridPanel(XReader xReader,string nodeName)
        {
            var result = new GridPanel();
            var reader = new XReader() { XmlNode = xReader.XmlNode, NodeName = nodeName };
            var xmlHelper = new XHelper();
            var targets = xmlHelper.TRead<GridPanel>(reader);
            var target = targets.FirstOrDefault();
            if (target == null)
            {
                return result;
            }
            result = target.Instance;
            result.Columns = getGridColumns(new XReader() { XmlNode = target.XRow.Node });
            result.ColumnCount = result.Columns.Count;
            result.TotalColumns = getTotalColumns(new XReader() { XmlNode = target.XRow.Node } );
            return result;
        }
        #endregion

        private List<DataValue> getDataValues(XReader xReader,string nodeName)
        {
            var reader = new XReader() { XmlNode = xReader.XmlNode, NodeName = nodeName };
            var xmlHelper = new XHelper();
            var result = xmlHelper.TMapper<DataValue>(reader);
            return result.ToList<DataValue>();
        }
        private List<Command> getCommands(XReader xReader,string nodeName)
        {
            var reader = new XReader() { XmlNode = xReader.XmlNode, NodeName = "Command" };
            var xmlHelper = new XHelper();
            var result = xmlHelper.TMapper<Command>(reader);
            return result.ToList<Command>();
        }
        private void IniUiBizCommands(XReader reader, UiBiz biz)
        {
            biz.DataValues = getDataValues(reader, "DataValues\\DataValue");
            biz.TrimCommands = getCommands(reader, "TrimCommands\\Command");
            biz.VerifyCommands = getCommands(reader, "VerifyCommands\\Command");
            biz.ExcuteCommand = getCommands(reader, "ExcuteCommands\\Command").FirstOrDefault();
        }
        private Insert getInsert(XmlNode node)
        {
            var result = new Insert();
            var reader = new XReader() { XmlNode = node, NodeName = "Insert" };
            var xmlHelper = new XHelper();
            var target = xmlHelper.TRead<Insert>(reader).FirstOrDefault();
            if (target == null)
            {
                return result;
            }
            result = target.Instance;
            reader = new XReader() { XmlNode = target.XRow.Node };
            result.Panel = getParamPanel(reader, "Insert");
            IniUiBizCommands(reader, result);
            return result;
        }
        private Delete getDelete(XmlNode node)
        {
            var result = new Delete();
            var reader = new XReader() { XmlNode = node, NodeName = "Delete" };
            var xmlHelper = new XHelper();
            var target = xmlHelper.TRead<Delete>(reader).FirstOrDefault();
            if (target == null)
            {
                return result;
            }
            result = target.Instance;
            reader = new XReader() { XmlNode = target.XRow.Node };
            IniUiBizCommands(reader, result);
            return result;
        }
        private Update getUpdate(XmlNode node)
        {
            var result = new Update();
            var reader = new XReader() { XmlNode = node, NodeName = "Update" };
            var xmlHelper = new XHelper();
            var target = xmlHelper.TRead<Update>(reader).FirstOrDefault();
            if (target == null)
            {
                return result;
            }
            result = target.Instance;
            reader = new XReader() { XmlNode = target.XRow.Node };
            result.Panel = getParamPanel(reader, "Update");
            IniUiBizCommands(reader, result);
            return result;
        }
        private Select getSelect(XmlNode node)
        {
            var result = new Select();
            var reader = new XReader() { XmlNode = node, NodeName = "Select" };
            var xmlHelper = new XHelper();
            var target = xmlHelper.TRead<Select>(reader).FirstOrDefault();
            if (target == null)
            {
                return result;
            }
            result = target.Instance;
            reader = new XReader() { XmlNode = target.XRow.Node };
            result.ParamPanel = getParamPanel(reader, "ParamPanel");
            result.MainGrid = getGridPanel(reader, "MainGrid");
            result.MainDetail = getParamPanel(reader, "MainDetail");
            result.SummaryGrid = getGridPanel(reader, "SummaryGrid");
            result.DetailGrid = getGridPanel(reader, "DetailGrid");
            IniUiBizCommands(reader, result);
            return result;
        }
        #endregion

        #region xmlHelper
        private void IniCommands(List<Command> commands)
        {
            foreach (Command command in commands)
            {
                command.Ini();
            }
        }
        private void IniCommands(UiBiz biz)
        {
            IniCommands(biz.TrimCommands);
            IniCommands(biz.VerifyCommands);
            biz.ExcuteCommand.Ini();
        }
        private void UiHelperCommand(UiHelper uiHelper)
        {
            uiHelper.WebPage.PageLoadCommands.Insert(0, new Command() { ClassName = typeof(DefaultCommand.PageLoadCommand).FullName });
            IniCommands(uiHelper.WebPage.PageLoadCommands);

            uiHelper.Insert.UiHelper = uiHelper;
            uiHelper.Insert.TrimCommands.Insert(0, new Command() { ClassName = typeof(DefaultCommand.Insert.TrimCommand).FullName });
            uiHelper.Insert.VerifyCommands.Insert(0, new Command() { ClassName = typeof(DefaultCommand.Insert.VerifyCommand).FullName });
            if (uiHelper.Insert.ExcuteCommand == null)
            {
                uiHelper.Insert.ExcuteCommand = new Command() { ClassName = typeof(DefaultCommand.Insert.ExecuteCommand).FullName };
            }
            IniCommands(uiHelper.Insert);

            uiHelper.Delete.UiHelper = uiHelper;
            uiHelper.Delete.TrimCommands.Insert(0, new Command() { ClassName = typeof(DefaultCommand.Delete.TrimCommand).FullName });
            uiHelper.Delete.VerifyCommands.Insert(0, new Command() { ClassName = typeof(DefaultCommand.Delete.VerifyCommand).FullName });
            if (uiHelper.Delete.ExcuteCommand == null)
            {
                uiHelper.Delete.ExcuteCommand = new Command() { ClassName = typeof(DefaultCommand.Delete.ExecuteCommand).FullName };
            }
            IniCommands(uiHelper.Delete);

            uiHelper.Update.UiHelper = uiHelper;
            uiHelper.Update.TrimCommands.Insert(0, new Command() { ClassName = typeof(DefaultCommand.Update.TrimCommand).FullName });
            uiHelper.Update.VerifyCommands.Insert(0, new Command() { ClassName = typeof(DefaultCommand.Update.VerifyCommand).FullName });
            if (uiHelper.Update.ExcuteCommand == null)
            {
                uiHelper.Update.ExcuteCommand = new Command() { ClassName = typeof(DefaultCommand.Update.ExecuteCommand).FullName };
            }
            IniCommands(uiHelper.Update);

            uiHelper.Select.UiHelper = uiHelper;
            uiHelper.Select.TrimCommands.Insert(0, new Command() { ClassName = typeof(DefaultCommand.Select.TrimCommand).FullName });
            uiHelper.Select.VerifyCommands.Insert(0, new Command() { ClassName = typeof(DefaultCommand.Select.VerifyCommand).FullName });
            if (uiHelper.Select.ExcuteCommand == null)
            {
                uiHelper.Select.ExcuteCommand = new Command() { ClassName = typeof(DefaultCommand.Select.ExecuteCommand).FullName };
            }
            IniCommands(uiHelper.Select);
        }
        private void getUiHelper(UiHelper uiHelper)
        {
            UiHelperCommand(uiHelper);
        }
        public void IniUiHelper(UiHelper uiHelper, string fileName)
        {
            XReader reader = new XReader() { XmlFile = fileName, NodeName = "uiconfig" };
            var targets = new XHelper().TRead<string>(reader);
            XTarget<string> target = targets.FirstOrDefault();
            if (target == null)
            {
                return;
            }
            uiHelper.FileInfo = new System.IO.FileInfo(fileName);
            uiHelper.UiData = getUiData(target.XRow.Node);
            uiHelper.WebPage = getWebPage(target.XRow.Node);
            uiHelper.Insert = getInsert(target.XRow.Node);
            uiHelper.Delete = getDelete(target.XRow.Node);
            uiHelper.Update = getUpdate(target.XRow.Node);
            uiHelper.Select = getSelect(target.XRow.Node);
            getUiHelper(uiHelper);
        }
        #endregion
    }
}
