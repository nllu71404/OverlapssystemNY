using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Services.Extensions;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common.Result;
using OverlapssytemApplication.Common.Errors;

namespace Overlapssystem.Services
{
    public class ShoppingApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ShoppingApiService> _logger;

        public ShoppingApiService(HttpClient http, ILogger<ShoppingApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // GET
        public async Task<Result<List<UpdateShoppingDTO>>> GetShoppingByResidentId(int residentId)
        {
            if (residentId <= 0)
                return Error.Validation("Ugyldigt resident id");

            try
            {
                var response = await _http.GetAsync($"api/Shopping/Shopping/{residentId}");

                return await response.ReadApiResponse<List<UpdateShoppingDTO>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetShoppingByResidentId fejlede for ResidentId {ResidentId}", residentId);
                return Error.Technical("Kunne ikke hente shopping data");
            }
        }

        // ADD
        public async Task<Result<int>> AddShopping(AddShoppingDTO dto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Shopping/TilføjShopping", dto);

                return await response.ReadApiResponse<int>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddShopping fejlede");
                return Error.Technical("Kunne ikke oprette shopping");
            }
        }

        // UPDATE
        public async Task<Result> UpdateShopping(int shoppingId, UpdateShoppingDTO dto)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/Shopping/{shoppingId}", dto);

                await response.ReadApiResponse<object>();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateShopping fejlede for Id {ShoppingId}", shoppingId);
                return Error.Technical("Kunne ikke opdatere shopping");
            }
        }

        // DELETE
        public async Task<Result> DeleteShopping(int shoppingId)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Shopping/{shoppingId}");

                await response.ReadApiResponse<object>();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteShopping fejlede for Id {ShoppingId}", shoppingId);
                return Error.Technical("Kunne ikke slette shopping");
            }
        }
    }
}