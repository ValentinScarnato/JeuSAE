using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JeuSAE
{
    /// <summary>
    /// Logique d'interaction pour FenetreMort.xaml
    /// </summary>
    public partial class FenetreMort : Window
    {
        public FenetreMort()
        {
            InitializeComponent();
        }
        public void RedemarrerApplication()
        {
            // Récupérez le chemin d'exécution de l'application
            string cheminApplication = Process.GetCurrentProcess().MainModule.FileName;

            // Fermez l'instance actuelle de l'application
            Process.Start(cheminApplication);
            Application.Current.Shutdown();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RedemarrerApplication();
            this.DialogResult = true;

        }

        /*WindowJeu windowJeu = new WindowJeu();
            windowJeu.ShowDialog();
            Close();
        */
    }
}
