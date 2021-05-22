using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.IServices
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync(int companyId);
        Task<int> AddAsync(Blog obj);
        Task DeleteAsync(int id);
    }
}
