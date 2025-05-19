using Projet.Model.Entites;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Projet
{
    public partial class MainWindow : Window
    {
        // Collections liées au UI via DataBinding
        public ObservableCollection<Clients> Clients { get; set; } = new ObservableCollection<Clients>();
        public ObservableCollection<Produit> Produits { get; set; } = new ObservableCollection<Produit>();
        public ObservableCollection<Commande> Commandes { get; set; } = new ObservableCollection<Commande>();

        private int commandeIdCounter = 1;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this; // Pour le Binding dans XAML (si utilisé)
        }

        

        private void ChargerDonneesDeTest()
        {
            // Types de bois
            var boisChene = new TypeDeBois(1, "Chêne");
            var boisHetre = new TypeDeBois(2, "Hêtre");

            // Clients
            Clients.Add(new Clients(1, "Alice", "Particulier", "alice@email.com"));
            Clients.Add(new Clients(2, "Bob", "Entreprise", "bob@corp.com"));

            // Produits
            Produits.Add(new Ustensils(1, "Cuillère", boisChene, 5.0f, "Cuisine"));
            Produits.Add(new Figurines(2, "Lion", boisHetre, 20.0f, "Animaux"));
            Produits.Add(new Meubles(3, "Table", boisChene, 150.0f, "Salle à manger"));

            // Commande de test
            var commande = new Commande(commandeIdCounter++, Clients[0]);
            commande.AjouterProduit(Produits[0]);
            commande.AjouterProduit(Produits[2]);
            Commandes.Add(commande);
        }
        private void AjouterClient_Click(object sender, RoutedEventArgs e)
        {
            var fenetre = new AjouterClientWindow();
            fenetre.Owner = this;

            if (fenetre.ShowDialog() == true)
            {
                int newId = Clients.Count + 1;
                Clients.Add(new Clients(newId, fenetre.Nom, fenetre.Type, fenetre.Email));
            }
        }


        private void AjouterObjet_Click(object sender, RoutedEventArgs e)
        {
            var fenetre = new AjouterObjetWindow();
            fenetre.Owner = this;

            if (fenetre.ShowDialog() == true)
            {
                int newId = Produits.Count + 1;
                var bois = new TypeDeBois(1, fenetre.TypeBois);
                Produits.Add(new Ustensils(newId, fenetre.Nom, bois, fenetre.Prix, fenetre.Usage));
            }
        }


        private void PasserCommande_Click(object sender, RoutedEventArgs e)
        {
            var clientSelectionne = ClientsDataGrid.SelectedItem as Clients;
            var produitSelectionne = ObjetsDataGrid.SelectedItem as Produit;

            if (clientSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un client.");
                return;
            }

            if (produitSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un produit.");
                return;
            }

            var commande = new Commande(commandeIdCounter++, clientSelectionne);
            commande.AjouterProduit(produitSelectionne);

            Commandes.Add(commande);
            MessageBox.Show("Commande ajoutée !");
        }


        private void DateFilterPicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Exemple simple : récupérer la date sélectionnée
            var datePicker = sender as System.Windows.Controls.DatePicker;
            if (datePicker != null && datePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = datePicker.SelectedDate.Value;
                MessageBox.Show($"Date sélectionnée : {selectedDate.ToShortDateString()}");
                // Ici, tu pourrais filtrer ta liste Commandes en fonction de cette date
            }
        }

    }
}
