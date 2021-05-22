using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.Settings
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

        public string EnableSsl { get; set; }

        public string To { get; set; }

        public string EmailFeature { get; set; }
       
    }
}
