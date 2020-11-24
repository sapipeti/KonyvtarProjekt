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
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow()
        {
            InitializeComponent();
        }
        long maxID = -1;
        private void letrehozButtonAction(object sender, RoutedEventArgs e)
        {
            if(konyvcimTextBox.Text.ToString().Equals("")||kiadoTextBox.Text.ToString().Equals("")||kiadasevTextBox.Text.ToString().Equals("")||isbnTextBox.Text.ToString().Equals("")||darabszamTextBox.Text.ToString().Equals("")|| mufajokListView.Items.Count<1||szerzokListView.Items.Count < 1)
            {
                MessageBox.Show("Nem töltöttél ki minden mezőt");
            }
            else
            {
                foreach (var item in KonyvDataProvider.GetKonyvek())
                {
                    if (item.Id > maxID)
                    {
                        maxID = item.Id;
                    }
                }
                maxID++;
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

                KonyvDataProvider.CreateKonyv(new WebApi_Common.Models.Konyv(maxID, konyvcimTextBox.Text, Convert.ToInt64(isbnTextBox.Text), kiadoTextBox.Text, Convert.ToInt32(kiadasevTextBox.Text), mufajokLista, szerzokLista, Convert.ToInt32(darabszamTextBox.Text), new List<string>(), new List<DateTime>(), new List<int>()));
                MessageBox.Show("Sikeres könyv létrehozás!");
                maxID = -1;
            }
        }

        private void torolButtonAction(object sender, RoutedEventArgs e)
        {
            //Cellák törlése
            konyvcimTextBox.Text = "";
            kiadoTextBox.Text = "";
            kiadasevTextBox.Text = "";
            isbnTextBox.Text = "";
            darabszamTextBox.Text = "";
            mufajokTextBox.Text = "";
            szerzokTextBox.Text = "";
            mufajokListView.Items.Clear();
            szerzokListView.Items.Clear();

        }

        private void mufajAddButtonAction(object sender, RoutedEventArgs e)
        {
            if (!mufajokTextBox.Text.ToString().Equals(""))
            {
                mufajokListView.Items.Add(mufajokTextBox.Text);
                mufajokTextBox.Text= "";
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
    }
    
}
