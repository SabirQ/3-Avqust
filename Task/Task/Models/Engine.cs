using System.Collections.Generic;
using Task.Models.Base;

namespace Task.Models
{
    public class Engine:BaseEntity
    {
        public string Name { get; set; }
        public short HP { get; set; }
        public string Value { get; set; }
        public string Torque { get; set; }
        public List<Car> Cars { get; set; }

    }
}
