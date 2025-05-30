using System;
using System.Globalization;
using System.Windows.Data;

namespace PartCutter.Lib
{
    internal class GrayAreaSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            switch (parameter.ToString())
            {
                case "top": return values[0];
                case "bottom": return (double)values[1] - (double)values[0];
                case "left": return values[0];
                case "right": return (double)values[1] - (double)values[0];
                default: return 0;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
