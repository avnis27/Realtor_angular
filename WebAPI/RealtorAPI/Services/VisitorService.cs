using DAL.IRepository;
using DAL.Models;
using RealtorAPI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.Services
{
    public class VisitorService:IVisitorService
    {
        private readonly IVisitorRepository VisitorRepository;
        public VisitorService(IVisitorRepository VisitorRepository)
        {
            this.VisitorRepository = VisitorRepository;
        }
        public async Task<int> AddAsync(Visitor obj)
        {
            return await this.VisitorRepository.AddAsync(obj);
        }

        public async Task DeleteAsync(int id)
        {
            await this.VisitorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Visitor>> GetAllAsync(int companyId)
        {
            return await this.VisitorRepository.GetAllAsync(companyId);
        }
    }
}
