using Microsoft.AspNetCore.Mvc;
using OverlapssytemApplication.Interfaces;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinController : ControllerBase
    {
        private readonly IMedicinService _medicinServices;

        public MedicinController(IMedicinService medicinServices)
        {
            _medicinServices = medicinServices;
        }

        //Put: api/MedicinTid
        [HttpPut("AngivMedicinTid")]
        public async Task<ActionResult> SetMedicinChecked(int medicinTimeId, bool isChecked)
        {
            await _medicinServices.SetMedicinCheckedAsync(medicinTimeId, isChecked);
            return Ok();
        }
    }
}
