using AwesomeShop.Services.Orders.Core.ValueObjects;
using System;

namespace AwesomeShop.Services.Orders.Core.Events
{
    public class OrderCreatedEvent : IDomainEvent
    {
        public OrderCreatedEvent(Guid id, decimal totalPrice, PaymentInfo paymentInfo, string fullName, string email)
        {
            Id = id;
            TotalPrice = totalPrice;
            PaymentInfo = paymentInfo;
            FullName = fullName;
            Email = email;
        }

        public Guid Id;
        public decimal TotalPrice;
        public PaymentInfo PaymentInfo;
        public string FullName;
        public string Email;
    }
}
