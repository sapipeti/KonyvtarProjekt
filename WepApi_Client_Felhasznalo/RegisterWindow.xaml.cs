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
using System.Runtime;
using WebApi_Common.DataProviders;
using WebApi_Common.Models;


namespace WepApi_Client_Felhasznalo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Regisztracio_Button_Action(object sender, RoutedEventArgs e)
        {
            if (NeptunKodTextBox.Text.ToString().Equals("") || JelszoPassWordBox1.Password.ToString().Equals("") || JelszoPassWordBox2.Password.ToString().Equals("")) 
            {
                MessageBox.Show("Üres felhasználónév vagy jelszó mező!");
            }
            else if (!JelszoPassWordBox1.Password.ToString().Equals(JelszoPassWordBox1.Password.ToString()))
            {
                MessageBox.Show("A két megadott jelszó nem egyezik meg!");
            }else if (JelszoPassWordBox1.Password.ToString().Length<5)
            {
                MessageBox.Show("A jelszónak legalább 5 karakter hosszúnak kell lennie!");
            }
            else
            {
                FelhasznaloAdatok fAdat =FelhasznaloAdatDataProvider.GetData(NeptunKodTextBox.Text.ToString());
                if (!String.IsNullOrEmpty(fAdat.neptunKod))
                {
                    MessageBox.Show("Ezzel a Neptun kóddal már regisztráltak!");
                }
                else
                {
                    FelhasznaloAdatDataProvider.CreateFelhasznalo(new FelhasznaloAdatok(NeptunKodTextBox.Text.ToString(),JelszoPassWordBox1.Password.ToString()));
                    MessageBox.Show("Sikeres regisztráció!");
                    LoginWindow lw = new LoginWindow();
                    lw.Show();
                    this.Close();
                    
                }
            }
        }

        private void Vissza_Button_Action(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Close();
        }
    }
}
