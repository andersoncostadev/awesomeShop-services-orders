﻿using AwesomeShop.Services.Orders.Core.Entities;
using AwesomeShop.Services.Orders.Core.Repositories;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Orders.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _collection;

        public OrderRepository(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<Order>("orders");
        }
        public async Task AddAsync(Order order)
        {
            await _collection.InsertOneAsync(order);
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _collection.Find(o => o.Id == id).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            await _collection.ReplaceOneAsync(o => o.Id == order.Id, order);
        }
    }
}
