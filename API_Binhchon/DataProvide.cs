using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace API_Binhchon
{
    public class DataProvide
    {
        private static String connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.9.195.245)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=DBKHTG)));User Id=X16_HANT;Password=Evnhanoi123;";
        private static OracleConnection conn;
        private static OracleCommand cm;
        private static OracleDataReader dr;
        //private static OracleDataReader dr;

        public OracleConnection OraConn
        {
            get
            {
                if (conn == null)
                {
                    //string strConn = "server = CMIS2HM" + ";user=CMIS01" + ";password =CmIs01";

                    conn = new OracleConnection();
                    conn.ConnectionString = connectionString;
                }
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                return conn;
            }
        }
        public DataSet getDataSet(String query)
        {
            DataSet ds = new DataSet();
            conn = new OracleConnection(connectionString);
            conn.Open();
            OracleDataAdapter da = new OracleDataAdapter(query, conn);
            da.Fill(ds/*, query.Split(' ')[3]*/);
            conn.Close();
            return ds;
        }
        public string change(String query)
        {
            string re = "";
            try
            {
                conn = new OracleConnection(connectionString);
                cm = new OracleCommand(query, conn);
                conn.Open();
                re = cm.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                re = ex.ToString();
            }
            finally
            {
                conn.Dispose();
            }
            return re;
        }
        public Int64 getIntegerNumber(String query)
        {
            Int64 result = 0;
            conn = new OracleConnection(connectionString);
            cm = new OracleCommand(query, conn);
            conn.Open();
            dr = cm.ExecuteReader(CommandBehavior.SingleResult);
            while (dr.Read())
            {
                try
                {
                    result = Int64.Parse(dr[0].ToString());
                }
                catch (System.Exception)
                {

                }
            }
            conn.Close();
            return result;
        }
        public Int64 ins_binhchon(string ma_dviqly, string MAC_adrress)
        {
            string sql = "INSERT INTO X16_HANT.BINHCHON_2023 (ID, MA_DVIQLY, ADRRES_MAC) VALUES ( null,'"+ma_dviqly+"','"+MAC_adrress+"')";
            Int64 re = Int64.Parse(change(sql));
            return re;
        }

        public Int64 get_mac(string MAC_adrress)
        {
            string sql = "select count(*) as tong from X16_HANT.BINHCHON_2023  where  ADRRES_MAC = '"+MAC_adrress+"'";
            Int64 re = getIntegerNumber(sql);
            return re;
        }
        public Int64 get_ma_dvi(string ma_dviqly)
        {
            string sql = "select count(*) as tong from X16_HANT.BINHCHON_2023  where  MA_DVIQLY = '" + ma_dviqly + "'";
            Int64 re = getIntegerNumber(sql);
            return re;
        }

    }
}