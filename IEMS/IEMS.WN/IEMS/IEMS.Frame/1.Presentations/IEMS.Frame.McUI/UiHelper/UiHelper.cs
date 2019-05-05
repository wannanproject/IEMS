using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace IEMS.Frame.McUI
{
    using MSTL.LogAgent;
    using MSTL.XmlOrm;

    /// <summary>
    /// 配置页面
    /// </summary>
    public class UiHelper
    {
        #region 系统日志  log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion

        #region 常量标示数据
        private const string BEGIN_MCUI_STATE = "_Begin";
        private const string END_MCUI_STATE = "_End";
        private const string DATE_MCUI_STATE = "_Date";
        private const string TIME_MCUI_STATE = "_Time";
        private const string DATETIME_MCUI_STATE = "_DateTime";
        private const string TEXT_MCUI_STATE = "_Text";
        private const string MCUI_STATE_END = "_MCUI_State";

        public const string DATETIME_STATE_END = DATETIME_MCUI_STATE + MCUI_STATE_END;
        public const string DATE_DATETIME_STATE_END = DATE_MCUI_STATE + DATETIME_STATE_END;
        public const string TIME_DATETIME_STATE_END = TIME_MCUI_STATE + DATETIME_STATE_END;

        public const string DATE_STATE_END = DATE_MCUI_STATE + MCUI_STATE_END;
        public const string BEGIN_DATE_STATE_END = BEGIN_MCUI_STATE + DATE_STATE_END;
        public const string END_DATE_STATE_END = END_MCUI_STATE + DATE_STATE_END;


        public const string TEXT_STATE_END = TEXT_MCUI_STATE + MCUI_STATE_END;
        public const string BEGIN_TEXT_STATE_END = BEGIN_MCUI_STATE + TEXT_STATE_END;
        public const string END_TEXT_STATE_END = END_MCUI_STATE + TEXT_STATE_END;
        #endregion
        
        #region 构造函数
        #region 获取文件路径
        /// <summary>
        /// 获取根目录
        /// </summary>
        /// <returns></returns>
        private DirectoryInfo getRootDirectory()
        {
            FileInfo fi = new FileInfo(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            return fi.Directory.Parent;
        }
        /// <summary>
        /// 界面配置文件索引
        /// </summary>
        private class McUIInfo
        {
            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 路径
            /// </summary>
            public string Uri { get; set; }
        }
        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private void iniMapperXml(FileInfo fi)
        {
            var instances = new McUIInfo[0];
            XReader reader = new XReader(){ XmlFile = fi.FullName, NodeName = "McUI" };
            var mapper = new XHelper();
            instances = mapper.TMapper<McUIInfo>(reader);
            if (instances == null || instances.Length == 0)
            {
                return;
            }
            foreach (McUIInfo ui in instances)
            {
                string fileName = Path.Combine(fi.Directory.FullName, ui.Uri);
                fileName = new FileInfo(fileName).FullName;
                log.Debug(ui.Name + " = " + fileName);

                UiHelper uiHelper = new UiHelper();
                new UiXHelper().IniUiHelper(uiHelper, fileName);
                uiHelper.Name = ui.Name;
                if (!uiHelperStore.ContainsKey(ui.Name))
                {
                    uiHelperStore.Add(ui.Name, uiHelper);
                }
            }
        }
        private void getMapperXmlFiles(DirectoryInfo dir, ref List<FileInfo> files)
        {
            foreach (var fi in dir.GetFiles())
            {
                if (fi.Name.ToLower() == "@McUI.xml".ToLower())
                {
                    files.Add(fi);
                }
            }
            foreach (var _dir in dir.GetDirectories())
            {
                getMapperXmlFiles(_dir, ref files);
            }
        }
        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private void iniMapperXml()
        {
            var dir = getRootDirectory();
            var files = new List<FileInfo>();
            getMapperXmlFiles(dir, ref files);
            foreach (var fi in files)
            {
                iniMapperXml(fi);
            }
        }


        #endregion
        private UiHelper()
        {
            this.WebPage = new WebPage();
            this.UiData = new UiData();
            this.Insert = new Insert();
            this.Delete = new Delete();
            this.Update = new Update();
            this.Select = new Select();
            this.AllParamField = new Dictionary<string, ParamField>();
        }
        #endregion

        #region 获取 UiHelper
        /// <summary>
        /// 配置页面集合
        /// </summary>
        private static Dictionary<string, UiHelper> uiHelperStore = new Dictionary<string, UiHelper>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 数据类型转化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object changeType(object value, Type type)
        {
            if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, Convert.ToInt32(value));
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = changeType(value, innerType);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }
            if (value is string && type == typeof(Guid)) return new Guid(value as string);
            if (value is string && type == typeof(Version)) return new Version(value as string);
            if (!(value is IConvertible)) return value;
            return Convert.ChangeType(value, type);
        }
        /// <summary>
        /// 获取配置页面
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static UiHelper getInstance(string name)
        {
            UiHelper result = null;
            new UiHelper().iniMapperXml();
            uiHelperStore.TryGetValue(name, out result);
            return result;
        }

        /// <summary>
        /// 获取配置页面
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static UiHelper GetInstance(string name, string type)
        {
            UiHelper result = null;
            if (string.IsNullOrWhiteSpace(name))
            {
                return result;
            }
            if (!uiHelperStore.TryGetValue(name, out result))
            {
                result = getInstance(name);
            }
            if (result == null)
            {
                return null;
            }
            try
            {
                result.UiType = (UiType)changeType(type, typeof(UiType));
            }
            catch
            { }
            if (result.UiType == UiType.None)
            {
                result.UiType = UiType.Report;
            }
            return result;
        }
        /// <summary>
        /// 缓存清空
        /// </summary>
        public static void ClearCache()
        {
            uiHelperStore.Clear();
        }
        #endregion

        #region 公共属性
        private FileInfo _fileInfo = null;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public void SetUiType(string type)
        {

        }
        /// <summary>
        /// 模版类型
        /// </summary>
        public UiType UiType { get; private set; }
        /// <summary>
        /// 配置文件
        /// </summary>
        public FileInfo FileInfo
        {
            get
            {
                return _fileInfo;
            }
            set
            {
                _fileInfo = value;
            }
        }
        /// <summary>
        /// 配置对应的插件名
        /// </summary>
        private string getPluginName()
        {
            string result = string.Empty;
            string ss = this.FileInfo.FullName;
            string p = "\\Plugins\\";
            ss = ss.Substring(ss.ToLower().IndexOf(p.ToLower()) + p.Length);
            result = ss.Substring(0, ss.IndexOf("\\"));
            return result;
        }
        /// <summary>
        /// 配置对应的插件名
        /// </summary>
        public string PluginName { get { return getPluginName(); } }
        /// <summary>
        /// 页面信息
        /// </summary>
        public WebPage WebPage { get; set; }
        /// <summary>
        /// 配置信息
        /// </summary>
        public UiData UiData { get; set; }
        /// <summary>
        /// 增加 配置
        /// </summary>
        public Insert Insert { get; set; }
        /// <summary>
        /// 删除 配置
        /// </summary>
        public Delete Delete { get; set; }
        /// <summary>
        /// 修改 配置
        /// </summary>
        public Update Update { get; set; }
        /// <summary>
        /// 查询 配置
        /// </summary>
        public Select Select { get; set; }
        /// <summary>
        /// 界面输入控件集合
        /// </summary>
        public Dictionary<string, ParamField> AllParamField { get; set; }
        #endregion
    }

    #region 配置组件
    #region 基础信息
    /// <summary>
    /// 配置页面基本信息
    /// </summary>
    public class UiXmlInfo
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public FileInfo FileInfo { get; set; }
    }
    /// <summary>
    /// 模版类型
    /// </summary>
    public enum UiType
    {
        None = 0,
        Crud = 1,
        SearchBox = 2,
        Report = 3,
    }

    #region public class Caption
    /// <summary>
    /// 配置显示说明
    /// </summary>
    public class Caption
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 提示
        /// </summary>
        public string Hint { get; set; }
    }
    #endregion

    #region public class Crud
    /// <summary>
    /// 唯一键
    /// </summary>
    public class Unique
    {
        public Unique()
        {
            this.FieldList = new string[0];
        }
        private string _field = string.Empty;
        private void Ini()
        {
            if (!string.IsNullOrWhiteSpace(_field))
            {
                this.FieldList = _field.Split(',');
            }
        }
        /// <summary>
        /// 字段列表
        /// </summary>
        public string Fields
        {
            get
            {
                return _field;
            }
            set
            {
                this._field = value;
                Ini();
            }
        }
        /// <summary>
        /// 字段列表
        /// </summary>
        public string[] FieldList { get; set; }
    }
    /// <summary>
    /// 主键
    /// </summary>
    public class Primarykey
    {
        /// <summary>
        /// 主键名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 是否为自增 非自增取最大值+1
        /// </summary>
        public bool Identity { get; set; }
    }
    /// <summary>
    /// 字段 序列
    /// </summary>
    public class SeqField
    {
        /// <summary>
        /// 主键名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 序列名称
        /// </summary>
        public string SeqName { get; set; }
    }
    /// <summary>
    /// 增删改  相关数据
    /// </summary>
    public class Crud
    {
        public Crud()
        {
            this.Uniques = new List<Unique>();
            this.SeqFields = new List<SeqField>();
        }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public Primarykey Primarykey { get; set; }
        /// <summary>
        /// 删除标示
        /// </summary>
        public string DeleteFlag { get; set; }
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<Unique> Uniques { get; set; }
        /// <summary>
        /// 字段 序列
        /// </summary>
        public List<SeqField> SeqFields { get; set; }
    }
    #endregion

    #region public class ParamField
    /// <summary>
    /// 界面条件类型
    /// </summary>
    public enum ParamFieldType
    {
        TextBox = 0,
        ComboBox = 1,
        DateBox = 2,
        DateTimeBox = 3,
        SearchBox = 4,
        NumberBox = 5,
        BeginEndDateBox = 6,
        BeginEndTextBox = 7,
    }
    /// <summary>
    /// 界面条件字段
    /// </summary>
    public class ParamField
    {
        public ParamField()
        {
            this.IsShow = true;
        }
        /// <summary>
        /// 界面显示信息
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 对应数据库字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 默认数据值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 字段只读
        /// </summary>
        public bool ReadOnly { get; set; }
        /// <summary>
        /// 是否可为空
        /// </summary>
        public bool Nullable { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public string Datasource { get; set; }
        /// <summary>
        /// 展示类型
        /// </summary>
        public ParamFieldType Type { get; set; }
        /// <summary>
        /// 是否显示此字段
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 是否为多行文本框
        /// </summary>
        public bool IsMult { get; set; }
    }
    #endregion

    #region public class WebPage
    /// <summary>
    /// JavaScript 文件信息
    /// </summary>
    public class JavaScript
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileUrl { get; set; }
    }
    /// <summary>
    /// 界面信息
    /// </summary>
    public class WebPage
    {
        public WebPage()
        {
            this.PageLoadCommands = new List<Command>();
            this.JavaScripts = new List<JavaScript>();
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 数据格式化
        /// </summary>
        public List<Command> PageLoadCommands { get; set; }
        /// <summary>
        /// Js脚本文件
        /// </summary>
        public List<JavaScript> JavaScripts { get; set; }
    }
    #endregion

    #region public class GridColumn
    /// <summary>
    /// 查询结果类型
    /// </summary>
    public enum GridColumnType
    {
        Default = 0,
        Date = 1,
        Summary = 2,
    }
    /// <summary>
    /// 查询结果字段
    /// </summary>
    public class GridColumn
    {
        public GridColumn()
        {
            this.IsShow = true;
        }
        /// <summary>
        /// 对应数据库字段名称
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public GridColumnType ColumnType { get; set; }
        /// <summary>
        /// 字段格式化
        /// </summary>
        public string ColumnFormt { get; set; }
        /// <summary>
        /// 是否显示此字段
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 锁定列
        /// </summary>
        public bool Locked { get; set; }
        /// <summary>
        /// 字段宽度值
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 字段说明
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 字段备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 字段宽度比
        /// </summary>
        public int Flex { get; set; }
    }
    #endregion

    #region public class WebPanel
    public class WebPanel
    {
        public WebPanel()
        {
        }
        /// <summary>
        /// 界面Column数量
        /// </summary>
        public int ColumnCount { get; set; }
        /// <summary>
        /// 弹出窗口高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 弹出窗口宽度
        /// </summary>
        public int Width { get; set; }
    }
    #endregion

    #region public class GridPanel

    /// <summary>
    /// 汇总列
    /// </summary>
    public class Total
    {
        public Total()
        {
            this.FieldList = new string[0];
        }
        private string _field = string.Empty;
        private void Ini()
        {
            if (!string.IsNullOrWhiteSpace(_field))
            {
                this.FieldList = _field.Split(',');
            }
        }
        public int Index { get; set; }

        /// <summary>
        /// 字段列表
        /// </summary>
        public string Fields
        {
            get
            {
                return _field;
            }
            set
            {
                this._field = value;
                Ini();
            }
        }
        /// <summary>
        /// 字段列表
        /// </summary>
        public string[] FieldList { get; set; }
    }
    /// <summary>
    /// 查询结果Panel
    /// </summary>
    public class GridPanel : WebPanel
    {
        public GridPanel()
        {
            this.Columns = new List<GridColumn>();
            this.PageSize = 15;
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderString { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 合计字段
        /// </summary>
        public Total TotalColumns { get; set; }
        /// <summary>
        /// 显示字段
        /// </summary>
        public List<GridColumn> Columns { get; set; }
    }
    #endregion

    #region public class ParamPanel
    /// <summary>
    /// 条件Panel
    /// </summary>
    public class ParamPanel : WebPanel
    {
        public ParamPanel()
        {
            this.ParamFields = new List<ParamField>();
        }
        /// <summary>
        /// 参数字段列表
        /// </summary>
        public List<ParamField> ParamFields { get; set; }
    }
    #endregion

    #region public class DataValue
    /// <summary>
    /// 后台数据集
    /// </summary>
    public class DataValue
    {
        /// <summary>
        /// 对应数据库字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 默认数据值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public string Datasource { get; set; }
    }
    #endregion
    #endregion

    #region 业务信息
    /// <summary>
    /// 单表基本信息
    /// </summary>
    public class UiData
    {
        public UiData()
        {
            this.Crud = new Crud();
        }
        /// <summary>
        /// 增删改  相关数据
        /// </summary>
        public Crud Crud { get; set; }
        private Dictionary<string, Caption> captionDic = new Dictionary<string, Caption>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 添加多个说明
        /// </summary>
        /// <param name="lst"></param>
        public void AddRangeCaption(List<Caption> lst)
        {
            if (lst == null)
            {
                return;
            }
            foreach (Caption caption in lst)
            {
                AddCaption(caption);
            }
        }
        /// <summary>
        /// 添加说明
        /// </summary>
        /// <param name="caption"></param>
        public void AddCaption(Caption caption)
        {
            string key = caption.Name.ToLower();
            if (captionDic.ContainsKey(key))
            {
                return;
            }
            captionDic.Add(key, caption);
        }
        /// <summary>
        /// 获取说明
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Caption GetCaption(string caption, string field)
        {
            string name = caption;
            if (string.IsNullOrWhiteSpace(name))
            {
                name = field;
            }
            Caption result = new Caption()
            {
                Name = name,
                Value = name,
                Hint = name
            };
            if (string.IsNullOrWhiteSpace(name))
            {
                return result;
            }
            if (this.captionDic.ContainsKey(name.ToLower()))
            {
                this.captionDic.TryGetValue(name.ToLower(), out result);
                return result;
            }
            else
            {
                AddCaption(result);
                return result;
            }
        }
    }

    /// <summary>
    /// 业务基类
    /// </summary>
    public class UiBiz
    {
        public UiBiz()
        {
            this.TrimCommands = new List<Command>();
            this.VerifyCommands = new List<Command>();
            this.DataValues = new List<DataValue>();
        }
        /// <summary>
        /// 更新用户ID
        /// </summary>
        public string UserField { get; set; }
        /// <summary>
        /// 更新时间ID
        /// </summary>
        public string TimeField { get; set; }
        /// <summary>
        /// 数据格式化
        /// </summary>
        public List<Command> TrimCommands { get; set; }
        /// <summary>
        /// 数据校验
        /// </summary>
        public List<Command> VerifyCommands { get; set; }
        /// <summary>
        /// 执行
        /// </summary>
        public Command ExcuteCommand { get; set; }
        /// <summary>
        /// 页面配置类
        /// </summary>
        public UiHelper UiHelper { get; set; }
        /// <summary>
        /// 配置数据
        /// </summary>
        public List<DataValue> DataValues { get; set; }
    }

    /// <summary>
    /// 增加业务
    /// </summary>
    public class Insert : UiBiz
    {
        public Insert()
        {
            this.Panel = new ParamPanel();
        }
        /// <summary>
        /// 界面信息
        /// </summary>
        public ParamPanel Panel { get; set; }
    }
    /// <summary>
    /// 删除业务
    /// </summary>
    public class Delete : UiBiz
    {
    }
    /// <summary>
    /// 修改业务
    /// </summary>
    public class Update : UiBiz
    {
        public Update()
        {
            this.Panel = new ParamPanel();
        }
        /// <summary>
        /// 更新界面类
        /// </summary>
        public ParamPanel Panel { get; set; }
    }
    /// <summary>
    /// 查询业务
    /// </summary>
    public class Select : UiBiz
    {
        public Select()
        {
            this.ParamPanel = new ParamPanel();
            this.MainGrid = new GridPanel();
            this.MainDetail = new ParamPanel();
            this.SummaryGrid = new GridPanel();
            this.DetailGrid = new GridPanel();
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        public ParamPanel ParamPanel { get; set; }
        /// <summary>
        /// 主查询结果
        /// </summary>
        public GridPanel MainGrid { get; set; }
        /// <summary>
        /// 主查询明细
        /// </summary>
        public ParamPanel MainDetail { get; set; }
        /// <summary>
        /// 汇总信息
        /// </summary>
        public GridPanel SummaryGrid { get; set; }
        /// <summary>
        /// 明细信息
        /// </summary>
        public GridPanel DetailGrid { get; set; }
    }
    #endregion
    #endregion
}
