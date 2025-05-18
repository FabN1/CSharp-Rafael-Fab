using System;

namespace Projet.Model.Entites
{
    [Serializable]
    public abstract class Produit
    {
        protected int id;
        protected string nom;
        protected TypeDeBois typeDeBois;

        protected Produit(int id, string nom, TypeDeBois typeDeBois)
        {
            this.id = id;
            this.nom = nom;
            this.typeDeBois = typeDeBois;
        }

        public int Id => id;
        public string Nom
        {
            get => nom;
            set => nom = value;
        }

        public TypeDeBois TypeDeBois
        {
            get => typeDeBois;
            set => typeDeBois = value;
        }

        public abstract double CalculerPrix();

        public override string ToString()
        {
            return $"Produit: {nom}, Bois: {typeDeBois.NomBois}";
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj is not Produit p) return false;
            return id == p.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
