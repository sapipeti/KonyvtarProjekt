using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebApi_Common.DataProviders;
using WebApi_Common.Models;

namespace WebApi_Client_Konyvtaros
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        List<Konyv> konyvek = new List<Konyv>();
        List<KonyvKonyvtaros> konyvek_tabla = new List<KonyvKonyvtaros>();
        string OszlopNev;

        public SearchWindow()
        {
            InitializeComponent();

            String[] oszlopok = { "Id", "Cím", "ISBN", "Kiadó", "Kiadás_Év", "Műfajok", "Szerző", "Visszahozas", "KolcsonzottDB", "NeptunKod", "Darabszam" };
            OszlopComboBox.ItemsSource = oszlopok;

            UpdateData();
            Tablazat.ItemsSource = konyvek_tabla;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Tablazat.ItemsSource);
            view.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(KeresesTextBox.Text))
            {
                return true;
            }
            else
            {
                switch (OszlopNev)
                {
                    case "Id":
                        return ((item as KonyvKonyvtaros).Id.ToString().IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Cím":
                        return ((item as KonyvKonyvtaros).Cím.IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "ISBN":
                        return ((item as KonyvKonyvtaros).ISBN.ToString().IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Kiadó":
                        return ((item as KonyvKonyvtaros).Kiadó.IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Kiadás_Év":
                        return ((item as KonyvKonyvtaros).Kiadás_Év.ToString().IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Műfajok":
                        return ((item as KonyvKonyvtaros).Műfajok.IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Szerző":
                        return ((item as KonyvKonyvtaros).Szerző.IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Visszahozas":
                        return ((item as KonyvKonyvtaros).VisszaHozas.ToString().IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "KolcsonzottDB":
                        return ((item as KonyvKonyvtaros).KolcsonzottDB.ToString().IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "NeptunKod":
                        return ((item as KonyvKonyvtaros).NeptunKod.ToString().IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Darabszam":
                        return ((item as KonyvKonyvtaros).Darabszám.ToString().IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    default: return true;
                }


            }

        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Tablazat.ItemsSource).Refresh();
        }

        public void UpdateData()
        {
            konyvek_tabla.Clear();
            konyvek = KonyvDataProvider.GetKonyvek().ToList();
            string mufaj = "", szerzo = "", darab = "", neptunkod = "", datum = "";
            foreach (var item in konyvek)
            {
                foreach (var item2 in item.Műfajok)
                {
                    if (!mufaj.Equals(""))
                    {
                        mufaj += "," + item2;
                    }
                    else
                    {
                        mufaj += item2;
                    }
                }
                foreach (var item2 in item.Szerző)
                {
                    if (!szerzo.Equals(""))
                    {
                        szerzo += "," + item2;
                    }
                    else
                    {
                        szerzo += item2;
                    }
                }
                if (item.NeptunKod != null)
                {
                    for (int i = 0; i < item.NeptunKod.Count; i++)
                    {
                        if (item.KolcsonzottDB != null)
                        {
                            if (!darab.Equals(""))
                            {
                                darab += "," + item.KolcsonzottDB[i];
                            }
                            else
                            {
                                darab += item.KolcsonzottDB[i];
                            }
                        }
                        if (item.NeptunKod != null)
                        {
                            if (!neptunkod.Equals(""))
                            {
                                neptunkod += "," + item.NeptunKod[i];
                            }
                            else
                            {
                                neptunkod += item.NeptunKod[i];
                            }
                        }
                        if (item.VisszaHozas != null)
                        {
                            if (!datum.Equals(""))
                            {
                                datum += "," + item.VisszaHozas[i];
                            }
                            else
                            {
                                datum += item.VisszaHozas[i];
                            }
                        }
                    }
                }
                konyvek_tabla.Add(new KonyvKonyvtaros(item.Id, item.Cím, item.ISBN, item.Kiadó, item.Kiadás_Év, mufaj, szerzo, item.Darabszám, neptunkod, datum, darab));
                mufaj = "";
                szerzo = "";
                darab = "";
                neptunkod = "";
                datum = "";
            }
        }

        private void OszlopComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
            OszlopNev = OszlopComboBox.SelectedItem.ToString();
        }

        private void kiadButton_Click(object sender, RoutedEventArgs e)
        {
            KonyvKiadWindow kkw = new KonyvKiadWindow(((KonyvKonyvtaros)Tablazat.SelectedItem).Id);
            kkw.Show();
            this.Close();
        }
    }
}
