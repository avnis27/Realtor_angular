using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Blog
    {    
        public int Id { get; set; }
        public int CompanyId { get; set; }        
        public string Title { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
    }
}
