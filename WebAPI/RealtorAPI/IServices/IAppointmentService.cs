using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.IServices
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAsync(int companyId, int userId);
        Task<int> AddAsync(Appointment obj);
        Task DeleteAsync(int id);
    }
}
