using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using CIS560_FinalProject.ExtensionMethods;

namespace CIS560_FinalProject
{
    /// <summary>
    /// Interaction logic for HoldControl.xaml
    /// </summary>
    public partial class HoldControl : UserControl
    {
        string connect = "Data Source=mssql.cs.ksu.edu;Initial Catalog=USERNAME;User ID=USERNAME;Password=PASSWORD";

        public HoldControl(int userId)
        {
            InitializeComponent();

            string query = "Select i.ItemId,i.Title, i.PublishDate, c.Name, i.HeldAccount From Items as i INNER JOIN Creator as c on c.CreatorWorkId = i.CreatorWorkId WHERE i.InStock = 0 and(i.HeldAccount is NULL or i.HeldAccount = " + userId + ")";
            string query2 = "Select * FROM Items where HeldAccount = " + userId;
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlData2 = new SqlDataAdapter(query2, sqlConnection);
                DataSet ds = new DataSet();
                int results = sqlData2.Fill(ds);
                if (results != 0)
                {
                    MessageBox.Show("Account already has item on hold. Selecting another item will replace that hold.");
                }
                SqlDataAdapter sqlData = new SqlDataAdapter(query, sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                HoldGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void SearchTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("Select i.ItemId, i.Title, i.PublishDate, c.Name From Items as i INNER JOIN Creator as c on c.CreatorWorkId = i.CreatorWorkId  WHERE i.InStock = 0 and i.Title LIKE '%" + (sender as TextBox).Text + "%' and(i.HeldAccount is NULL)", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                HoldGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void SearchCreator_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("Select i.ItemId, i.Title, i.PublishDate, c.Name From Items as i INNER JOIN Creator as c on c.CreatorWorkId = i.CreatorWorkId  WHERE i.InStock = 0 and c.Name LIKE '%" + (sender as TextBox).Text + "%' and(i.HeldAccount is NULL)", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                HoldGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void HoldItems_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                var selectedItems = HoldGrid.SelectedItems;
                foreach (DataRowView data in selectedItems)
                {
                    string query = "UPDATE Items Set HeldAccount = " + (int)DataContext + "Where ItemId = " + data["ItemId"];

                    
                    SqlCommand update = new SqlCommand(query, sqlConnection);
                    update.ExecuteNonQuery();
                }

                sqlConnection.Close();

            }
            var screen = new PopulateUsers();
            var parentControl = this.FindAncestor<ParentControl>();
            parentControl?.ScreenSwap(screen);
        }
    }
}
