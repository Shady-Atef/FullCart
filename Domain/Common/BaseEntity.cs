using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; protected set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();
        public int Version { get; protected set; }
        public long? CreatedBy { get; protected set; }
        public DateTime? CreationTime { get; protected set; }
        public long? UpdatedBy { get; protected set; }
        public DateTime? UpdateTime { get; protected set; }
        public long? DeletedBy { get; protected set; }
        public DateTime? DeletedTime { get; protected set; }
        public bool IsDeleted { get; protected set; }

        protected void SetCreatedAudit(long UserID)
        {
            CreatedBy = UserID;
            CreationTime = DateTime.Now;
        }
        protected void SetUpdateAudit(long UserID)
        {
            UpdatedBy = UserID;
            UpdateTime = DateTime.Now;
        }
    }
}
