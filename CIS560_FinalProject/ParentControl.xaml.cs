using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace CIS560_FinalProject
{
    /// <summary>
    /// Interaction logic for ParentControl.xaml
    /// </summary>
    public partial class ParentControl : UserControl
    {


        public ParentControl()
        {
            InitializeComponent();
        }

        private void PopulateUsers_Click(object sender, RoutedEventArgs e)
        {
            var screen = new PopulateUsers();
            ScreenSwap(screen);
        }

        public void ScreenSwap(FrameworkElement element)
        {
            Container.Child = element;   
        }


    }
}
