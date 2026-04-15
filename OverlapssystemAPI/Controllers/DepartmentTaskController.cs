using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentTaskController : ControllerBase
    {
        private readonly IDepartmentTaskService _departmentTaskService;
        public DepartmentTaskController(IDepartmentTaskService departmentTaskService)
        {
            _departmentTaskService = departmentTaskService;
        }

        //Hent alle


        //[HttpGet("HenterResident")]
        //public async Task<ActionResult> GetResidents()
        //{
        //    //Skal kalde LoadResidentAsync og returnerer resultatet
        //    var residents = await _residentServices.LoadResidentsAsync();
        //    return Ok(residents);
        //}
        //Hent departmentTask på id

        //Tilføj

        //Update

        //Delete
    }
}
