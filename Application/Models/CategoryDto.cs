using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string Img { get; set; }
        public string ParentAr { get; set; }
        public string ParentEn { get; set; }
        public IEnumerable<SubCategoryDto> Children { get; set; }
        public string ProductType { get; set; }
        public IEnumerable<string> Products { get; set; }
        public string Status { get; set; }
        public IEnumerable<SubCategoryDto> ChildrenCat { get; set; }

    }
}
