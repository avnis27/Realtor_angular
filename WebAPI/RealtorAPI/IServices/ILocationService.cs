using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.IServices
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetMasterAsync();
    }
}
