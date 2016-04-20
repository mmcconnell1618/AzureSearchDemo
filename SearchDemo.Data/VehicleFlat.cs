using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SearchDemo.Data
{
    public class VehicleFlat
    {
        public VehicleFlat()
        {
            Features = new List<string>();
            Packages = new List<string>();
        }

        public string StockNumber { get; set; }
        public int? Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Miles { get; set; }
        public bool? IsNew { get; set; }
        public string VehicleType { get; set; }
        public int? StoreNumber { get; set; }
        public bool? IsImport { get; set; }
        public string Transmission { get; set; }
        public int? Cylinders { get; set; }
        public int? MPGCity { get; set; }
        public int? MPGHighway { get; set; }
        public string ColorInterior { get; set; }
        public string ColorExterior { get; set; }
        public string Size { get; set; }

        public List<string> Features { get; set; }
        public List<string> Packages { get; set; }
        public DateTimeOffset DateLastUpdated { get; set; }
    }
}
