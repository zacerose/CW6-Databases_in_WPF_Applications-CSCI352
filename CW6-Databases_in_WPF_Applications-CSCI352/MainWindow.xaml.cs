// Name: Zachary Rose
// Date: 2/13/2023
// Class: CSCI 352
// Allows access to an Access database (can view tables and make insertions) through a basic WPF application.
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CW6_Databases_in_WPF_Applications_CSCI352
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OleDbConnection cn;
        public MainWindow()
        {
            InitializeComponent();
            cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\CW6-Databases_in_WPF_Applications.accdb");
        }

        // helper method, builds a string from the output of the SELECT query
        private string getTable(string query)
        {
            OleDbCommand cmd = new OleDbCommand(query, cn);
            cn.Open();

            OleDbDataReader read = cmd.ExecuteReader();
            string data = "";
            while (read.Read())
            {
                for (int i = 0; i < read.VisibleFieldCount; i++)
                {
                    data += read[i].ToString() + '\t';
                }
                data += "\n";
            }
            cn.Close();
            return data;
        }
        // helper method, attempts to perform INSERT query on the table
        private void addToTable(string query)
        {
            OleDbCommand cmd = new OleDbCommand(query, cn);
            cn.Open();

            try
            {
                cmd.ExecuteNonQuery();
                txt_display.Text = "Added successfully";
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                txt_display.Text = ex.Message;
            }
            cn.Close();
        }
        private void btn_see_assets_Click(object sender, RoutedEventArgs e)
        {
            string query = "select * from Assets";
           
            string data = "Assets:\nEmpID\tAssetID\tDescription\n" + getTable(query);

            txt_display.Text = data;
        }

        private void btn_see_employees_Click(object sender, RoutedEventArgs e)
        {
            string query = "select * from Employees";

            string data = "Employees:\nEmpID\tFName\tLName\n" + getTable(query);

            txt_display.Text = data;
        }

        private void btn_add_assets_Click(object sender, RoutedEventArgs e)
        {

            string[] words = txt_add_to_table.Text.Split(' ');

            // some basic checking so I don't explode my database
            if (words.Length < 3)
            {
                // invalid input
                return;
            }
            // quickly confirming the first two fields can be converted into integers
            if ( !(Int32.TryParse(words[0], out int whatever) && Int32.TryParse(words[1], out int whatever2))){
                // invalid input
                txt_display.Text = "Please input a valid EmployeeID, AssetID, and description";
                return;
            }
            
            // Collates anything past the ID fields into a single description string to allow for spaces (adds spaces back in from split)
            string description = "";
            for (int i = 2; i < words.Length; i++)
            {
                description += words[i] + ' ';
            }
            // prevents SQL injection by replacing quotes with spaces.
            string query = "INSERT INTO Assets VALUES ('" + words[0] + "\', \'" + words[1] + "\', \'" + description.Replace('\'',' ').Replace('\"', ' ') + "\');";

            addToTable(query);

        }

        private void btn_add_employees_Click(object sender, RoutedEventArgs e)
        {
            string[] words = txt_add_to_table.Text.Split(' ');

            // some basic checking so I don't explode my database
            if (words.Length < 3)
            {
                // invalid input              
                return;
            }

            // confirms the first word is a number
            if (!Int32.TryParse(words[0], out int whatever))
            {
                // invalid input
                txt_display.Text = "Please input a valid unique EmployeeID, FirstName, and LastName";
                return;
            }

            // prevents SQL injection by replacing quotes with spaces.
            string query = "INSERT INTO Employees VALUES ('" + words[0] + "\', \'" + 
                words[1].Replace('\'', ' ').Replace('\"', ' ') + "\', \'" + words[2].Replace('\'', ' ').Replace('\"', ' ') + "\');";

            addToTable(query);
        }
    }
}
