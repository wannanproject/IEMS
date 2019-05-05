using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace IEMS.Frame.McUI
{
    using MSTL.DbAccess;
    using IEMS.Frame.AppBiz;

    /// <summary>
    /// 执行函数返回结果
    /// </summary>
    public class CommandResult
    {
        public CommandResult()
        {
            this.isBreak = false;
        }
        /// <summary>
        /// 是否返回，如果为true则break
        /// </summary>
        public bool isBreak { get; set; }
        private int _iResult = 0;
        /// <summary>
        /// 结果数值
        /// </summary>
        public int iResult
        {
            get
            {
                return _iResult;
            }
            set
            {
                _iResult = value;
                if (value < 0)
                {
                    this.isBreak = true;
                }
            }
        }
        /// <summary>
        /// 结果文档信息
        /// </summary>
        public string sResult { get; set; }
        /// <summary>
        /// 结果类信息
        /// </summary>
        public object oResult { get; set; }
        /// <summary>
        /// 结果异常信息
        /// </summary>
        public Exception Exception { get; set; }
    }
    /// <summary>
    /// 参数 KeyValue
    /// </summary>
    public class ParamKeyValue
    {
        public ParamKeyValue()
        {
            this.Key = string.Empty;
            this.Value = null;
        }
        public ParamKeyValue(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }
        public string Key { get; set; }
        public object Value { get; set; }
    }
    /// <summary>
    /// 系统参数
    /// </summary>
    public class SysParam
    {
        public SysParam()
        {
            this.UserId = new ParamKeyValue();
            this.DeleteFlag = new ParamKeyValue();
            this.Primarykey = new ParamKeyValue();
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public ParamKeyValue UserId { get; set; }
        /// <summary>
        /// 删除标示
        /// </summary>
        public ParamKeyValue DeleteFlag { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public ParamKeyValue Primarykey { get; set; }
    }
    /// <summary>
    /// Command 参数
    /// </summary>
    public class CommandParam
    {
        public CommandParam()
        {
            this.UiParam = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            this.SysParam = new SysParam();
        }
        /// <summary>
        /// 配置界面
        /// </summary>
        public UiHelper UiHelper { get { return UiBiz.UiHelper; } }
        /// <summary>
        /// 数据执行
        /// </summary>
        public DbHelper DbHelper { get; set; }
        /// <summary>
        /// 执行业务类
        /// </summary>
        public UiBiz UiBiz { get; set; }
        /// <summary>
        /// 界面参数
        /// </summary>
        public Dictionary<string, object> UiParam { get; set; }
        /// <summary>
        /// 平台参数
        /// </summary>
        public SysParam SysParam { get; set; }
        /// <summary>
        /// 数据操作标示
        /// </summary>
        public string StatementId { get; set; }
        /// <summary>
        /// 业务执行类
        /// </summary>
        public IMcUIManager McUIManager { get; set; }
    }
    /// <summary>
    /// 执行函数接口
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="param">条件</param>
        /// <returns>成功</returns>
        CommandResult Execute(CommandParam commandParam);
    }

    #region 反射执行方法类 Command
    /// <summary>
    /// 反射执行方法类
    /// </summary>
    public class Command
    {
        public void Ini()
        {
            string fileName = this.FileName;
            string className = this.ClassName;
            string filePath = string.Empty;
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                string path = new FileInfo(Assembly.GetAssembly(this.GetType()).Location).Directory.FullName;
                path = AppDomain.CurrentDomain.BaseDirectory;
                filePath = Path.Combine(path, "bin", fileName);
            }
            this.Instance = IniCommand<ICommand>(filePath, className);
        }
        /// <summary>
        /// 反射生成实例类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private T IniCommand<T>(string filePath, string className)
        {
            T result = default(T);
            try
            {
                Assembly ass;
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    ass = Assembly.GetAssembly(this.GetType());
                }
                else
                {
                    ass = Assembly.LoadFrom(filePath);
                }
                if (ass == null)
                {
                    return result;
                }
                Type type = ass.GetType(className);
                if (type == null)
                {
                    return result;
                }
                if (!typeof(T).IsAssignableFrom(type))
                {
                    return result;
                }
                result = (T)Activator.CreateInstance(type);
                return result;
            }
            catch { }
            return result;
        }
        #region 属性
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 示例
        /// </summary>
        public ICommand Instance { get; private set; }
        #endregion
        /// <summary>
        /// 业务命令执行
        /// </summary>
        /// <param name="commandParam"></param>
        /// <returns></returns>
        public CommandResult Execute(CommandParam commandParam)
        {
            CommandResult result = new CommandResult();
            if (this.Instance != null)
            {
                result = ((ICommand)Activator.CreateInstance(this.Instance.GetType())).Execute(commandParam);
            }
            return result;
        }
    }
    #endregion
}
