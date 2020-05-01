using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System;
using CIS560_FinalProject.ExtensionMethods;

namespace CIS560_FinalProject
{
    /// <summary>
    /// Interaction logic for CheckOutControl.xaml
    /// </summary>
    public partial class CheckOutControl : UserControl
    {

        /// <summary>
        /// Change this string to the db needed 
        /// </summary>
        string connect = "Data Source=mssql.cs.ksu.edu;Initial Catalog=USERNAME;User ID=USERNAME;Password=PASSWORD";
        public CheckOutControl(int userId)
        {
            InitializeComponent();
            string query = "Select i.ItemId,i.Title, i.PublishDate, c.Name, i.HeldAccount From Items as i INNER JOIN Creator as c on c.CreatorWorkId = i.CreatorWorkId WHERE i.InStock = 1 and(i.HeldAccount is NULL or i.HeldAccount = " + userId + ")";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter(query, sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                CheckOutGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void CheckOutItems_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Add the query here to update and insert the rows
                var selectedItems = CheckOutGrid.SelectedItems;
                foreach(DataRowView data in selectedItems)
                {
                    string query = "";
                    
                    string query2 = "";
                    if (data["HeldAccount"].ToString() != null && data["HeldAccount"].ToString() != "")
                    {
                        string finder = "SELECT TOP 1[DATE] FROM Transactions Where ItemId =" + data["ItemId"] + "Order By TransId Desc";
                        SqlCommand cmd = new SqlCommand(finder, sqlConnection);
                        var originaldateTime = (System.DateTimeOffset)cmd.ExecuteScalar();
                        var now = DateTime.Now;
                        var newTime = now.Subtract(originaldateTime.DateTime);


                        query = "UPDATE Items Set InStock = 0, HeldAccount = NULL WHERE ItemId = " + data["ItemId"];
                        query2 = "INSERT INTO Transactions([Return], CustomerId, Date, CheckedOut, ItemId, WasHold, DateDif) VALUES (0, " + (int)DataContext + ", Convert(datetime,'" + now + "'), Convert(datetime,'" + now + "')," + data["ItemId"] + ", 1, " + newTime.Days +")";
                    } else
                    {
                        query = "UPDATE Items Set InStock = 0 WHERE ItemId = " + data["ItemId"];
                        query2 = "INSERT INTO Transactions([Return], CustomerId, Date, CheckedOut, ItemId) VALUES (0, " + (int)DataContext + ", Convert(datetime,'" + DateTime.Now + "'), Convert(datetime,'" + DateTime.Now + "')," + data["ItemId"] + ")";
                    }
                    SqlCommand update = new SqlCommand(query, sqlConnection);
                    update.ExecuteNonQuery();
                    update = new SqlCommand(query2, sqlConnection);
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
                ///Change this query
                SqlDataAdapter sqlData = new SqlDataAdapter("Select i.ItemId, i.Title, i.PublishDate, c.Name From Items as i INNER JOIN Creator as c on c.CreatorWorkId = i.CreatorWorkId  WHERE i.InStock = 1 and i.Title LIKE '%" + (sender as TextBox).Text + "%' and(i.HeldAccount is NULL or i.HeldAccount = " + (int)DataContext + ")", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                CheckOutGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void SearchCreator_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query
                SqlDataAdapter sqlData = new SqlDataAdapter("Select i.ItemId, i.Title, i.PublishDate, c.Name From Items as i INNER JOIN Creator as c on c.CreatorWorkId = i.CreatorWorkId  WHERE i.InStock = 1 and c.Name LIKE '%" + (sender as TextBox).Text + "%' and(i.HeldAccount is NULL or i.HeldAccount = " + (int)DataContext + ")", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                CheckOutGrid.ItemsSource = dt.DefaultView;
            }
        }
    }
}
