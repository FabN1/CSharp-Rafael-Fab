using System.Windows;

namespace Projet
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            LoginWindow loginWindow = new LoginWindow();
            bool? result = loginWindow.ShowDialog();
            // Vérification du résultat du login


            if (loginWindow.etatConnexion == true)
            {
                MessageBox.Show("Connexion réussie !");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Closed += (s, e) => Application.Current.Shutdown();
                mainWindow.Show();
            }
            else
            {
                // Fermeture si login échoué ou annulé
                Shutdown();
            }
        }
    }
}
