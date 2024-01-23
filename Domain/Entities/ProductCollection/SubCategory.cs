using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ProductCollectionAggregate
{
    public class SubCategory : BaseEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        [ForeignKey("Category")]
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual IReadOnlyList<ProductCategory> ProductCategory => _ProductCategory.Where(s => !s.IsDeleted).ToList();
        private readonly List<ProductCategory> _ProductCategory = new();


    }
}