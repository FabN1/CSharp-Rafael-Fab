using Projet.Model.Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Projet.Model.Entites
{
    [Serializable]
    [XmlInclude(typeof(Figurines))]
    [XmlInclude(typeof(Ustensils))]
    [XmlInclude(typeof(Meubles))]
    [XmlInclude(typeof(Clients))]
    public class Commande
    {
        private int id;
        private Clients client;
        private List<Produit> produits;
        private DateTime dateCommande;


        public Commande() { }
        public Commande(int id, Clients client)
        {
            this.id = id;
            this.client = client;
            this.produits = new List<Produit>();
            this.dateCommande = DateTime.Now;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public Clients Client
        {
            get => client;
            set => client = value;
        }

        public List<Produit> Produits
        {
            get => produits;
            set => produits = value;
        }

        public DateTime DateCommande
        {
            get => dateCommande;
            set => dateCommande = value;
        }

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
            sb.AppendLine($"Commande ID: {id} – Client: {(client?.Nom ?? "inconnu")} – Date: {dateCommande}");

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
