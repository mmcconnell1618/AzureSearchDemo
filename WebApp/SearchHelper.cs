using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Search.Models;
using Microsoft.Azure.Search;
using System.Configuration;

namespace WebApp
{
    public class SearchHelper
    {
        private static SearchServiceClient _searchClient;
        private static SearchIndexClient _indexClient;
        private static string IndexName = "";
        public static string errorMessage;

        static SearchHelper()
        {
            try
            {
                string searchServiceName = ConfigurationManager.AppSettings["SearchServiceName"];
                string apiKey = ConfigurationManager.AppSettings["SearchServiceApiKey"];
                IndexName = ConfigurationManager.AppSettings["SearchIndexName"];

                // Create an HTTP reference to the catalog index
                _searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));
                _indexClient = _searchClient.Indexes.GetClient(IndexName);

            }
            catch (Exception e)
            {
                errorMessage = e.Message.ToString();
            }
        }

        public DocumentSearchResult<SearchDemo.Data.VehicleFlat> Search(
            string searchText, 
            int yearMin,
            int yearMax,
            SearchFacetsHelper facets,
            string sortType, 
            int currentPage)
        {
            string q = searchText;
            if (string.IsNullOrWhiteSpace(q))
                q = "*";


            // Execute search based on query string
            try
            {
                SearchParameters sp = new SearchParameters()
                {
                    SearchMode = SearchMode.All, // Any = matches any param, All = matches all params
                    Top = 10,
                    Skip = currentPage - 1,
                    // Limit results
                    Select = new List<String>() {"StockNumber", "Year", "Make", "Model", "Miles",
                        "IsNew", "StoreNumber", "DateLastUpdated"},
                    IncludeTotalResultCount = true,
                    Facets = new List<String>() {
                        "Make", "Model","Miles,values:10000|20000|30000|40000|50000|60000|70000|80000",
                        "IsNew","VehicleType",/*"IsImport","Transmission","Cylinders","MPGCity","MPGHighway",*/
                        "ColorExterior","ColorInterior",/*"Size",*/"Features","Packages"},
                };

                // Define the sort type
                if (sortType == "year")
                    sp.OrderBy = new List<String>() { "Year desc" };
                else if (sortType == "make")
                    sp.OrderBy = new List<String>() { "Make" };
                else if (sortType == "miles_low")
                    sp.OrderBy = new List<String>() { "Miles asc" };
                else if (sortType == "miles_high")
                    sp.OrderBy = new List<String>() { "Miles desc" };
                else if (sortType == "mpgcity")
                    sp.OrderBy = new List<String>() { "MPGCity" };
                else if (sortType == "mpghighway")
                    sp.OrderBy = new List<String>() { "MPGHighway" };

                // Add filtering
                string filter = null;
                filter = "Year ge " + yearMin + " and Year le " + yearMax;

                filter += facets.SelectionsToFilter();

                sp.Filter = filter;

                return _indexClient.Documents.Search<SearchDemo.Data.VehicleFlat>(q, sp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error querying index: {0}\r\n", ex.Message.ToString());
            }
            return null;
        }
     
        public DocumentSuggestResult Suggest(string searchText, bool fuzzy)
        {
            // Execute search based on query string
            try
            {
                SuggestParameters sp = new SuggestParameters()
                {
                    UseFuzzyMatching = fuzzy,
                    Top = 8
                };

                return _indexClient.Documents.Suggest(searchText, "sg", sp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error querying index: {0}\r\n", ex.Message.ToString());
            }
            return null;
        }
      
    }
}