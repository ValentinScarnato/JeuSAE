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
using System.Windows.Threading;
using System.Xml.Linq;

namespace JeuSAE
{
    /// <summary>
    /// Logique d'interaction pour jeu.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*----------------------------------------------------*/
        /*--------------------CONSTANTES----------------------*/
        /*----------------------------------------------------*/

        public static int DEGATS_PAR_ZOMBIE = 10;
        private bool gauche, droite, haut, bas = false;
        public static int VITESSE_JOUEUR = 1, VIE = 100;
        public static string position = "droite";
        int munitions = 10, kills = 0;

        /*----------------------------------------------------*/
        /*---------------GENERATION D'IMAGES------------------*/
        /*----------------------------------------------------*/

        ImageBrush iconeMunition = new ImageBrush();
        ImageBrush iconeVie = new ImageBrush();
        ImageBrush joueur_ = new ImageBrush();
        ImageBrush iconeCrane = new ImageBrush();
        ImageBrush zombar = new ImageBrush();


        public MainWindow()
        {
            WindowJeu menu = new WindowJeu();
            menu.ShowDialog();
            InitializeComponent();

            joueur_.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/joueur_shotgun.png"));
            joueur.Fill = joueur_;
            iconeCrane.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/crane.png"));
            icone_crane.Fill = iconeCrane;
            iconeMunition.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/munition.png"));
            icone_munition.Fill = iconeMunition;
            iconeVie.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/coeurs.png"));
            icone_vie.Fill = iconeVie; 
            zombar.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/idle0000.png"));
            Genere_Zombies(zombar);

            /*----------------------------------------------------*/
            /*-------------------TEMPS-------------------*/
            /*----------------------------------------------------*/

                DispatcherTimer minuterie = new DispatcherTimer();

                minuterie.Interval = TimeSpan.FromMilliseconds(16);

                minuterie.Tick += Jeu;

                minuterie.Start();
               /* if (haut == true)
            {

                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + 1);
                    
            }*/
        }
        /*----------------------------------------------------*/
        /*-------------------DEPLACEMENTS 2-------------------*/
        /*----------------------------------------------------*/
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                gauche = true;
            else if (e.Key == Key.Right)
                droite = true;
            else if (e.Key == Key.Up)
                haut = true;
            else if (e.Key == Key.Down)
                bas = true;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                gauche = false;
            else if (e.Key == Key.Right)
                droite = false;
            else if (e.Key == Key.Up)
                haut = false;
            else if (e.Key == Key.Down)
                bas = false;
        }

        

        /*----------------------------------------------------*/
        /*---------------GENERATION DE ZOMBIES----------------*/
        /*----------------------------------------------------*/
        public void Genere_Zombies(ImageBrush zombiiee)
        {
            int nbr, i=0;
            Random n = new Random();
            nbr = n.Next(10, 20);
            if (i < nbr)
            InitializeComponent();
            Random rnd = new Random();
            ZombieCanvas.Fill = zombiiee;
            Rectangle zombie = new Rectangle();
            //zombiiee.ImageSource = 
            //zombiiee.Left = rnd.Next(0, 900);
            //zombiiee.Top = rnd.Next(0, 800);


        }
        private void Jeu(object? sender, EventArgs e)
        {
            // déplacement à gauche et droite de vitessePlayer avec vérification des limites de fenêtre gauche et droite
            if (gauche == true && Canvas.GetLeft(joueur) > 0)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - VITESSE_JOUEUR);
            }
            else if (droite == true && Canvas.GetLeft(joueur) + joueur.Width < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + VITESSE_JOUEUR);
            }
            else if (droite == true && Canvas.GetTop(joueur) > 0)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + VITESSE_JOUEUR);
            }
            else if (droite == true && Canvas.GetTop(joueur) + joueur.Width < Application.Current.MainWindow.Width)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + VITESSE_JOUEUR);
            }


        }





    }
}