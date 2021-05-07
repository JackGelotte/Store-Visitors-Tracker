using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class SensorLog
    {
        
        public int Id { get; set; }
        [InverseProperty("Enter")]
        public StoreSection Enter { get; set; }
        [InverseProperty("Exit")]
        public StoreSection Exit { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
