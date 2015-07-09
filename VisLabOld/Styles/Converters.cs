using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;

namespace VisLab.Styles
{
    public class StringFormattingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(parameter.ToString(), value);
        }
        #region IValueConverter Members


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(parameter.ToString(), value);
        }

        #endregion
    }

    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Brushes.Gold : Brushes.LightGoldenrodYellow;
        }
        #region IValueConverter Members


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }

        #endregion
    }

    public class PercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / 100 * double.Parse((string)parameter);
        }
        #region IValueConverter Members


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.Parse((string)parameter) / (double)value * 100;
        }

        #endregion
    }
}
