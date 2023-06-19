using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGS.Models
{
    public class DENEME_SONUC
    {
        public int ID { get; set; }
        public int DENEMEID { get; set; }
        public int DOGRU { get; set; }
        public int YANLIS { get; set; }
        public int BOS { get; set; }
        public DateTime TARIH { get; set; }
        public MUFREDAT mf { get; set; }
        public DENEMELER dn { get; set; }
        public int YETKILIID { get; set; }
        public YETKILI yetki { get; set; }
    }
}