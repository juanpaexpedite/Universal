using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Linq;
using System.Collections.ObjectModel;
using Windows.UI.Text;
using System.Reflection;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UniversalExamples.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TypographyPage : Page
    {
        public TypographyPage()
        {
            this.InitializeComponent();
            
            InitializeCapitals();

            InitializeWeights();
        }

        public List<ValueClass> Capitals { get; } = new List<ValueClass>();

        private void InitializeCapitals()
        {
            Type capitalsType = typeof(FontCapitals);
            foreach (var capital in Enum.GetValues(capitalsType))
            {
                var name = Enum.GetName(capitalsType, capital);
                Capitals.Add(new ValueClass() { Name = name, Value = capital });
            }

            CapitalsListView.ItemsSource = Capitals;
        }

        public List<ValueClass> Weights { get; } = new List<ValueClass>();

        private void InitializeWeights()
        {
            Type weightsType = typeof(FontWeights);

            foreach(var weightProperty in weightsType.GetProperties())
            {
                var value = (FontWeight)weightProperty.GetValue(null);

                Weights.Add(new ValueClass()
                {
                    Name = $"{weightProperty.Name}: {value.Weight}",
                    Value = value
                });
            }

            WeightListView.ItemsSource = Weights;
        }

        public class ValueClass
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }
    }
}
