using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfApp.Converters;

namespace Tests.WpfApp.Tests.Converters
{
    [TestFixture]
    public class TemperatureToColorConverterTests
    {
        private TemperatureToColorConverter _converter;

        [SetUp]
        public void Setup()
        {
            _converter = new TemperatureToColorConverter();
        }

        [TestCase(0f)]
        [TestCase(30f)]
        [TestCase(45.5f)]
        [TestCase(59.9f)]
        public void Convert_TemperatureIsLow_ReturnsGreen(float temperature)
        {
            Object? result = _converter.Convert(temperature, typeof(Brush), null, null);

            Assert.That(result, Is.EqualTo(Brushes.Green), $"Температура {temperature}°C должна возвращать зеленый цвет");
        }

        [TestCase(60f)]
        [TestCase(65.5f)]
        [TestCase(70f)]
        [TestCase(79.9f)]
        public void Convert_TemperatureIsMedium_ReturnsOrange(float temperature)
        {
            Object? result = _converter.Convert(temperature, typeof(Brush), null, null);

            Assert.That(result, Is.EqualTo(Brushes.Orange), $"Температура {temperature}°C должна возвращать оранжевый цвет");
        }

        [TestCase(80f)]
        [TestCase(85.5f)]
        [TestCase(95f)]
        [TestCase(100f)]
        [TestCase(120f)]
        public void Convert_TemperatureIsHigh(float temperature)
        {
            Object? result = _converter.Convert(temperature, typeof(Brush), null, null);
            Assert.That(result, Is.EqualTo(Brushes.Red), $"Температура {temperature}°C должна возвращать оранжевый цвет");
        }

        [TestCase(-10f)]
        [TestCase(-1f)]
        [TestCase(-0.1f)]
        public void Convert_TemperatureIsNegative_ReturnsGray(float temperature)
        {
            Object? result = _converter.Convert(temperature, typeof(Brush), null, null);
            Assert.That(result, Is.EqualTo(Brushes.Gray), $"Температура {temperature}°C должна возвращать серый цвет");
        }
    }
}
