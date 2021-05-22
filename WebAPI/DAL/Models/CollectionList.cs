using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CollectionList
    {
        public ErrorCode ErrorCode { get; set; }
        public List<Word> Words { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class Word
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public string CultureId { get; set; }
    }
}
