using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using CIS560_FinalProject.ExtensionMethods;

namespace CIS560_FinalProject
{
    /// <summary>
    /// Interaction logic for PopulateUsers.xaml
    /// </summary>
    public partial class PopulateUsers : UserControl
    {

        /// <summary>
        /// Change this string to the db needed 
        /// </summary>
        string connect = "Data Source=mssql.cs.ksu.edu;Initial Catalog=USERNAME;User ID=USERNAME;Password=PASSWORD";
        public PopulateUsers()
        {
            InitializeComponent();

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query
                SqlDataAdapter sqlData = new SqlDataAdapter("Select * From CustomerAccount", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                dv.ItemsSource = dt.DefaultView;
            }
        }

        private void dv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ParentControl = this.FindAncestor<ParentControl>();
            var x = dv.SelectedItem;
            var screen = new ActionSelection();
            var userId = (x as DataRowView)["CustomerId"];
            screen.DataContext = userId;
            ParentControl?.ScreenSwap(screen);
        }

        private void AccountName_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query
                SqlDataAdapter sqlData = new SqlDataAdapter("Select * From CustomerAccount as cc WHERE cc.Name LIKE '%" + (sender as TextBox).Text + "%'", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                dv.ItemsSource = dt.DefaultView;
            }
        }
    }
}
