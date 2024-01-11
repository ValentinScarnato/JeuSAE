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

        public static int TEMPS_MAXIMAL_ENTRE_ZOMBIE = 8, TEMPS_MINIMAL_ENTRE_ZOMBIE = 3;
        public static int DEGATS_PAR_ZOMBIE = 10;
        private static int VITESSE_BALLE_JOUEUR = 20;
        bool gauche, droite, haut, bas = false;
        bool FinDePartie = false;
        public static int VITESSE_JOUEUR = 10, VITESSE_ZOMBIE = 6, VIE_JOUEUR = 100;
        int nombreZombiesManche = 4, ennemisTotaux = 0;
        string ORIENTATION_JOUEUR = "haut";
        int MUNITIONS_JOUEUR = 10, KILLS_JOUEUR = 0;
        int BANDEAU = 60;
        int vieJoueur = 100;
        Key avancer = Key.Z;
        Key reculer = Key.S;
        Key allerADroite = Key.D;
        Key allerAGauche = Key.Q;
        private List<Rectangle> objetASupprimer = new List<Rectangle>();


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
        String rour;





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
            //Genere_Zombies(zombar);
            Generation_Zombies(nombreZombiesManche);

            /*----------------------------------------------------*/
            /*-------------------TEMPS----------------------------*/
            /*----------------------------------------------------*/

            DispatcherTimer minuterie = new DispatcherTimer();

            minuterie.Interval = TimeSpan.FromMilliseconds(16);

            minuterie.Tick += Moteur_Jeu;

            minuterie.Start();

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {

                Rectangle balleJoueur = new Rectangle
                {
                    Tag = "Balle joueur",
                    Height = 5,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Yellow
                };
                Canvas.SetTop(balleJoueur, Canvas.GetTop(joueur) - balleJoueur.Height);
                Canvas.SetLeft(balleJoueur, Canvas.GetLeft(joueur) + joueur.Width / 2);
                fond.Children.Add(balleJoueur);
            }
            if (e.Key == allerAGauche)
            {
                gauche = true;
                ORIENTATION_JOUEUR = "gauche";

            }

            if (e.Key == allerADroite)
            {
                droite = true;
                ORIENTATION_JOUEUR = "droite";

            }
            if (e.Key == avancer)
            {
                haut = true;
                ORIENTATION_JOUEUR = "haut";

            }
            if (e.Key == reculer)
            {
                bas = true;
                ORIENTATION_JOUEUR = "bas";

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





        /*public void Genere_Zombies(ImageBrush zombiiee)
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


        }*/
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
        private void Generation_Zombies(int nombreZombiesMax)
        {
            Random aleatoire = new Random();
            for (int i = 0; i < nombreZombiesMax; i++)
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
                ennemisTotaux++;
                if (ennemisTotaux >= 1)
                    nombre_ennemis.Content = ennemisTotaux + " ennemi restants";
                else
                    nombre_ennemis.Content = ennemisTotaux + " ennemis restant";
                /* int tempsEntreZombie = aleatoire.Next(TEMPS_MINIMAL_ENTRE_ZOMBIE, TEMPS_MAXIMAL_ENTRE_ZOMBIE);        Tentative de temps entre zombie (ça veut pas)
                 System.Threading.Thread.Sleep(tempsEntreZombie*1000);*/

            }

        }
        private void Balle()
        {
            foreach (Rectangle x in fond.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "Balle joueur")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - VITESSE_BALLE_JOUEUR);
                    Rect balle = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (Canvas.GetTop(x) < 10)
                    {
                        objetASupprimer.Add(x);
                    }


                    foreach (var y in fond.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemy = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            if (balle.IntersectsWith(enemy))
                            {
                                objetASupprimer.Add(x);
                                objetASupprimer.Add(y);
                                ennemisTotaux -= 1;
                            }
                        }
                    }
                }
            }
        }
        private void Moteur_Jeu(object sender, EventArgs e)
        {
            Deplacements();
            Balle();




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





