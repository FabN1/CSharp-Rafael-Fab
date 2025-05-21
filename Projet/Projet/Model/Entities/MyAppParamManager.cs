using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Projet
{
    public class MyAppParamManager
    {
        // Chemin de base dans la registry (sous HKEY_CURRENT_USER)
        private const string BaseRegistryPath = @"Software\MonApp"; // À personnaliser

        // Propriétés représentant les paramètres
        public string NomUtilisateur { get; set; }
        public int TaillePolice { get; set; }
        public bool ModeSombre { get; set; }
        // Charge les paramètres depuis la Registry
        public void LoadRegistryParameter()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(BaseRegistryPath);

            if (key != null)
            {
                NomUtilisateur = key.GetValue("NomUtilisateur") as string ?? "Invité";
                TaillePolice = int.TryParse(key.GetValue("TaillePolice")?.ToString(), out int taille) ? taille : 12;
                ModeSombre = bool.TryParse(key.GetValue("ModeSombre")?.ToString(), out bool sombre) && sombre;
                key.Close();
            }
            else
            {
                // Valeurs par défaut si la clé n'existe pas
                NomUtilisateur = "Invité";
                TaillePolice = 12;
                ModeSombre = false;
            }
        }

        // Sauvegarde les paramètres dans la Registry
        public void SaveRegistryParameter()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(BaseRegistryPath);

            if (key != null)
            {
                key.SetValue("NomUtilisateur", NomUtilisateur);
                key.SetValue("TaillePolice", TaillePolice);
                key.SetValue("ModeSombre", ModeSombre);
                key.Close();
            }
        }
    }
}
