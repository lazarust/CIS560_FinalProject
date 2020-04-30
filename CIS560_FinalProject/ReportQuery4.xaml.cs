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
                SqlCommand cmd = new SqlCommand("SELECT FORMAT(DATE, 'MMMM') as Month, COUNT(*) as TransactionCount FROM Transactions Group By MONTH(Date), FORMAT(DATE, 'MMMM') Order By Month(DATE)", sqlConnection);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                foreach (DataRow x in dt.Rows)
                {
                    var count = x["TransactionCount"].ToString();
                    switch (x["Month"].ToString())
                    {
                        case "January":
                            JanuaryTotals.Text = count;
                            break;

                        case "Febraury":
                            FebruaryTotals.Text = count;
                            break;

                        case "March":
                            MarchTotals.Text = count;
                            break;

                        case "April":
                            AprilTotals.Text = count;
                            break;

                        case "May":
                            MayTotals.Text = count;
                            break;

                        case "June":
                            JuneTotals.Text = count;
                            break;

                        case "July":
                            JulyTotals.Text = count;
                            break;

                        case "August":
                            AugustTotals.Text = count;
                            break;

                        case "September":
                            SeptemberTotals.Text = count;
                            break;

                        case "October":
                            OctoberTotals.Text = count;
                            break;

                        case "November":
                            NovemberTotals.Text = count;
                            break;

                        case "December":
                        default:
                            DecemberTotals.Text = count;
                            break;

                    }
                }

            }
        }
    }
}
