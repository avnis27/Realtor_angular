using DAL.IRepository;
using DAL.Models;
using RealtorAPI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.Services
{
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository agentRepository;
        public AgentService(IAgentRepository agentRepository)
        {
            this.agentRepository = agentRepository;
        }
        public async Task<Agent> GetAllAsync(int companyId)
        {
            return await this.agentRepository.GetAllAsync(companyId);
        }

        public async Task UpdateAsync(Agent obj)
        {
            await this.agentRepository.UpdateAsync(obj);
        }
    }
}
