using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGS.Models
{
    public class STATS
    {
        public int toplamSoru { get; set; }
        public int toplamDogru { get; set; }
        public int toplamYanlis { get; set; }
        public int toplamBos { get; set; }
        public int toplamNET { set; get; }
    }
}