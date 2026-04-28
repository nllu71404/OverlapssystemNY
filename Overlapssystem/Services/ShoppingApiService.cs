using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using Overlapssystem.Services.Extensions;

namespace Overlapssystem.Services
{
    public class ShoppingApiService
    {
        private readonly HttpClient _http;
        public ShoppingApiService(HttpClient http)
        {
            _http = http;
        }

        //Hent
        public async Task<List<UpdateShoppingDTO>> GetShoppingByResidentId(int residentId)
        {
            var response = await _http.GetAsync($"api/Shopping/Shopping/{residentId}");
            var dtoList = await response.ReadApiResponse<List<UpdateShoppingDTO>>();
            return dtoList?.ToList() ?? new List<UpdateShoppingDTO>();
        }
        //Tilføj
        public async Task<int> AddShopping(AddShoppingDTO addShoppingDTO)
        {
            var response = await _http.PostAsJsonAsync("api/Shopping/TilføjShopping", addShoppingDTO);
            return await response.ReadApiResponse<int>();
        }

        //Update
        public async Task UpdateShopping(int shoppingId, UpdateShoppingDTO shoppingDto)
        {
            var response = await _http.PutAsJsonAsync($"api/Shopping/{shoppingId}", shoppingDto);
            await response.ReadApiResponse<object>();
        }

        //Delete
        public async Task DeleteShopping(int shoppingId)
        {
            var response = await _http.DeleteAsync($"api/Shopping/{shoppingId}");
            await response.ReadApiResponse<object>();
        }

    }
}
