using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepository
{
    public interface IAgentRepository
    {
        Task<Agent> GetAllAsync(int companyId);
        Task UpdateAsync(Agent obj);        
    }
}
