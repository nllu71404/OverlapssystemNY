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
using Microsoft.Extensions.Logging;

namespace OverlapssytemApplication.Services
{
    public class ShoppingService : IShoppingService
    {
        private readonly IShoppingRepository _shoppingrepository;
        private readonly ILogger<ShoppingService> _logger;

        public ShoppingService(IShoppingRepository shoppingrepository, ILogger<ShoppingService> logger)
        {
            _shoppingrepository = shoppingrepository;
            _logger = logger;
        }

        // Hent shopping
        public async Task<Result<List<ShoppingModel>>> GetShoppingByResidentIdAsync(int residentId)
        {
            if (residentId <= 0)
                return Error.Validation("Ugyldigt beboer ID");

            try
            {
                var result = await _shoppingrepository.GetShoppingByResidentIdAsync(residentId);

                return result ?? new List<ShoppingModel>(); // implicit success
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af handledage");
                return Error.Technical("Kunne ikke hente handledage");
            }
        }

        // Slet shopping
        public async Task<Result> DeleteShoppingAsync(int shoppingId)
        {
            if (shoppingId <= 0)
                return Error.Validation("Ugyldigt handledag ID");

            try
            {
                await _shoppingrepository.DeleteShoppingAsync(shoppingId);

                return Result.Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Handledag blev ikke fundet");
                return Error.NotFound("Kunne ikke finde handledag at slette");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved sletning af handledag");
                return Error.Technical("Kunne ikke slette handledag");
            }
        }

        // Opret shopping
        public async Task<Result<int>> CreateShoppingAsync(ShoppingModel shopping)
        {
            if (shopping.ResidentID <= 0)
                return Error.Validation("Ugyldigt beboer ID");

            try
            {
                var id = await _shoppingrepository.SaveNewShoppingAsync(shopping);

                return id; // implicit success
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af handledag");
                return Error.Technical("Kunne ikke oprette handledag");
            }
        }

        // Update shopping
        public async Task<Result> UpdateShoppingAsync(ShoppingModel shopping)
        {
            if (shopping.ShoppingID <= 0)
                return Error.Validation("Ugyldigt handledag ID");

            if (shopping.ResidentID <= 0)
                return Error.Validation("Ugyldigt beboer ID");

            try
            {
                await _shoppingrepository.UpdateShoppingAsync(shopping);

                return Result.Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Handledag blev ikke fundet");
                return Error.NotFound("Kunne ikke finde handledag at opdatere");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af handledag");
                return Error.Technical("Kunne ikke opdatere handledag");
            }
        }
    }
}
