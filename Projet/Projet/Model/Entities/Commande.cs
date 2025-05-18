using Projet.Model.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projet.Model.Entites
{
    [Serializable]
    public class Commande
    {
        private int id;
        private Clients client;
        private List<Produit> produits;
        private DateTime dateCommande;

        public Commande(int id, Clients client)
        {
            this.id = id;
            this.client = client;
            this.produits = new List<Produit>();
            this.dateCommande = DateTime.Now;
        }

        public int Id => id;

        public Clients Client => client;

        public List<Produit> Produits => produits;

        public DateTime DateCommande => dateCommande;

        public void AjouterProduit(Produit produit)
        {
            if (produit != null)
                produits.Add(produit);
        }

        public void SupprimerProduit(Produit produit)
        {
            produits.Remove(produit);
        }

        public double CalculerTotal()
        {
            double total = 0;
            foreach (var p in produits)
            {
                total += p.CalculerPrix();
            }
            return total;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Commande ID: {id} - Client: {client.Nom} - Date: {dateCommande}");
            foreach (var produit in produits)
            {
                sb.AppendLine("  " + produit.ToString());
            }
            sb.AppendLine($"Total: {CalculerTotal()}");
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Commande c && id == c.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
