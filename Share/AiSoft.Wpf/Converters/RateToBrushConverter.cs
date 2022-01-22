using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AiSoft.Wpf.Converters
{
    public class RateToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal.TryParse(value?.ToString(), out var rate);
            if (Math.Abs(rate) < 0.000001m)
            {
                return Application.Current?.FindResource("RateDefaultBrush");
            }
            var isGreenRed = (Application.Current?.FindResource("RateBrushIsGreenRed") as bool?) == true;
            var greenBrush = Application.Current?.FindResource("RateGreenBrush") as SolidColorBrush;
            var redBrush = Application.Current?.FindResource("RateRedBrush") as SolidColorBrush;
            if (rate > 0)
            {
                return isGreenRed ? greenBrush : redBrush;
            }
            else
            {
                return isGreenRed ? redBrush : greenBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}