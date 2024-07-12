using System;

namespace AwesomeShop.Services.Orders.Core.Entities
{
    public class OrderItem : IEntityBase
    {
        public OrderItem(Guid productId, decimal price, int quantity)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
    }
}
