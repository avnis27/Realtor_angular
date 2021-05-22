using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.IServices
{
    public interface INewsLetterService
    {
        Task<IEnumerable<NewsLetterSubscription>> GetAllAsync(int companyId);
        Task<int> AddAsync(NewsLetterSubscription obj);
        Task DeleteAsync(int id);
    }
}
