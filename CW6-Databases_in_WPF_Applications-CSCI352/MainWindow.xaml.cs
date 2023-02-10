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
            cn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source =| DataDirectory |\\CW6 - Databases_in_WPF_Applications.accdb");
        }

        private void btn_see_assets_Click(object sender, RoutedEventArgs e)
        {
            string query = "select * from Assets";
           
            OleDbCommand cmd = new OleDbCommand(query, cn);
            cn.Open();

            OleDbDataReader read = cmd.ExecuteReader();
            string data = "";
            while(read.Read())
            {
                data += read[0].ToString() + "\n";
            }
            txt_display.Text = data;
        }
    }
}
