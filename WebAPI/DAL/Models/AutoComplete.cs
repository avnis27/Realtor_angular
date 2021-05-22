using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AutoComplete
    {
        public ErrorCode ErrorCode { get; set; }
        public List<SubArea> SubArea { get; set; }
    }

    public class NorthEast
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class SouthWest
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class Viewport
    {
        public NorthEast NorthEast { get; set; }
        public SouthWest SouthWest { get; set; }
    }

    public class SubArea
    {
        public string Location { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public string InternalResult { get; set; }
        public Viewport Viewport { get; set; }
        public string GEOId { get; set; }
        public List<object> Polygons { get; set; }
    }
}
