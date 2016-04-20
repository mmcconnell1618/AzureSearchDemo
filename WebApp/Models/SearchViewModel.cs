using System.Collections.Generic;
using Microsoft.Azure.Search.Models;

namespace WebApp.Models
{
    public class SearchViewModel
    {
        public FacetResults Facets { get; set; }
        public IList<SearchResult<SearchDemo.Data.VehicleFlat>> Results { get; set; }
        public int? Count { get; set; }

        public string SearchQuery { get; set; }
        public int CurrentPage { get; set; }
        public string SortType { get; set; }
        public string CurrentFacets { get; set; }

        public SearchFacetsHelper FacetHelper { get; set; }

        public int YearMin { get; set; }

        public int YearMax { get; set; }

        public SearchViewModel()
        {
            SearchQuery = string.Empty;
            CurrentPage = 1;
            SortType = "miles_low";
            Count = 0;
            CurrentFacets = string.Empty;
            YearMin = 2005;
            YearMax = 2016;
            FacetHelper = new SearchFacetsHelper(string.Empty);
        }
    }
}