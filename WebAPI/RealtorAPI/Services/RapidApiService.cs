using DAL.IRepository;
using DAL.Models;
using RealtorAPI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.Services
{
    public class RapidApiService : IRapidApiService
    {
        private readonly IRapidApiRepository rapidApiRepository;

        public RapidApiService(IRapidApiRepository rapidApiRepository)
        {
            this.rapidApiRepository = rapidApiRepository;
        }
        public async Task<RapidApiDetail> GetMasterAsync(int companyId)
        {
            return await this.rapidApiRepository.GetMasterAsync(companyId);
        }

        public async Task UpdateAsync(int currentId)
        {
            await this.rapidApiRepository.UpdateAsync(currentId);
        }
    }
}
