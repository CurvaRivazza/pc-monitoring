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
                return temperature switch
                {
                    < 0 => Brushes.Gray,
                    < 60 => Brushes.Green,
                    >= 60 and < 80 => Brushes.Orange,
                    >= 80 => Brushes.Red,
                    _ => Brushes.Gray
                };
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
