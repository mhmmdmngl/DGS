using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DGS.Models;

namespace DGS.Context
{
    public class DENEME_SONUC_CTX
    {
        public List<DENEME_SONUC> tumsonucDondur(int yetID)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from DENEME_SONUC where YETKILIID = :YETKILIID";
                return x.Query<DENEME_SONUC>(sorgu, new { YETKILIID = yetID}).ToList();
            }
        }
        public DENEME_SONUC sonucdondur(int id)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from DENEME_SONUC where ID = :ID";
                return x.Query<DENEME_SONUC>(sorgu, new { ID = id }).FirstOrDefault();
            }
        }
        public int sonucEkle(DENEME_SONUC se)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "insert into DENEME_SONUC (DENEMEID, DOGRU, YANLIS, BOS, YETKILIID) values (:DENEMEID, :DOGRU, :YANLIS, :BOS, :YETKILIID)";
                return x.Execute(sorgu, new { DENEMEID = se.DENEMEID,  DOGRU = se.DOGRU, YANLIS = se.YANLIS, BOS = se.BOS, YETKILIID = se.YETKILIID });
            }
        }
        public int sonucDuzenle(DENEME_SONUC sd)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "update DENEME_SONUC set DENEMEID= :DENEMEID,  DOGRU= :DOGRU, YANLIS= :YANLIS, BOS= :BOS where ID= :ID ";
                return x.Execute(sorgu, new { DENEMEID = sd.DENEMEID,  DOGRU = sd.DOGRU, YANLIS = sd.YANLIS, BOS = sd.BOS, ID = sd.ID});
            }
        }
        public int sonucSil(int id)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "delete DENEME_SONUC where ID= : ID";
                return x.Execute(sorgu, new { ID = id });
            }
        }
    }
}