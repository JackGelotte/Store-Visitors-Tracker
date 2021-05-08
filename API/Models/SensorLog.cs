using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public enum Direction
    {
        Enter, Exit
    }
    public class SensorLog
    {
        
        public int Id { get; set; }
        public Direction Direction { get; set; }
        public StoreSection EnterStoreSection { get; set; }
        public StoreSection ExitStoreSection { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
