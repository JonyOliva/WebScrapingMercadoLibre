using System;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;

namespace WebScrapingMercadoLibre
{
    class Dao
    {
        SqlConnection connection;
        public Dao()
        {
            connection = new SqlConnection(getConnectioString());
            Console.WriteLine(getIdfromDate(DateTime.Now));

            /*
            Logger logger = new Logger();
            logger.save(DateTime.Now, "otro ok");*/
        }

        private int getIdfromDate(DateTime date)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from fechas where fecha='" + date.ToString("MM/dd/yyyy hh:mm:ss") + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                int id = reader.GetInt32(0);
                reader.Close();
                connection.Close();
                //Console.WriteLine(reader.GetDateTime(1));
                return id;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }
        private bool saveDate(DateTime date)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                string fecha = DateTime.Now.ToString("MM/dd/yyyy hh:mm:s tt", new CultureInfo("en-US"));
                cmd.CommandText = "INSERT INTO FECHAS(Fecha) SELECT '" + date.ToString("MM/dd/yyyy hh:mm:ss") + "'";
                int res = cmd.ExecuteNonQuery();
                connection.Close();
                return Convert.ToBoolean(res);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        private string getConnectioString()
        {
            using (FileStream file = new FileStream("conn.cfg", FileMode.Open, FileAccess.Read))
            {
                StreamReader reader = new StreamReader(file);
                string conn = reader.ReadLine();
                reader.Close();
                return conn;
            }
        }

    }
}
