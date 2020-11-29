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
    /// Interaction logic for KonyvEditWindow.xaml
    /// </summary>
    public partial class KonyvEditWindow : Window
    {
        long ID = -1;
        public List<Konyv> _konyvek = KonyvDataProvider.GetKonyvek().ToList();
        public Konyv konyv = new Konyv();
        public Konyv updated_konyv = new Konyv();
        public List<string> NeptunKod;
        public List<DateTime> VisszaHozas;
        public List<int> KolcsonzottDB;
        public KonyvEditWindow(long id)
        {
            InitializeComponent();
            ID = id;
            foreach (var item in _konyvek)
            {
                if (item.Id == id)
                {
                    konyv = item;
                    break;
                }
            }

            konyvcimTextBox.Text = konyv.Cím;
            kiadoTextBox.Text = konyv.Kiadó;
            isbnTextBox.Text = konyv.ISBN.ToString();
            kiadasevTextBox.Text = konyv.Kiadás_Év.ToString();
            darabszamTextBox.Text = konyv.Darabszám.ToString();
            foreach (var item in konyv.Műfajok)
            {
                mufajokListView.Items.Add(item);
            }
            foreach (var item in konyv.Szerző)
            {
                szerzokListView.Items.Add(item);
            }
            NeptunKod = konyv.NeptunKod;
            VisszaHozas = konyv.VisszaHozas;
            KolcsonzottDB = konyv.KolcsonzottDB;



        }

        private void megsemButtonClick(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow();
            sw.Show();
            this.Close();
        }

        private void modositButton_Click(object sender, RoutedEventArgs e)
        {
            if (konyvcimTextBox.Text.ToString().Equals("") || kiadoTextBox.Text.ToString().Equals("") || kiadasevTextBox.Text.ToString().Equals("") || isbnTextBox.Text.ToString().Equals("") || darabszamTextBox.Text.ToString().Equals("") || mufajokListView.Items.Count < 1 || szerzokListView.Items.Count < 1)
            {
                MessageBox.Show("Nem töltöttél ki minden mezőt!");
            }
            else
            {
                
                List<string> szerzokLista = new List<string>();
                List<string> mufajokLista = new List<string>();

                foreach (var item in mufajokListView.Items)
                {
                    mufajokLista.Add(item.ToString());
                }
                foreach (var item in szerzokListView.Items)
                {
                    szerzokLista.Add(item.ToString());
                }

                KonyvDataProvider.UpdateKonyv(new WebApi_Common.Models.Konyv(ID, konyvcimTextBox.Text, Convert.ToInt64(isbnTextBox.Text), kiadoTextBox.Text, Convert.ToInt32(kiadasevTextBox.Text), mufajokLista, szerzokLista, Convert.ToInt32(darabszamTextBox.Text), NeptunKod, VisszaHozas, KolcsonzottDB));
                MessageBox.Show("Sikeres könyv módosítás!");
                SearchWindow sw = new SearchWindow();
                sw.Show();
                this.Close();
            }
        }

        private void mufajAddButtonAction(object sender, RoutedEventArgs e)
        {
            if (!mufajokTextBox.Text.ToString().Equals(""))
            {
                mufajokListView.Items.Add(mufajokTextBox.Text);
                mufajokTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Adj meg egy műfajt!");
            }
        }

        private void szerzoAddButtonAction(object sender, RoutedEventArgs e)
        {
            if (!szerzokTextBox.Text.ToString().Equals(""))
            {
                szerzokListView.Items.Add(szerzokTextBox.Text);
                szerzokTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Adj meg egy szerzőt!");
            }
        }

        private void mufajDeleteButtonAction(object sender, RoutedEventArgs e)
        {
            if (mufajokListView.SelectedItem != null)
            {
                mufajokListView.Items.Remove(mufajokListView.SelectedItem);
            }
            else
            {
                MessageBox.Show("Válassz ki egy műfajt az eltávolításhoz!");
            }
        }

        private void szerzoDeleteButtonAction(object sender, RoutedEventArgs e)
        {
            if (szerzokListView.SelectedItem != null)
            {
                szerzokListView.Items.Remove(szerzokListView.SelectedItem);
            }
            else
            {
                MessageBox.Show("'Válassz ki egy szerzőt az eltávolításhoz!");
            }
        }
    }
}
