using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Services
{
    public class ShoppingService : IShoppingService
    {
        private readonly IShoppingRepository _shoppingrepository;

        public ShoppingService(IShoppingRepository shoppingrepository)
        {
            _shoppingrepository = shoppingrepository;
        }

        //Hent shopping
        public async Task<Result<List<ShoppingModel>>> GetShoppingByResidentIdAsync(int residentId)
        {
            var result = await _shoppingrepository.GetShoppingByResidentIdAsync(residentId);

            return Result<List<ShoppingModel>>.Ok(result ?? new List<ShoppingModel>());
        }

        //Slet shopping
        public async Task<Result> DeleteShoppingAsync(int shoppingId)
        {
            try
            {
                await _shoppingrepository.DeleteShoppingAsync(shoppingId);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        //Opret shopping
        public async Task<Result<int>> SaveNewShoppingAsync(ShoppingModel shoppingModel)
        {
            try
            {
                var id = await _shoppingrepository.SaveNewShoppingAsync(shoppingModel);
                return Result<int>.Ok(id);
            }
            catch (Exception ex)
            {
                return Result<int>.Fail(ex.Message);
            }
        }

        //Update shopping
        public async Task<Result> UpdateShoppingAsync(ShoppingModel shopping)
        {
            try
            {
                await _shoppingrepository.UpdateShoppingAsync(shopping);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

    }
}
