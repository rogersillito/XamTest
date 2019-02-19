using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamTest
{
    public class ValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var alpha = (double) value / 360;
            var color = Color.DarkGoldenrod;
            return Color.FromRgba(color.R, color.G, color.B, alpha);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Only supports forward conversion");
        }
    }
}