using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVODQ6_SzervizApp
{
    public class DatabaseHelper
    {
        private readonly string connectionString = "Server=localhost;Database=SzervizDB;Uid=root;Pwd=;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public DataTable GetMunkalapok()
        {
            DataTable table = new DataTable();
            using (MySqlConnection conn = GetConnection())
            {
                string query = @"SELECT m.MunkalapID, m.Rendszam, m.Hiba_leirasa, m.Allapot, m.Rogzites_datuma, u.Nev 
                                 FROM Munkalapok m 
                                 JOIN Ugyfelek u ON m.UgyfelID = u.UgyfelID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            return table;
        }
    }
}
