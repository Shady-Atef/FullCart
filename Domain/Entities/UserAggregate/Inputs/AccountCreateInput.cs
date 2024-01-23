using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserAggregate.Inputs
{
    public class AccountCreateInput
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
        public object DefaultLanguageID { get; set; }
        public string PhoneNumber { get; set; }
        public long? CreatedBy { get; set; }
        public bool UserIsExist { get; set; }
        public string FullName { get; set; }
    }
}
