using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSTL.ResultStruct;
using MSTL.Extensions;

namespace IEMS.Frame.WebUI
{
    public static class TriggerFieldExtend
    {
        #region StringValue
        public static string StringValue(this TriggerField field)
        {
            var str = field.Text;
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            return str;
        }
        public static string StringValue(this TriggerField field, string defaultValue)
        {
            var result = field.StringValue();
            if (result == null)
            {
                return defaultValue;
            }
            return result;
        }
        #endregion

        #region IntValue
        public static int? IntValue(this TriggerField field)
        {
            var str = field.StringValue( string.Empty);
            return str.IntValue();
        }
        public static int IntValue(this TriggerField field, int defaultValue)
        {
            var str = field.StringValue( string.Empty);
            return str.IntValue(defaultValue);
        }
        #endregion

        #region Int64Value
        public static Int64? Int64Value(this TriggerField field)
        {
            var str = field.StringValue( string.Empty);
            return str.Int64Value();
        }
        public static Int64 Int64Value(this TriggerField field, Int64 defaultValue)
        {
            var str = field.StringValue( string.Empty);
            return str.Int64Value(defaultValue);
        }
        #endregion

        #region DecimalValue
        public static decimal? DecimalValue(this TriggerField field)
        {
            var str = field.StringValue( string.Empty);
            return str.DecimalValue();
        }
        public static decimal DecimalValue(this TriggerField field, decimal defaultValue)
        {
            var str = field.StringValue( string.Empty);
            return str.DecimalValue(defaultValue);
        }
        #endregion

        #region DoubleValue
        public static double? DoubleValue(this TriggerField field)
        {
            var str = field.StringValue( string.Empty);
            return str.DoubleValue();
        }
        public static double DoubleValue(this TriggerField field, double defaultValue)
        {
            var str = field.StringValue( string.Empty);
            return str.DoubleValue(defaultValue);
        }
        #endregion

        #region DateTimeValue
        public static DateTime? DateTimeValue(this TriggerField field)
        {
            var str = field.StringValue( string.Empty);
            return str.DateTimeValue();
        }
        public static DateTime DateTimeValue(this TriggerField field, DateTime defaultValue)
        {
            var str = field.StringValue( string.Empty);
            return str.DateTimeValue(defaultValue);
        }
        #endregion
    }
}
