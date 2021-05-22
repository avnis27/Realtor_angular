using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Appointment
    {    
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public DateTime PreferredDate { get; set; }
        public string PreferredTime { get; set; }
        public string ReferenceNumber { get; set; }
        public string PropertyID { get; set; }        
    }
}
