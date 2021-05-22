using DAL.IRepository;
using DAL.Models;
using RealtorAPI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.Services
{
    public class NewsLetterService: INewsLetterService
    {
        private readonly INewsLetterRepository NewsLetterRepository;
        public NewsLetterService(INewsLetterRepository NewsLetterRepository)
        {
            this.NewsLetterRepository = NewsLetterRepository;
        }
        public async Task<int> AddAsync(NewsLetterSubscription obj)
        {
            return await this.NewsLetterRepository.AddAsync(obj);
        }

        public async Task DeleteAsync(int id)
        {
            await this.NewsLetterRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<NewsLetterSubscription>> GetAllAsync(int companyId)
        {
            return await this.NewsLetterRepository.GetAllAsync(companyId);
        }
    }
}
