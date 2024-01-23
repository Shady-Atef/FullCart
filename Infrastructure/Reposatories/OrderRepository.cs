using Domain.OrderAggregate;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reposatories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public FullCartContext Context { get; }

        public OrderRepository(FullCartContext context) : base(context)
        {
            this.Context = context;
        }

        public void AddOrder(Order order)
        {
            Context.Orders.Add(order);
        }

        public Order GetOrderById(long orderID)
        {
            return Context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.Id == orderID)
                .FirstOrDefault();
        }
    }
}
