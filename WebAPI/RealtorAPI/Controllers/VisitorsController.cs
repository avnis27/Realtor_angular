using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RealtorAPI.IServices;
using RealtorAPI.Settings;
using RealtorAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealtorAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        private readonly AppSettings appSettings;
        private readonly IVisitorService visitorService;
        private readonly ICompanyService companyService;

        public VisitorsController(IVisitorService visitorService, IOptions<AppSettings> appSettings, ICompanyService companyService)
        {
            this.visitorService = visitorService;
            this.appSettings = appSettings.Value;
            this.companyService = companyService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Visitor obj)
        {
            try
            {
                var company = await this.companyService.GetAsync(obj.CompanyId);

                EmailSender emailSender = new EmailSender(appSettings);
                obj.VendorEMail = company.EMail;
                emailSender.SendGenericEmail(obj);

                var result = await this.visitorService.AddAsync(obj);

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

                var result = await this.visitorService.GetAllAsync(companyId);

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

                await this.visitorService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
