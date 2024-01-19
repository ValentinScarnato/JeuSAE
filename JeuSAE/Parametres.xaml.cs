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

        int mancheValeur = 1;
        bool fermer = true;
        public Parametres()
        {
            InitializeComponent();
            ImageBrush parametreFond = new ImageBrush();
            parametreFond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/Paramtre.png"));
            grid_Parametre.Fill = parametreFond;
            ((MainWindow)Application.Current.MainWindow).TricheManche();


        }

        private void click_Annuler(object sender, RoutedEventArgs e)
        {
            this.Close();


        }

        private void Accepter_Click(object sender, RoutedEventArgs e)
        {

            this.Close();


        }

        
        /*
public void bouton_droite_TextChanged(object sender, TextChangedEventArgs e)
{

bouton_droite.Text = tournerDroite;
}*/
    }
}
