using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    public partial class Parametres : Window
    {

        Key droite;
        Key haut;
        Key gauche;
        Key bas;
        Key tirer;
        Key tricher;
        KeyConverter convertir = new KeyConverter();
        bool difficulteDifficile, fermer = true;
        int mancheFinValeur = 0;


        ImageBrush parametreFond = new ImageBrush();
        
        public Parametres()
        {
            
            parametreFond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/Parametre.png"));

            InitializeComponent();
            tb_manche_fin.Text = ((MainWindow)Application.Current.MainWindow).mancheFin + "";

            fond.Fill = parametreFond;
            droite = ((MainWindow)Application.Current.MainWindow).allerADroite;
            haut = ((MainWindow)Application.Current.MainWindow).avancer;
            gauche = ((MainWindow)Application.Current.MainWindow).allerAGauche;
            bas = ((MainWindow)Application.Current.MainWindow).reculer;
            tirer = ((MainWindow)Application.Current.MainWindow).tirer;
            tricher = ((MainWindow)Application.Current.MainWindow).tricher;
            bouton_droite.Text = convertir.ConvertToString(droite);
            bouton_avancer.Text = convertir.ConvertToString(haut);
            bouton_reculer.Text = convertir.ConvertToString(bas);
            bouton_gauche.Text = convertir.ConvertToString(gauche);
            bouton_tirer.Text = convertir.ConvertToString(tirer);
            bouton_triche.Text = convertir.ConvertToString(tricher);

        }

        private void click_Annuler(object sender, RoutedEventArgs e)
        {
            if (fermer)
                this.Close();
        }

        private void Accepter_Click(object sender, RoutedEventArgs e)
        {
            if (fermer)
            {
                this.Close();

                ((MainWindow)Application.Current.MainWindow).difficile = difficulteDifficile;
                ((MainWindow)Application.Current.MainWindow).mancheFin = mancheFinValeur;
            }

        }



        private void normal_Selected(object sender, RoutedEventArgs e)
        {
            difficulteDifficile = false;
        }

        private void difficile_Selected(object sender, RoutedEventArgs e)
        {
            difficulteDifficile = true;

        }

        private void tb_manche_fin_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_manche_fin.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!fermer)
                e.Cancel = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (fermer)
                this.Close();
        }

        

        private void tb_manche_fin_TextChanged(object sender, TextChangedEventArgs e)
        {
            String texteManche = tb_manche_fin.Text;

            if (int.TryParse(texteManche, out mancheFinValeur))
            {
                if (mancheFinValeur < 0)
                {
                    erreurLabel.Content = "Nombre doit etre positif";
                    erreurLabel.Foreground = Brushes.Red;
                    fermer = false;
                }
                if (mancheFinValeur >= 0)
                {
                    erreurLabel.Foreground = Brushes.Transparent;
                    ((MainWindow)Application.Current.MainWindow).mancheFin = mancheFinValeur;
                    fermer = true;



                }
            }
            else
            {
                erreurLabel.Foreground = Brushes.Red;

                erreurLabel.Content = "Pas un nombre";
                fermer = false;
            }
        }
    }
}
