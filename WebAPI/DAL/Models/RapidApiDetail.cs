using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class RapidApiDetail
    {    
        public int Id { get; set; }
        public string EMail { get; set; }
        public string x_rapidapi_key { get; set; }
        public string x_rapidapi_host { get; set; }
        public int CompanyId { get; set; }        
    }
}
