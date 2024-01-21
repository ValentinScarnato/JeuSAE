using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    public partial class Parametres : Window
    {

        Key droite;
        Key haut;
        Key gauche;
        Key bas;
        Key tirer;
        KeyConverter convertir = new KeyConverter();

        public Parametres()
        {
            InitializeComponent();
            ImageBrush parametreFond = new ImageBrush();
            parametreFond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/Paramtre.png"));
            grid_Parametre.Fill = parametreFond;
            droite = ((MainWindow)Application.Current.MainWindow).allerADroite;
            haut = ((MainWindow)Application.Current.MainWindow).avancer;
            gauche = ((MainWindow)Application.Current.MainWindow).allerADroite;
            bas = ((MainWindow)Application.Current.MainWindow).reculer;
            tirer = ((MainWindow)Application.Current.MainWindow).tirer;
            bouton_droite.Text = convertir.ConvertToString(droite);
            bouton_avancer.Text = convertir.ConvertToString(haut);
            bouton_reculer.Text = convertir.ConvertToString(bas);
            bouton_gauche.Text = convertir.ConvertToString(gauche);
            bouton_tirer.Text = convertir.ConvertToString(tirer);


        }

        private void click_Annuler(object sender, RoutedEventArgs e)
        {
            this.Close();


        }

        private void Accepter_Click(object sender, RoutedEventArgs e)
        {

            this.Close();


        }

        

        private void normal_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void difficile_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
