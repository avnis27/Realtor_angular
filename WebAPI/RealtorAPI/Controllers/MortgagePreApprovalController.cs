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
    public class MortgagePreApprovalController : ControllerBase
    {
        private readonly AppSettings appSettings;
        private readonly IVisitorService visitorService;
        private readonly ICompanyService companyService;

        public MortgagePreApprovalController(IVisitorService visitorService, IOptions<AppSettings> appSettings, ICompanyService companyService)
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
                emailSender.SendMortgagePreApprovalEmail(obj);              

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
