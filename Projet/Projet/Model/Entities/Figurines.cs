using Projet.Model.Entites;
using System;

namespace Projet.Model.Entites
{
    [Serializable]
    public class Figurines : Produit
    {
        private float prix;
        private string usage;
        public override string Categorie => "Figurine";

        public Figurines() { }

        public Figurines(int id, string nom, TypeDeBois typeDeBois, float prix, string usage)
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
            return base.ToString() + $" - Figurine (thème: {usage}, prix: {prix})";
        }

        public override bool Equals(object obj)
        {
            return obj is Figurines f && id == f.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
