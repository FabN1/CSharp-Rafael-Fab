using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Projet
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, Dictionary<string, List<string>>> data = new Dictionary<string, Dictionary<string, List<string>>>()
        {
            { "Ustensils", new Dictionary<string, List<string>>{
                { "cuilleres", new List<string> { "cuilleres", "bols", "couteaux" } },
                { "bols", new List<string> { "cuilleres", "bols", "couteaux" } },
                { "couteaux", new List<string> { "cuilleres", "bols", "couteaux" } }
            }},

            { "Figurines", new Dictionary<string, List<string>>{
                { "Animaux", new List<string> { "cuilleres", "bols", "couteaux" } },
                { "Personnages", new List<string> { "cuilleres", "bols", "couteaux" } },
                { "Objets", new List<string> { "cuilleres", "bols", "couteaux" } }
            }},
            { "Meubles", new Dictionary<string, List<string>>{
                { "Tables", new List<string> { "cuilleres", "bols", "couteaux" } },
                { "Chaisses", new List<string> { "cuilleres", "bols", "couteaux" } },
                { "Armoires", new List<string> { "cuilleres", "bols", "couteaux" } }
            }},
        };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Connexion_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (username == "admin" && password == "1234")
            {
                MessageBox.Show("Connexion réussie !");
                CategoriePanel.Visibility = Visibility.Visible;
                ContenuPanel.Visibility = Visibility.Visible;

                CategorieComboBox.ItemsSource = data.Keys;
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
            }
        }

        private void CategorieComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCategorie = CategorieComboBox.SelectedItem as string;

            if (selectedCategorie != null && data.ContainsKey(selectedCategorie))
            {
                ElementsListBox.ItemsSource = data[selectedCategorie].Keys;
                ArticlesListBox.ItemsSource = null; // Reset articles
            }
        }

        private void ElementsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCategorie = CategorieComboBox.SelectedItem as string;
            string selectedElement = ElementsListBox.SelectedItem as string;

            if (selectedCategorie != null && selectedElement != null && data[selectedCategorie].ContainsKey(selectedElement))
            {
                ArticlesListBox.ItemsSource = data[selectedCategorie][selectedElement];
            }
        }
    }
}