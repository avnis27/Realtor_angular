using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepository
{
    public interface IVisitorRepository
    {
        Task<IEnumerable<Visitor>> GetAllAsync(int companyId);   
        Task<int> AddAsync(Visitor obj);
        //Task<int> UpdateAsync(Visitor obj);
        Task DeleteAsync(int id);
    }
}
