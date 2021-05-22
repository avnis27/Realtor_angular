using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtorAPI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealtorAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService locationService;
        public LocationsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //LatitudeMax=81.14747595814636&CurrentPage=1&LongitudeMin=-136.83037765324116&LongitudeMax=-10.267941690981388&RecordsPerPage=10&LatitudeMin=-22.26872153207163&SortOrder=A&NumberOfDays=0&BedRange=0-0&CultureId=1&BathRange=0-0&SortBy=1&PriceMin=0
                //var claimsIdentity = this.User.Identity as ClaimsIdentity;
                //int userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
                //var result = new Listing();
                var result = await this.locationService.GetMasterAsync();

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

        }

    }
}
