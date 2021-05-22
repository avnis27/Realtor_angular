using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepository
{
    public interface ISavedListingRepository
    {
        Task<IEnumerable<SavedListing>> GetAllAsync(int companyId, int userId);
        Task<int> AddAsync(SavedListing obj);        
        Task DeleteAsync(int id);
    }
}
