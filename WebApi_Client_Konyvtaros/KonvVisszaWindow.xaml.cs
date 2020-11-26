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
    /// Interaction logic for KonvVisszaWindow.xaml
    /// </summary>
    public partial class KonvVisszaWindow : Window
    {
        public List<Konyv> _konyvek = KonyvDataProvider.GetKonyvek().ToList();
        FelhasznaloAdatok fAdat;
        public Konyv konyv = new Konyv();
        public Konyv updated_konyv = new Konyv();

        public KonvVisszaWindow(long id)
        {
            InitializeComponent();

            foreach (var item in _konyvek)
            {
                if (item.Id == id)
                {
                    konyv = item;
                    break;
                }
            }

            konyvIdTextBox.Text = konyv.Id.ToString();
            konyvCimTextBox.Text = konyv.Cím;
            int i = 0;
            foreach (var item in konyv.Szerző)
            {
                if (i != 0)
                {
                    konyvSzerzoTextBox.Text += "," + item;
                }
                else
                {
                    konyvSzerzoTextBox.Text += item;
                }
                i++;
            }

            updated_konyv.Cím = konyv.Cím;
            updated_konyv.Id = konyv.Id;
            updated_konyv.ISBN = konyv.ISBN;
            updated_konyv.Darabszám = konyv.Darabszám;
            updated_konyv.Kiadás_Év = konyv.Kiadás_Év;
            updated_konyv.Kiadó = konyv.Kiadó;
            updated_konyv.Szerző = konyv.Szerző;
            updated_konyv.VisszaHozas = konyv.VisszaHozas;
            updated_konyv.NeptunKod = konyv.NeptunKod;
            updated_konyv.KolcsonzottDB = konyv.KolcsonzottDB;
            updated_konyv.Műfajok = konyv.Műfajok;

            if (konyv.KolcsonzottDB.Count == 0)
            {
                MessageBox.Show("Nincs mit visszavinni!", "Hiba");
                this.Close();
            }
               
        }

        private void Megsem_Action(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow();
            sw.Show();
            this.Close();
        }

        private void VisszaHozas_Action(object sender, RoutedEventArgs e)
        {
            int i = 0;
            if (darabszamTextBox.Text.Equals("") || neptunkodTextBox.Text.Equals(""))
            {
                MessageBox.Show("Kérlek tölts ki minden mezőt!", "Hiba");
            }
            else
            {
                if (updated_konyv.NeptunKod == null)
                {
                    updated_konyv.NeptunKod = new List<string>();
                }
                foreach (var item in updated_konyv.NeptunKod)
                {
                    if (item.Equals(neptunkodTextBox.Text.ToString()))
                    {
                        break;
                    }
                    i++;
                }

                updated_konyv.NeptunKod.RemoveAt(i);

                if (updated_konyv.KolcsonzottDB == null)
                {
                    updated_konyv.KolcsonzottDB = new List<int>();
                }
                updated_konyv.KolcsonzottDB.RemoveAt(i);
                updated_konyv.VisszaHozas.RemoveAt(i);

                KonyvDataProvider.UpdateKonyv(updated_konyv);

                MessageBox.Show("Sikeres könyvviszahozás " + neptunkodTextBox.Text.ToString() + " felhasználónak!");

                SplashWindow sw = new SplashWindow();
                sw.Show();
                this.Close();
            }
        }

        private void neptunkodTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int i = 0;
            _konyvek = KonyvDataProvider.GetKonyvek().ToList();
            foreach (var item in _konyvek)
            {
                foreach (var item2 in item.NeptunKod)
                {
                    if (item2.Equals(neptunkodTextBox.Text.ToString()) && item.Id==Convert.ToInt64(konyvIdTextBox.Text.ToString()))
                    {
                        darabszamTextBox.Text = item.KolcsonzottDB[i].ToString();
                        break;
                    }
                    i++;
                }
                i = 0;
            }
        }
    }
}
