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
        public IList<Konyv> _konyvek;

        public KonyvKiadWindow(Konyv konyv)
        {
            InitializeComponent();
            
            if(konyv != null)
            {
                konyvIdTextBox.Text = konyv.Id.ToString();
            }
        }

        private void Megsem_Click(object sender, RoutedEventArgs e)
        {
            SplashWindow sw = new SplashWindow();
            sw.Show();
            this.Close();
        }

        private void Kiadas_Click(object sender, RoutedEventArgs e)
        {
            if(konyvIdTextBox.Text.Equals("") || darabszamTextBox.Text.Equals("") || !datePicker.SelectedDate.HasValue || neptunkodTextBox.Text.Equals(""))
            {
                MessageBox.Show("Kérlek tölts ki minden mezőt!", "Hiba");
            }
            else
            {
                
                SplashWindow sw = new SplashWindow();
                sw.Show();
                this.Close();
            }
        }
    }
}
