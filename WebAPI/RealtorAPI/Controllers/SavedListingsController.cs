using DAL.Models;
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
    public class SavedListingsController : ControllerBase
    {
        private readonly ISavedListingService SavedListingService;
        private readonly IRealtorService realtorService;

        public SavedListingsController(ISavedListingService SavedListingService, IRealtorService realtorService)
        {
            this.SavedListingService = SavedListingService;
            this.realtorService = realtorService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SavedListing obj)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
                if (userId == 0)
                    return Unauthorized();
                var result = await this.SavedListingService.AddAsync(obj);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        // GET: api/Todo
        [HttpGet("{companyId}")]
        public async Task<ActionResult> Get(int companyId)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
                if (userId == 0)
                    return Unauthorized();

                var result = await this.realtorService.GetSavedListingMLS(companyId, userId);

                for (int i = 0; i < 5; i++)
                {
                    if (result == null)
                    {
                        result = await this.realtorService.GetSavedListingMLS(companyId, userId);
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);

                await this.SavedListingService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
