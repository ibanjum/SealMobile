using System;
using System.Globalization;
using Xamarin.Forms;

namespace Seal.Converters
{
    public class MenuItemHeightConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = value as ImageSource;

            if (source == null)
                return 0;

            return 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Not Implemented");
        }
    }
}
