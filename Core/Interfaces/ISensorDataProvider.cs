using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    // абстракция для источника данных с датчиков
    public interface ISensorDataProvider : IDisposable
    {
        Task<IEnumerable<SensorData>> GetSensorDataAsync();
    }
}
