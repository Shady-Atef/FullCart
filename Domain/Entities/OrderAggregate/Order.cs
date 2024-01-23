using Domain.Common;
using Domain.CustomerAggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.OrderAggregate
{
    public class Order : BaseEntity
    {
        public OrderStatus Status { get; private set; }
        [ForeignKey("Customer")]
        public long CustomerId { get; private set; }
        public virtual Customer Customer { get; private set; }
        public DateTime OrderDate { get; private set; }

        public virtual List<OrderItem> OrderItems { get; private set; }
        public Order()
        {

        }
        public Order(long customerId, long orderId)
        {
            if (customerId <= 0)
            {
                throw new ArgumentNullException(nameof(customerId));
            }
            Id = orderId;
            CustomerId = customerId;
            Status = OrderStatus.Placed;
            OrderDate = DateTime.UtcNow;
            OrderItems = new List<OrderItem>();
        }

        public void AddOrderItem(long orderItemID, long productID, int quantity, decimal unitPrice)
        {
            var orderItem = new OrderItem(orderItemID, productID, quantity, unitPrice);
            OrderItems.Add(orderItem);
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            // Add any business rules or validation logic for status transitions

            Status = newStatus;
        }

        public decimal CalculateTotal()
        {
            return OrderItems.Sum(item => item.CalculateTotal());
        }
    }
}
