using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        CollectionView view;

        public SearchWindow()
        {
            InitializeComponent();

            String[] oszlopok = { "Id", "Cím", "ISBN", "Kiadó", "Kiadás_Év", "Műfajok", "Szerző", "Visszahozas", "KolcsonzottDB", "NeptunKod", "Darabszam" };
            OszlopComboBox.ItemsSource = oszlopok;

            UpdateData();
            Tablazat.ItemsSource = konyvek_tabla;

            view = (CollectionView)CollectionViewSource.GetDefaultView(Tablazat.ItemsSource);
            view.Filter = UserFilter;

            ColorDataGridRows();
        }

        //Beszinezzük azokat a sorokat amelyekben szereplő könyveket nem lehet kiadni.
        public void ColorDataGridRows()
        {
            var itemsSource = view as IEnumerable;
            Tablazat.ItemContainerGenerator.StatusChanged += (z, e) =>
            {
                if (itemsSource != null)
                {
                    int sor = 0;
                    foreach (var item in itemsSource.OfType<KonyvKonyvtaros>())
                    {
                        int prop = item.Darabszám;
                        string prop2 = item.KolcsonzottDB;
                        string[] tomb = prop2.Split(',');
                        int[] myInts;
                        //A tömböt int tömbbé alakítjuk
                        if (!prop2.Equals(""))
                        {
                            myInts = Array.ConvertAll(tomb, s => int.Parse(s));
                        }
                        else
                        {
                            myInts = new int[] {0};
                        }
                        for (int i = 0; i < myInts.Length; i++)
                            {
                                prop -= myInts[i];
                            }
                            //Feltétel a színezéshez
                            if (prop < 1)
                            {
                                //Táblázat sor beszínezése
                                if (Tablazat.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                                {
                                    DataGridRow row = (DataGridRow)Tablazat.ItemContainerGenerator.ContainerFromIndex(sor);
                                    if (row != null)
                                    {
                                        row.Background = Brushes.Red;
                                    }
                                }
                            }
                            sor++;
                        
                    }
                    }
            };
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
            ColorDataGridRows();
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
            bool hiba = false;
            KonyvKiadWindow kkw = new KonyvKiadWindow(((KonyvKonyvtaros)Tablazat.SelectedItem).Id);
            try
            {
                kkw.Show();
            }
            catch (Exception)
            {
                hiba = true;
            }
            if (!hiba)
            {
                this.Close();
            }
        }

        private void kiadButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (((KonyvKonyvtaros)Tablazat.SelectedItem).KolcsonzottDB != "")
            {
                bool hiba = false;
                KonvVisszaWindow kvw = new KonvVisszaWindow(((KonyvKonyvtaros)Tablazat.SelectedItem).Id);
                try
                {
                    kvw.Show();
                }
                catch (Exception)
                {
                    hiba = true;
                }
                if (!hiba)
                {
                    this.Close();
                }
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            bool hiba = false;
            KonyvEditWindow kew = new KonyvEditWindow(((KonyvKonyvtaros)Tablazat.SelectedItem).Id);
            try
            {
                kew.Show();
            }
            catch (Exception)
            {
                hiba = true;
            }
            if (!hiba)
            {
                this.Close();
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            SplashWindow sw = new SplashWindow();
            sw.Show();
            this.Close();
        }
    }
}
