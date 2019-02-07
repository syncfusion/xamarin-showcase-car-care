using System;
using System.Globalization;
using Xamarin.Forms;

namespace CarCare
{
    /// <summary>
    /// Converter for Changing Backgroundcolor for Showing status label in ProjectPage
    /// </summary>
    public class StringToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                switch (status.ToLower())
                {
                    case "in progress":
                        return (Color)Application.Current.Resources["BlueColor"];
                    case "ready for delivery":
                        return (Color)Application.Current.Resources["YellowColor"];
                    case "hold":
                        return (Color)Application.Current.Resources["OrangeYellowColor"];
                    case "delivered":
                        return (Color)Application.Current.Resources["LeafGreenColor"];
                    default:
                        return (Color)Application.Current.Resources["GrayColor"];
                }

            }
            return Color.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.Red;
        }
    }
}