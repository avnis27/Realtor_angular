using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepository
{
    public interface INewsLetterRepository
    {
        Task<IEnumerable<NewsLetterSubscription>> GetAllAsync(int companyId);
        Task<int> AddAsync(NewsLetterSubscription obj);
        Task DeleteAsync(int id);
    }
}
