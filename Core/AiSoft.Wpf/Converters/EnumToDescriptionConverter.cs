using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using AiSoft.Tools.Helpers;

namespace AiSoft.Wpf.Converters
{
    public class EnumToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Enum val ? EnumHelper.GetDescription(val) : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}