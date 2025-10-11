using CommunityToolkit.Mvvm.ComponentModel;
using Core.Enums;
using Core.Events;
using Core.Models;
using Sensors.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        [ObservableProperty]
        private ObservableCollection<SensorData> _cpuSensors = new ObservableCollection<SensorData>();  // коллекция датчиков, относящихся к процессору

        [ObservableProperty]
        private float _cpuTemperature;

        [ObservableProperty]
        private float _cpuLoad;

        [ObservableProperty]
        private float _cpuPower;

        public CpuMonitorViewModel(SensorMonitoringService sensorMonitoringService)
        {
            _sensorMonitoringService = sensorMonitoringService;
            _sensorMonitoringService.SensorDataUpdated += OnSensorDataUpdated;
            _sensorMonitoringService.StartMonitoring(1000);  // обновление страницы каждую секунду
        }

        private void OnSensorDataUpdated(object sender, SensorDataUpdatedEventArgs e)
        {

        }

        private void UpdateMainIndicators(IEnumerable<SensorData> data)
        {
            // поиск максимальной температуры процессора
            IEnumerable<SensorData> tempSensors = data.Where(s => s.Type == SensorTypes.Temperature);
            _cpuTemperature = tempSensors.Any() ? tempSensors.Max(s => s.Value) : 0;

            // поиск общей загрузки процессора
            IEnumerable<SensorData> loadSensors = data.Where(s => s.Type == SensorTypes.Load && s.Name.ToLower().Contains("total"));
            _cpuLoad = loadSensors.Any() ? loadSensors.Max(s => s.Value) : 0;

            // поиск мощности процессора
            IEnumerable<SensorData> powerSensors = data.Where(s => s.Type == SensorTypes.Power);
            _cpuPower = powerSensors.Any() ? powerSensors.Max(s => s.Value) : 0;
        }
    }
}
