using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductCollectionAggregate
{
    public class ProductCategory: BaseEntity
    {
        [Key]
        public new long Id { get; set; }
        public long ProductId { get; set; }
        public long SubCategoryId { get; set; }
        public virtual SubCategory  SubCategory { get; set; }
        public virtual Product  Product { get; set; }

        public ProductCategory CreateNewProductCategory(long? productId, long subCategoryId)
        {
            this.ProductId = productId.Value;
            this.SubCategoryId = subCategoryId;
            return this;
        }
    }
}
