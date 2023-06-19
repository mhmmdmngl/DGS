using Dapper;
using DGS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGS.Context
{
    public class AYRINTILAR_CTX
    {
        public List<AYRINTILAR> ayrintiListesiDondur()
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from AYRINTILAR";
                return x.Query<AYRINTILAR>(sorgu).ToList();
            }
        }
        //Bu fonksiyon sonradan eklendi...Ayrıntılar tablosunda belirli bir denemeID'ye sahip tüm satırları geri döndürür
        public List<AYRINTILAR> ayrintiDondurbyDenemeID(int denemeID)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from AYRINTILAR where DENEMEID = :DENEMEID";
                return x.Query<AYRINTILAR>(sorgu, new { DENEMEID = denemeID }).ToList();
            }
        }
        public AYRINTILAR ayrintiDondur(int id)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from AYRINTILAR where ID = :ID";
                return x.Query<AYRINTILAR>(sorgu, new { ID = id }).FirstOrDefault();
            }
        }
        public int ayrintiEkle(AYRINTILAR ay)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "insert into AYRINTILAR (DENEMEID, KONUID, DOGRU, YANLIS, BOS, YETKILIID) values (:DENEMEID, :KONUID, :DOGRU, :YANLIS, :BOS, :YETKILIID)";
                return x.Execute(sorgu, new { DENEMEID = ay.DENEMEID, KONUID = ay.KONUID, DOGRU = ay.DOGRU, YANLIS = ay.YANLIS, BOS = ay.BOS, YETKILIID = ay.yetkiliID });
            }
        }
        public int ayrintiDuzenle(AYRINTILAR sd)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "update AYRINTILAR set DENEMEID = :DENEMEID, KONUID = :KONUID, DOGRU = :DOGRU, YANLIS =:YANLIS, BOS = :BOS where ID= :ID ";
                return x.Execute(sorgu, new { DENEMEID = sd.DENEMEID, KONUID = sd.KONUID, DOGRU = sd.DOGRU, YANLIS = sd.YANLIS, BOS = sd.BOS, ID = sd.ID });
            }
        }
        public int ayrintiSil(int id)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "delete AYRINTILAR where ID= : ID";
                return x.Execute(sorgu, new { ID = id });
            }
        }
    }
}