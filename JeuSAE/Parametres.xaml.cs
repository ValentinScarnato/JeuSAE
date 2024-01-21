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

        /*Key droite = Key.D;
        Key haut = Key.Z;
        Key gauche = Key.Q;
        Key bas = Key.S;*/

        public Parametres()
        {
            InitializeComponent();
            ImageBrush parametreFond = new ImageBrush();
            parametreFond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/Paramtre.png"));
            grid_Parametre.Fill = parametreFond;
           /* droite = ((MainWindow)Application.Current.MainWindow).allerADroite;
            haut = ((MainWindow)Application.Current.MainWindow).avancer;
            gauche = ((MainWindow)Application.Current.MainWindow).allerADroite;
            bas = ((MainWindow)Application.Current.MainWindow).reculer;*/



        }

        private void click_Annuler(object sender, RoutedEventArgs e)
        {
            this.Close();


        }

        private void Accepter_Click(object sender, RoutedEventArgs e)
        {

            this.Close();


        }


       







        private void bouton_droite_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           /* bouton_droite.Text = "";

            droite = e.Key;
            ((MainWindow)Application.Current.MainWindow).allerADroite = droite;
            KeyConverter keyConverter = new KeyConverter();
            bouton_droite.Text = keyConverter.ConvertToString(droite);
            bouton_droite.Focus();*/
        }

        private void bouton_droite_GotFocus(object sender, RoutedEventArgs e)
        {
          // bouton_droite.Text ="" ;

        }



        private void bouton_droite_KeyDown(object sender, KeyEventArgs e)
        {
           /* bouton_droite.Text = "";
            bouton_droite.Focus();*/


        }

        private void bouton_avancer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            /*bouton_gauche.Text = "";

            gauche = e.Key;
            ((MainWindow)Application.Current.MainWindow).allerAGauche = gauche;
            KeyConverter keyConverter = new KeyConverter();
            bouton_droite.Text = keyConverter.ConvertToString(gauche);
            bouton_avancer.Focus();     */
        }

        private void bouton_avancer_GotFocus(object sender, RoutedEventArgs e)
        {
           // bouton_droite.Text = "";


        }

        private void bouton_avancer_KeyDown(object sender, KeyEventArgs e)
        {
            bouton_droite.Text = "";
            bouton_avancer.Focus();


        }






    }
}
