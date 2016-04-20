using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SearchDemo.Data
{
    public class VehicleFeature
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
