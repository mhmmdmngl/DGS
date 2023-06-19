using DGS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace DGS.Context
{
    public class SORULAR_CTX
    {
        public List<SORULAR> soruBankasiDondur(int yetID)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from SORULAR where YETKILIID = :YETKILIID";
                return x.Query<SORULAR>(sorgu, new { YETKILIID = yetID }).ToList();
            }
        }
        public SORULAR testDondur(int id)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from SORULAR where ID = :ID";
                return x.Query<SORULAR>(sorgu, new { ID = id}).FirstOrDefault();
            }
        }
        public int testEkle(SORULAR te)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "insert into SORULAR (KONUID, DOGRU, YANLIS, BOS, YETKILIID) values (:KONUID, :DOGRU, :YANLIS, :BOS, :YETKILIID)";
                return x.Execute(sorgu, new { KONUID = te.KONUID, DOGRU = te.DOGRU, YANLIS = te.YANLIS, BOS = te.BOS, YETKILIID = te.YETKILIID });
            }
        }
        public int testDuzenle(SORULAR td)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "update SORULAR set KONUID= :KONUID, DOGRU= :DOGRU, YANLIS= :YANLIS, BOS= :BOS";
                return x.Execute(sorgu, new { KONUID = td.KONUID, DOGRU = td.DOGRU, YANLIS = td.YANLIS, BOS = td.BOS });
            }
        }
        public int testSil(int id)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "delete from SORULAR where ID= :ID";
                return x.Execute(sorgu, new { ID = id });
            }
        }
    }
}