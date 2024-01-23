using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductSpecificationTypeDto
    {
        public string? SpeceficationTypesAr { get; set; }
        public string? SpeceficationTypesEn { get; set; }
        public List<ProductSpecificationDto> Specifications { get; set; }
    }
    
}
