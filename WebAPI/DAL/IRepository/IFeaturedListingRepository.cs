using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepository
{
    public interface IFeaturedListingRepository
    {
        Task<IEnumerable<FeaturedListing>> GetAllAsync(int companyId);
        Task<int> AddAsync(FeaturedListing obj);
        Task DeleteAsync(int id);
    }
}
