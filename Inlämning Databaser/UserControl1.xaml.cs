using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inlämning_Databaser
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private string connectionString = "127.0.0.1;Inlämning databas;root;HorizonAloy1234!;";

        public UserControl1()
        {
            InitializeComponent();
            LoadSpelData();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Anslutning till MySQL-databasen lyckades.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid anslutning till MySQL-databasen: " + ex.Message);
            }
        }
        



        private void LoadSpelData()
        {
            string query = "SELECT * FROM Spel";
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

            spelListBox.ItemsSource = spelTable.DefaultView;
            spelListBox.DisplayMemberPath = "game_name";
            spelListBox.SelectedValuePath = "id";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string publisher = publisherTextBox.Text;
            string region = regionTextBox.Text;
            int releaseYear = Convert.ToInt32(releaseYearTextBox.Text);
            string genre = genreTextBox.Text;

            AddSpel(publisher, region, releaseYear, genre);
            LoadSpelData();

            ClearTextBoxes();
        }

        private void AddSpel(string publisher, string region, int releaseYear, string genre)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Spel (publisher, region, release_year, genre) VALUES (@spel_publisher, @spel_region, @spel_release_year, @spel_genre)", connection))
                {
                    command.Parameters.AddWithValue("@spel_publisher", publisher);
                    command.Parameters.AddWithValue("@spel_region", region);
                    command.Parameters.AddWithValue("@spel_release_year", releaseYear);
                    command.Parameters.AddWithValue("@spel_genre", genre);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            publisherTextBox.Text = string.Empty;
            regionTextBox.Text = string.Empty;
            releaseYearTextBox.Text = string.Empty;
            genreTextBox.Text = string.Empty;
        }
    }
}
