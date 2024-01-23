using Domain.Common;
using Domain.ProductCollection_Agg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductCollectionAggregate
{
    public class ProductSpec : BaseEntity
    {
        [ForeignKey("Product")]
        public long ProductId { get; set; }
        [ForeignKey("ProductSpecification")]
        public long SpecId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Specification ProductSpecification { get; set; }
    }
}
