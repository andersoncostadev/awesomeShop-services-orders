using AwesomeShop.Services.Orders.Core.Entities;
using System;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Orders.Core.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order> GetByIdAsync(Guid id);
        Task UpdateAsync(Order order);
    }
}
