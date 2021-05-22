using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public int UserTypeId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string ActivationCode { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpires { get; set; }

    }
}
