using AwesomeShop.Services.Orders.Core.Enums;
using AwesomeShop.Services.Orders.Core.Events;
using AwesomeShop.Services.Orders.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwesomeShop.Services.Orders.Core.Entities
{
    public class Order : AggregateRooot
    {
        public Order(Customer customer, DeliveryAddress deliveryAddress, PaymentsAddress paymentsAddress, PaymentInfo paymentInfo, List<OrderItem> items)
        {
            Id = Guid.NewGuid();
            TotalPrice = items.Sum(i => i.Quantity * i.Price);
            Customer = customer;
            DeliveryAddress = deliveryAddress;
            PaymentsAddress = paymentsAddress;
            PaymentInfo = paymentInfo;
            Items = items;
            Status = OrderStatus.Started;

            CreatedAt = DateTime.Now;

            AddEvent(new OrderCreatedEvent(Id, TotalPrice, paymentInfo, Customer.FullName, Customer.Email));
        }

        public decimal TotalPrice { get; private set; }
        public Customer Customer { get; private set; }
        public DeliveryAddress DeliveryAddress { get; private set; }
        public PaymentsAddress PaymentsAddress { get; private set; }
        public PaymentInfo PaymentInfo { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public OrderStatus Status { get; private set; }

        public void SetAsCompleted() => Status = OrderStatus.Completed;

        public void SetAsRejected() => Status = OrderStatus.Rejected;
        
    }
}
