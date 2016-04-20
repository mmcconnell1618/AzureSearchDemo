using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDemo.Data
{
    public class DemoDataBuilder
    {
        List<string> PossibleFeatures = new List<string>() { "2WD", "4WD", "3rd Row Seat", "AC", "ABS Brakes", "Auto-Cruise Control", "Self Driving", "Bio-Weapon Defense Mode", "Electric Motors", "Leather Seats", "Blind Spot Monitor", "Cloth Seats", "Navigation System", "Power Locks", "Sunroof", "Cruise Control", "Power Seats", "Seat Heaters", "Remote Start", "Bluetooth", "Run Flat Tires", "Satellite Radio", "Tow Hitch" };
        List<string> PossiblePackages = new List<string>() { "Luxury Edition", "Touring Edition", "Winter Package", "Sport Package", "California Emission", "Comfort Package", "Elite Package", "Limited Edition", "Signature Series", "Platinum Edition", "Ultimate Package" };
        List<VehicleMakeInfo> PossibleMakes = VehicleMakeInfo.AllMakes();
        List<string> PossibleVehicleTypes = new List<string>() { "Convertible", "Coupe", "Crossover", "Diesel Engine", "Hybrid", "Luxury", "Van", "Pickup Truck", "Sedan", "Sport Utility", "Sports Car" };
        List<string> PossibleTransmissions = new List<string>() { "Automatic", "Manual" };
        List<string> PossibleColors = new List<string>() { "Red", "Green", "Blue", "White", "Silver", "Black", "Taupe", "Brown", "Purple", "Olive", "Magenta", "Plaid", "Teal" };
        List<string> PossibleSizes = new List<string>() { "Compact", "Full-Size", "Mid-Size" };

        private SqlContext db = new SqlContext();
        private Utilities utils = new Utilities();

        public DemoDataBuilder()
        {

        }

        public void CreateRandomVehicles(int howMany, int startingStockNumber)
        {
            for (int i = 0; i < howMany; i++)
            {
                CreateRandomVehicle(startingStockNumber + i);
                if (i % 10 == 0) { Console.Write("."); }
            }
        }

        public void CreateRandomVehicle(int stockNumber)
        {
            var v = new Vehicle();
            v.ColorExterior = RandomListItem(this.PossibleColors);
            v.ColorInterior = RandomListItem(this.PossibleColors);
            v.Cylinders = RandomCylinders();
            v.DateLastUpdated = DateTime.UtcNow;
            v.IsImport = utils.RandomInteger(0, 1) == 1;
            v.IsNew = utils.RandomInteger(0, 1) == 1; ;

            var make = GetRandomMake();
            v.Make = make.Make;
            v.Model = RandomModel(make);

            v.Miles = utils.RandomInteger(5, 120000);
            v.MPGCity = utils.RandomInteger(10, 45);
            v.MPGHighway = utils.RandomInteger(20, 65);
            v.StockNumber = utils.PadStringLeft(stockNumber.ToString(),10,"0");
            v.StoreNumber = utils.RandomInteger(1,150);
            v.Transmission = RandomListItem(this.PossibleTransmissions);

            AddRandomFeatures(v, this.PossibleFeatures);
            AddRandomPackages(v, this.PossiblePackages);

            v.VehicleSize = RandomListItem(this.PossibleSizes);
            v.VehicleType = RandomListItem(this.PossibleVehicleTypes);
            v.Year = utils.RandomInteger(2007, 2016);

            db.Vehicles.Add(v);
            db.SaveChanges();
        }

        public void RandomMakes()
        {
            Dictionary<string, int> makeCounts = new Dictionary<string, int>();

            foreach(var make in this.PossibleMakes)
            {
                makeCounts.Add(make.Make, 0);
            }

            for (int i = 0; i < 100000;i++)
            {
                var m = GetRandomMake();
                makeCounts[m.Make] += 1;
            }

            foreach(var make in this.PossibleMakes)
            {
                Console.WriteLine(makeCounts[make.Make] + "\t" + make.Make);
            }
        }
        private string RandomListItem(List<string> possible)
        {
            var index = utils.RandomInteger(0, possible.Count - 1);
            try
            {
                return possible[index];
            }
            catch
            {
                return string.Empty;
            }
        }

        private void AddRandomFeatures(Vehicle v, List<string> possibles)
        {
            var howMany = utils.RandomInteger(1, 9);
            List<string> addedFeatures = new List<string>();

            for(int i = 0; i < howMany;i++)
            {
                string f = RandomListItem(possibles);
                if (addedFeatures.Contains(f) == false)
                {
                    v.VehicleFeatures.Add(new VehicleFeature() { Description = f });
                }
            }
        }

        private void AddRandomPackages(Vehicle v, List<string> possibles)
        {
            var howMany = utils.RandomInteger(1, 8);

            // Some vehicles won't have any packages
            if (howMany < 5) return;

            List<string> addedPackages = new List<string>();

            for (int i = 5; i < howMany; i++)
            {
                string f = RandomListItem(possibles);
                if (addedPackages.Contains(f) == false)
                {
                    v.VehiclePackages.Add(new VehiclePackage() { Description = f });
                }
            }
        }

        private int RandomCylinders()
        {
            var cyl = utils.RandomInteger(1, 6);
            switch (cyl)
            {
                case 1:
                    return 4;
                case 2:
                    return 5;
                case 3:
                    return 6;
                case 4:
                    return 8;
                case 5:
                    return 10;
                default:
                    return 4;
            }            
        }

        private VehicleMakeInfo GetRandomMake()
        {
            int max = this.PossibleMakes.Count;
            int index = utils.RandomInteger(0, max - 1);
            try
            {
                return this.PossibleMakes[index];
            }
            catch
            {
                return this.PossibleMakes.First();
            }
        }

        private string RandomModel(VehicleMakeInfo make)
        {
            int max = make.Models.Count;
            int index = utils.RandomInteger(0, max - 1);
            try
            {
                return make.Models[index];
            }
            catch
            {
                return make.Models.First();
            }
        }
    }
}
