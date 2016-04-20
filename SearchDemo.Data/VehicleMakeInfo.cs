using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDemo.Data
{
    public class VehicleMakeInfo
    {
        public string Make { get; set; }
        public List<string> Models { get; set; }

        public static List<VehicleMakeInfo> AllMakes()
        {
            List<VehicleMakeInfo> result = new List<VehicleMakeInfo>();
            result.Add(VehicleMakeInfo.Acura());
            result.Add(VehicleMakeInfo.Audi());
            result.Add(VehicleMakeInfo.BMW());
            result.Add(VehicleMakeInfo.Cadillac());
            result.Add(VehicleMakeInfo.Chevy());
            result.Add(VehicleMakeInfo.Chrysler());
            result.Add(VehicleMakeInfo.Dodge());
            result.Add(VehicleMakeInfo.Fiat());
            result.Add(VehicleMakeInfo.Ford());
            result.Add(VehicleMakeInfo.GMC());
            result.Add(VehicleMakeInfo.Honda());
            result.Add(VehicleMakeInfo.Lexus());
            result.Add(VehicleMakeInfo.Mini());
            result.Add(VehicleMakeInfo.Porsche());
            result.Add(VehicleMakeInfo.Smart());
            result.Add(VehicleMakeInfo.Toyota());
            result.Add(VehicleMakeInfo.Volvo());
            return result;
        }
        public static VehicleMakeInfo Acura()
        {
            return new VehicleMakeInfo() { Make = "Acura",
                Models = new List<string>() { "ILX", "MDX", "RDX", "TL", "TLX","NSX","RLX","RL" } };
        }

        public static VehicleMakeInfo Audi()
        {
            return new VehicleMakeInfo()
            {
                Make = "Audi",
                Models = new List<string>() { "A3","A4","A5","A6","A7","A8","Q3","Q5" }
            };
        }

        public static VehicleMakeInfo BMW()
        {
            return new VehicleMakeInfo()
            {
                Make = "BMW",
                Models = new List<string>() { "128","135","228","320","325","328","335","428","525","528","740","750","M3","M5","X3","X5","Z4" }
            };
        }

        public static VehicleMakeInfo Cadillac()
        {
            return new VehicleMakeInfo()
            {
                Make = "Cadillac",
                Models = new List<string>() { "ATS","CTS","Escalade","SRX","STS" }
            };
        }

        public static VehicleMakeInfo Chevy()
        {
            return new VehicleMakeInfo()
            {
                Make = "Chevy",
                Models = new List<string>() { "Camaro","Corvette","Cavalier","Cruze","Volt","HHR","Malibu","Impala","Silverado","Suburban","Traverse","Trax" }
            };
        }

        public static VehicleMakeInfo Chrysler()
        {
            return new VehicleMakeInfo()
            {
                Make = "Chrysler",
                Models = new List<string>() { "300","Pacifica","Town and Country","Sebring","PT Cruiser" }
            };
        }

        public static VehicleMakeInfo Dodge()
        {
            return new VehicleMakeInfo()
            {
                Make = "Dodge",
                Models = new List<string>() { "Avenger","Caravan","Challenger","Charger","Dart","Durango","RAM 1500","RAM 2500","RAM 3500" }
            };
        }

        public static VehicleMakeInfo Fiat()
        {
            return new VehicleMakeInfo()
            {
                Make = "Fiat",
                Models = new List<string>() { "500","500E","500L","500X" }
            };
        }

        public static VehicleMakeInfo Ford()
        {
            return new VehicleMakeInfo()
            {
                Make = "Ford",
                Models = new List<string>() { "Crown Vic","Escape","Expedition","Explorer","F150","F250","F350","Focus","Fiesta","Mustang","Taurus" }
            };
        }

        public static VehicleMakeInfo GMC()
        {
            return new VehicleMakeInfo()
            {
                Make = "GMC",
                Models = new List<string>() { "Acadia","Canyon","Envoy","Sierra 1500","Sierra 2500","Terrain","Yukon" }
            };
        }

        public static VehicleMakeInfo Honda()
        {
            return new VehicleMakeInfo()
            {
                Make = "Honda",
                Models = new List<string>() { "Accord","Civic","CR-V","Fit","Insight","Odyssey","Pilot" }
            };
        }

        public static VehicleMakeInfo Lexus()
        {
            return new VehicleMakeInfo()
            {
                Make = "Lexus",
                Models = new List<string>() { "ES 300","ES 350","GS 300","GS 350", "GS 450","IS 250","IS 300","LS 430","LX 460","LX 470","LX 570" }
            };
        }

        public static VehicleMakeInfo Porsche()
        {
            return new VehicleMakeInfo()
            {
                Make = "Porsche",
                Models = new List<string>() { "911","Boxster","Cayenne","Cayman","Macan","Panamera" }
            };
        }

        public static VehicleMakeInfo Toyota()
        {
            return new VehicleMakeInfo()
            {
                Make = "Toyota",
                Models = new List<string>() { "4Runner","Avalon","Camry","Corolla","FJ Cruiser", "Highlander","Prius","Rav4","Sienna","Yaris" }
            };
        }

        public static VehicleMakeInfo Volvo()
        {
            return new VehicleMakeInfo()
            {
                Make = "Volvo",
                Models = new List<string>() { "C30","C70","S40","S60","S80","V60","V70","XC60","XC90" }
            };
        }

        public static VehicleMakeInfo Smart()
        {
            return new VehicleMakeInfo()
            {
                Make = "Smart",
                Models = new List<string>() { "Fortwo" }
            };
        }

        public static VehicleMakeInfo Mini()
        {
            return new VehicleMakeInfo()
            {
                Make = "Mini",
                Models = new List<string>() { "Cooper","Cooper Coupe","Cooper Roadster","Cooper Clubman","Cooper Paceman" }
            };
        }
        
    }
}
