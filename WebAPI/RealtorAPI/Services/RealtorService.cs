using DAL.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RealtorAPI.IServices;
using RealtorAPI.Settings;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;


namespace RealtorAPI.Services
{
    public class RealtorService : IRealtorService
    {
        private readonly RapidApiUrls rapidApiUrls;
        private readonly IRapidApiService rapidApiService;
        private readonly IFeaturedListingService featuredListingService;
        private readonly ISavedListingService savedListingService;
        private readonly ICacheService cacheService;
       

        public RealtorService(IOptions<RapidApiUrls> rapidApiUrls, IRapidApiService rapidApiService, IFeaturedListingService featuredListingService, ISavedListingService savedListingService, ICacheService cacheService)
        {
            this.rapidApiUrls = rapidApiUrls.Value;
            this.rapidApiService = rapidApiService;
            this.featuredListingService = featuredListingService;
            this.savedListingService = savedListingService;
            this.cacheService = cacheService;
        }

        public async Task<JObject> GetResidentialData(int companyId, string queryString)
        {
            //commercial
            //https://realtor-canadian-real-estate.p.rapidapi.com/properties/list-commercial?LatitudeMax=81.14747595814636&CurrentPage=1&LongitudeMin=-136.83037765324116&LongitudeMax=-10.267941690981388&RecordsPerPage=10&LatitudeMin=-22.26872153207163&SortOrder=A&NumberOfDays=0&BedRange=0-0&CultureId=1&BathRange=0-0&SortBy=1&PriceMin=0
            //residential
            //https://realtor-canadian-real-estate.p.rapidapi.com/properties/list-residential?CurrentPage=1&LatitudeMin=43.4728479&LongitudeMax=-79.5624177&RecordsPerPage=10&LongitudeMin=-79.9624177&LatitudeMax=43.9928479&BedRange=0-0&BathRange=0-0&NumberOfDays=0&SortBy=1&BuildingTypeId=1&SortOrder=A&RentMin=0
            //list-by-mls
            //https://realtor-canadian-real-estate.p.rapidapi.com/properties/list-by-mls?ReferenceNumber=30513228-1&CultureId=1
            //detail-by-id-mls
            //var client = new RestClient("https://realtor-canadian-real-estate.p.rapidapi.com/properties/detail?ReferenceNumber=W5054882&PropertyID=22639958&PreferedMeasurementUnit=1&CultureId=1");
           

            try
            {
                //queryString = queryString.Replace(";", "&");
                //var rapidApiData = await this.rapidApiService.GetMasterAsync(companyId);
                //if (rapidApiData == null)
                //    return null;

                //var client = new RestClient(string.Concat(this.rapidApiUrls.list_residential, queryString));
                //var request = new RestRequest(Method.GET);
                //request.AddHeader("x-rapidapi-key", rapidApiData.x_rapidapi_key);
                //request.AddHeader("x-rapidapi-host", rapidApiData.x_rapidapi_host);
                //IRestResponse response = await client.ExecuteAsync(request);
                //response.Content
                //return response;
                var cachedJson = cacheService.Get<JObject>(BusinessConstants.PROP_SUMMRY_RESIDENTIAL + queryString);
                if (cachedJson == null)
                {
                    var rapidApiData = await this.rapidApiService.GetMasterAsync(companyId);
                    if (rapidApiData == null)
                        return null;
                    var client = new HttpClient();
                    var body = new Listing();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(string.Concat(this.rapidApiUrls.list_residential, queryString)),
                        Headers =
                            {
                                { "x-rapidapi-key", rapidApiData.x_rapidapi_key },
                                { "x-rapidapi-host", rapidApiData.x_rapidapi_host },
                             },
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        if (response.ReasonPhrase.Contains("Too Many Requests"))
                        {
                            await this.rapidApiService.UpdateAsync(rapidApiData.Id);
                            await GetResidentialData(companyId, queryString);
                            return null;
                        }
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        JObject json = JObject.Parse(result);
                        cacheService.Set(BusinessConstants.PROP_SUMMRY + queryString, json);
                        return json;
                        //return await response.Content.ReadAsAsync<Object>(new[] { new JsonMediaTypeFormatter() });
                    }
                }
                else
                {
                    return cachedJson;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<JObject> GetCommercialData(int companyId, string queryString)
        {
            //commercial
            //https://realtor-canadian-real-estate.p.rapidapi.com/properties/list-commercial?LatitudeMax=81.14747595814636&CurrentPage=1&LongitudeMin=-136.83037765324116&LongitudeMax=-10.267941690981388&RecordsPerPage=10&LatitudeMin=-22.26872153207163&SortOrder=A&NumberOfDays=0&BedRange=0-0&CultureId=1&BathRange=0-0&SortBy=1&PriceMin=0
            //residential
            //https://realtor-canadian-real-estate.p.rapidapi.com/properties/list-residential?CurrentPage=1&LatitudeMin=43.4728479&LongitudeMax=-79.5624177&RecordsPerPage=10&LongitudeMin=-79.9624177&LatitudeMax=43.9928479&BedRange=0-0&BathRange=0-0&NumberOfDays=0&SortBy=1&BuildingTypeId=1&SortOrder=A&RentMin=0
            //list-by-mls
            //https://realtor-canadian-real-estate.p.rapidapi.com/properties/list-by-mls?ReferenceNumber=30513228-1&CultureId=1
            //detail-by-id-mls
            //var client = new RestClient("https://realtor-canadian-real-estate.p.rapidapi.com/properties/detail?ReferenceNumber=W5054882&PropertyID=22639958&PreferedMeasurementUnit=1&CultureId=1");


            try
            {
                //queryString = queryString.Replace(";", "&");
                var cachedJson = cacheService.Get<JObject>(BusinessConstants.PROP_SUMMRY_COMMERCIAL + queryString);
                if (cachedJson == null)
                {
                    var rapidApiData = await this.rapidApiService.GetMasterAsync(companyId);
                    if (rapidApiData == null)
                        return null;
                    var client = new HttpClient();
                    var body = new Listing();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(string.Concat(this.rapidApiUrls.list_commercial, queryString)),
                        Headers =
                            {
                                { "x-rapidapi-key", rapidApiData.x_rapidapi_key },
                                { "x-rapidapi-host", rapidApiData.x_rapidapi_host },
                             },
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        if (response.ReasonPhrase.Contains("Too Many Requests"))
                        {
                            await this.rapidApiService.UpdateAsync(rapidApiData.Id);
                            await GetCommercialData(companyId, queryString);
                            return null;
                        }
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        JObject json = JObject.Parse(result);
                        cacheService.Set(BusinessConstants.PROP_SUMMRY + queryString, json);
                        return json;
                        //return await response.Content.ReadAsAsync<Listing>(new[] { new JsonMediaTypeFormatter() });
                    }
                }
                else
                {
                    return cachedJson;
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<AutoComplete> GetAutoComplete()
        {
            //commercial
            //
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://realtor-canadian-real-estate.p.rapidapi.com/locations/auto-complete?Area=Brampton&CultureId=1"),
                Headers =
    {
        { "x-rapidapi-key", "47eaab767bmsh8878137159d69ffp18130fjsn48bfea983ce0" },
        { "x-rapidapi-host", "realtor-canadian-real-estate.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<AutoComplete>(new[] { new JsonMediaTypeFormatter() });

            }
        }

        public async Task<Statistics> GetStatistics()
        {
            var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://realtor-canadian-real-estate.p.rapidapi.com/properties/get-statistics?Latitude=49.1241922&Longitude=-85.8230136&CultureId=1"),
            Headers =
    {
        { "x-rapidapi-key", "47eaab767bmsh8878137159d69ffp18130fjsn48bfea983ce0" },
        { "x-rapidapi-host", "realtor-canadian-real-estate.p.rapidapi.com" },
    },
        };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Statistics>(new[] { new JsonMediaTypeFormatter() });
                
            }
        }

        public async Task<CollectionList> GetList()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://realtor-canadian-real-estate.p.rapidapi.com/keywords/list"),
                Headers =
    {
        { "x-rapidapi-key", "47eaab767bmsh8878137159d69ffp18130fjsn48bfea983ce0" },
        { "x-rapidapi-host", "realtor-canadian-real-estate.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<CollectionList>(new[] { new JsonMediaTypeFormatter() });
            }
        }

        public async Task<JObject> GetPropertyDetail(int companyId, string queryString)
        {
            //ReferenceNumber = W5054882 & PropertyID = 22639958
            queryString = queryString.Replace(";", "&");
            var defaultString = "&PreferedMeasurementUnit=1&CultureId=1";
            //https://realtor-canadian-real-estate.p.rapidapi.com/properties/detail?ReferenceNumber=W5054882&PropertyID=22639958&PreferedMeasurementUnit=1&CultureId=1
            try
            {
                var cachedJson = cacheService.Get<JObject>(BusinessConstants.PROP_DETAIL + queryString);
                if (cachedJson == null)
                {
                    var rapidApiData = await this.rapidApiService.GetMasterAsync(companyId);
                    if (rapidApiData == null)
                        return null;
                    var client = new HttpClient();
                    var body = new Listing();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(string.Concat(this.rapidApiUrls.detail, queryString, defaultString)),
                        Headers =
                            {
                                { "x-rapidapi-key", rapidApiData.x_rapidapi_key },
                                { "x-rapidapi-host", rapidApiData.x_rapidapi_host },
                             },
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        if (response.ReasonPhrase.Contains("Too Many Requests"))
                        {
                            await this.rapidApiService.UpdateAsync(rapidApiData.Id);
                            await GetPropertyDetail(companyId, queryString);
                            return null;
                        }
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        JObject json = JObject.Parse(result);
                        cacheService.Set(BusinessConstants.PROP_DETAIL + queryString, json);
                        return json;
                        //return await response.Content.ReadAsAsync<PropertyDetail>(new[] { new JsonMediaTypeFormatter() });
                    }
                }
                else
                {
                    return cachedJson;
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
                return null;
            }    
        }

        public async Task<IEnumerable<JObject>> GetAllFeaturedListingMLS(int companyId)
        {
            //ReferenceNumber = W5054882 & PropertyID = 22639958
            var details = new List<JObject>();
            var defaultString = "&PreferedMeasurementUnit=1&CultureId=1";
            //https://realtor-canadian-real-estate.p.rapidapi.com/properties/detail?ReferenceNumber=W5054882&PropertyID=22639958&PreferedMeasurementUnit=1&CultureId=1
            try
            {
                var featuredlistings = await this.featuredListingService.GetAllAsync(companyId);
                if (featuredlistings != null && featuredlistings.Count() > 0)
                {
                    var rapidApiData = await this.rapidApiService.GetMasterAsync(companyId);
                    if (rapidApiData == null)
                        return null;

                    foreach (FeaturedListing featuredListing in featuredlistings)
                    {
                        var cachedJson = cacheService.Get<JObject>(BusinessConstants.PROP_SUMMRY + featuredListing.ReferenceNumber);
                        if (cachedJson == null)
                        {
                            var queryString = "ReferenceNumber=" + featuredListing.ReferenceNumber;
                            var client = new HttpClient();
                            var body = new Listing();
                            var request = new HttpRequestMessage
                            {
                                Method = HttpMethod.Get,
                                RequestUri = new Uri(string.Concat(this.rapidApiUrls.list_by_mls, queryString, defaultString)),
                                Headers =
                            {
                                { "x-rapidapi-key", rapidApiData.x_rapidapi_key },
                                { "x-rapidapi-host", rapidApiData.x_rapidapi_host },
                             },
                            };
                            using (var response = await client.SendAsync(request))
                            {
                                if(response.ReasonPhrase.Contains("Too Many Requests"))
                                {
                                    await this.rapidApiService.UpdateAsync(rapidApiData.Id);
                                    await GetAllFeaturedListingMLS(companyId);
                                    return null;
                                }
                                response.EnsureSuccessStatusCode();
                                var result = await response.Content.ReadAsStringAsync();
                                JObject json = JObject.Parse(result);
                                //var detail = await response.Content.ReadAsAsync<Listing>(new[] { new JsonMediaTypeFormatter() });
                                details.Add(json);
                                cacheService.Set(BusinessConstants.PROP_SUMMRY + featuredListing.ReferenceNumber, json);
                            }
                        }
                        else
                        {
                            details.Add(cachedJson);
                        }                        
                    }
                }
                return details;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<JObject>> GetSavedListingMLS(int companyId,int userId)
        {
            //ReferenceNumber = W5054882 & PropertyID = 22639958
            var details = new List<JObject>();
            var defaultString = "&PreferedMeasurementUnit=1&CultureId=1";
            //https://realtor-canadian-real-estate.p.rapidapi.com/properties/detail?ReferenceNumber=W5054882&PropertyID=22639958&PreferedMeasurementUnit=1&CultureId=1
            try
            {
                var featuredlistings = await this.savedListingService.GetAllAsync(companyId, userId);
                if (featuredlistings != null && featuredlistings.Count() > 0)
                {
                    var rapidApiData = await this.rapidApiService.GetMasterAsync(companyId);
                    if (rapidApiData == null)
                        return null;

                    foreach (SavedListing featuredListing in featuredlistings)
                    {
                        try
                        {
                            var cachedJson = cacheService.Get<JObject>(BusinessConstants.PROP_SUMMRY + featuredListing.ReferenceNumber);
                            if (cachedJson == null)
                            {
                                var queryString = "ReferenceNumber=" + featuredListing.ReferenceNumber;
                                var client = new HttpClient();
                                var body = new Listing();
                                var request = new HttpRequestMessage
                                {
                                    Method = HttpMethod.Get,
                                    RequestUri = new Uri(string.Concat(this.rapidApiUrls.list_by_mls, queryString, defaultString)),
                                    Headers =
                            {
                                { "x-rapidapi-key", rapidApiData.x_rapidapi_key },
                                { "x-rapidapi-host", rapidApiData.x_rapidapi_host },
                             },
                                };
                                using (var response = await client.SendAsync(request))
                                {
                                    if (response.ReasonPhrase.Contains("Too Many Requests"))
                                    {
                                        await this.rapidApiService.UpdateAsync(rapidApiData.Id);
                                        await GetSavedListingMLS(companyId,userId);
                                        return null;
                                    }
                                    response.EnsureSuccessStatusCode();
                                    var result = await response.Content.ReadAsStringAsync();
                                    JObject json = JObject.Parse(result);
                                    cacheService.Set(BusinessConstants.PROP_SUMMRY + featuredListing.ReferenceNumber, json);
                                    //var detail = await response.Content.ReadAsAsync<Listing>(new[] { new JsonMediaTypeFormatter() });
                                    details.Add(json);
                                }
                            }
                            else
                            {
                                details.Add(cachedJson);
                            }
                        }
                        catch { }

                    }
                }
                return details;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
                return null;
            }
        }


        public async Task<JObject> GetPropertyDetailbyMLS(int companyId, string queryString)
        {
            //ReferenceNumber = W5054882 & PropertyID = 22639958
            var defaultString = "&CultureId=1";
            ////https://realtor-canadian-real-estate.p.rapidapi.com/properties/list-by-mls?ReferenceNumber=30513228-1&CultureId=1
            try
            {
                var cachedJson = cacheService.Get<JObject>(BusinessConstants.PROP_DETAIL + queryString);
                if (cachedJson == null)
                {
                    var rapidApiData = await this.rapidApiService.GetMasterAsync(companyId);
                    if (rapidApiData == null)
                        return null;
                    var client = new HttpClient();
                    var body = new Listing();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(string.Concat(this.rapidApiUrls.list_by_mls, queryString, defaultString)),
                        Headers =
                            {
                                { "x-rapidapi-key", rapidApiData.x_rapidapi_key },
                                { "x-rapidapi-host", rapidApiData.x_rapidapi_host },
                             },
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        if (response.ReasonPhrase.Contains("Too Many Requests"))
                        {
                            await this.rapidApiService.UpdateAsync(rapidApiData.Id);
                            await GetPropertyDetailbyMLS(companyId, queryString);
                            return null;
                        }
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        JObject json = JObject.Parse(result);
                        cacheService.Set(BusinessConstants.PROP_DETAIL + queryString, json);
                        return json;
                        //return await response.Content.ReadAsAsync<Listing>(new[] { new JsonMediaTypeFormatter() });
                    }
                }
                else
                {
                    return cachedJson;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
