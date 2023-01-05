using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace wpfLoginScreen
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-O7JQGLD\THANHHIEP; Initial Catalog=LoginDB; Integrated Security=true;");
            try
            {
                if(sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT COUNT(1) FROM tblUser WHERE Username=@Username AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if(count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }else
                {
                    MessageBox.Show("Username or password is incorrect.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }finally
            {
                sqlCon.Close();
            }
        }
    }
}
