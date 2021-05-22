using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class FeaturedListing
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string ReferenceNumber { get; set; }
        public string PropertyID { get; set; }        
    }
}
