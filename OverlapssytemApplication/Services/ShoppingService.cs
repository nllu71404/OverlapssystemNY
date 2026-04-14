using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Interfaces;
using OverlapssystemDomain.Enums;

namespace OverlapssytemApplication.Services
{
    public class ShoppingService : IShoppingService
    {
        private readonly IShoppingRepository _shoppingrepository;
        public ShoppingService(IShoppingRepository shoppingrepository)
        {
            _shoppingrepository = shoppingrepository;
        }
        public async Task<List<ShoppingModel>> GetShoppingByResidentIdAsync(int residentId)
        {
            return await _shoppingrepository.GetShoppingByResidentIdAsync(residentId);
        }

        public async Task DeleteShoppingAsync(int shoppingId)
        {
            await _shoppingrepository.DeleteShoppingAsync(shoppingId);
        }

        public async Task<int> SaveNewShoppingAsync(ShoppingModel shoppingModel)
        {
            return await _shoppingrepository.SaveNewShoppingAsync(shoppingModel);
        }

        public async Task UpdateShoppingAsync(ShoppingModel shopping)
        {
            await _shoppingrepository.UpdateShoppingAsync(shopping);
        }
    }
}
