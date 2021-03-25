using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AiSoft.Wpf.Converters
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string val && !string.IsNullOrWhiteSpace(val);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}