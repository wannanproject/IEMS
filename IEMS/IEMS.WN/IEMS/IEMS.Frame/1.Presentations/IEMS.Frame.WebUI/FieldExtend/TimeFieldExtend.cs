using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEMS.Frame.WebUI
{
    public static class TimeFieldExtend
    {
        #region DateTimeValue
        public static DateTime? DateTimeValue(this TimeField field, DateField dateField)
        {
            if (dateField == null)
            {
                return null;
            }
            var dateNull = dateField.DateTimeValue();
            if (dateNull == null)
            {
                return null;
            }
            var date = (DateTime)dateNull;
            var str = field.Text;
            if (string.IsNullOrWhiteSpace(str))
            {
                return date.Date;
            }
            var result = date;
            str = date.ToString("yyyy-MM-dd") + " " + str.Trim();
            if (DateTime.TryParse(str, out result))
            {
                return result;
            }
            return date.Date;
        }
        public static DateTime DateTimeValue(this TimeField field, DateTime defaultValue, DateField dateField)
        {
            if (dateField == null)
            {
                return defaultValue;
            }
            var dtime = field.DateTimeValue(dateField);
            if (dtime == null)
            {
                return defaultValue;
            }
            return (DateTime)dtime;
        }
        #endregion

        #region StringValue
        private static string DateTimeStringValue(DateTime dtime)
        {
            return dtime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string StringValue(this TimeField field, DateField dateField)
        {
            var dtime = field.DateTimeValue(dateField);
            if (dtime == null)
            {
                return null;
            }
            return DateTimeStringValue((DateTime)dtime);
        }
        public static string StringValue(this TimeField field, DateTime defaultValue, DateField dateField)
        {
            var dtime = field.DateTimeValue(defaultValue, dateField);
            if (dtime == null)
            {
                return null;
            }
            return DateTimeStringValue((DateTime)dtime);
        }
        #endregion
    }
}
