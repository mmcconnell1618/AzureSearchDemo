using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace WebApp
{
    public class FacetSelection : IEquatable<FacetSelection>
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public bool Equals(FacetSelection other)
        {
            return (other.Name == this.Name && other.Value == this.Value);
        }
    }

    public class SearchFacetsHelper
    {
        public List<FacetSelection> Selections { get; set; }

        public SearchFacetsHelper(string currentFacets)
        {
            Selections = new List<FacetSelection>();
            ParseSelections(currentFacets);
        }

        public string SelectionsToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var s in this.Selections)
            {
                sb.Append(s.Name);
                sb.Append(":");
                sb.Append(s.Value);
                sb.Append("|");
            }

            string result = sb.ToString().TrimEnd('|');
            return result;
        }

        public string SelectionsToFilter()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var s in this.Selections)
            {
                sb.Append(SelectionToFilter(s));
            }

            return sb.ToString();
        }

        private string SelectionToFilter(FacetSelection selection)
        {
            string result = string.Empty;
            
            switch (selection.Name.ToLower())
            {
                case "isnew":
                    result = " and IsNew eq true";
                    break;
                case "isimport":
                    result = " and IsImport eq true";
                    break;
                case "miles":
                    int max = int.Parse(selection.Value) + 10000;
                    result = " and Miles ge " + selection.Value + " and Miles lt " + max; 
                    break;
                case "features":
                    result = " and Features/any(d:d eq '" + selection.Value + "')";
                    break;
                case "packages":
                    result = " and Packages/any(d:d eq '" + selection.Value + "')";
                    break;
                default:
                    result = " and " + selection.Name + " eq '" + selection.Value + "'";
                    break;
            }

            return result;
        }
        private void ParseSelections(string input)
        {
            foreach (string selection in input.Split('|'))
            {
                var parts = selection.Split(':');
                if (parts.Length < 2) continue;

                var existing = this.Selections.Where(y => y.Name == parts[0]).Where(y => y.Value == parts[1]).FirstOrDefault();
                if (existing != null) continue;

                Selections.Add(new FacetSelection() { Name = parts[0], Value = parts[1] });
            }
        }

        public void RemoveFacet(string facetToRemove)
        {
            string working = facetToRemove.Trim().TrimEnd('|').TrimStart('|');
            string[] parts = working.Split(':');
            if (parts.Length > 1)
            {
                string name = parts[0];
                string value = parts[1];
                var selection = new FacetSelection() { Name = name, Value = value };
                if (this.Selections.Contains(selection))
                {
                    this.Selections.Remove(selection);
                }
            }
        }
    }
}