using System;
using System.Globalization;
using Xamarin.Forms;

namespace CarCare
{
    /// <summary>
    /// Converter for adding text to default value in DataGrid column
    /// </summary>
    public class DoubletoTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? value + "h" : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}