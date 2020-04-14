using System.Windows;
using System.Windows.Media;

namespace CIS560_FinalProject.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static T FindAncestor<T>(this DependencyObject element) where T: DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element);

            if (parent == null) return null;

            if (parent is T) return parent as T;

            return parent.FindAncestor<T>();
        }
    }
}
