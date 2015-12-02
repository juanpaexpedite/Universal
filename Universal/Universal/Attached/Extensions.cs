using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universal.Controls;
using Windows.UI.Xaml;

namespace Universal.Attached
{
    public class Extensions : DependencyObject
    {
        #region Flyout Property
        public static void SetFlyout(UIElement element, TemplatedFlyout value)
        {
            element.SetValue(FlyoutProperty, value);
        }
        public static TemplatedFlyout GetFlyout(UIElement element)
        {
            return (TemplatedFlyout)element.GetValue(FlyoutProperty);
        }

        public static readonly DependencyProperty FlyoutProperty =
            DependencyProperty.RegisterAttached(nameof(FlyoutProperty),
                typeof(TemplatedFlyout), typeof(Extensions), new PropertyMetadata(null, TemplatedFlyoutChanged));

        private static void TemplatedFlyoutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiSender = d as UIElement;
            var flyout = e.NewValue as TemplatedFlyout;

            flyout.Initialize(uiSender);
        }
        #endregion

    }
}
