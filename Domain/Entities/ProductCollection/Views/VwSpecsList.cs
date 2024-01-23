using Domain.ProductCollectionAggregate.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductCollectionAggregate.Views
{
    [Table("Vw_SpecsList")]
    public class VwSpecsList
    {
        [Key]
        public string ProductId { get; set; }
        public decimal Price { get; set; }
        public Status AvailabilityStatus { get; set; }
        //public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string SpecificationsAr { get; set; }
        public string SpecificationsEn { get; set; }
        public string SpeceficationTypesId { get; set; }
        public string SpeceficationTypesIdV2 { get; set; }
        public string SpeceficationTypesAr { get; set; }
        public string SpeceficationTypesEn { get; set; }
        public bool IsDeleted { get; set; }
        public string SpecId { get; set; }
        //public string SpecId { get; set; }
        public string SpecIdV2 { get; set; }
        //public string SpecTypeId { get; set; }
    }
}
