using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    // модель для показания датчика
    public class SensorData
    {
        public string Name { get; set; }
        public SensorTypes Type { get; set; }
        float Value { get; set; }  // текущее значение
        string Identifier { get; set; }  // идентификатор датчика в системе
        string ParentName { get; set; }  // название компонента владельца
    }
}
