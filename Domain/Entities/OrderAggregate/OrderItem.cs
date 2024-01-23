using Domain.Common;
using Domain.CustomerAggregate;
using Domain.ProductCollectionAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        [ForeignKey("Order")]
        public long OrderID { get; set; }
        [ForeignKey("Product")]
        public long ProductID { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        #region Linked Entities
        public virtual Order Order { get; set; }
        public virtual Product Product { get; private set; }
        #endregion
        public OrderItem()
        {

        }
        public OrderItem(long orderItemID, long productID, int quantity, decimal unitPrice)
        {
            if (productID <= 0)
            {
                throw new ArgumentNullException(nameof(productID));
            }
            Id = orderItemID;
            ProductID = productID;
            SetQuantity(quantity);
            SetUnitPrice(unitPrice);
        }

        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            Quantity = quantity;
        }

        public void SetUnitPrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
            {
                throw new ArgumentException("Unit price must be greater than zero.");
            }

            UnitPrice = unitPrice;
        }

        public decimal CalculateTotal()
        {
            return Quantity * UnitPrice;
        }
    }

}
