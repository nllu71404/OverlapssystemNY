using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssytemApplication.Interfaces
{
    public interface IShoppingService
    {
        Task<List<ShoppingModel>> GetShoppingByResidentIdAsync(int residentId);
        Task SaveNewShoppingAsync(ShoppingModel shopping);
        Task UpdateShoppingAsync(ShoppingModel shopping);
        Task DeleteShoppingAsync(int shoppingId);

    }
}
