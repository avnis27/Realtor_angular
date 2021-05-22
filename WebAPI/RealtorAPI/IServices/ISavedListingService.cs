using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.IServices
{
    public interface ISavedListingService
    {
        Task<IEnumerable<SavedListing>> GetAllAsync(int companyId, int userId);
        Task<int> AddAsync(SavedListing obj);
        Task DeleteAsync(int id);
    }
}
