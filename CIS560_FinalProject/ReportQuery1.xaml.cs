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
                SqlDataAdapter sqlData = new SqlDataAdapter("Select CustomerId, Name From CustomerAccount", sqlConnection);
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
                SqlDataAdapter sqlData = new SqlDataAdapter("Select * From CustomerAccount as cc WHERE cc.Name LIKE '%" + (sender as TextBox).Text + "%'", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                dv.ItemsSource = dt.DefaultView;
            }
        }

        private void dv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var x = dv.SelectedItem;
            var userId = (x as DataRowView)["CustomerId"];

            string query = "SELECT (SELECT COUNT(*) FROM( Select t.TransId, i.ItemId, i.Title, t.CustomerId, [Return] From Transactions as t INNER JOIN Items as i on i.ItemId = t.ItemId WHERE t.TransId in (SELECT max(TransId) FROM Transactions Group By ItemId) and t.CustomerId = "+userId+") as s) as CountRet,(SELECT COUNT(*) as CountYear FROM(Select t.TransId, t.CustomerId, [Return] From Transactions as t WHERE  t.CustomerId = "+ userId+ " and t.Date > DATEADD(year, -1, GETDATE())) as s) as CountYear, (SELECT Top 1 i.Title FROM Transactions as t INNER JOIN Items as i on i.ItemId = t.ItemId Where t.CustomerId = "+ userId +" Group By t.ItemId, i.Title Order By Count(*) DESC) as TopTitle";

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query
                SqlDataAdapter sqlData = new SqlDataAdapter(query, sqlConnection);

                DataTable dt = new DataTable();
                sqlData.Fill(dt);
                var y = dt.Rows;
                var z = y[0];
                AmountHeld.Text = z[2].ToString();
                AmountCheckedout.Text = z[0].ToString();
                AmountYearCheckedOut.Text = z[1].ToString();
            }

        }
    }
}
