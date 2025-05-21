using System;
using System.Xml.Serialization;


namespace Projet.Model.Entites
{
    [Serializable]
    [XmlInclude(typeof(Ustensils))]
    [XmlInclude(typeof(Figurines))]
    [XmlInclude(typeof(Meubles))]
    public abstract class Produit
    {
        protected int id;
        protected string nom;
        protected TypeDeBois typeDeBois;

        public virtual string Categorie => "Inconnu";

        public Produit() { }
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
            return $"{nom}; {typeDeBois.NomBois}";
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
