using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;
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

        // Hent shopping
        public async Task<Result<List<ShoppingModel>>> GetShoppingByResidentIdAsync(int residentId)
        {
            if (residentId <= 0)
                return Error.Validation("Ugyldigt resident ID");

            try
            {
                var result = await _shoppingrepository.GetShoppingByResidentIdAsync(residentId);

                return result ?? new List<ShoppingModel>(); // implicit success
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke hente shopping");
            }
        }

        // Slet shopping
        public async Task<Result> DeleteShoppingAsync(int shoppingId)
        {
            if (shoppingId <= 0)
                return Error.Validation("Ugyldigt shopping ID");

            try
            {
                await _shoppingrepository.DeleteShoppingAsync(shoppingId);

                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Shopping findes ikke");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke slette shopping");
            }
        }

        // Opret shopping
        public async Task<Result<int>> SaveNewShoppingAsync(ShoppingModel shoppingModel)
        {
            if (shoppingModel == null)
                return Error.Validation("Shopping må ikke være null");

            try
            {
                var id = await _shoppingrepository.SaveNewShoppingAsync(shoppingModel);

                return id; // implicit success
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke oprette shopping");
            }
        }

        // Update shopping
        public async Task<Result> UpdateShoppingAsync(ShoppingModel shopping)
        {
            if (shopping == null)
                return Error.Validation("Shopping må ikke være null");

            try
            {
                await _shoppingrepository.UpdateShoppingAsync(shopping);

                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Shopping findes ikke");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke opdatere shopping");
            }
        }
    }
}
