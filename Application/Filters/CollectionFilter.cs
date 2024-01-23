using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Filters
{
    public class CollectionFilter : FiltersParent
    {
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public int? Status { get; set; }
        public List<string>? SpecId { get; set; }
        public List<string>? SpecTypeId { get; set; }
    }
}
