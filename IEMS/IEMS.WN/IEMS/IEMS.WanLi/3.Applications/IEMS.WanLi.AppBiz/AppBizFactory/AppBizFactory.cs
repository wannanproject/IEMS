using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.AppBiz
{
    public class AppBizFactory
    {
        private static T createInstance<T>(string dbName = null) where T : IAppBizManager
        {
            var type = typeof(T);
            var ass = type.Assembly;
            var fullName = type.FullName;
            var className = type.Name;
            fullName = fullName.Substring(0, fullName.Length - className.Length);
            className = className.Substring(1);
            fullName = fullName + className;
            var classType = ass.GetType(fullName);
            if (classType == null)
            {
                throw new Exception("类型未找到," + fullName);
            }
            var result = string.IsNullOrWhiteSpace(dbName)
                ? Activator.CreateInstance(classType)
                : Activator.CreateInstance(classType, new object[] { dbName });
            if (result == null)
            {
                throw new Exception("实例未创建" + fullName);
            }
            if (result is T)
            {
                return (T)result;
            }
            throw new Exception("类型不匹配," + fullName);
        }
        public static T CreateInstance<T>() where T : IAppBizManager
        {
            return createInstance<T>();
        }
    }
}