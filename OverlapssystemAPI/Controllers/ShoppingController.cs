using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Common;

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
            
            var result = await _shoppingService.GetShoppingByResidentIdAsync(residentId);

            if (!result.Success)
            {
                return Handle(result);
            }

            var shoppingDTOs = result.Value.Select(MapToGetShoppingModel).ToList();
            return Handle(Result.Ok(shoppingDTOs));
        }

        //Tilføj
        [HttpPost("TilføjShopping")]
        public async Task<IActionResult> AddShopping([FromBody] AddShoppingDTO addShoppingDTO)
        {
            var shoppingModel = MapToAddShoppingModel(addShoppingDTO);
            var result = await _shoppingService.CreateShoppingAsync(shoppingModel);

            return Handle(result); 


        }

        //Update
        [HttpPut("{shoppingId}")]
        public async Task<IActionResult> UpdateShopping(int shoppingId, [FromBody] UpdateShoppingDTO shoppingDTO)
        {
            var shoppingModel = MapToUpdateShoppingModel(shoppingDTO);
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


        // ----- Mapping -----

        private static ShoppingModel MapToAddShoppingModel(AddShoppingDTO shoppingDTO)
        {
            return new ShoppingModel
            {
                ResidentID = shoppingDTO.ResidentID,
                Day = shoppingDTO.Day,
                Time = shoppingDTO.Time,
                PaymentMethod = shoppingDTO.PaymentMethod
            };
        }
        private static ShoppingModel MapToUpdateShoppingModel(UpdateShoppingDTO shoppingDTO)
        {
            return new ShoppingModel
            {
                ShoppingID = shoppingDTO.ShoppingID,
                ResidentID = shoppingDTO.ResidentID,
                Day = shoppingDTO.Day,
                Time = shoppingDTO.Time,
                PaymentMethod = shoppingDTO.PaymentMethod
            };
        }

        private static UpdateShoppingDTO MapToGetShoppingModel(ShoppingModel shoppingModel)
        {
            return new UpdateShoppingDTO
            {
                ShoppingID = shoppingModel.ShoppingID,
                ResidentID = shoppingModel.ResidentID,
                Day = shoppingModel.Day,
                Time = shoppingModel.Time,
                PaymentMethod = shoppingModel.PaymentMethod
            };
        }
    }
}
