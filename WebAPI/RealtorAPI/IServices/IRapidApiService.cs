using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.IServices
{
    public interface IRapidApiService
    {
       public Task<RapidApiDetail> GetMasterAsync(int companyId);
        public Task UpdateAsync(int currentId);
    }
}
