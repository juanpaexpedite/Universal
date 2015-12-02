using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;

namespace Universal.Controls
{
    public class TemplatedFlyout : DependencyObject
    {
        #region Constructor
        public TemplatedFlyout()
        {

        }
        #endregion

        #region Fields
        private Popup popup;
        private FrameworkElement senderElement;
        private FrameworkElement frameworkContent;
        #endregion

        #region Initialization
        public void Initialize(UIElement sender)
        {
            senderElement = sender as FrameworkElement;
            senderElement.DataContextChanged += (s, e) => frameworkContent.DataContext = senderElement.DataContext;

            popup = new Popup
            {
                IsLightDismissEnabled = true,
                ChildTransitions = new TransitionCollection()
            };

            popup.ChildTransitions.Add(new PaneThemeTransition { Edge = EdgeTransitionLocation.Bottom });

            frameworkContent = Template.LoadContent() as FrameworkElement;
            frameworkContent.DataContext = senderElement.DataContext;

            popup.Child = frameworkContent;

            FocusKeeper();

            sender.Tapped += (s, e) => Show();
        }
        #endregion

        #region Template
        public DataTemplate Template
        {
            get { return (DataTemplate)GetValue(TemplateProperty); }
            set { SetValue(TemplateProperty, value); }
        }

        public static readonly DependencyProperty TemplateProperty =
            DependencyProperty.Register(nameof(Template), typeof(DataTemplate), typeof(TemplatedFlyout), new PropertyMetadata(null));
        #endregion

        #region Container
        public FrameworkElement Container
        {
            get { return (FrameworkElement)GetValue(ContainerProperty); }
            set
            {
                SetValue(ContainerProperty, value);
            }
        }

        public static readonly DependencyProperty ContainerProperty =
            DependencyProperty.Register(nameof(Container), typeof(FrameworkElement), typeof(TemplatedFlyout), new PropertyMetadata(null));
        #endregion

        #region Keyboard
        bool keyboardOpen = false;
        InputPane keyboard;
        private void FocusKeeper()
        {
            keyboard = InputPane.GetForCurrentView();

            popup.Closed += (s, e) =>
            {
                if (keyboardOpen)
                    popup.IsOpen = true;
            };

            if (keyboard != null)
            {
                keyboard.Showing += (s, e) => keyboardOpen = true;
                keyboard.Hiding += (s, e) => keyboardOpen = false;
            }
        }
        #endregion

        #region Show
        public void Show()
        {
            popup.RequestedTheme = ((Window.Current.Content as Frame).Content as Page).RequestedTheme;

            var h = frameworkContent.Height + Container?.ActualHeight;

            popup.SetValue(Canvas.TopProperty, Window.Current.Bounds.Height - h);

            popup.IsOpen = true;

            if (frameworkContent is Panel)
            {
                var panel = frameworkContent as Panel;
                if (panel.Children.Any())
                {
                    if (panel.Children.First() is Control)
                    {
                        (panel.Children.First() as Control).Focus(FocusState.Keyboard);
                    }
                }
            }
        }
        #endregion
    }
}
