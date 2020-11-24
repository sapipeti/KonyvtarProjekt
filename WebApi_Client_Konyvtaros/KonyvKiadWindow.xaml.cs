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
        public Konyv konyv1 = new Konyv();

        public KonyvKiadWindow(KonyvKonyvtaros konyv)
        {
            InitializeComponent();
            
            
            konyvIdTextBox.Text = konyv.Id.ToString();
            konyvCimTextBox.Text = konyv.Cím;
            konyvSzerzoTextBox.Text = konyv.Szerző.ToString();

            konyv1.Cím = konyv.Cím;
            konyv1.Id = konyv.Id;
            konyv1.ISBN = konyv.ISBN;
            konyv1.Kiadás_Év = konyv.Kiadás_Év;
            konyv1.Kiadó = konyv.Kiadó;
            konyv1.Szerző.Add(konyv.Szerző);
            konyv1.VisszaHozas.Add(DateTime.Parse(konyv.VisszaHozas));
            konyv1.NeptunKod.Add(konyv.NeptunKod);
            konyv1.KolcsonzottDB.Add(int.Parse(konyv.KolcsonzottDB));
            konyv1.Műfajok.Add(konyv.Műfajok);

            
        }

        private void Megsem_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow();
            sw.Show();
            this.Close();
        }

        private void Kiadas_Click(object sender, RoutedEventArgs e)
        {
            if(darabszamTextBox.Text.Equals("") || !datePicker.SelectedDate.HasValue || neptunkodTextBox.Text.Equals(""))
            {
                MessageBox.Show("Kérlek tölts ki minden mezőt!", "Hiba");
            }
            else
            {
                if (ValidateKiad())
                {
                    konyv1.NeptunKod.Add(neptunkodTextBox.Text);
                    konyv1.KolcsonzottDB.Add(int.Parse(darabszamTextBox.Text));
                    konyv1.VisszaHozas.Add(datePicker.SelectedDate.Value);
                    KonyvDataProvider.UpdateKonyv(konyv1);
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
