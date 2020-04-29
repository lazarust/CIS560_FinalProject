using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace CIS560_FinalProject
{
    /// <summary>
    /// Interaction logic for ReportQuery2.xaml
    /// </summary>
    public partial class ReportQuery2 : UserControl
    {
        /// <summary>
        /// Change this string to the db needed 
        /// </summary>
        string connect = "Data Source=mssql.cs.ksu.edu;Initial Catalog=USERNAME;User ID=USERNAME;Password=PASSWORD";

        public ReportQuery2()
        {
            InitializeComponent();

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query to return all the items checked out for a given month ordered by amount of times
                SqlDataAdapter sqlData = new SqlDataAdapter("Select Count(*) as Count, t.ItemId, i.Title, i.PublishDate From Transactions as t INNER JOIN Items as i on t.ItemId = i.ItemId  Group By t.ItemId, i.Title, i.PublishDate", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                TopItems.ItemsSource = dt.DefaultView;

            }
        }
    }
}
