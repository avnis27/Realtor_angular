using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtorAPI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService agentService;
        public AgentsController(IAgentService agentService)
        {
            this.agentService = agentService;
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Agent obj)
        {
            try
            {
                await this.agentService.UpdateAsync(obj);                

                return Ok();
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
                var result = await this.agentService.GetAllAsync(companyId);

                return Ok(result);

                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

        }
    }
}
