using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Filters
{
    public class FiltersParent
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 10;
        public SortDirection? SortBy { get; set; } = SortDirection.Des;
    }
}
