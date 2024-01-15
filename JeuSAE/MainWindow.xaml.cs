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

        public static int TEMPS_MAXIMAL_ENTRE_ZOMBIE = 8, TEMPS_MINIMAL_ENTRE_ZOMBIE = 3, MUNITIONS_MAX_JOUEUR = 15, NOMBRE_ZOMBIES_MANCHE = 20, VIE_JOUEUR = 100;
        public static int DEGATS_PAR_ZOMBIE = 10;
        public static String ORIENTATION_HAUT = "haut", ORIENTATION_BAS = "bas", ORIENTATION_DROITE = "droite", ORIENTATION_GAUCHE = "gauche";
        private static int VITESSE_BALLE_JOUEUR = 20;
        bool gauche, droite, haut, bas = false;
        bool FinDePartie = false;
        public static int VITESSE_JOUEUR = 10, VITESSE_ZOMBIE = 6;
        int ennemisRestants = NOMBRE_ZOMBIES_MANCHE, nombreEnnemisMap = 0, nombreZombieMaxMemeTemps = 5;

        string orientationJoueur = "droite";
        int killsJoueur = 0;
        int BANDEAU = 60;
        int vieJoueur = VIE_JOUEUR;
        private int nombreDeBalles = 15;
        Key avancer = Key.Z;
        Key reculer = Key.S;
        Key allerADroite = Key.D;
        Key allerAGauche = Key.Q;
        Key tirer = Key.Space;
        Key tournerDroite = Key.E;
        Key tournerGauche = Key.A;
        private List<Rectangle> objetASupprimer = new List<Rectangle>();
        private List<Rectangle> zombies = new List<Rectangle>();
        private List<Rectangle> balles = new List<Rectangle>();


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
        ImageBrush joueurGauche = new ImageBrush();




        public void GenerationImage()
        {
            caisseDecor.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/caisse_fond.png"));
            caisse_decor_1.Fill = caisseDecor;
            caisse_decor_2.Fill = caisseDecor;
            caisse_decor_3.Fill = caisseDecor;
            caisse_decor_4.Fill = caisseDecor;
            boiteMunition.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/boite_munitions.png"));
            boite_munitions.Fill = boiteMunition;
            joueurGauche.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/boite_munitions.png"));

            joueur_.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/joueur_droite.png"));
            joueur.Fill = joueur_;
            iconeCrane.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/crane.png"));
            icone_crane.Fill = iconeCrane;
            iconeMunition.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/munition.png"));
            icone_munition.Fill = iconeMunition;
            iconeVie.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/coeurs.png"));
            icone_vie.Fill = iconeVie;
            zombar.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/idle0000.png"));
        }
        public MainWindow()
        {
            //WindowJeu menu = new WindowJeu();
            //menu.ShowDialog();
            InitializeComponent();
            Temps();
            GenerationImage();
            Generation_Zombies(nombreZombieMaxMemeTemps);




        }
        /*----------------------------------------------------*/
        /*-------------------TEMPS----------------------------*/
        /*----------------------------------------------------*/

        public void Temps()
        {
            DispatcherTimer minuterie = new DispatcherTimer();

            minuterie.Interval = TimeSpan.FromMilliseconds(16);

            minuterie.Tick += Moteur_Jeu;

            minuterie.Start();
        }
        /*----------------------------------------------------*/
        /*-----------------Appui touche-----------------------*/
        /*----------------------------------------------------*/

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space)
            {
                GenerationBalle();
            }

            if (e.Key == allerAGauche)
            {
                gauche = true;


            }

            if (e.Key == allerADroite)
            {
                droite = true;


            }
            if (e.Key == avancer)
            {
                haut = true;


            }
            if (e.Key == reculer)
            {
                bas = true;


            }
            if (e.Key == tournerDroite)
            {
                if (orientationJoueur == ORIENTATION_GAUCHE)
                    orientationJoueur = ORIENTATION_HAUT;
                else if (orientationJoueur == ORIENTATION_HAUT)
                    orientationJoueur = ORIENTATION_DROITE;
                else if (orientationJoueur == ORIENTATION_DROITE)
                    orientationJoueur = ORIENTATION_BAS;
                else
                    orientationJoueur = ORIENTATION_GAUCHE;
            }
            if (e.Key == tournerGauche)
            {
                if (orientationJoueur == ORIENTATION_DROITE)
                    orientationJoueur = ORIENTATION_HAUT;
                else if (orientationJoueur == ORIENTATION_HAUT)
                    orientationJoueur = ORIENTATION_GAUCHE;
                else if (orientationJoueur == ORIENTATION_GAUCHE)
                    orientationJoueur = ORIENTATION_BAS;
                else
                    orientationJoueur = ORIENTATION_DROITE;
            }
        }



        private void Window_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == allerAGauche)
                gauche = false;

            if (e.Key == allerADroite)
                droite = false;

            if (e.Key == avancer)
                haut = false;
            if (e.Key == reculer)
                bas = false;
        }

        private void bouton_pause_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /*----------------------------------------------------*/
        /*--------------- GENERATION DE BALLES ---------------*/
        /*----------------------------------------------------*/
        private void GenerationBalle()
        {
            if (nombreDeBalles > 0)
            {
                Rectangle balleJoueur = new Rectangle
                {
                    Tag = "Balle",
                    Height = 5,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Yellow
                };
                Canvas.SetTop(balleJoueur, Canvas.GetTop(joueur) - balleJoueur.Height);
                Canvas.SetLeft(balleJoueur, Canvas.GetLeft(joueur) + joueur.Width / 2);
                fond.Children.Add(balleJoueur);
                nombreDeBalles--;
            }
        }



        /*----------------------------------------------------*/
        /*-------------------- DEPLACEMENTS ------------------*/
        /*----------------------------------------------------*/
        public void Deplacements()
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
        /*-------------- GENERATION DE ZOMBIES ---------------*/
        /*----------------------------------------------------*/
        private void Generation_Zombies(int nombreZombieMaxMemeTemps)
        {
            Random aleatoire = new Random();
            for (int i = 0; i < nombreZombieMaxMemeTemps; i++)
            {
                Rectangle ennemi = new Rectangle
                {
                    Tag = "Ennemi",
                    Height = 86,
                    Width = 86,
                    Fill = zombar
                };
                int pointApparition = aleatoire.Next(1, 5);
                switch (pointApparition)
                {
                    case 1:
                        Canvas.SetTop(ennemi, 30);
                        Canvas.SetLeft(ennemi, 60);
                        break;
                    case 2:
                        Canvas.SetTop(ennemi, 60);
                        Canvas.SetLeft(ennemi, 300);
                        break;
                    case 3:
                        Canvas.SetTop(ennemi, 500);
                        Canvas.SetLeft(ennemi, 400);
                        break;
                    case 4:
                        Canvas.SetTop(ennemi, 800);
                        Canvas.SetLeft(ennemi, 90);
                        break;
                    case 5:
                        Canvas.SetTop(ennemi, 1600);
                        Canvas.SetLeft(ennemi, 1000);
                        break;
                }

                fond.Children.Add(ennemi);
                nombreEnnemisMap++;

                /* int tempsEntreZombie = aleatoire.Next(TEMPS_MINIMAL_ENTRE_ZOMBIE, TEMPS_MAXIMAL_ENTRE_ZOMBIE);        Tentative de temps entre zombie (ça veut pas)
                 System.Threading.Thread.Sleep(tempsEntreZombie*1000);*/

            }

        }
        private void NombreEnnemis()
        {
            if (ennemisRestants >= 1)
                nombre_ennemis.Content = ennemisRestants + " ennemi restants";
            else
                nombre_ennemis.Content = ennemisRestants + " ennemis restant";
        }
        private void NombreBalles()
        {

            nombre_balles.Content = nombreDeBalles + " | " + MUNITIONS_MAX_JOUEUR;

        }
        private void NombreKills()
        {

            nombre_kill.Content = killsJoueur;

        }
        private void Interactions()
        {
            Rect zoneJoueur = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);

            foreach (var x in fond.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "Balle")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) - VITESSE_BALLE_JOUEUR);

                    if (Canvas.GetTop(x) < 10)
                    {
                        objetASupprimer.Add(x);
                    }
                    Rect balle = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);


                    foreach (var y in fond.Children.OfType<Rectangle>())
                    {

                        if (y is Rectangle && (string)y.Tag == "Ennemi")
                        {
                            Rect ennemiZone = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                            if (balle.IntersectsWith(ennemiZone))
                            {
                                objetASupprimer.Add(x);
                                objetASupprimer.Add(y);
                                nombreEnnemisMap--;
                                ennemisRestants--;
                                killsJoueur += 1;
                            }

                        }

                    }

                }
                if (x is Rectangle && (string)x.Tag == "Ennemi")
                {

                    Rect ennemiZone = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (zoneJoueur.IntersectsWith(ennemiZone) && vieJoueur > 0)
                    {
                        vieJoueur -= 5;
                        System.Threading.Thread.Sleep(80);

                    }
                }

            }




            foreach (Rectangle y in objetASupprimer)
            {
                fond.Children.Remove(y);
            }
        }

        private void Vie()
        {
            BarreDeVie.Value = vieJoueur;
            if (vieJoueur <= 0)
            {
                vieJoueur = 0;
                FenetreMort fenetremort = new FenetreMort();
                fenetremort.ShowDialog();     
                

            }
            
        }
        private void OrientationJoueur()
        {
            if (orientationJoueur == ORIENTATION_GAUCHE)
            {

            }
        }
        private void Moteur_Jeu(object sender, EventArgs e)
        {
            Deplacements();
            NombreEnnemis();
            Interactions();
            NombreBalles();
            NombreKills();
            Vie();
            OrientationJoueur();
            if (nombreEnnemisMap == 0 && killsJoueur != NOMBRE_ZOMBIES_MANCHE)
                Generation_Zombies(nombreZombieMaxMemeTemps);
            orientation.Content = orientationJoueur;

        }




    }
}





