using System.Windows;

namespace Projet
{
    public partial class LoginWindow : Window
    {
        public bool etatConnexion = false;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Exemple basique de vérification
            if (username == "admin" && password == "1234")
            {
                etatConnexion = true;
                DialogResult = true;

            }
            else
            {
                MessageBox.Show("Identifiants incorrects !");
            }
        }
    }
}
