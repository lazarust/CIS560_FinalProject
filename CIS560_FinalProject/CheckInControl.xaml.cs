using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System;
using CIS560_FinalProject.ExtensionMethods;

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

        public CheckInControl(int selection)
        {
            InitializeComponent();
            string query = "Select t.TransId, i.ItemId, i.Title, t.CustomerId, [Return], c.Name as CreatorName From Transactions as t INNER JOIN Items as i on i.ItemId = t.ItemId INNER JOIN Creator as c on c.CreatorWorkId = i.CreatorWorkId WHERE i.InStock = 0 and [Return] = 0 and t.TransId in (SELECT max(TransId) FROM Transactions Group By ItemId) and t.CustomerId = " + selection;
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter(query, sqlConnection);
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
                var selectedItems = CheckInGrid.SelectedItems;
                foreach (DataRowView dataRowView in selectedItems)
                {
                    string query = "INSERT INTO Transactions([Return], InStock, CustomerId, Date, ItemId, Columns) VALUES (1, 1, " + dataRowView["CustomerId"] + ", Convert(datetime," + DateTime.Now.ToString("yyyy-dd-MM") + "), " + dataRowView["ItemId"] + ", 5)";
                    SqlCommand update = new SqlCommand(query, sqlConnection);
                    update.ExecuteNonQuery();

                    query = "UPDATE Items Set InStock = 1 Where ItemId = " + dataRowView["ItemId"];
                    update = new SqlCommand(query, sqlConnection);
                    update.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }

            var screen = new PopulateUsers();
            var parentControl = this.FindAncestor<ParentControl>();
            parentControl?.ScreenSwap(screen);
        }

        private void SearchTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                string query = "Select t.TransId, i.ItemId, i.Title, t.CustomerId, [Return], c.Name as CreatorName From Transactions as t INNER JOIN Items as i on i.ItemId = t.ItemId INNER JOIN Creator as c on c.CreatorWorkId = i.CreatorWorkId WHERE i.InStock = 0 and [Return] = 0 and t.TransId in (SELECT max(TransId) FROM Transactions Group By ItemId) and t.CustomerId = " + (int)DataContext + " and i.Title LIKE '%" + (sender as TextBox).Text + "%'";
                SqlDataAdapter sqlData = new SqlDataAdapter(query , sqlConnection);
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
                string query = "Select t.TransId, i.ItemId, i.Title, t.CustomerId, [Return], c.Name as CreatorName From Transactions as t INNER JOIN Items as i on i.ItemId = t.ItemId INNER JOIN Creator as c on c.CreatorWorkId = i.CreatorWorkId WHERE i.InStock = 0 and [Return] = 0 and t.TransId in (SELECT max(TransId) FROM Transactions Group By ItemId) and t.CustomerId = " + (int)DataContext + "and c.Name LIKE '%" + (sender as TextBox).Text + "%'";
                SqlDataAdapter sqlData = new SqlDataAdapter(query, sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                CheckInGrid.ItemsSource = dt.DefaultView;
            }
        }
    }
}
