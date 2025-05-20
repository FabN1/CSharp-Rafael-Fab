using System.Windows.Controls;
using System.Windows;

namespace Projet
{
    public partial class AjouterObjetWindow : Window
    {
        public string Nom { get; private set; }
        public string TypeBois { get; private set; }
        public float Prix { get; private set; }
        public string Usage { get; private set; }
        public string CategorieObjet { get; private set; }

        public AjouterObjetWindow()
        {
            InitializeComponent();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            Nom = NomTextBox.Text;
            TypeBois = TypeBoisTextBox.Text;
            float.TryParse(PrixTextBox.Text, out float prix);
            Prix = prix;
            Usage = UsageTextBox.Text;

            CategorieObjet = ((ComboBoxItem)TypeObjetComboBox.SelectedItem).Content.ToString();

            DialogResult = true;
            Close();
        }
    }
}