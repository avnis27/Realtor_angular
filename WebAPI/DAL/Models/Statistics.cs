using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Statistics
    {
        public ErrorCode ErrorCode { get; set; }
        public List<Datum> Data { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


    public class Value
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class Datum
    {
        public string key { get; set; }
        public List<Value> value { get; set; }
    }
}
