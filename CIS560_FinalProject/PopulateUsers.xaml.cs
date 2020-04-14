using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using CIS560_FinalProject.ExtensionMethods;

namespace CIS560_FinalProject
{
    /// <summary>
    /// Interaction logic for PopulateUsers.xaml
    /// </summary>
    public partial class PopulateUsers : UserControl
    {

        /// <summary>
        /// Change this string to the db needed 
        /// </summary>
        string connect = "Data Source=mssql.cs.ksu.edu;Initial Catalog=USERNAME;User ID=USERNAME;Password=PASSWORD";
        public PopulateUsers()
        {
            InitializeComponent();

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();
                ///Change this query
                SqlDataAdapter sqlData = new SqlDataAdapter("Select * From Clubs.Club", sqlConnection);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);

                dv.ItemsSource = dt.DefaultView;
            }
        }

        private void dv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ParentControl = this.FindAncestor<ParentControl>();
            var userId = (sender as DataGrid).SelectedIndex;
            var screen = new ActionSelection();
            screen.DataContext = userId;
            ParentControl?.ScreenSwap(screen);
        }
    }
}
