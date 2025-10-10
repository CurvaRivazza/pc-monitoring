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
        public float Value { get; set; }  // текущее значение
        public string Identifier { get; set; }  // идентификатор датчика в системе
        public string ParentName { get; set; }  // название компонента владельца
    }
}
