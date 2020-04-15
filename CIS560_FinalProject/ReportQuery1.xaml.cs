using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace CIS560_FinalProject
{
    /// <summary>
    /// Interaction logic for ReportQuery1.xaml
    /// </summary>
    public partial class ReportQuery1 : UserControl
    {
        /// <summary>
        /// Change this string to the db needed 
        /// </summary>
        string connect = "Data Source=mssql.cs.ksu.edu;Initial Catalog=USERNAME;User ID=USERNAME;Password=PASSWORD";

        public ReportQuery1()
        {
            InitializeComponent();

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query
                SqlDataAdapter sqlData = new SqlDataAdapter("Select * From Clubs.Club", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                dv.ItemsSource = dt.DefaultView;
            }
        }

        private void AccountName_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query
                SqlDataAdapter sqlData = new SqlDataAdapter("Select * From Clubs.Club as cc WHERE cc.Name LIKE '%" + (sender as TextBox).Text + "%'", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                dv.ItemsSource = dt.DefaultView;
            }
        }

        private void dv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var userId = (sender as DataGrid).SelectedIndex;
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query
                SqlDataAdapter sqlData = new SqlDataAdapter("Select * From Clubs.Club as cc WHERE cc.ClubId LIKE '%" + userId + "%'", sqlConnection);

                //Fill these text values with amount returned from query
                AmountHeld.Text = "";
                AmountCheckedout.Text = "";
                AmountYearCheckedOut.Text = "";
            }

        }
    }
}
