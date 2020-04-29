using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace CIS560_FinalProject
{
    /// <summary>
    /// Interaction logic for ReportQuery4.xaml
    /// </summary>
    public partial class ReportQuery4 : UserControl
    {
        /// <summary>
        /// Change this string to the db needed 
        /// </summary>
        string connect = "Data Source=mssql.cs.ksu.edu;Initial Catalog=USERNAME;User ID=USERNAME;Password=PASSWORD";

        public ReportQuery4()
        {
            InitializeComponent();

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query to get the total checkouts from every month
                SqlDataAdapter sqlData = new SqlDataAdapter("SELECT FORMAT(DATE, 'MMMM') as Month, COUNT(*) as TransactionCount FROM Transactions Group By MONTH(Date), FORMAT(DATE, 'MMMM') Order By Month(DATE)", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                Trans.ItemsSource = dt.DefaultView;

            }
        }
    }
}
