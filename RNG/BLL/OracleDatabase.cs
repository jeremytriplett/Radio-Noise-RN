using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace RNG.BLL
{
    class OracleDatabase
    {
        //old code, ignore
        public static void InsertRNGBit(int bitValue, int bitNum)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = "Data Source=localhost:1521/orcl.home;User Id=system;Password=;";
            conn.Open();

            string sql = "INSERT INTO rng_bits VALUES(:bitValue, :bitNum)";

            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("bitValue", bitValue));
            cmd.Parameters.Add(new OracleParameter("bitNum", bitNum));

            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public static void Add(string s)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = "Data Source=localhost:1521/orcl.home;User Id=system;Password=;";
            conn.Open();

            string sql = "INSERT INTO check_rng VALUES (:s)";

            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("s", Convert.ToInt32(s)));

            cmd.ExecuteNonQuery();
            conn.Close();

        }


        public static List<string> GetBits(int numBitsToTake)
        {

            List<string> ret = new List<string>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = "Data Source=localhost:1521/orcl.home;User Id=system;Password=;";
            conn.Open();

            string sql = "SELECT max(bit_num) FROM rng_bits";
            OracleCommand cmd = new OracleCommand(sql, conn);

            int rowThreshold = Convert.ToInt32(cmd.ExecuteScalar()) - numBitsToTake;


            sql = "SELECT bit_value FROM rng_bits WHERE bit_num > :rowThreshold";
            cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("rowThreshold", rowThreshold));

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ret.Add(reader.GetInt32(0).ToString());
            }

            sql = "DELETE FROM rng_bits WHERE bit_num > :rowThreshold";
            cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("rowThreshold", rowThreshold));
            cmd.ExecuteNonQuery();

            conn.Close();

            return ret;

           
        }
    }
}
