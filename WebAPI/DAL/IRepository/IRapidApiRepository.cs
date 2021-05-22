using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepository
{
    public interface IRapidApiRepository
    {
        Task<RapidApiDetail> GetMasterAsync(int companyId);
        Task UpdateAsync(int currentId);
    }
}
