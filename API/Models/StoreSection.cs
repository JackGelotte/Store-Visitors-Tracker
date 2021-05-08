using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class StoreSection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [InverseProperty("EnterStoreSection")]
        public ICollection<SensorLog> EnterSensorLog { get; set; }
        [InverseProperty("ExitStoreSection")]
        public ICollection<SensorLog> ExitSensorLog { get; set; }

    }
}
