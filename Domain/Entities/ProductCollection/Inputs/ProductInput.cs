
using Domain.ProductCollection_Agg.Inputs;

namespace Domain.ProductCollectionAggregate.Inputs
{
    public class ProductInput
    {
        public long? ProductId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public decimal Price { get; set; }
        public Status AvailabilityStatus { get; set; }
        public List<long> SubCategories { get; set; }
        public int ProducerId { get; set; }
        public int TotalQty { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public string InformationEn { get; set; }
        public string DescriptionEn { get; set; }
        public string InformationAr { get; set; }
        public string DescriptionAr { get; set; }


        public void Validate()
        {
            if (true)
            {
                
            }
        }
    }
}