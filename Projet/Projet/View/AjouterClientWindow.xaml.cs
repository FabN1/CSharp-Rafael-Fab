using System.Windows.Controls;
using System.Windows;

namespace Projet
{
    public partial class AjouterClientWindow : Window
    {
        public string Nom { get; private set; }
        public string Type { get; private set; }
        public string Email { get; private set; }

        public AjouterClientWindow()
        {
            InitializeComponent();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            Nom = NomTextBox.Text;
            Type = ((ComboBoxItem)TypeComboBox.SelectedItem).Content.ToString();
            Email = EmailTextBox.Text;

            DialogResult = true;
            Close();
        }
    }
}