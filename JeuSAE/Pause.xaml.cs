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
    /// Logique d'interaction pour Pause.xaml
    /// </summary>
    public partial class Pause : Window
    {
        MainWindow mainwindow;
        public Pause()
        {
            InitializeComponent();
        }

        private void texte_pause_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void bouton_reprendre_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).mineuteur.Start();
            ((MainWindow)Application.Current.MainWindow).interval.Start();
            ((MainWindow)Application.Current.MainWindow).minuteur2.Start();
            Close();
        }

        private void bouton_quitter_Click(object sender, RoutedEventArgs e)
        {
            RedemarrerApplication();
            this.DialogResult = false;
        }
        public void RedemarrerApplication()
        {
            string cheminApplication = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(cheminApplication);
            Application.Current.Shutdown();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).mineuteur.Start();
            ((MainWindow)Application.Current.MainWindow).interval.Start();
            ((MainWindow)Application.Current.MainWindow).minuteur2.Start();

        }
    }
}
