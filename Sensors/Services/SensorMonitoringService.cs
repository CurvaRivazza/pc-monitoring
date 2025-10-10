using Core.Events;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sensors.Services
{
    // главный сервис, управляет провайдером данных
    public class SensorMonitoringService
    {
        private readonly ISensorDataProvider _provider;
        private Timer _updateTimer { get; set; }
        public event EventHandler<SensorDataUpdatedEventArgs> SensorDataUpdated;  // возникает при каждом обновлении данных

        public SensorMonitoringService(ISensorDataProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        // запуск мониторинга с заданным интервалом
        public void StartMonitoring(int updateIntervalMs)
        {
            StopMonitoring();  // остановка предыдущего таймера
            _updateTimer = new Timer(async _ => await UpdateSensorDataAsync(), null, 0, updateIntervalMs);
        }

        // остановка мониторинга
        public void StopMonitoring()
        {
            _updateTimer?.Dispose();
            _updateTimer = null;
        }

        private async Task UpdateSensorDataAsync()
        {
            try
            {
                var sensorData = await _provider.GetSensorDataAsync();
                SensorDataUpdated?.Invoke(this, new SensorDataUpdatedEventArgs(sensorData));  // уведомляем подписчиков о новых данных
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении данных: {ex.Message}");
            }
        }
    }
}
