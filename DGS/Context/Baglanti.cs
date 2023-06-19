using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DGS.Context
{
    public class Baglanti
    {
        private readonly string connString = "Data Source=(DESCRIPTION="
                     + "(ADDRESS=(PROTOCOL=TCP)(HOST=193.140.140.8)(PORT=1521))"
                     + "(CONNECT_DATA=(SERVICE_NAME=orc1)));"
                     + "User Id=BAHARDENEME;Password=!*1235321!;";

        //Veri tabanı bağlantısı yapıyor
        public IDbConnection GetConnection()
        {
            var conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }
        public void CloseConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Open || conn.State == ConnectionState.Broken)
            {
                conn.Close();
            }
        }
    }
}