using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGS.Models
{
    public class SORULAR
    {
        public int ID { get; set; }
        public int KONUID { get; set; }
        public int DOGRU { get; set; }
        public int YANLIS { get; set; }
        public int BOS { get; set; }
        public DateTime TARIH { get; set; }
        public MUFREDAT mf { set; get; }
        public int YETKILIID { get; set; }
        public YETKILI yet { set; get; }
    }
}