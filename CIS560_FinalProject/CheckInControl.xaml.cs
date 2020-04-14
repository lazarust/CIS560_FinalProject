using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace CIS560_FinalProject
{
    /// <summary>
    /// Interaction logic for CheckInControl.xaml
    /// </summary>
    public partial class CheckInControl : UserControl
    {
        /// <summary>
        /// Change this string to the db needed 
        /// </summary>
        string connect = "Data Source=mssql.cs.ksu.edu;Initial Catalog=USERNAME;User ID=USERNAME;Password=PASSWORD";

        public CheckInControl()
        {
            InitializeComponent();
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query the data context is the users id so looking through the transaction table and finding all the checked out items for the user
                SqlDataAdapter sqlData = new SqlDataAdapter("Select * From Clubs.Club", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                CheckInGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void CheckInItems_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Add the query here to update and insert the rows
                
            }
        }

        private void SearchTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query
                SqlDataAdapter sqlData = new SqlDataAdapter("Select * From Clubs.Club as cc WHERE cc.Name LIKE '%" + (sender as TextBox).Text + "%'", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                CheckInGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void SearchCreator_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query
                SqlDataAdapter sqlData = new SqlDataAdapter("Select * From Clubs.Club as cc WHERE cc.Name LIKE '%" + (sender as TextBox).Text + "%'", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                CheckInGrid.ItemsSource = dt.DefaultView;
            }
        }
    }
}
