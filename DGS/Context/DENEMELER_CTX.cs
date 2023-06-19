using DGS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace DGS.Context
{
    public class DENEMELER_CTX
    {
        public List<DENEMELER> denemeListesiDondur(int yetkiliID)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from DENEMELER where YETKILIID = :YETKILIID";
                return x.Query<DENEMELER>(sorgu, new { YETKILIID = yetkiliID }).ToList();
            }
        }
        public DENEMELER denemeDondur(int Id)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from DENEMELER where ID = :ID";
                return x.Query<DENEMELER>(sorgu, new { ID = Id }).FirstOrDefault();
            }
        }
        public int denemeEkle(DENEMELER de)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "insert into DENEMELER (DENEMEADI, YETKILIID) values (:DENEMEADI, :YETKILIID)";
                return x.Execute(sorgu, new { DENEMEADI = de.DENEMEADI, YETKILIID = de.YETKILIID });
            }
        }
        public int denemeDuzenle(DENEMELER dd)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "update DENEMELER set DENEMEADI = :DENEMEADI where ID = :ID";
                return x.Execute(sorgu, new { DENEMEADI = dd.DENEMEADI, ID = dd.ID });
            }
        }
        public int denemeSil(int id)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "delete DENEMELER where ID= :ID";
                return x.Execute(sorgu, new { ID = id });
            }
        }
    }
}