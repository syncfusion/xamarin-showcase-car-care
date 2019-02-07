using System;
using System.Globalization;
using Xamarin.Forms;

namespace CarCare
{
    /// <summary>
    /// Converter for TextColor of Project status in ProjectPage
    /// </summary>
    public class StringToTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                switch (status.ToLower())
                {
                    case "ready for delivery":
                        return Color.Black;
                    default:
                        return Color.White;
                }
            }
            return Color.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.Red;
        }
    }
}