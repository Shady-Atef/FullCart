using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ProductCollectionAggregate
{
    public class ProdColl : BaseEntity
    {
        [ForeignKey("Product")]
        public long ProductId { get; set; }
        [ForeignKey("ProductCollection")]
        public long ProductCollectionId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductCollection ProductCollection { get; set; }
    }
}