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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebApi_Common.DataProviders;
using WebApi_Common.Models;


namespace WepApi_Client_Felhasznalo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Konyv> konyvek = new List<Konyv>();
        List<KonyvKliens> konyvek_kliens = new List<KonyvKliens>();
        string NeptunKod;
        string OszlopNev;

        public MainWindow(String NeptunKod)
        {
            this.NeptunKod = NeptunKod;
            InitializeComponent();
            udvozles_Label.Content += (" "+NeptunKod);

            String[] oszlopok = { "Id", "Cím", "ISBN", "Kiadó", "Kiadás_Év", "Műfajok", "Szerző", "Visszahozas", "KolcsonzottDB" };
            OszlopComboBox.ItemsSource = oszlopok;

            UpdateData();
            Tablazat.ItemsSource = konyvek_kliens;
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
                        return ((item as KonyvKliens).Id.ToString().IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Cím":
                        return ((item as KonyvKliens).Cím.IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "ISBN":
                        return ((item as KonyvKliens).ISBN.ToString().IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Kiadó":
                        return ((item as KonyvKliens).Kiadó.IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Kiadás_Év":
                        return ((item as KonyvKliens).Kiadás_Év.ToString().IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Műfajok":
                        return ((item as KonyvKliens).Műfajok.IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Szerző":
                        return ((item as KonyvKliens).Szerző.IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "Visszahozas":
                        return ((item as KonyvKliens).VisszaHozas.ToString().IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    case "KolcsonzottDB":
                        return ((item as KonyvKliens).KolcsonzottDB.ToString().IndexOf(KeresesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
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
            konyvek_kliens.Clear();
            konyvek = KonyvDataProvider.GetKonyvek().ToList();
            int i = 0, index = -1;
            string mufaj = "", szerzo = "";
            foreach (var item in konyvek)
            {
                foreach (var neptunkod in item.NeptunKod)
                {
                    if (neptunkod.ToString().Equals(NeptunKod))
                    {
                        index = i;
                        break;
                    }
                    i++;
                }
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
                if (index != -1)
                {
                    konyvek_kliens.Add(new KonyvKliens(item.Id, item.Cím, item.ISBN, item.Kiadó, item.Kiadás_Év, mufaj, szerzo/*, item.VisszaHozas[i], item.KolcsonzottDB[i]*/));
                }

            }
        }

        private void OszlopComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
            OszlopNev = OszlopComboBox.SelectedItem.ToString();
        }
    }
}
