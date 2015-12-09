using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universal.Compositions;
using Windows.UI.Xaml;

namespace Universal.Attached
{
    public class Composition
    {
        #region Flyout Property
        public static void SetEffect(UIElement element, IComposition value)
        {
            element.SetValue(EffectProperty, value);
        }
        public static IComposition GetEffect(UIElement element)
        {
            return (IComposition)element.GetValue(EffectProperty);
        }

        public static readonly DependencyProperty EffectProperty =
            DependencyProperty.RegisterAttached(nameof(EffectProperty),
                typeof(IComposition), typeof(Extensions), new PropertyMetadata(null, EffectChanged));

        private static void EffectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiSender = d as UIElement;
            var effect = e.NewValue as IComposition;

            effect.Initialize(uiSender);
        }
        #endregion
    }
}
