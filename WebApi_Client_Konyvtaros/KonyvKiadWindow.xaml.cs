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
        public List<FelhasznaloAdatok> fAdat = FelhasznaloAdatDataProvider.GetData().ToList();
        public List<string> fAdatString { get; set; }
        public string TestText { get; set; }

        public Konyv konyv = new Konyv();
        public Konyv updated_konyv = new Konyv();

        public KonyvKiadWindow()
        {
            
        }

        public KonyvKiadWindow(long id)
        {
            InitializeComponent(); 
            fAdatString = new List<string>();
            foreach (var item in fAdat)
            {
                fAdatString.Add(item.neptunKod.ToString());
            }

            DataContext = this;

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
            if (elerhetoDB < 1)
            {
                MessageBox.Show("Az adott könyvből nincs elérhető a könyvtárban", "Hiba");
                this.Close();
            }
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
                var nk = neptunkodTextBox.Text;
                var db = darabszamTextBox.Text;
                var dt = datePicker.SelectedDate.HasValue;
                if (ValidateKiad(nk, db, dt))
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

        public bool ValidateKiad(String neptunkod, String darabszam, bool datetime)
        {
            if (String.IsNullOrEmpty(neptunkod))
            {
                MessageBox.Show("Adj meg neptunkódot!");
                return false;
            }

            if (String.IsNullOrEmpty(darabszam))
            {
                MessageBox.Show("Adj meg darabszámot!");
                return false;
            }
            if (!datetime)
            {
                MessageBox.Show("Válassz ki dátumot!");
                return false;
            }
            return true;
        }
    }
}
