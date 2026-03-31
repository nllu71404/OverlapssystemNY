using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssystemDomain.Interfaces
{
    public interface IShoppingRepository
    {
        Task<List<ShoppingModel>> GetAllShoppingAsync();
        Task<List<ShoppingModel>> GetShoppingByResidentIdAsync(int residentId);
        Task<int> SaveNewShoppingAsync(ShoppingModel shopping);
        Task DeleteShoppingAsync(int shoppingId);
        Task UpdateShoppingAsync(ShoppingModel shopping);
    }
}
