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
using System.Windows.Shapes;

namespace Projet
{
    public partial class AjouterObjetWindow : Window
    {
        public string Nom { get; private set; }
        public string TypeBois { get; private set; }
        public float Prix { get; private set; }
        public string Usage { get; private set; }

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

            DialogResult = true;
            Close();
        }
    }

}
