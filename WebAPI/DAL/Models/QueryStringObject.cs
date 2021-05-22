using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class QueryStringObject
    {
        //LatitudeMax=81.14747595814636&CurrentPage=1&LongitudeMin=-136.83037765324116&LongitudeMax=-10.267941690981388&RecordsPerPage=10&
        //LatitudeMin=-22.26872153207163&SortOrder=A&NumberOfDays=0&BedRange=0-0&CultureId=1&BathRange=0-0&SortBy=1&PriceMin=0
        public string Type { get; set; }
        public string LatitudeMax { get; set; }
        public string LatitudeMin { get; set; }
        public string LongitudeMin { get; set; }
        public string LongitudeMax { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int RecordsPerPage { get; set; } = 10;
        public string SortOrder { get; set; } = "A";
        public int NumberOfDays { get; set; } = 0;
        public string BedRange { get; set; } = "0-0";
        public int CultureId { get; set; } = 1;
        public int BathRange { get; set; } = 0;
        public int SortBy { get; set; } = 1;
        public int PriceMin { get; set; } = 0;
        public int BuildingTypeId { get; set; } = 1;
        public int TransactionTypeId { get; set; } = 2;
    }
}
