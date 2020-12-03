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
    public partial class LoginWindow : Window
    {
        public Action CloseAction { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Bejelentkezes_Button_Action(object sender, RoutedEventArgs e)
        {
            if (JelszoPasswordBox.Password.ToString().Equals("") || NeptunKodTextBox.Text.ToString().Equals(""))
            {
                MessageBox.Show("Add meg a felhasználónevet és a jelszót a bejelntkezéshez!");
            }
            else
            {
                FelhasznaloAdatok fAdat = FelhasznaloAdatDataProvider.GetData(NeptunKodTextBox.Text.ToString());
                if(fAdat.jelszo==null || !fAdat.jelszo.Equals(JelszoPasswordBox.Password.ToString()))
                {
                    MessageBox.Show("Hibás felhasználónév vagy jelszó!");
                }
                else
                {
                    MainWindow mw = new MainWindow(NeptunKodTextBox.Text.ToString());
                    mw.Show();
                    this.Close();
                }
            }
        }

        private void Regisztracio_Button_Action(object sender, RoutedEventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            rw.Show();
            this.Close();
        }
    }
}
