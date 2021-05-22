using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Location
    {      
        public int Id { get; set; }
        public string Name { get; set; }
        public string LatitudeMin { get; set; }
        public string LatitudeMax { get; set; }
        public string LongitudeMin { get; set; }
        public string LongitudeMax { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
