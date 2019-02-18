using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamTest
{
    public class ValueToAngleStringReverseBindingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0d;  // IGNORED for our use-case!
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // we're only really interested in this conversion direction: it's for a one-way reverse binding!
            var strVal = value ?? "0";
            return string.Format($"{strVal:0}°");
        }
    }
}