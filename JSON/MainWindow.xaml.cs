using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
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
using MahApps.Metro.Controls;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;

namespace JSON_Data
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<JSON.Datum> RepetitieRuimtes = new ObservableCollection<Datum>();
        private IEnumerable<JSON.Datum> _linqRes;
        WebClient wc = new WebClient();
        string filePath = Environment.CurrentDirectory + @"\JSONdata.json";
        string url = "http://datasets.antwerpen.be/v4/gis/repetitieruimteoverzicht.json";
        private Rootobject data;
        private string jsondata;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DownloadJSONdata();
            }
            catch (Exception)
            {
                MessageBox.Show("Download failed and local content not found");
            }
            dataListBox.ItemsSource = RepetitieRuimtes;
        }

        void DownloadJSONdata()
        {
            if (File.Exists(filePath))
            {
                jsondata = File.ReadAllText(filePath);
            }
            else
            {
                jsondata = wc.DownloadString(url);
                File.WriteAllText(filePath, jsondata);
            }
            data = JsonConvert.DeserializeObject<Rootobject>(jsondata);

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
                    datum.locatie = new Location(Convert.ToDouble(datum.point_lat.Replace(".", ",")), Convert.ToDouble(datum.point_lng.Replace(".", ",")));
                    RepetitieRuimtes.Add(datum);
                }
            }
        }

        private void dataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataListBox.SelectedItem != null)
            {
                SummaryGrid.DataContext = MapGrid.DataContext = dataListBox.SelectedItem;
                Datum t = new Datum();
                t = (Datum)dataListBox.SelectedItem;
                leMap.Center = t.locatie;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text != "")
            {
                switch (FilterBox.SelectedIndex)
                {
                    case 0:
                        _linqRes = RepetitieRuimtes.Where(r => r.naam.ToLower().Contains(SearchBox.Text.ToLower()));
                        break;
                    case 1:
                        _linqRes = RepetitieRuimtes.Where(r => r.district.ToLower().Contains(SearchBox.Text.ToLower()));
                        break;
                }
                dataListBox.ItemsSource = _linqRes;
            }
            else
                dataListBox.ItemsSource = RepetitieRuimtes;
            
            if (dataListBox.Items.Count != 0)
                dataListBox.SelectedIndex = 0;
        }
    }
}
