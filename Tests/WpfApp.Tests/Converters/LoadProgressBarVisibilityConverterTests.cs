using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Converters;

namespace Tests.WpfApp.Tests.Converters
{
    [TestFixture]
    public class LoadProgressBarVisibilityConverterTests
    {
        private LoadProgressBarVisibilityConverter _converter;

        [SetUp]
        public void Setup()
        {
            _converter = new LoadProgressBarVisibilityConverter();
        }

        [Test]
        public void Convert_SensorTypeIsLoad_ReturnsVisible()
        {
            SensorTypes sensorType = SensorTypes.Load;
            Object? result = _converter.Convert(sensorType, typeof(Visibility), null, null);
            Assert.That(result, Is.EqualTo(Visibility.Visible));
        }

        [TestCase(SensorTypes.Temperature)]
        [TestCase(SensorTypes.Voltage)]
        [TestCase(SensorTypes.Clock)]
        [TestCase(SensorTypes.Power)]
        public void Convert_SensorTypeIsNotLoad_ReturnsCollapsed(SensorTypes sensorType)
        {
            Object? result = _converter.Convert(sensorType, typeof(Visibility), null, null);
            Assert.That(result, Is.EqualTo(Visibility.Collapsed));
        }

        [TestCase("some text")]
        [TestCase(100)]
        [TestCase(45.3f)]
        [TestCase(null)]
        public void Convert_InvalidTypes_ReturnsCollapsed(Object? sensorType)
        {
            Object? result = _converter.Convert(sensorType, typeof(Visibility), null, null);
            Assert.That(result, Is.EqualTo(Visibility.Collapsed), $"Значение типа {sensorType?.GetType()} должно возвращать Collapsed");
        }

        [Test]
        public void Convert_ObjectInstance_ReturnsCollapsed()
        {
            Object sensorType = new object();
            Object? result = _converter.Convert(sensorType, typeof(Visibility), null, null);
            Assert.That(result, Is.EqualTo(Visibility.Collapsed), $"Пустой объект должен возвращать Collapsed");
        }

        [TestCase(typeof(Visibility))]
        [TestCase(typeof(object))]
        [TestCase(typeof(Enum))]
        public void Convert_DifferentTargetTypes_ReturnsBrush(Type targetType)
        {
            SensorTypes sensorType = SensorTypes.Clock;
            Object? result = _converter.Convert(sensorType, targetType, null, null);
            Assert.That(result, Is.InstanceOf<Visibility>(), $"Для типа {targetType} должен возвращаться тип Visibility");
        }
    }
}
