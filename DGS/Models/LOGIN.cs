using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGS.Models
{
    public class LOGIN
    {
        public string EMAIL { get; set; }
        public string PAROLA { get; set; }
        public YETKILI yeti { get; set; }
    }
}