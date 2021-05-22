using DAL.IRepository;
using DAL.Models;
using RealtorAPI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.Services
{
    public class BlogService:IBlogService
    {
        private readonly IBlogRepository BlogRepository;
        public BlogService(IBlogRepository BlogRepository)
        {
            this.BlogRepository = BlogRepository;
        }
        public async Task<int> AddAsync(Blog obj)
        {
            return await this.BlogRepository.AddAsync(obj);
        }

        public async Task DeleteAsync(int id)
        {
            await this.BlogRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Blog>> GetAllAsync(int companyId)
        {
            return await this.BlogRepository.GetAllAsync(companyId);
        }
    }
}
