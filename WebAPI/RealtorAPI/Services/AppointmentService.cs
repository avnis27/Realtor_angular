using DAL.IRepository;
using DAL.Models;
using RealtorAPI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }
        public async Task<int> AddAsync(Appointment obj)
        {
            return await this.appointmentRepository.AddAsync(obj);
        }

        public async Task DeleteAsync(int id)
        {
            await this.appointmentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync(int companyId, int userId)
        {
            return await this.appointmentRepository.GetAllAsync(companyId, userId);
        }
    }
}
