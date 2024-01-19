using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
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
        int mancheValeur;
        bool fermer = true;
        public Pause()
        {
            InitializeComponent();
            fond.Children.Add(erreurLabel);

        }

        Label erreurLabel = new Label
        {
            Width = double.NaN,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Content = "",
            FontSize = 72,
            Foreground = Brushes.Transparent,
            Height = 100

        };
        

        private void selectionner_manche_TextChanged(object sender, TextChangedEventArgs e)
        {
            String texteManche = selectionner_manche.Text;
            if (int.TryParse(texteManche, out mancheValeur))
            {
                if (mancheValeur < 1)
                {
                    erreurLabel.Content = "Nombre doit etre >1";
                    erreurLabel.Foreground = Brushes.Red;
                    fermer = false;
                }
                if (mancheValeur >= 1)
                {
                    fermer = true;
                    erreurLabel.Foreground = Brushes.Transparent;
                    ((MainWindow)Application.Current.MainWindow).manche=mancheValeur;
                }
            }
            else
            {
                erreurLabel.Foreground = Brushes.Red;

                erreurLabel.Content="Pas un nombre";
                fermer = false;

            }
        }


        private void selectionner_manche_GotFocus(object sender, RoutedEventArgs e)
        {
            selectionner_manche.Clear();
        }
        private void bouton_reprendre_Click(object sender, RoutedEventArgs e)
        {
            if (fermer)
            {
                ((MainWindow)Application.Current.MainWindow).mineuteur.Start();
                ((MainWindow)Application.Current.MainWindow).interval.Start();
                ((MainWindow)Application.Current.MainWindow).minuteur2.Start();
                ((MainWindow)Application.Current.MainWindow).TricheManche();

                Close();
            }
        }

        private void bouton_quitter_Click(object sender, RoutedEventArgs e)
        {
            if (fermer)
            {
                RedemarrerApplication();
                this.DialogResult = false;
            }
        }
        public void RedemarrerApplication()
        {

            string cheminApplication = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(cheminApplication);
            Application.Current.Shutdown();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (fermer)
            {
                ((MainWindow)Application.Current.MainWindow).mineuteur.Start();
                ((MainWindow)Application.Current.MainWindow).TricheManche();

                ((MainWindow)Application.Current.MainWindow).interval.Start();
                ((MainWindow)Application.Current.MainWindow).minuteur2.Start();
                Close();
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!fermer)
                e.Cancel = true;
            ((MainWindow)Application.Current.MainWindow).TricheManche();

        }
    }
}
