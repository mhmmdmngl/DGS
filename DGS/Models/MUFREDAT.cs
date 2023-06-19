using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGS.Models
{
    public class MUFREDAT
    {
        public int ID { get; set; }
        public string KONUADI { get; set; }
        public SORULAR soru { set; get; }
    }
}