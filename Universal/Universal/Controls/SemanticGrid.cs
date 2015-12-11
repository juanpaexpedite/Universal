using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universal.Managers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Universal.Controls
{
    public class SemanticGrid : Grid, ISemanticZoomInformation
    {
        #region ItemsSource
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(Grid), new PropertyMetadata(null));
        #endregion

        #region Current Item
        public object CurrentItem { get; set; }
        #endregion

        public SemanticGrid()
        {
            this.Loaded += SemanticView_Loaded;
            this.Unloaded += SemanticGrid_Unloaded;
        }

        private void SemanticGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Loaded -= SemanticView_Loaded;
                foreach (var childItem in this.Children.OfType<FrameworkElement>().Where(fe => fe.Tag != null))
                {
                    childItem.Tapped -= ChildItem_Tapped;
                }
            }
            catch
            {

            }
        }

        private void SemanticView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var childItem in this.Children.OfType<FrameworkElement>().Where(fe => fe.Tag != null))
            {
                childItem.Tapped += ChildItem_Tapped;
            }
        }

        private void ChildItem_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var childItem = sender as FrameworkElement;

            //Our 'key'
            var key = (childItem.Tag as String);

            //Convert ItemsSource to List<object>
            var list = CollectionManager.ToObjectList(ItemsSource);

            if (list != null)
            {
                //Find the current real group item
                this.CurrentItem = list.FirstOrDefault(item => item.ToString() == key);
            }

            //Change to ZoomedIn
            SemanticZoomOwner.IsZoomedInViewActive = true;
        }

        public bool IsActiveView { get; set; }
        public bool IsZoomedInView { get; set; }
        public SemanticZoom SemanticZoomOwner { get; set; }
        public void StartViewChangeFrom(SemanticZoomLocation source, SemanticZoomLocation destination)
        {
            destination.Item = CurrentItem;
        }
        public void CompleteViewChange() { }

        public void CompleteViewChangeFrom(SemanticZoomLocation source, SemanticZoomLocation destination) { }

        public void CompleteViewChangeTo(SemanticZoomLocation source, SemanticZoomLocation destination) { }

        public void InitializeViewChange() { }

        public void MakeVisible(SemanticZoomLocation item) { }

        public void StartViewChangeTo(SemanticZoomLocation source, SemanticZoomLocation destination) { }
    }
}
