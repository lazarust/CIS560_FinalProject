using System.Windows.Controls;
using System.Data.SqlClient;

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
                SqlDataAdapter sqlData = new SqlDataAdapter("Select * From Clubs.Club", sqlConnection);

                //Fill these values with what's returned from the query
                JanuaryTotals.Text = "";
                FebruaryTotals.Text = "";
                MarchTotals.Text = "";
                AprilTotals.Text = "";
                MayTotals.Text = "";
                JuneTotals.Text = "";
                JulyTotals.Text = "";
                AugustTotals.Text = "";
                SeptemberTotals.Text = "";
                OctoberTotals.Text = "";
                NevemberTotals.Text = "";
                DecemberTotals.Text = "";
            }
        }
    }
}
