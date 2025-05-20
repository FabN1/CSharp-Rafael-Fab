using System;

namespace Projet.Model.Entites
{
    [Serializable]
    public class TypeDeBois
    {
        protected int id;
        protected string nomBois;

        public TypeDeBois(int id, string nomBois)
        {
            this.id = id;
            this.nomBois = nomBois;
        }

        public int Id => id;

        public string NomBois
        {
            get => nomBois;
            set => nomBois = value;
        }

        public override string ToString()
        {
            return $"{nomBois}";
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj is not TypeDeBois t) return false;
            return id == t.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
