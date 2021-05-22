using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PropertyDetail
    {
        public ErrorCode ErrorCode { get; set; }
        public string HashCode { get; set; }
        public string Id { get; set; }
        public string MlsNumber { get; set; }
        public string PublicRemarks { get; set; }
        public string LastUpdated { get; set; }
        public Building Building { get; set; }
        public Land Land { get; set; }
        public List<Individual> Individual { get; set; }
        public Property Property { get; set; }
        public string UploadedBy { get; set; }
        public Business Business { get; set; }
        public string RelativeURLEn { get; set; }
        public string RelativeURLFr { get; set; }
        public List<object> History { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    
    public class Room
    {
        public string Type { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string Level { get; set; }
        public string Dimension { get; set; }
    }   
}
