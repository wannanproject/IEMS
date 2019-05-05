using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEMS.Frame.WebUI
{
    public static class DateFieldExtend
    {
        #region DateTimeValue
        public static DateTime? DateTimeValue(this DateField field, int addDays)
        {
            var str = field.Text;
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            var result = DateTime.Now;
            if (DateTime.TryParse(str, out result))
            {
                if (result > DateTime.MinValue.AddDays(1000))
                {
                    return result.AddDays(addDays);
                }
            }
            return null;
        }
        public static DateTime DateTimeValue(this DateField field, DateTime defaultValue, int addDays)
        {
            var dtime = field.DateTimeValue();
            if (dtime == null)
            {
                if (defaultValue != null)
                {
                    return defaultValue.AddDays(addDays);
                }
            }
            return ((DateTime)dtime).AddDays(addDays);
        }
        #endregion

        #region StringValue
        private static string DateTimeStringValue(DateTime dtime)
        {
            return dtime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string StringValue(this DateField field, int addDays)
        {
            var dtime = field.DateTimeValue(addDays);
            if (dtime == null)
            {
                return null;
            }
            return DateTimeStringValue((DateTime)dtime);
        }
        public static string StringValue(this DateField field, DateTime defaultValue, int addDays)
        {
            var dtime = field.DateTimeValue(defaultValue,addDays);
            if (dtime == null)
            {
                return null;
            }
            return DateTimeStringValue((DateTime)dtime);
        }
        #endregion



        #region DateTimeValue
        public static DateTime? DateTimeValue(this DateField field)
        {
            return field.DateTimeValue(0);
        }
        public static DateTime DateTimeValue(this DateField field, DateTime defaultValue)
        {
            return field.DateTimeValue(defaultValue, 0);
        }
        #endregion

        #region StringValue
        public static string StringValue(this DateField field)
        {
            return field.StringValue(0);
        }
        public static string StringValue(this DateField field, DateTime defaultValue)
        {
            return field.StringValue(defaultValue, 0);
        }
        #endregion
    }
}
