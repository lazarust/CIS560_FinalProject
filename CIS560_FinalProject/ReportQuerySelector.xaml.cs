using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CIS560_FinalProject.ExtensionMethods;

namespace CIS560_FinalProject
{
    /// <summary>
    /// Interaction logic for ReportQuerySelector.xaml
    /// </summary>
    public partial class ReportQuerySelector : UserControl
    {
        public ReportQuerySelector()
        {
            InitializeComponent();
        }

        private void Query1_Click(object sender, RoutedEventArgs e)
        {
            var screen = new ReportQuery1();
            var ParentControl = this.FindAncestor<ParentControl>();
            ParentControl?.ScreenSwap(screen);
        }

        private void Query2_Click(object sender, RoutedEventArgs e)
        {
            var screen = new ReportQuery2();
            var ParentControl = this.FindAncestor<ParentControl>();
            ParentControl?.ScreenSwap(screen);
        }

        private void Query4_Click(object sender, RoutedEventArgs e)
        {
            var screen = new ReportQuery4();
            var ParentControl = this.FindAncestor<ParentControl>();
            ParentControl?.ScreenSwap(screen);
        }

        private void Query3_Click(object sender, RoutedEventArgs e)
        {
            var screen = new ReportQuery3();
            var ParentControl = this.FindAncestor<ParentControl>();
            ParentControl?.ScreenSwap(screen);
        }
    }
}
