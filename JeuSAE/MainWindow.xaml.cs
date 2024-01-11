using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
using System.Windows.Navigation;
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
        bool gauche, droite, haut, bas = false;
        bool FinDePartie = false;
        public static int VITESSE_JOUEUR = 10, VIE_JOUEUR = 100;

        string ORIENTATION_JOUEUR = "haut";
        int MUNITIONS_JOUEUR = 10, KILLS_JOUEUR = 0;
        int BANDEAU = 60;
        
        /*----------------------------------------------------*/
        /*---------------GENERATION D'IMAGES------------------*/
        /*----------------------------------------------------*/

        ImageBrush iconeMunition = new ImageBrush();
        ImageBrush iconeVie = new ImageBrush();
        ImageBrush joueur_ = new ImageBrush();
        ImageBrush iconeCrane = new ImageBrush();
        ImageBrush zombar = new ImageBrush();
        ImageBrush boiteMunition = new ImageBrush();
        ImageBrush caisseDecor = new ImageBrush();
        ImageBrush boiteArme = new ImageBrush();





        public MainWindow()
        {
            WindowJeu menu = new WindowJeu();
            menu.ShowDialog();
            InitializeComponent();
            caisseDecor.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/caisse_fond.png"));
            caisse_decor_1.Fill = caisseDecor;
            caisse_decor_2.Fill = caisseDecor;
            caisse_decor_3.Fill = caisseDecor;
            caisse_decor_4.Fill = caisseDecor;
            boiteMunition.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/boite_munitions.png"));
            boite_munitions.Fill = boiteMunition;
            boiteArme.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/boite_arme.png"));
            boite_arme.Fill = boiteArme;

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
            /*-------------------TEMPS----------------------------*/
            /*----------------------------------------------------*/

            DispatcherTimer minuterie = new DispatcherTimer();

            minuterie.Interval = TimeSpan.FromMilliseconds(16);

            minuterie.Tick += Jeu;

            minuterie.Start();
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            double xJoueur = Canvas.GetLeft(joueur);
            double yJoueur = Canvas.GetTop(joueur);

            if (e.Key == Key.Q)
            {
                gauche = true;
                ORIENTATION_JOUEUR = "gauche";

            }
            
            if (e.Key == Key.D)
            {
                droite = true;
                ORIENTATION_JOUEUR = "droite";

            }
            if (e.Key == Key.Z)
            {
                haut = true;
                ORIENTATION_JOUEUR = "haut";

            }
            if (e.Key == Key.S)
            {
                bas = true;
                ORIENTATION_JOUEUR = "bas";

            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            double xJoueur = Canvas.GetLeft(joueur);
            double yJoueur = Canvas.GetTop(joueur);

            if (e.Key == Key.Q)
                gauche = false;

            if (e.Key == Key.D)
                droite = false;


            if (e.Key == Key.Z)
                haut = false;


            if (e.Key == Key.S)
                bas = false;


        }




        /*----------------------------------------------------*/
        /*---------------GENERATION DE ZOMBIES----------------*/
        /*----------------------------------------------------*/
        public void Genere_Zombies(ImageBrush zombiiee)
        {
            int nbr, i = 0;
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
        private void Jeu(object sender, EventArgs e)
        {
            Rect _joueur = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);

            
            
            if (gauche == true && Canvas.GetLeft(joueur) > 0)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - VITESSE_JOUEUR);
            }
           
            else if (droite == true && Canvas.GetLeft(joueur) + joueur.Width < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + VITESSE_JOUEUR);
            }
            else if (haut == true && Canvas.GetTop(joueur) > BANDEAU)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - VITESSE_JOUEUR);
            }
            else if (bas == true && Canvas.GetTop(joueur) + joueur.Width < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + VITESSE_JOUEUR);
            }
            
            
            



        }

        /*----------------------------------------------------*/
        /*---------------GESTION DU TIR ----------------------*/
        /*----------------------------------------------------*/
        /*private void TirJoueur(string orientation)
        {
            if (e.Key == Key.Space)
            {

                //vide la liste des items
                //itemsToRemove.Clear();
                Rectangle newBullet = new Rectangle
                {
                    Tag = "balleJoueur",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                };
                // on place le tir à l’endroit du joueur
                Canvas.SetTop(newBullet, Canvas.GetTop(joueur) - newBullet.Height);
                Canvas.SetLeft(newBullet, Canvas.GetLeft(joueur) + joueur.Width / 2);
                // on place le tir dans le canvas
                fond.Children.Add(newBullet);
            }*/
        
        }


    }



}
