using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JSON;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;

namespace JSON_Data
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string url = "http://datasets.antwerpen.be/v4/gis/repetitieruimteoverzicht.json";

            WebClient wc = new WebClient();
            string jsondata = wc.DownloadString(url);

            Rootobject data = JsonConvert.DeserializeObject<Rootobject>(jsondata);
            List<JSON.Datum> RepetitieRuimtes = new List<Datum>();
            bool unique;
            foreach (var datum in data.data)
            {
                unique = true;
                for (int i = 0; i < RepetitieRuimtes.Count; i++)
                {
                    if (RepetitieRuimtes[i].id == datum.id && unique)
                        unique = false;
                }
                if (unique)
                {
                    datum.locatie = new Location(Convert.ToDouble(datum.point_lng.Replace(".", ",")), Convert.ToDouble(datum.point_lat.Replace(".", ",")));
                    RepetitieRuimtes.Add(datum);
                }
            }
            dataListBox.ItemsSource = RepetitieRuimtes;
            dataListBox.SelectedIndex = 0;
        }

        private void dataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SummaryGrid.DataContext = MapGrid.DataContext = dataListBox.SelectedItem;
            leMap.SetView(leMap.BoundingRectangle);
        }
    }
}
