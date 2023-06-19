using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGS.Models
{
    public class DENEMELER
    {
        public int ID { get; set; }
        public string DENEMEADI { get; set; }
        public int YETKILIID { set; get; }
        public YETKILI yet { set; get; }
    }
}