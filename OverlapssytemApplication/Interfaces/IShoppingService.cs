using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;

namespace OverlapssytemApplication.Interfaces
{
    public interface IShoppingService
    {
        Task<List<ShoppingModel>> GetShoppingByResidentIdAsync(int residentId);
        Task <int>SaveNewShoppingAsync(ShoppingModel shoppingModel);
        Task UpdateShoppingAsync(ShoppingModel shopping);
        Task DeleteShoppingAsync(int shoppingId);

    }
}
