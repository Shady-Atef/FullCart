using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductCollectionAggregate
{
    public class ProductStore : BaseEntity
    {
        [Key]
        public new long Id { get; set; }
        public decimal OriginalPrice { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public int Qty { get; private set; }

        [ForeignKey("Product")]
        public long ProductId { get; private set; }
        public virtual Product Product { get; private set; }

        public ProductStore()
        {
                
        }
        public ProductStore CreateNewProduct(int totalQty, DateTime expirationDate, decimal originalPrice)
        {
            Qty = totalQty;
            ExpirationDate = expirationDate;
            OriginalPrice = originalPrice;
            return this;
        }

        internal void Delete(long deletedBy)
        {
            this.IsDeleted = true;
            this.DeletedBy = deletedBy;
            this.DeletedTime = DateTime.Now;
        }
    }
}
