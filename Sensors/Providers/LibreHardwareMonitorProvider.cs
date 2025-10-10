using Core.Enums;
using Core.Interfaces;
using Core.Models;
using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors.Providers
{
    // реализация ISensorDataProvider, использует библиотеку LibreHardwareMonitorLib
    public class LibreHardwareMonitorProvider : ISensorDataProvider
    {
        private Computer _computer;
        private bool _isInitialized = false;

        public LibreHardwareMonitorProvider()
        {
            // настройка компонентов для мониторинга
            _computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsNetworkEnabled = false,
                IsStorageEnabled = true
            };
        }
        public Task<IEnumerable<SensorData>> GetSensorDataAsync()
        {
            if(!_isInitialized)
            {
                _computer.Open();
                _isInitialized = true;
            }

            _computer.Accept(new UpdateVisitor());  // обновление данных с датчиков

            List<SensorData> sensorData = new List<SensorData>();

            // проход по всему оборудованию и датчикам
            foreach(IHardware hardware in _computer.Hardware)
            {
                foreach(ISensor sensor in hardware.Sensors)
                {
                    if (sensor.Value.HasValue)
                    {
                        SensorData data = MapToSensorData(sensor, hardware.Name);
                        if(data != null)
                        {
                            sensorData.Add(data);
                        }
                    }
                }
            }

            return Task.FromResult<IEnumerable<SensorData>>(sensorData);
        }

        private SensorData MapToSensorData(ISensor sensor, string parentName)
        {
            SensorTypes? sensorType = MapSensorType(sensor.SensorType);
            if (sensorType == null) return null;  // пропуск типов, которые не поддерживаются приложением

            return new SensorData
            {
                Name = sensor.Name,
                Type = sensorType.Value,
                Value = sensor.Value ?? 0,
                Identifier = sensor.Identifier.ToString(),
                ParentName = parentName
            };
        }

        private SensorTypes? MapSensorType(SensorType sensorType)
        {
            switch (sensorType) 
            {
                case SensorType.Temperature: return SensorTypes.Temperature;
                case SensorType.Load: return SensorTypes.Load;
                case SensorType.Voltage: return SensorTypes.Voltage;
                case SensorType.Clock: return SensorTypes.Clock;
                case SensorType.Power: return SensorTypes.Power;
                default: return null;
            }
        }

        public void Dispose()
        {
            _computer?.Close();
            _computer = null;
        }
    }

    // visitor для обновления данных
    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }

        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware)
            {
                subHardware.Accept(this);
            }
        }

        public void VisitParameter(IParameter parameter)
        {
        }

        public void VisitSensor(ISensor sensor)
        {
        }
    }
}
