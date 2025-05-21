using Projet.Model.Entites;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.Win32;

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
            ClientsDataGrid.ItemsSource = Clients;
            ObjetsDataGrid.ItemsSource = Produits;
            CommandesDataGrid.ItemsSource = Commandes;
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

                Produit nouveauProduit = fenetre.CategorieObjet switch
                {
                    "Ustensil" => new Ustensils(newId, fenetre.Nom, bois, fenetre.Prix, fenetre.Usage),
                    "Figurine" => new Figurines(newId, fenetre.Nom, bois, fenetre.Prix, fenetre.Usage),
                    "Meuble" => new Meubles(newId, fenetre.Nom, bois, fenetre.Prix, fenetre.Usage)
                };

                if (nouveauProduit != null)
                {
                    Produits.Add(nouveauProduit);
                }
            }
        }



        private void PasserCommande_Click(object sender, RoutedEventArgs e)
        {
            var clientSelectionne = ClientsDataGrid.SelectedItem as Clients;
            var objetsSelectionnes = ObjetsDataGrid.SelectedItems;

            if (clientSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un client.");
                return;
            }

            if (objetsSelectionnes == null || objetsSelectionnes.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un ou plusieurs objets.");
                return;
            }

            if (DateFilterPicker.SelectedDate == null)
            {
                MessageBox.Show("Veuillez sélectionner une date pour la commande.");
                return;
            }

            DateTime dateCommande = DateFilterPicker.SelectedDate.Value;
            var commande = new Commande(commandeIdCounter++, clientSelectionne, dateCommande);


            foreach (Produit produit in objetsSelectionnes)
            {
                commande.AjouterProduit(produit);
            }

            Commandes.Add(commande);
            MessageBox.Show("Commande avec plusieurs objets ajoutée !");
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


        public void SauvegarderEnJson(string cheminFichier)
        {
            var donnees = new DonneesApplication
            {
                Clients = Clients.ToList(),
                Produits = Produits.ToList(),
                Commandes = Commandes.ToList()
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new ProduitJsonConverter() } // pour gérer les types dérivés
            };

            File.WriteAllText(cheminFichier, JsonSerializer.Serialize(donnees, options));
        }

        public class ProduitJsonConverter : System.Text.Json.Serialization.JsonConverter<Produit>
        {
            public override Produit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using var jsonDoc = JsonDocument.ParseValue(ref reader);
                var jsonObject = jsonDoc.RootElement;
                var type = jsonObject.GetProperty("Categorie").GetString();

                return type switch
                {
                    "Ustensil" => JsonSerializer.Deserialize<Ustensils>(jsonObject.GetRawText(), options),
                    "Figurine" => JsonSerializer.Deserialize<Figurines>(jsonObject.GetRawText(), options),
                    "Meuble" => JsonSerializer.Deserialize<Meubles>(jsonObject.GetRawText(), options),
                    _ => throw new NotSupportedException($"Type de produit inconnu : {type}")
                };
            }

            public override void Write(Utf8JsonWriter writer, Produit value, JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
            }
        }

        public void ChargerDepuisJson(string cheminFichier)
        {
            if (!File.Exists(cheminFichier)) return;

            var options = new JsonSerializerOptions
            {
                Converters = { new ProduitJsonConverter() }
            };

            var donnees = JsonSerializer.Deserialize<DonneesApplication>(File.ReadAllText(cheminFichier), options);
            if (donnees != null)
            {
                Clients.Clear();
                foreach (var c in donnees.Clients) Clients.Add(c);

                Produits.Clear();
                foreach (var p in donnees.Produits) Produits.Add(p);

                Commandes.Clear();
                foreach (var cmd in donnees.Commandes) Commandes.Add(cmd);
            }
        }
        private void SauvegarderJson_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Fichiers JSON (*.json)|*.json",
                FileName = "donnees.json"
            };

            if (dialog.ShowDialog() == true)
            {
                SauvegarderEnJson(dialog.FileName);
                MessageBox.Show("Données sauvegardées avec succès !");
            }
        }

        private void ChargerJson_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Fichiers JSON (*.json)|*.json"
            };

            if (dialog.ShowDialog() == true)
            {
                ChargerDepuisJson(dialog.FileName);
                MessageBox.Show("Données chargées avec succès !");
            }
        }

        private void SauvegarderXml_Click(object sender, RoutedEventArgs e)
        {
            var commandeSelectionnee = CommandesDataGrid.SelectedItem as Commande;

            if (commandeSelectionnee == null)
            {
                MessageBox.Show("Veuillez sélectionner une commande à sauvegarder.");
                return;
            }

            var dialog = new SaveFileDialog
            {
                Filter = "Fichiers XML (*.xml)|*.xml",
                FileName = $"commande_{commandeSelectionnee.Id}.xml"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(Commande));
                    using var fs = new FileStream(dialog.FileName, FileMode.Create);
                    serializer.Serialize(fs, commandeSelectionnee);

                    MessageBox.Show("Commande sauvegardée avec succès !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la sauvegarde : {ex.Message}");
                }
            }
        }

        private void ChargerXml_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Fichiers XML (*.xml)|*.xml"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(Commande));
                    using var fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                    var commande = (Commande)serializer.Deserialize(fs);

                    if (commande != null)
                    {
                        Commandes.Add(commande);
                        CommandesDataGrid.Items.Refresh(); // Important si DataGrid lié
                        MessageBox.Show("Commande chargée et ajoutée !");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
            }
        }

        private MyAppParamManager paramManager = new MyAppParamManager();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            paramManager.LoadRegistryParameter();
            AppliquerParametres();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            paramManager.SaveRegistryParameter();
        }

        private void OuvrirParametres_Click(object sender, RoutedEventArgs e)
        {
            paramManager.LoadRegistryParameter();
            var fenetreParametres = new ParametresWindow(paramManager);
            fenetreParametres.Owner = this;

            if (fenetreParametres.ShowDialog() == true)
            {
                // Tu peux éventuellement réagir à la mise à jour ici
                AppliquerParametres();
                MessageBox.Show("Paramètres appliqués !");
            }
        }
        private void AppliquerParametres()
        {
            // Applique le nom d'utilisateur (par exemple dans le titre)
            this.Title = $"Application - {paramManager.NomUtilisateur}";

            // Applique la taille de police à tous les éléments principaux
            this.FontSize = paramManager.TaillePolice;

            // Applique le mode sombre si nécessaire
            if (paramManager.ModeSombre)
            {
                this.Background = System.Windows.Media.Brushes.DarkSlateGray;
                ContenuPanel.Background = System.Windows.Media.Brushes.Gray;
                Foreground = System.Windows.Media.Brushes.White;
            }
            else
            {
                this.Background = System.Windows.Media.Brushes.Beige;
                ContenuPanel.Background = System.Windows.Media.Brushes.Bisque;
                Foreground = System.Windows.Media.Brushes.Black;
            }
        }


    }
}
