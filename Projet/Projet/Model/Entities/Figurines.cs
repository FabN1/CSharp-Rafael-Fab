using Projet.Model.Entites;
using System;

namespace Projet.Model.Entites
{
    [Serializable]
    public class Figurines : Produit
    {
        private float prix;
        private string theme;
        public override string Categorie => "Figurine";

        public Figurines(int id, string nom, TypeDeBois typeDeBois, float prix, string theme)
            : base(id, nom, typeDeBois)
        {
            this.prix = prix;
            this.theme = theme;
        }

        public float Prix
        {
            get => prix;
            set => prix = value;
        }

        public string Theme
        {
            get => theme;
            set => theme = value;
        }

        public override double CalculerPrix()
        {
            return prix;
        }

        public override string ToString()
        {
            return base.ToString() + $" - Figurine (thème: {theme}, prix: {prix})";
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
