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
                string query = @"SELECT m.MunkalapID, m.Rendszam, m.Hiba_leirasa, m.Allapot, m.Rogzites_datuma, m.UgyfelID, u.Nev 
                                 FROM Munkalapok m 
                                 JOIN Ugyfelek u ON m.UgyfelID = u.UgyfelID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            return table;
        }

        public void FrissitMunkalap(int id, string rendszam, string hiba, string allapot, int ugyfelId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string query = @"UPDATE Munkalapok 
                                 SET Rendszam = @rendszam, Hiba_leirasa = @hiba, Allapot = @allapot, UgyfelID = @ugyfelId 
                                 WHERE MunkalapID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@rendszam", rendszam);
                cmd.Parameters.AddWithValue("@hiba", hiba);
                cmd.Parameters.AddWithValue("@allapot", allapot);
                cmd.Parameters.AddWithValue("@ugyfelId", ugyfelId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteMunkalap(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string query = "DELETE FROM Munkalapok WHERE MunkalapID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetUgyfelek()
        {
            DataTable table = new DataTable();
            using (MySqlConnection conn = GetConnection())
            {
                string query = "SELECT UgyfelID, Nev FROM Ugyfelek ORDER BY Nev ASC";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            return table;
        }

        public void HozzaadMunkalap(string rendszam, string hiba, string allapot, int ugyfelId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string query = @"INSERT INTO Munkalapok (Rendszam, Hiba_leirasa, Allapot, UgyfelID) 
                                 VALUES (@rendszam, @hiba, @allapot, @ugyfelId)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@rendszam", rendszam);
                cmd.Parameters.AddWithValue("@hiba", hiba);
                cmd.Parameters.AddWithValue("@allapot", allapot);
                cmd.Parameters.AddWithValue("@ugyfelId", ugyfelId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void HozzaadUgyfel(string nev, string telefonszam)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string query = "INSERT INTO Ugyfelek (Nev, Telefonszam) VALUES (@nev, @tel)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nev", nev);
                cmd.Parameters.AddWithValue("@tel", telefonszam);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
