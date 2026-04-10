using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly IShoppingService _shoppingService;

        public ShoppingController(IShoppingService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        //Hent
        [HttpGet("Shopping/{residentId}")]
        public async Task<ActionResult> GetShoppingByResidentId(int residentId)
        {
            var shopping = await _shoppingService.GetShoppingByResidentIdAsync(residentId);
            return Ok(shopping);
        }

        //Tilføj
        [HttpPost("TilføjShopping")]
        public async Task<ActionResult> SaveNewShopping([FromBody] ShoppingModel shoppingModel)
        {
            await _shoppingService.SaveNewShoppingAsync(shoppingModel);
            return Ok(shoppingModel);
        }

        //Update
        [HttpPut("{shoppingId}")]
        public async Task<ActionResult> UpdateShopping(int shoppingId, [FromBody] ShoppingModel shoppingModel)
        {
            await _shoppingService.UpdateShoppingAsync(shoppingModel);
            return Ok(shoppingModel);
        }

        //Delete
        [HttpDelete("{shoppingId}")]
        public async Task<ActionResult> DeleteShopping(int shoppingId)
        {
            await _shoppingService.DeleteShoppingAsync(shoppingId);
            return Ok(shoppingId);
        }
    }
}
