using Dapper;
using DGS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGS.Context
{
    public class YETKILI_CTX
    {
        public List<YETKILI> yetkiliListesiDondur()
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from YETKILI";
                return x.Query<YETKILI>(sorgu).ToList();
            }
        }
       
        public YETKILI yetkiliDondur(int id)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from YETKILI where ID = :ID";
                return x.Query<YETKILI>(sorgu, new { ID = id }).FirstOrDefault();
            }
        }

        public int yetkiliEkle(YETKILI ye)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "insert into YETKILI (EMAIL, PAROLA) values (:EMAIL, :PAROLA)";
                return x.Execute(sorgu, new {EMAIL = ye.EMAIL, PAROLA = ye.PAROLA});
            }
        }

        public int yetkiliDuzenle(YETKILI yd)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "update YETKILI set EMAIL = :EMAIL, PAROLA = :PAROLA where ID= :ID ";
                return x.Execute(sorgu, new { EMAIL = yd.EMAIL, PAROLA = yd.PAROLA });
            }
        }
        public int yetkiliSil(int id)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "delete YETKILI where ID = : ID";
                return x.Execute(sorgu, new { ID = id });
            }
        }
        public YETKILI yetkiliDondurByMail(string mail)
        {
            Baglanti bag = new Baglanti();
            using (var x = bag.GetConnection())
            {
                string sorgu = "select * from YETKILI where EMAIL = :EMAIL";
                return x.Query<YETKILI>(sorgu, new { EMAIL = mail }).FirstOrDefault();
            }
        }
    }
}