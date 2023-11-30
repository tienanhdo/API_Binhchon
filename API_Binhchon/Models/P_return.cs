using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Binhchon.Models
{
    public class P_return
    {
            public string STATUS { get; set; }
            public string MESSAGE { get; set; }
            public Object DATA { get; set; }
    }

    public class P_data
    {
        public string TEN_DVIQLY { get; set; }
        public string TONG { get; set; }
    }

}