using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGS.Models
{
    public class YETKILI
    {
        public int ID { get; set; }
        public string EMAIL { get; set; }
        public string PAROLA { get; set; }
        public byte[] RESIM { set; get; }
        public string AD { get; set; }
        public string SOYAD { get; set; }
    }
}