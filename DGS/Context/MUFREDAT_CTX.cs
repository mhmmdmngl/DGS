using DGS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace DGS.Context
{
    public class MUFREDAT_CTX
    {
        public List<MUFREDAT> mufredatDondur()
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from MUFREDAT";
                return x.Query<MUFREDAT>(sorgu).ToList();
            }
        }
        public MUFREDAT konuDondur(int id)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from MUFREDAT where ID = :ID";
                return x.Query<MUFREDAT>(sorgu, new { ID = id}).FirstOrDefault();
            }
        }
        public int konuEkle(MUFREDAT ke)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "insert into MUFREDAT (KONUADI) values (:KONUADI)";
                return x.Execute(sorgu, new { KONUADI = ke.KONUADI });
            }
        }
        public int konuduzenle(MUFREDAT kd)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "update MUFREDAT set KONUADI = :KONUADI where ID= :ID";
                return x.Execute(sorgu, new {KONUADI = kd.KONUADI, ID = kd.ID });
            }
        }
        public int konusil(int id)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "delete from MUFREDAT where ID = :ID";
                return x.Execute(sorgu, new { ID = id});
            }
        }
    }
}