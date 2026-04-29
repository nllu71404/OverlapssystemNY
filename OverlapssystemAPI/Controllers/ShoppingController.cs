using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Interfaces;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingController : ApiControllerBase
    {
        private readonly IShoppingService _shoppingService;

        public ShoppingController(IShoppingService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        //Hent
        [HttpGet("Shopping/{residentId}")]
        public async Task<IActionResult> GetShoppingByResidentId(int residentId)
        {
            var shopping = await _shoppingService.GetShoppingByResidentIdAsync(residentId);
            return Handle(shopping);
        }

        //Tilføj
        [HttpPost("TilføjShopping")]
        public async Task<IActionResult> SaveNewShopping([FromBody] AddShoppingDTO addShoppingDTO)
        {
            var shopping = new ShoppingModel
            {
                ResidentID = addShoppingDTO.ResidentID,
                Day = addShoppingDTO.Day,
                Time = addShoppingDTO.Time,
                PaymentMethod = addShoppingDTO.PaymentMethod
            };
            
            
            var id = await _shoppingService.SaveNewShoppingAsync(shopping);
           
                return Handle(id); //Fejlhåndtering

      
        }

        //Update
        [HttpPut("{shoppingId}")]
        public async Task<IActionResult> UpdateShopping(int shoppingId, [FromBody] ShoppingModel shoppingModel)
        {
            var result = await _shoppingService.UpdateShoppingAsync(shoppingModel);
            return Handle(result);
        }

        //Delete
        [HttpDelete("{shoppingId}")]
        public async Task<IActionResult> DeleteShopping(int shoppingId)
        {
            var result = await _shoppingService.DeleteShoppingAsync(shoppingId);
            return Handle(result);
        }
    }
}
