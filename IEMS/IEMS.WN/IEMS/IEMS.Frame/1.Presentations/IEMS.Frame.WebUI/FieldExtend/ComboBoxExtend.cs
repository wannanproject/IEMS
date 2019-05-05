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
    public static class ComboBoxExtend
    {
        #region StringValue
        public static string StringValue(this ComboBox field)
        {
            var str = field.Text;
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            return str;
        }
        public static string StringValue(this ComboBox field, string defaultValue)
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
        public static int? IntValue(this ComboBox field)
        {
            var str = field.StringValue( string.Empty);
            return str.IntValue();
        }
        public static int IntValue(this ComboBox field, int defaultValue)
        {
            var str = field.StringValue( string.Empty);
            return str.IntValue(defaultValue);
        }
        #endregion

        #region Int64Value
        public static Int64? Int64Value(this ComboBox field)
        {
            var str = field.StringValue( string.Empty);
            return str.Int64Value();
        }
        public static Int64 Int64Value(this ComboBox field, Int64 defaultValue)
        {
            var str = field.StringValue( string.Empty);
            return str.Int64Value(defaultValue);
        }
        #endregion

        #region DecimalValue
        public static decimal? DecimalValue(this ComboBox field)
        {
            var str = field.StringValue( string.Empty);
            return str.DecimalValue();
        }
        public static decimal DecimalValue(this ComboBox field, decimal defaultValue)
        {
            var str = field.StringValue( string.Empty);
            return str.DecimalValue(defaultValue);
        }
        #endregion

        #region DoubleValue
        public static double? DoubleValue(this ComboBox field)
        {
            var str = field.StringValue( string.Empty);
            return str.DoubleValue();
        }
        public static double DoubleValue(this ComboBox field, double defaultValue)
        {
            var str = field.StringValue( string.Empty);
            return str.DoubleValue(defaultValue);
        }
        #endregion

        #region DateTimeValue
        public static DateTime? DateTimeValue(this ComboBox field)
        {
            var str = field.StringValue( string.Empty);
            return str.DateTimeValue();
        }
        public static DateTime DateTimeValue(this ComboBox field, DateTime defaultValue)
        {
            var str = field.StringValue( string.Empty);
            return str.DateTimeValue(defaultValue);
        }
        #endregion


        #region ComboBoxSelect
        public static void SelectFirst(this ComboBox field)
        {
            if (field!=null && field.Items!=null && field.Items.Count > 0)
            {
                field.Value = field.Items[0].Value;
            }
        }
        public static void SelectValue(this ComboBox field, string value)
        {
            if (value==null)
            {
                value = string.Empty;
            }
            if (field != null && field.Items != null && field.Items.Count > 0)
            {
                foreach(var item in field.Items)
                {
                    if (item.Value != null && item.Value.ToLower() == value.ToLower())
                    {
                        field.Value = item.Value;
                        return;
                    }
                }
            }
        }
        #endregion
    }
}
