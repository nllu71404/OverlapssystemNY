using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;

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
        public async Task<List<ShoppingModel>> GetShoppingByResidentId(int residentId)
        {
            return await _http.GetFromJsonAsync<List<ShoppingModel>>($"api/Shopping/Shopping/{residentId}");
        }
        //Tilføj
        public async Task<int> SaveNewShopping(AddShoppingDTO addShoppingDTO)
        {
            var response = await _http.PostAsJsonAsync("api/Shopping/TilføjShopping", addShoppingDTO);
            return await response.Content.ReadFromJsonAsync<int>();
        }

        //Update
        public async Task UpdateShopping(int shoppingId, ShoppingModel shoppingModel)
        {
            await _http.PutAsJsonAsync($"api/Shopping/{shoppingId}", shoppingModel);
        }

        //Delete
        public async Task DeleteShopping(int shoppingId)
        {
            await _http.DeleteAsync($"api/Shopping/{shoppingId}");
        }

    }
}
