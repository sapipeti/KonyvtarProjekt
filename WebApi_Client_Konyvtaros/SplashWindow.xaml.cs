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

namespace WebApi_Client_Konyvtaros
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
        }

        private void KonyvKeresButton_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow kkeresw = new SearchWindow();
            kkeresw.Show();
            this.Close();
        }

        private void KonyvAddButton_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow ab = new AddBookWindow();
            ab.Show();
            this.Close();
        }
    }
}
