using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure.Search;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private SearchHelper _searchHelper = new SearchHelper();

        public ActionResult Index(string q = "", string sortType = "miles_low", int currentPage = 1)
        {
            var facetHelper = new SearchFacetsHelper("");

            var response = _searchHelper.Search(q, 2005, 2016, facetHelper, sortType, currentPage);
            
            var model = new SearchViewModel() { Results = response.Results,
                Facets = response.Facets,
                Count = Convert.ToInt32(response.Count),
                CurrentPage = currentPage,
                SortType = sortType,
                YearMin = 2005,
                YearMax = 2016,
                SearchQuery = q,
                FacetHelper = facetHelper
                };

            return View(model);
        }
    
        [HttpPost]
        public ActionResult Search(string q, 
            int yearMin,
            int yearMax, 
            string currentFacets, 
            string facetToRemove,
            string sortType, 
            int currentPage = 1)
        {
            var facetHelper = new SearchFacetsHelper(currentFacets);
            if (facetToRemove.Trim().Length > 0) facetHelper.RemoveFacet(facetToRemove);

            var response = _searchHelper.Search(q, yearMin, yearMax, facetHelper, sortType, currentPage);

            var model = new SearchViewModel() {
                Results = response.Results,
                Facets = response.Facets,
                Count = Convert.ToInt32(response.Count),
                CurrentPage = currentPage,
                CurrentFacets = facetHelper.SelectionsToString(),
                YearMin = yearMin,
                YearMax = yearMax,
                SortType = sortType,
                SearchQuery = q,
                FacetHelper = facetHelper
            };

            return View("Index", model);            
        }

        [HttpGet]
        public ActionResult Suggest(string term, bool fuzzy = true)
        {
            // Call suggest query and return results
            var response = _searchHelper.Suggest(term, fuzzy);
            List<string> suggestions = new List<string>();
            foreach (var result in response.Results)
            {
                suggestions.Add(result.Text);
            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = suggestions
            };

        }
    }
}