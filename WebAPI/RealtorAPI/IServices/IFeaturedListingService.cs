using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.IServices
{
    public interface IFeaturedListingService
    {
        Task<IEnumerable<FeaturedListing>> GetAllAsync(int companyId);
        Task<int> AddAsync(FeaturedListing obj);
        Task DeleteAsync(int id);
    }
}
