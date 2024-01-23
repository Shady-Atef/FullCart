using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductSpecificationDto
    {
        public string? SpecId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? SpecTypeId { get; set; }
        public bool Checked { get; set; }
    }
}
