using DAL.IRepository;
using DAL.Models;
using RealtorAPI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.Services
{
    public class SavedListingService: ISavedListingService
    {
        private readonly ISavedListingRepository SavedListingRepository;
        public SavedListingService(ISavedListingRepository SavedListingRepository)
        {
            this.SavedListingRepository = SavedListingRepository;
        }
        public async Task<int> AddAsync(SavedListing obj)
        {
            return await this.SavedListingRepository.AddAsync(obj);
        }

        public async Task DeleteAsync(int id)
        {
            await this.SavedListingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SavedListing>> GetAllAsync(int companyId, int userId)
        {
            return await this.SavedListingRepository.GetAllAsync(companyId, userId);
        }
    }
}
