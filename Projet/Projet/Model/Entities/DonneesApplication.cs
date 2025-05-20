using Projet.Model.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    class DonneesApplication
    {
        public List<Clients> Clients { get; set; }
        public List<Produit> Produits { get; set; }
        public List<Commande> Commandes { get; set; }
    }
}
