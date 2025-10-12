using CommunityToolkit.Mvvm.ComponentModel;
using Core.Enums;
using Core.Events;
using Core.Models;
using Sensors.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModels
{
    // класс управления данными для страницы мониторинга процессора
    public partial class CpuMonitorViewModel : PageViewModel
    {
        public override string Title => "Процессор";
        private readonly SensorMonitoringService _sensorMonitoringService;

        private ObservableCollection<SensorData> _cpuSensors = new ObservableCollection<SensorData>();  // коллекция датчиков, относящихся к процессору

        private float _cpuTemperature;

        private float _cpuLoad;

        private float _cpuPower;

        public ObservableCollection<SensorData> CpuSensors
        {
            get => _cpuSensors;
            set => SetProperty(ref _cpuSensors, value);
        }

        public float CpuTemperature
        {
            get => _cpuTemperature;
            set => SetProperty(ref _cpuTemperature, value);
        }

        public float CpuLoad
        {
            get => _cpuLoad;
            set => SetProperty(ref _cpuLoad, value);
        }

        public float CpuPower
        {
            get => _cpuPower;
            set => SetProperty(ref _cpuPower, value);
        }

        public CpuMonitorViewModel(SensorMonitoringService sensorMonitoringService)
        {
            _sensorMonitoringService = sensorMonitoringService;
            _sensorMonitoringService.SensorDataUpdated += OnSensorDataUpdated;
            _sensorMonitoringService.StartMonitoring(1000);  // обновление страницы каждую секунду
        }

        private void OnSensorDataUpdated(object sender, SensorDataUpdatedEventArgs e)
        {
            IEnumerable<SensorData> cpuSensors = e.Data.Where(s =>
        s.Name?.ToLower().Contains("cpu") == true ||
        s.ParentName?.ToLower().Contains("processor") == true ||
        s.Name?.ToLower().Contains("package") == true ||         
        s.Name?.ToLower().Contains("tdie") == true ||            
        s.Name?.ToLower().Contains("tctl") == true);

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                CpuSensors.Clear();
                var uniqueSensors = cpuSensors.GroupBy(s => s.Name)
                                    .Select(g => g.First())
                                    .ToList();
                foreach (SensorData sensor in uniqueSensors)
                {
                    CpuSensors.Add(sensor);
                }

                UpdateMainIndicators(cpuSensors);
            });
        }

        private void UpdateMainIndicators(IEnumerable<SensorData> data)
        {
            // поиск максимальной температуры процессора
            IEnumerable<SensorData> tempSensors = data.Where(s => s.Type == SensorTypes.Temperature);
            CpuTemperature = tempSensors.Any() ? tempSensors.Max(s => s.Value) : 0;

            // поиск общей загрузки процессора
            IEnumerable<SensorData> loadSensors = data.Where(s => s.Type == SensorTypes.Load && s.Name.ToLower().Contains("total"));
            CpuLoad = loadSensors.Any() ? loadSensors.Max(s => s.Value) : 0;

            // поиск мощности процессора
            IEnumerable<SensorData> powerSensors = data.Where(s => s.Type == SensorTypes.Power);
            CpuPower = powerSensors.Any() ? powerSensors.Max(s => s.Value) : 0;
        }
    }
}
