using Domain.Common;
using Domain.CustomerAggregate.Events;
using Domain.CustomerAggregate.Inputs;
using Domain.Entities.UserAggregate;
using Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomerAggregate
{
    public class Customer : BaseEntity
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

        private readonly List<Order> _Order = new();
        [BackingField(nameof(_Order))]
        public virtual IReadOnlyList<Order> Orders => _Order.ToList();


        public void AddNewCustomer(AddCustomerInput customer)
        {
            ProfilePicture = customer.ProfilePicture;
            FirstName = customer.FirstName;
            FamilyName = customer.FamilyName;
            Mobile = customer.Mobile;
            Email = customer.Email;
            Password = customer.Password;
            Username = customer.UserName;
            Id = (int)customer.CustomerId;
            UserId = customer.UserId;
            SetCreatedAudit(customer.UserId);

            OnEvent?.Invoke(this, new CustomerCreatedEventArgs
            {
                Customer = this,
            });
        }
    }
}
