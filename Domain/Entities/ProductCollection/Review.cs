using Domain.Common;
using Domain.ProductCollectionAggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ProductCollection_Agg
{
    public class Review : BaseEntity
    {
        public string ReviewText { get; private set; }
        public float Rate { get; private set; }

        [ForeignKey("Product")]
        public long ProductId { get; set; }
        public virtual Product Product { get;private set; }

        internal void Delete(long deletedBy)
        {
            this.DeletedBy = deletedBy;
            this.DeletedTime = DateTime.Now;
            this.IsDeleted = true;
        }
    }
}