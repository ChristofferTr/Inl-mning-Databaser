using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;


namespace SpelDatabasApp
{
    class Program
    {

        
        static void Main(string[] args)
        {
            string connectionString = "Server=your_server_address;Database=your_database_name;Uid=your_username;Pwd=your_password;";


            // Hämta alla spel från databasen
            DataTable spelTable = GetAllSpel(connectionString);
            DisplaySpel(spelTable);

            // Lägg till ett nytt spel i databasen
            AddSpel(connectionString, "Electronic Arts", "North America", 2022, "Action", "Example Game");

            // Hämta alla spel från databasen igen efter tillägg
            spelTable = GetAllSpel(connectionString);
            DisplaySpel(spelTable);

            Console.ReadLine();
        }

        private static void DisplaySpel(DataTable spelTable)
        {
            throw new NotImplementedException();
        }

        static DataTable GetAllSpel(string connectionString)
        {
            string query = "CALL GetAllSpel()";
            DataTable spelTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(spelTable);
                    }
                }
            }

            return spelTable;
        }

        static void AddSpel(string connectionString, string publisher, string region, int releaseYear, string genre, string gameName)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("AddSpel", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@spel_publisher", publisher);
                    command.Parameters.AddWithValue("@spel_region", region);
                    command.Parameters.AddWithValue("@spel_release_year", releaseYear);
                    command.Parameters.AddWithValue("@spel_genre", genre);
                    command.Parameters.AddWithValue("@spel_game_name", gameName);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
} 
       
