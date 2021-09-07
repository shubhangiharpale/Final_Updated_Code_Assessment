using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Updated_Code_Assessment.DTO
{
    public class LoginRequest
    {

        //public Environment Environment { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SessionProductId { get; set; }
        public long ClientTypeId { get; set; }
        public string LanguageCode { get; set; }
        public long NumLaunchTokens { get; set; }
        public string MarketType { get; set; }
    }

    public partial class Environment
    {

    }

}

