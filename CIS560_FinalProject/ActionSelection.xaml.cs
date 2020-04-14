using System.Windows;
using System.Windows.Controls;
using CIS560_FinalProject.ExtensionMethods;

namespace CIS560_FinalProject
{
    /// <summary>
    /// Interaction logic for ActionSelection.xaml
    /// </summary>
    public partial class ActionSelection : UserControl
    {
        public ActionSelection()
        {
            InitializeComponent();
        }

        private void CheckIn_Click(object sender, RoutedEventArgs e)
        {
            var screen = new CheckInControl();
            var parentControl = this.FindAncestor<ParentControl>();
            screen.DataContext = DataContext;
            parentControl?.ScreenSwap(screen);
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            var screen = new CheckOutControl();
            var parentControl = this.FindAncestor<ParentControl>();
            screen.DataContext = DataContext;
            parentControl?.ScreenSwap(screen);
        }
    }
}
