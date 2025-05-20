using Projet.Model.Entites;
using System;

namespace Projet.Model.Entites
{
    [Serializable]
    public class Ustensils : Produit
    {
        private float prix;
        private string usage;
        public override string Categorie => "Ustensil";

        public Ustensils(int id, string nom, TypeDeBois typeDeBois, float prix, string usage)
            : base(id, nom, typeDeBois)
        {
            this.prix = prix;
            this.usage = usage;
        }

        public float Prix
        {
            get => prix;
            set => prix = value;
        }

        public string Usage
        {
            get => usage;
            set => usage = value;
        }

        public override double CalculerPrix()
        {
            return prix;
        }

        public override string ToString()
        {
            return base.ToString() + $" - Ustensile (usage: {usage}, prix: {prix})";
        }

        public override bool Equals(object obj)
        {
            return obj is Ustensils u && id == u.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
