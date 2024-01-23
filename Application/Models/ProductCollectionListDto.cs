using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductCollectionListDto
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public float? Rate { get; set; }
        public decimal Discount { get; set; }
        public AvailabilityStatusEnumDto AvailabilityStatus { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string CategoryId { get; set; }
        public string Img { get; set; }
        public string CategoryAr { get; set; }
        public string CategoryEn { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public decimal DiscountAmount { get; set; }
        //public string CategoryNameAr { get; set; }
        //public string CategoryNameEn { get; set; }
        //public string ProductNameEn { get; set; }
        //public string ProductNameAr { get; set; }
        //public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public string SubCategoryId { get; set; }

    }
}
