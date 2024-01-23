using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ProductCollectionAggregate
{
    public class Attachment : BaseEntity
    {
        [Key]
        public new long Id { get; set; }
        public string AttachmentPath { get; private set; }
        public int DisplayOrder { get;private set; }
        [ForeignKey("Product")]
        public long ProductId { get; private set; }
        public virtual Product Product { get; set; }
        public Attachment()
        {
                
        }
        public Attachment(string attachmentPath, int displayOrder, long createdBy, long id)
        {
            this.AttachmentPath = attachmentPath;
            this.DisplayOrder = displayOrder;
            this.CreatedBy = createdBy;
            this.ProductId = id;
        }

        internal void Delete(long deletedBy)
        {
            this.IsDeleted = true;
            this.DeletedBy = deletedBy;
            this.DeletedTime = DateTime.Now;
        }

    }
}