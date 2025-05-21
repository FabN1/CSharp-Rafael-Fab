using System;
using System.Windows;

namespace Projet
{
    public partial class ParametresWindow : Window
    {
        private MyAppParamManager _manager;

        public ParametresWindow(MyAppParamManager manager)
        {
            InitializeComponent();
            _manager = manager;

            // Charger les valeurs existantes
            NomUtilisateurBox.Text = _manager.NomUtilisateur;
            TaillePoliceBox.Text = _manager.TaillePolice.ToString();
            ModeSombreCheck.IsChecked = _manager.ModeSombre;
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            _manager.NomUtilisateur = NomUtilisateurBox.Text;
            if (int.TryParse(TaillePoliceBox.Text, out int taille))
            {
                _manager.TaillePolice = taille;
            }
            _manager.ModeSombre = ModeSombreCheck.IsChecked == true;

            _manager.SaveRegistryParameter();
            MessageBox.Show("Paramètres sauvegardés !");
            this.DialogResult = true;
            this.Close();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
