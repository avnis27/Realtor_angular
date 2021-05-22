using DAL.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
    public class RealtorController : ControllerBase
    {
        private readonly IRealtorService _realtorService;
        

        public RealtorController(IRealtorService realtorService)
        {
            this._realtorService = realtorService;
            
        }

        // GET: api/Todo
        [EnableCors]
        [HttpGet("{companyId}/{type}/{queryString}")]
        public async Task<IActionResult> Get(int companyId, string type,string queryString)
        {
            try
            {
                string[] stringArray = queryString.Split(';');
                foreach(string query in stringArray)
                {

                }
                //LatitudeMax=81.14747595814636&CurrentPage=1&LongitudeMin=-136.83037765324116&LongitudeMax=-10.267941690981388&RecordsPerPage=10&LatitudeMin=-22.26872153207163&SortOrder=A&NumberOfDays=0&BedRange=0-0&CultureId=1&BathRange=0-0&SortBy=1&PriceMin=0
                //var claimsIdentity = this.User.Identity as ClaimsIdentity;
                //int userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
                //var result = new Listing();
                if (type.Trim().ToLower() == "residential")
                {
                    var result = await this._realtorService.GetResidentialData(companyId, queryString);

                    for (int i = 0; i < 5; i++)
                    {
                        if (result == null)
                        {
                            result = await this._realtorService.GetResidentialData(companyId, queryString);
                        }
                    }
                    return Ok(result);
                }
                if (type.Trim().ToLower() == "commercial")
                {
                    var result = await this._realtorService.GetCommercialData(companyId, queryString);
                    for (int i = 0; i < 5; i++)
                    {
                        if (result == null)
                        {
                            result = await this._realtorService.GetCommercialData(companyId, queryString);
                        }
                    }

                    return Ok(result);
                }
                if (type.Trim().ToLower() == "detail")
                {
                    var result = await this._realtorService.GetPropertyDetail(companyId, queryString);
                    for (int i = 0; i < 5; i++)
                    {
                        if (result == null)
                        {
                            result = await this._realtorService.GetPropertyDetail(companyId, queryString);
                        }
                    }

                    return Ok(result);
                }
                if (type.Trim().ToLower() == "featuredlisting")
                {
                    var result = await this._realtorService.GetAllFeaturedListingMLS(companyId);
                    for (int i = 0; i < 5; i++)
                    {
                        if (result == null)
                        {
                            result = await this._realtorService.GetAllFeaturedListingMLS(companyId);
                        }
                    }


                    return Ok(result);
                }

                if (type.Trim().ToLower() == "mls")
                {
                    var result = await this._realtorService.GetPropertyDetailbyMLS(companyId, queryString);
                    for (int i = 0; i < 5; i++)
                    {
                        if (result == null)
                        {
                            result = await this._realtorService.GetPropertyDetailbyMLS(companyId, queryString);
                        }
                    }

                    return Ok(result);
                }

                return BadRequest();

                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

        }

        // GET: api/Todo
        [HttpGet("{id}")]
        public async Task<ActionResult<AutoComplete>> Get(string id)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
                var result = await this._realtorService.GetAutoComplete();
                
                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

        }

        // GET: api/Todo
        [HttpGet("{id}/{statistic}")]
        public async Task<ActionResult<Statistics>> Get(string id,string statistic)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
                var result = await this._realtorService.GetStatistics();

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        // GET: api/Todo
        [EnableCors]
        [HttpPost("{companyid}")]
        public async Task<IActionResult> Post([FromBody] QueryStringObject obj, int companyId)
        {
            try
            {
                string queryString = string.Empty;

                

                //LatitudeMax=81.14747595814636&CurrentPage=1&LongitudeMin=-136.83037765324116&LongitudeMax=-10.267941690981388&RecordsPerPage=10&LatitudeMin=-22.26872153207163&SortOrder=A&NumberOfDays=0&BedRange=0-0&CultureId=1&BathRange=0-0&SortBy=1&PriceMin=0
                //var claimsIdentity = this.User.Identity as ClaimsIdentity;
                //int userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
                //var result = new Listing();
                if (obj.Type.Trim().ToLower() == "residential")
                {
                    obj.BuildingTypeId = 1;
                    obj.TransactionTypeId = 2;
                    queryString = "LatitudeMax=" + obj.LatitudeMax + "&CurrentPage=" + obj.CurrentPage + "&LongitudeMin=" + obj.LongitudeMin + "&" +
                        "LongitudeMax=" + obj.LongitudeMax + "&RecordsPerPage=" + obj.RecordsPerPage + "&LatitudeMin=" + obj.LatitudeMin + "&" +
                        "SortOrder=" + obj.SortOrder + "&NumberOfDays=" + obj.NumberOfDays + "&BedRange=" + obj.BedRange + "&" +
                        "CultureId=1&BathRange=" + obj.BathRange + "&SortBy" + obj.SortOrder + "&PriceMin=" + obj.PriceMin + "&BuildingTypeId=" + obj.BuildingTypeId + "&TransactionTypeId=" + obj.TransactionTypeId + "";

                    var result = await this._realtorService.GetResidentialData(companyId, queryString);

                    for (int i = 0; i < 5; i++)
                    {
                        if (result == null)
                        {
                            result = await this._realtorService.GetResidentialData(companyId, queryString);
                        }
                    }

                    return Ok(result);
                }
                if (obj.Type.Trim().ToLower() == "commercial")
                {
                   
                    queryString = "LatitudeMax=" + obj.LatitudeMax + "&CurrentPage=" + obj.CurrentPage + "&LongitudeMin=" + obj.LongitudeMin + "&" +
                        "LongitudeMax=" + obj.LongitudeMax + "&RecordsPerPage=" + obj.RecordsPerPage + "&LatitudeMin=" + obj.LatitudeMin + "&" +
                        "SortOrder=" + obj.SortOrder + "&NumberOfDays=" + obj.NumberOfDays + "&BedRange=" + obj.BedRange + "&" +
                        "CultureId=1&BathRange=" + obj.BathRange + "&SortBy" + obj.SortOrder + "&PriceMin=" + obj.PriceMin + "&BuildingTypeId=" + obj.BuildingTypeId + "&TransactionTypeId=" + obj.TransactionTypeId + "";
                    var result = await this._realtorService.GetCommercialData(companyId, queryString);
                    for (int i = 0; i < 5; i++)
                    {
                        if (result == null)
                        {
                            result = await this._realtorService.GetCommercialData(companyId, queryString);
                        }
                    }

                    return Ok(result);
                }
                

                return BadRequest();


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

        }

        ////// GET: api/Todo
        ////[HttpGet("{id}/{id1}/{collection}")]
        ////public async Task<ActionResult<CollectionList>> Get1(string id,string id1, string collection)
        ////{
        ////    try
        ////    {
        ////        var claimsIdentity = this.User.Identity as ClaimsIdentity;
        ////        int userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
        ////        var result = await this._realtorService.GetList();

        ////        if (result == null)
        ////        {
        ////            return NotFound();
        ////        }

        ////        return result;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        return StatusCode(500, ex.ToString());
        ////    }
        ////}
        // GET: api/Todo
        //[HttpGet("{id}/{id1}/{id2}/{propertydetail}")]
        //public async Task<ActionResult<PropertyDetail>> Get(string id, string id1, string id2, string propertydetail)
        //{
        //    try
        //    {
        //        var claimsIdentity = this.User.Identity as ClaimsIdentity;
        //        int userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
        //        var result = await this._realtorService.GetPropertyDetail();

        //        if (result == null)
        //        {
        //            return NotFound();
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.ToString());
        //    }
        //}
    }
}
