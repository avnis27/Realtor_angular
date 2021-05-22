using DAL.IRepository;
using DAL.Models;
using RealtorAPI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.Services
{
    public class FeaturedListingService: IFeaturedListingService
    {
        private readonly IFeaturedListingRepository FeaturedListingRepository;
        public FeaturedListingService(IFeaturedListingRepository FeaturedListingRepository)
        {
            this.FeaturedListingRepository = FeaturedListingRepository;
        }
        public async Task<int> AddAsync(FeaturedListing obj)
        {
            return await this.FeaturedListingRepository.AddAsync(obj);
        }

        public async Task DeleteAsync(int id)
        {
            await this.FeaturedListingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<FeaturedListing>> GetAllAsync(int companyId)
        {
            return await this.FeaturedListingRepository.GetAllAsync(companyId);
        }
    }
}
