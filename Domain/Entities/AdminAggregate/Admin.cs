using Domain.Common;
using Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AdminAggregate
{
    public class Admin : BaseEntity
    {
        public string Mobile { get; private set; }
        public string FirstName { get; private set; }
        public string FamilyName { get; private set; }
        public string ProfilePicture { get; private set; }
        public string Email { get; private set; }

        [NotMapped]
        public string Username { get; private set; }
        [NotMapped]
        public string Password { get; private set; }


        public event EventHandler OnEvent;

        [ForeignKey("ApplicationUser")]
        public long UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
