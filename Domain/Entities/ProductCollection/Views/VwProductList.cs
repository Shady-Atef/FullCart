using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ProductCollectionAggregate.Inputs;

namespace Domain.ProductCollectionAggregate.Views
{
    [Table("Vw_ProductList")]
    public class VwProductList
    {
        [Key]
        public string ProductId { get; set; }
        public decimal Price { get; set; }
        public float? Rate { get; set; }
        public decimal DiscountAmount { get; set; }
        public Status AvailabilityStatus { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string CategoryNameAr { get; set; }
        public string CategoryNameEn { get; set; }
        public string ProductNameEn { get; set; }
        public string ProductNameAr { get; set; }
        public string CategoryId { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public string SubCategoryId { get; set; }
        public string SpecId { get; set; }
        public string SpecificationsAr { get; set; }
        public string SpecificationsEn { get; set; }
        public long? SpeceficationTypesId { get; set; }
        public string SpeceficationTypesAr { get; set; }
        public string SpeceficationTypesEn { get; set; }
    }
}
