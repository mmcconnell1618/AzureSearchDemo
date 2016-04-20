using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SearchDemo.Data
{
    public class Vehicle
    {
        public Vehicle()
        {
            VehicleFeatures = new HashSet<VehicleFeature>();
            VehiclePackages = new HashSet<VehiclePackage>();
        }

        [Required]
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string StockNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Miles {get;set;}
        public bool IsNew { get; set; }
        public string VehicleType { get; set; }
        public int StoreNumber { get; set; }
        public bool IsImport { get; set; }
        public string Transmission { get; set; }
        public int Cylinders { get; set; }
        public int MPGCity { get; set; }
        public int MPGHighway { get; set; }
        public string ColorInterior { get; set; }
        public string ColorExterior { get; set; }
        public string VehicleSize { get; set; }

        public virtual ICollection<VehicleFeature> VehicleFeatures { get; set; }
        public virtual ICollection<VehiclePackage> VehiclePackages { get; set; }
        public DateTime DateLastUpdated { get; set; }

        // Converts the SQL Relational Model of a vehicle into
        // a flat model suitable for Azure Search
        public VehicleFlat ToFlat()
        {
            VehicleFlat v = new VehicleFlat();
            v.ColorExterior = this.ColorExterior;
            v.ColorInterior = this.ColorInterior;
            v.Cylinders = this.Cylinders;
            v.DateLastUpdated = this.DateLastUpdated;
            v.IsImport = this.IsImport;
            v.IsNew = this.IsNew;
            v.Make = this.Make;
            v.Miles = this.Miles;
            v.Model = this.Model;
            v.MPGCity = this.MPGCity;
            v.MPGHighway = this.MPGHighway;
            v.StockNumber = this.StockNumber;
            v.StoreNumber = this.StoreNumber;
            v.Transmission = this.Transmission;
            v.Size = this.VehicleSize;
            v.VehicleType = this.VehicleType;
            v.Year = this.Year;
            foreach(var f in this.VehicleFeatures)
            {
                v.Features.Add(f.Description);
            }
            foreach(var p in this.VehiclePackages)
            {
                v.Packages.Add(p.Description);
            }

            return v;
        }
    }
}
