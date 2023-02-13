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
        private void addToTable(string query)
        {
            OleDbCommand cmd = new OleDbCommand(query, cn);
            cn.Open();

            cmd.ExecuteNonQuery();

            cn.Close();
        }
        private void btn_see_assets_Click(object sender, RoutedEventArgs e)
        {
            string query = "select * from Assets";
           
            string data = getTable(query);

            txt_display.Text = data;
        }

        private void btn_see_employees_Click(object sender, RoutedEventArgs e)
        {
            string query = "select * from Employees";

            string data = getTable(query);

            txt_display.Text = data;
        }

        private void btn_add_assets_Click(object sender, RoutedEventArgs e)
        {

            string[] words = txt_add_to_table.Text.Split(' ');

            // some basic checking so I don't explode my database
            if (words.Length < 3 )
            {
                // invalid input
                return;
            }
            // quickly confirming the first two fields can be converted into integers
            if ( !(Int32.TryParse(words[0], out int whatever) && Int32.TryParse(words[1], out int whatever2))){
                // invalid input
                return;
            }
            string query = "INSERT INTO Assets VALUES ('" + words[0] + "\', \'" + words[1] + "\', \'" + words[2] + "\');";

            //txt_display.Text = query;
            addToTable(query);

        }
    }
}
