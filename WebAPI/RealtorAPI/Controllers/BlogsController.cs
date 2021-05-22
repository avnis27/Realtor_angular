using DAL.Models;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    [Route("[controller]")]
    [ApiController]    
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService BlogService;

        public BlogsController(IBlogService BlogService)
        {
            this.BlogService = BlogService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Blog obj)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
                if (userId == 0)
                    return Unauthorized();
                var result = await this.BlogService.AddAsync(obj);

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
                //var claimsIdentity = this.User.Identity as ClaimsIdentity;
                
                var result = await this.BlogService.GetAllAsync(companyId);

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

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);

                await this.BlogService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
