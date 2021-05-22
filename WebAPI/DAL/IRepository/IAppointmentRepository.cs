﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepository
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync(int companyId,int userId);
        Task<int> AddAsync(Appointment obj);        
        Task DeleteAsync(long id);
    }
}
