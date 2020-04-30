using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace CIS560_FinalProject
{
    /// <summary>
    /// Interaction logic for ReportQuery3.xaml
    /// </summary>
    public partial class ReportQuery3 : UserControl
    {
        /// <summary>
        /// Change this string to the db needed 
        /// </summary>
        string connect = "Data Source=mssql.cs.ksu.edu;Initial Catalog=USERNAME;User ID=USERNAME;Password=PASSWORD";

        public ReportQuery3()
        {
            InitializeComponent();

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query
                SqlDataAdapter sqlData = new SqlDataAdapter("Select i.ItemId, i.Title, MAX(t.DateDif) as MaxWaitDays From Items as i Inner Join Transactions as t on i.ItemId = t.ItemId Group by i.ItemId, i.Title Having MAX(t.DateDif) is not null Order By MAX(t.DateDif) DESC", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                dv.ItemsSource = dt.DefaultView;
            }
        }
    }
}
