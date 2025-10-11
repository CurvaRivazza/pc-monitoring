using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp.Converters
{
    // конвертер для цвета температуры
    public class TemperatureToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float temperature)
            {
                if (temperature < 60)
                {
                    return Brushes.Green;
                }
                if (temperature >= 60 && temperature < 80)
                {
                    return Brushes.Orange;
                }
                if (temperature >= 80)
                {
                    return Brushes.Red;
                }
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
