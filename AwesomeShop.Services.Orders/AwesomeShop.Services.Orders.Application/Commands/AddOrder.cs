﻿using AwesomeShop.Services.Orders.Core.Entities;
using AwesomeShop.Services.Orders.Core.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwesomeShop.Services.Orders.Application.Commands
{
    public class AddOrder : IRequest<Guid>
    {
        public CustomerInputModel Customer { get; set; }
        public List<OrderItemInputModel> OrderItems { get; set; }
        public DeliveryAddressInputModel DeliveryAddress { get; set; }
        public PaymentAddressInputModel PaymentsAddress { get; set; }
        public PaymentInfoInputModel PaymentInfo { get; set; }

        public Order ToOrder()
        {
            return new Order(
                new Customer(Customer.Id, Customer.FullName, Customer.Email),
                new DeliveryAddress(DeliveryAddress.Street, DeliveryAddress.Number, DeliveryAddress.City, DeliveryAddress.ZipCode, DeliveryAddress.State),
                new PaymentsAddress(PaymentsAddress.State, PaymentsAddress.Number, PaymentsAddress.City, PaymentsAddress.ZipCode,PaymentsAddress.State),
                new PaymentInfo(PaymentInfo.CardNumber, PaymentInfo.FullName, PaymentInfo.ExpirationDate, PaymentInfo.CVV),
                OrderItems.Select(x => new OrderItem(x.ProductId, x.Price, x.Quantity)).ToList()
            );
        }
    }

    public class CustomerInputModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        
    }

    public class OrderItemInputModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        
    }

    public class DeliveryAddressInputModel
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }  
    }

    public class PaymentAddressInputModel
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class PaymentInfoInputModel
    {
        public string CardNumber { get; set; }  
        public string FullName { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
    }
}
