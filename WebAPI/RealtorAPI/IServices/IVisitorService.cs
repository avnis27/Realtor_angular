using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.IServices
{
    public interface IVisitorService
    {
        Task<IEnumerable<Visitor>> GetAllAsync(int companyId);
        Task<int> AddAsync(Visitor obj);
        //Task<int> UpdateAsync(Visitor obj);
        Task DeleteAsync(int id);
    }
}
