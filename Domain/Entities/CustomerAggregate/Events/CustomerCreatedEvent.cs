using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomerAggregate.Events
{
    public class CustomerCreatedEventArgs : EventArgs, INotification
    {
        public Customer Customer { get; set; }
    }
}
