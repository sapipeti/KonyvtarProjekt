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
    /// Interaction logic for KonyvKiadWindow.xaml
    /// </summary>
    public partial class KonyvKiadWindow : Window
    {
        public List<Konyv> _konyvek = KonyvDataProvider.GetKonyvek().ToList();
        FelhasznaloAdatok fAdat;
        public Konyv konyv = new Konyv();
        public Konyv updated_konyv = new Konyv();

        public KonyvKiadWindow(long id)
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

            //Megnézzük, hogy van e bent könyv a könyvtárban.
            int elerhetoDB = konyv.Darabszám;
            foreach (var item in konyv.KolcsonzottDB)
            {
                elerhetoDB -= item;
            }
            MessageBox.Show("Az adott könyvből nincs elérhető a könyvtárban", "Hiba");
            /*SearchWindow sw = new SearchWindow();
            sw.Show();*/
            this.Close();
        }

        private void Megsem_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow();
            sw.Show();
            this.Close();
        }

        private void Kiadas_Click(object sender, RoutedEventArgs e)
        {
            if (darabszamTextBox.Text.Equals("") || !datePicker.SelectedDate.HasValue || neptunkodTextBox.Text.Equals(""))
            {
                MessageBox.Show("Kérlek tölts ki minden mezőt!", "Hiba");
            } 
            else
            {
                if (ValidateKiad())
                {
                    if (updated_konyv.NeptunKod == null)
                    {
                        updated_konyv.NeptunKod = new List<string>();
                    }
                    updated_konyv.NeptunKod.Add(neptunkodTextBox.Text.ToString());

                    if (updated_konyv.KolcsonzottDB == null)
                    {
                        updated_konyv.KolcsonzottDB = new List<int>();
                    }
                    updated_konyv.KolcsonzottDB.Add(int.Parse(darabszamTextBox.Text));

                    if (updated_konyv.VisszaHozas == null)
                    {
                        updated_konyv.VisszaHozas = new List<DateTime>();
                    }
                    updated_konyv.VisszaHozas.Add(datePicker.SelectedDate.Value);
                    KonyvDataProvider.UpdateKonyv(updated_konyv);

                    MessageBox.Show("Sikeres könyvkiadás " + neptunkodTextBox.Text.ToString() + " felhasználónak!");

                    SplashWindow sw = new SplashWindow();
                    sw.Show();
                    this.Close();
                }
            }
        }

        private bool ValidateKiad()
        {
            if (String.IsNullOrEmpty(neptunkodTextBox.Text))
            {
                MessageBox.Show("Adj meg neptunkódot!");
                return false;
            }

            if (String.IsNullOrEmpty(darabszamTextBox.Text))
            {
                MessageBox.Show("Adj meg darabszámot!");
                return false;
            }
            if (!datePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Válassz ki dátumot!");
                return false;
            }
            return true;
        }

        private void neptunkodTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            fAdat = FelhasznaloAdatDataProvider.GetData(neptunkodTextBox.Text.ToString());
        }
    }
}
