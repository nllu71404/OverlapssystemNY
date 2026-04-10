using Microsoft.AspNetCore.Mvc;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PNMedicinController : ControllerBase
    {
        private readonly IPNMedicinService _pNMedicinService;

        public PNMedicinController (IPNMedicinService pnMedicinService)
        {
            _pNMedicinService = pnMedicinService;
        }
    }
}
