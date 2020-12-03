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
        List<KonyvKliens> konyvek_kikolcsonzott = new List<KonyvKliens>();
        List<KonyvKliensKolcsonozheto> konyvek_kikolcsonozheto = new List<KonyvKliensKolcsonozheto>();
        string NeptunKod;
        string OszlopNev;
        Boolean kikolcsonzott = true;

        String[] oszlopok_kikolcsonzott = { "Id", "Cím", "ISBN", "Kiadó", "Kiadás_Év", "Műfajok", "Szerző", "Visszahozas", "KolcsonzottDB" };
        String[] oszlopok_kikolcsonozheto = { "Id", "Cím", "ISBN", "Kiadó", "Kiadás_Év", "Műfajok", "Szerző", "Darabszám", "KiadhatóDarabszám" };


        public MainWindow(String NeptunKod)
        {
            this.NeptunKod = NeptunKod;
            InitializeComponent();
            udvozles_Label.Content += (" "+NeptunKod);

            UpdateData();
            if (kikolcsonzott)
            {
                Tablazat.ItemsSource = konyvek_kikolcsonzott;
                OszlopComboBox.ItemsSource = oszlopok_kikolcsonzott;
            }
            else
            {
                Tablazat.ItemsSource = konyvek_kikolcsonozheto;
                OszlopComboBox.ItemsSource = oszlopok_kikolcsonozheto;
            }
            
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
            string mufaj = "", szerzo = "";
            if (kikolcsonzott)
            {
                konyvek_kikolcsonzott.Clear();
                konyvek = KonyvDataProvider.GetKonyvek().ToList();
                int i = 0, index = -1;
                foreach (var item in konyvek)
                {
                    if (item.NeptunKod != null)
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

                    //Akkor jelenítjük meg a könyvet, ha a felhasználó kikölcsönözte azt.
                    if (index != -1)
                    {
                        konyvek_kikolcsonzott.Add(new KonyvKliens(item.Id, item.Cím, item.ISBN, item.Kiadó, item.Kiadás_Év, mufaj, szerzo, item.VisszaHozas[index], item.KolcsonzottDB[index]));
                    }
                    mufaj = "";
                    szerzo = "";
                    index = -1;
                    i = 0;
                }
            }
            else
            {
                konyvek_kikolcsonozheto.Clear();
                konyvek = KonyvDataProvider.GetKonyvek().ToList();
                int konyvDB=0;
                foreach (var item in konyvek)
                {
                    konyvDB = item.Darabszám;
                    if (item.KolcsonzottDB != null)
                    {
                        foreach (var item2 in item.KolcsonzottDB)
                        {
                            konyvDB -= item2;
                        }
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
                    if (konyvDB > 0)
                    {
                        konyvek_kikolcsonozheto.Add(new KonyvKliensKolcsonozheto(item.Id,item.Cím,item.ISBN,item.Kiadó,item.Kiadás_Év,mufaj,szerzo,item.Darabszám,konyvDB));
                    }
                    mufaj = "";
                    szerzo = "";
                    konyvDB = 0;
                }
            }
        }

        private void OszlopComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OszlopComboBox.SelectedIndex != -1)
            {
                UpdateData();
                OszlopNev = OszlopComboBox.SelectedItem.ToString();
            }
            
        }

        private void kikolcsonozhetoRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            kikolcsonzott = false;
            UpdateData();
            Tablazat.ItemsSource = konyvek_kikolcsonozheto;
            KeresesTextBox.Text = "";
            OszlopComboBox.SelectedIndex = -1;
            OszlopComboBox.ItemsSource = oszlopok_kikolcsonozheto;
        }

        private void kikolcsonozottRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            kikolcsonzott = true;
            UpdateData();
            Tablazat.ItemsSource = konyvek_kikolcsonzott;
            KeresesTextBox.Text = "";
            OszlopComboBox.SelectedIndex = -1;
            OszlopComboBox.ItemsSource = oszlopok_kikolcsonzott;

        }
    }
}
