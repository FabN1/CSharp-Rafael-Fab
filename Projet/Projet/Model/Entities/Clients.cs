using System;

namespace Projet.Model.Entites
{
    [Serializable]
    public class Clients
    {
        private int id;
        private string nom;
        private string type;
        private string email;

        public Clients() { }
        public Clients(int id, string nom, string type, string email)
        {
            this.id = id;
            this.nom = nom;
            this.type = type;
            this.email = email;
        }

        // Getters & Setters
        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Nom
        {
            get => nom;
            set => nom = value;
        }

        public string Type
        {
            get => type;
            set => type = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public override string ToString()
        {
            return $"{nom}; {type}; {email}";
        }

        public override bool Equals(object obj)
        {
            return obj is Clients c && id == c.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
