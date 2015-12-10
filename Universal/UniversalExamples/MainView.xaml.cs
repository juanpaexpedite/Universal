using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniversalExamples.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UniversalExamples
{
    /// <summary>
    /// I avoid using MVVM for this project to keep the library with its purpose
    /// </summary>
    public sealed partial class MainView : Page
    {
        public Type SaturationPage { get; } = typeof(SaturationEffectPage);
        public Type TypographyPage { get; } = typeof(TypographyPage);

        public string Test { get; } = "chicken";

        public MainView()
        {
            this.InitializeComponent();
            this.DataContext = this;
            SplitViewPanePanel.DataContext = this;
            this.Loaded += (s, e) =>
            {
                InitializeNavigation();
            };
        }
        

        private void InitializeNavigation()
        {
            int i = -1;
            foreach (var button in SplitViewPanePanel.Children.OfType<Button>())
            {
                if (++i == 0)
                {
                    var typeofpage = button.CommandParameter as Type;
                    SplitViewContentFrame.Navigate(typeofpage);
                }
                button.Tapped += (s, e) =>
                {
                    var typeofpage = button.CommandParameter as Type;
                    SplitViewContentFrame.Navigate(typeofpage);
                };
            }
        }
    }
}
