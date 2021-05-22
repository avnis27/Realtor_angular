using DAL.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.IServices
{
    public interface IRealtorService
    {
        Task<JObject> GetResidentialData(int companyId, string queryString);
        Task<JObject> GetCommercialData(int companyId, string queryString);
        Task<AutoComplete> GetAutoComplete();
        Task<Statistics> GetStatistics();
        Task<CollectionList> GetList();
        Task<JObject> GetPropertyDetail(int companyId, string queryString);
        Task<IEnumerable<JObject>> GetAllFeaturedListingMLS(int companyId);
        Task<IEnumerable<JObject>> GetSavedListingMLS(int companyId, int userId);
        Task<JObject> GetPropertyDetailbyMLS(int companyId, string queryString);
    }
}
