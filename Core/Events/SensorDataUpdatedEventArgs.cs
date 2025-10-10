using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    // событие, возникающее при каждом обновлении данных
    public class SensorDataUpdatedEventArgs : EventArgs
    {
        public IEnumerable<SensorData> Data { get; }
        
        public SensorDataUpdatedEventArgs(IEnumerable<SensorData> data) 
        {
            Data = data;
        }
    }
}
