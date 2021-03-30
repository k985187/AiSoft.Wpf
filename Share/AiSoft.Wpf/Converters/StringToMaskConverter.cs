using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using AiSoft.Tools.Extensions;

namespace AiSoft.Wpf.Converters
{
    public class StringToMaskConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string val && !string.IsNullOrWhiteSpace(val) ? val.Mask() : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}