using System;
using System.Windows;
using System.Windows.Data;

namespace ProgramAdmin.Converters
{
    /// <summary>
    /// Converts true to visible, and false to collapsed.
    /// </summary>
    class BoolToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool
                && ((bool)value))
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
