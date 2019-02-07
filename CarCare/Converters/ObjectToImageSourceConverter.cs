using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace CarCare
{
    /// <summary>
    /// Converter for setting last taken in project
    /// </summary>
    public class ObjectToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dummyPhotoPath = (Device.Idiom == TargetIdiom.Phone) ? "Car1.jpg" : "Car1Tablet.jpg";
            return value is List<Photo> carPhotos && carPhotos.Count > 0 ? carPhotos[carPhotos.Count - 1].CarPhotoPath : dummyPhotoPath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}