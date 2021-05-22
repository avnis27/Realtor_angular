using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.IServices
{
    public interface IAgentService
    {
        Task<Agent> GetAllAsync(int companyId);
        Task UpdateAsync(Agent obj);
    }
}
