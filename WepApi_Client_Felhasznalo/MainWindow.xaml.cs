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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            konyvek = KonyvDataProvider.GetKonyvek().ToList();
            string [] neptunKod = udvozles_Label.Content.ToString().Split(':');
            neptunKod[1]=neptunKod[1].Replace(" ", "");
            int i = 0,index=-1;
            string mufaj="", szerzo="";
            foreach (var item in konyvek)
            {
                foreach (var neptunkod in item.NeptunKod)
                {
                  if (neptunkod.ToString().Equals(neptunKod[1]))
                  {
                      index = i;
                      break;
                  }
                  i++;
                }
                foreach (var item2 in item.Műfajok)
                {
                    if (!mufaj.Equals("")) {
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
            Tablazat.ItemsSource = konyvek_kliens;
        }
    }
}
