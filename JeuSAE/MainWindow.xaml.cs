using System;
using System.Diagnostics;
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
using System.Threading;
using System.Media;

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
        public static readonly int MUNITIONS_MAX_DEBUT = 15, VIE_JOUEUR = 100, NOMBRE_ZOMBIES = 20, ZOMBIE_MEME_TEMPS = 5;
        public static readonly int DEGATS_PAR_ZOMBIE = 10;
        private static readonly int VITESSE_BALLE = 20, VITESSE_BALLE_TRICHE = 30;
        private static readonly int BANDEAU = 60;
        /*----------------------------------------------------*/
        /*----------------------DOUBLE------------------------*/
        /*----------------------------------------------------*/
        public static int VITESSE_JOUEUR = 8, VITESSE_JOUEUR_TRICHE = 15, VITESSE_ZOMBIE = 3;
        /*----------------------------------------------------*/
        /*--------------------TIMESPAN------------------------*/
        /*----------------------------------------------------*/

        private TimeSpan minuterie;

        /*----------------------------------------------------*/
        /*----------------------STRING------------------------*/
        /*----------------------------------------------------*/

        public static readonly String ORIENTATION_HAUT = "haut", ORIENTATION_BAS = "bas", ORIENTATION_DROITE = "droite", ORIENTATION_GAUCHE = "gauche";
        string orientationJoueur = "droite";
        String orientationZombieX = "", orientationZombieY = "";

        /*----------------------------------------------------*/
        /*-----------------------KEY--------------------------*/
        /*----------------------------------------------------*/

        public Key avancer = Key.Z;
        public Key reculer = Key.S;
        public Key allerADroite = Key.D;
        public Key allerAGauche = Key.Q;
        public Key tirer = Key.Space;
        public Key tournerDroite = Key.E;
        public Key tournerGauche = Key.A;
        public Key tricher = Key.K;

        /*----------------------------------------------------*/
        /*-----------------------INT--------------------------*/
        /*----------------------------------------------------*/


        int nombreDeBalles = MUNITIONS_MAX_DEBUT;
        int nombreZombieManche = 0, nombreEnnemisMap, ennemisRestants, killManche;
        int vitesseJoueur = VITESSE_JOUEUR, vitesseBalle = VITESSE_BALLE, munitionMaxJoueur = MUNITIONS_MAX_DEBUT;
        int nombreZombieMaxMemeTemps = 5, nombreMunitionMaxMemeTemps = 1;
        public int killsJoueur = 0;
        int vieJoueur = VIE_JOUEUR;
        public int manche = 1;
        public int chanceBalle , mancheFin =0;

        /*----------------------------------------------------*/
        /*-----------------------BOOLEEN----------------------*/
        /*----------------------------------------------------*/
        public bool difficile = false;
        bool gauche, droite, haut, bas = false;
        bool vieInfinie = false, ballesInfinies = false;
        bool triche = false;
        bool perdu = false;

        /*----------------------------------------------------*/
        /*-----------------------LISTES-----------------------*/
        /*----------------------------------------------------*/

        private List<Rectangle> objetASupprimer = new List<Rectangle>();
        private List<Rectangle> zombieListe = new List<Rectangle>();
        private List<Rectangle> munitionListe = new List<Rectangle>();
        private List<Rectangle> boiteListe = new List<Rectangle>();
        private List<Rectangle> soinListe = new List<Rectangle>();
        private List<Rectangle> balles = new List<Rectangle>();
        private List<Rectangle> balleG = new List<Rectangle>();
        private List<Rectangle> balleD = new List<Rectangle>();
        private List<Rectangle> balleH = new List<Rectangle>();
        private List<Rectangle> balleB = new List<Rectangle>();
        private BitmapImage[] imagesZombie = new BitmapImage[16];
        private int indexImageZombie = 0;
        private BitmapImage[] imagesFeu = new BitmapImage[19];
        private int indexImageFeu = 0;



        /*----------------------------------------------------*/
        /*--------------------IMAGEBRUSH----------------------*/
        /*----------------------------------------------------*/

        ImageBrush iconeMunition = new ImageBrush();
        ImageBrush iconeVie = new ImageBrush();
        ImageBrush joueur_ = new ImageBrush();
        ImageBrush iconeCrane = new ImageBrush();
        ImageBrush zombar = new ImageBrush();
        ImageBrush boiteMunition = new ImageBrush();
        ImageBrush caisseDecor = new ImageBrush();
        ImageBrush soin = new ImageBrush();
        ImageBrush pause = new ImageBrush();
        ImageBrush map = new ImageBrush();
        ImageBrush Balle = new ImageBrush();

        /*----------------------------------------------------*/
        /*-------------------------SON------------------------*/
        /*----------------------------------------------------*/

        MediaPlayer sonsZombie = new MediaPlayer();
        MediaPlayer sonsBalle = new MediaPlayer();
        MediaPlayer sonsTouche = new MediaPlayer();
        MediaPlayer sonVie = new MediaPlayer();
        MediaPlayer f = new MediaPlayer();


        /*----------------------------------------------------*/
        /*--------------------DISPATCHERTIMER-----------------*/
        /*----------------------------------------------------*/

        public DispatcherTimer interval = new DispatcherTimer();
        public DispatcherTimer mineuteur = new DispatcherTimer();
        public DispatcherTimer minuteur2 = new DispatcherTimer();

        /*----------------------------------------------------*/
        /*--------------- GENERATION IMAGE  ------------------*/
        /*----------------------------------------------------*/

        public void GenerationImage()
        {
            String chemin = AppDomain.CurrentDomain.BaseDirectory + "Image/caisse_fond.png";
            caisseDecor.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/caisse_fond.png"));
            boiteMunition.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/boite_munitions.png"));
            joueur_.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/joueur.png"));
            joueur.Fill = joueur_;
            iconeCrane.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/crane.png"));
            icone_crane.Fill = iconeCrane;
            iconeMunition.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/munition.png"));
            icone_munition.Fill = iconeMunition;
            iconeVie.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/coeurs.png"));
            icone_vie.Fill = iconeVie;
            zombar.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/idle0000.png"));
            soin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/Healthcare.png"));
            pause.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/pause.png"));
            bouton_pause.Background = pause;
            map.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/Map..png"));
            fond.Background = map;
            Balle.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/balle.png"));

        }
        private void ChargerImagesZombie()
        {
            for (int i = 0; i < imagesZombie.Length; i++)
            {
                string nomFichier = $"skeleton-move_{i + 1}.png";
                string cheminImage = $"pack://siteoforigin:,,,/Image/{nomFichier}";
                imagesZombie[i] = new BitmapImage(new Uri(cheminImage));
            }
        }

        /*----------------------------------------------------*/
        /*----------------- MAIN WINDOW ----------------------*/
        /*----------------------------------------------------*/

        public MainWindow()
        {

            WindowJeu menu = new WindowJeu();
            menu.ShowDialog();
            InitializeComponent();
            GenerationImage();
            Generation_Boite();
            Generation_Zombies(nombreZombieMaxMemeTemps);
            ChargerImagesFeu();
            InitialiserAnimationFeu(Flamme);
            if (!difficile)
                GenerationKitSoin();
            Generation_Munitions(nombreMunitionMaxMemeTemps);
            nombreZombieManche = NOMBRE_ZOMBIES + 2 * (manche - 1);
            nombreZombieMaxMemeTemps = ZOMBIE_MEME_TEMPS + 1 * (manche - 1);

            /*----------------------------------------------------*/
            /*---------------------TEMPS--------------------------*/
            /*----------------------------------------------------*/

            interval.Interval = TimeSpan.FromSeconds(15);
            interval.Tick += GenerMunitionsConditions;

            minuteur2.Interval = TimeSpan.FromSeconds(30);
            minuteur2.Tick += GenerKitSoinConditions;

            mineuteur.Interval = TimeSpan.FromMilliseconds(16);
            mineuteur.Tick += Moteur_Jeu;
            mineuteur.Start();
        }

        /*----------------------------------------------------*/
        /*-------------- Moteur du jeu -----------------------*/
        /*----------------------------------------------------*/
        private void Moteur_Jeu(object sender, EventArgs e)
        {
            GenerZombieConditions();
            Manche();
            Deplacements();
            NombreEnnemis();
            Interactions();
            NombreBalles();
            NombreKills();
            Vie();
            OrientationJoueur();
            TempsDeJeu();
            ChargerImagesZombie();
            FinManche();


        }



        private void InitialiserAnimationZombie(Rectangle ennemi)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += (sender, e) => AnimerZombie(ennemi);
            timer.Start();
        }
        private void InitialiserAnimationFeu(Rectangle Flamme)
        {
            DispatcherTimer time = new DispatcherTimer();
            time.Interval = TimeSpan.FromMilliseconds(100);
            time.Tick += (sender, e) => AnimerFeu(Flamme);
            time.Start();
        }
        private void ChargerImagesFeu()
        {
            for (int i = 0; i < imagesFeu.Length; i++)
            {
                string nomFichier = $"Fire+Sparks{i + 1}.png";
                string cheminImage = $"pack://siteoforigin:,,,/Image/{nomFichier}";
                imagesFeu[i] = new BitmapImage(new Uri(cheminImage));
            }
        }

        private void AnimerFeu(Rectangle Flamme)
        {
            if (imagesFeu.Length > 0)
            {
                BitmapImage image = imagesFeu[indexImageFeu];
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = image;
                Flamme.Fill = brush;
                indexImageFeu = (indexImageFeu + 1) % imagesFeu.Length;
            }
        }

        /*----------------------------------------------------*/
        /*------------------ TEMPS DE JEU TXT ----------------*/
        /*----------------------------------------------------*/

        private void TempsDeJeu()
        {
            minuterie = minuterie.Add(TimeSpan.FromMilliseconds(1500));
            texteMinuterie.Text = minuterie.ToString(@"hh\:mm");
        }

        /*----------------------------------------------------*/
        /*-------------------- DEPLACEMENTS ------------------*/
        /*----------------------------------------------------*/

        public void Deplacements()
        {
            if (!triche)
            {

                vitesseJoueur = VITESSE_JOUEUR;
            }
            else
            {
                vitesseJoueur = VITESSE_JOUEUR_TRICHE;
            }
            if (gauche == true && Canvas.GetLeft(joueur) > 0)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - vitesseJoueur);
            }

            else if (droite == true && Canvas.GetLeft(joueur) + joueur.Width < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + vitesseJoueur);
            }
            else if (haut == true && Canvas.GetTop(joueur) > BANDEAU + 20)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - vitesseJoueur);
            }
            else if (bas == true && Canvas.GetTop(joueur) + joueur.Width < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + vitesseJoueur);
            }

        }

        public void DeplacementZombie(Rectangle zomb)
        {
            if (Canvas.GetLeft(zomb) > Canvas.GetLeft(joueur)) // si le zombie se trouve a droite du joueur
            {
                Canvas.SetLeft(zomb, Canvas.GetLeft(zomb) - VITESSE_ZOMBIE); //faire aller le zombie vers la gauche
                orientationZombieX = ORIENTATION_GAUCHE; // définir orientation horizontale vers la gauche
                
            }
            if (Canvas.GetLeft(zomb) < Canvas.GetLeft(joueur)) // si le zombie se trouve a la gauche du joueur
            {
                Canvas.SetLeft(zomb, Canvas.GetLeft(zomb) + VITESSE_ZOMBIE); //  faire aller le zombie vers la droite
                orientationZombieX = ORIENTATION_DROITE; // définir orientation horizontale vers la droie
                
            }
            if (Canvas.GetTop(zomb) < Canvas.GetTop(joueur)) // si le zombie est en dessous du joueur
            {
                Canvas.SetTop(zomb, Canvas.GetTop(zomb) + VITESSE_ZOMBIE); // faire aller le zombie vers le bas
                orientationZombieY = ORIENTATION_HAUT; // définir l'orientation verticale du joueur vers le haut
                
            }
            if (Canvas.GetTop(zomb) > Canvas.GetTop(joueur)) // si le zombie est au dessus du joueur
            {
                Canvas.SetTop(zomb, Canvas.GetTop(zomb) - VITESSE_ZOMBIE); //faire aller le zombie vers le bas
                orientationZombieY = ORIENTATION_BAS; // définir orientation du zombie vers le bas
                
            }
            OrientationZombie(zomb, orientationZombieX, orientationZombieY);

        }

        /*----------------------------------------------------*/
        /*--------------- GENERATION DE BALLES ---------------*/
        /*----------------------------------------------------*/

        private void GenerationBalle()
        {
            
            if (nombreDeBalles > 0)
            {
                sonsBalle.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Sons/balle.mp3"));
                sonsBalle.Play();
                Rectangle balle = new Rectangle()
                
                {
                    Tag = "Balle",
                    Height = 5,
                    Width = 5,
                    Fill = Balle,
                    Stroke = Brushes.Yellow
                };

                if (orientationJoueur == ORIENTATION_GAUCHE)
                {
                    balleG.Add(balle);
                    Canvas.SetTop(balle, Canvas.GetTop(joueur) + joueur.Height / 4.1);
                    Canvas.SetLeft(balle, Canvas.GetLeft(joueur) - balle.Width);
                }
                else if (orientationJoueur == ORIENTATION_DROITE)
                {
                    balleD.Add(balle);
                    Canvas.SetTop(balle, Canvas.GetTop(joueur) + joueur.Height / 1.4);
                    Canvas.SetLeft(balle, Canvas.GetLeft(joueur) + joueur.Width);
                }
                else if (orientationJoueur == ORIENTATION_HAUT)
                {
                    balleH.Add(balle);
                    Canvas.SetTop(balle, Canvas.GetTop(joueur) - 30);
                    Canvas.SetLeft(balle, Canvas.GetLeft(joueur) + joueur.Width / 1.59);
                }
                else
                {
                    balleB.Add(balle);
                    Canvas.SetTop(balle, Canvas.GetTop(joueur) + joueur.Height + 30);
                    Canvas.SetLeft(balle, Canvas.GetLeft(joueur) + joueur.Width / 3);
                }
                fond.Children.Add(balle);
                balles.Add(balle);

                if (!ballesInfinies)
                    nombreDeBalles--;


            }

        }

        /*----------------------------------------------------*/
        /*-------------- GENERATION DE ZOMBIES ---------------*/
        /*----------------------------------------------------*/

        private void Generation_Zombies(int zombiesGeneration)
        {
            int i = 0;
            if (nombreZombieManche - killManche < zombiesGeneration)
                zombiesGeneration = nombreZombieManche - killManche;
            while (i < zombiesGeneration)
            {
                Rectangle ennemi = new Rectangle
                {
                    Tag = "Ennemi",
                    Height = 100,
                    Width = 100,
                };


                fond.Children.Add(ennemi);
                InitialiserAnimationZombie(ennemi);

                Random aleatoire = new Random();
                Random position = new Random();

                int coteApparition = aleatoire.Next(1, 4);

                switch (coteApparition)
                {
                    case 1:
                        Canvas.SetTop(ennemi, position.Next((int)ennemi.Height + BANDEAU, (int)Application.Current.MainWindow.Height));
                        Canvas.SetLeft(ennemi, (-ennemi.Width));
                        break;
                    case 2:
                        Canvas.SetTop(ennemi, (int)Application.Current.MainWindow.Height);
                        Canvas.SetLeft(ennemi, (position.Next((int)ennemi.Height, (int)Application.Current.MainWindow.Width)));
                        break;
                    case 3:
                        Canvas.SetTop(ennemi, position.Next((int)ennemi.Height + BANDEAU, (int)Application.Current.MainWindow.Height));
                        Canvas.SetLeft(ennemi, ennemi.Width + (int)Application.Current.MainWindow.Width);
                        break;
                }

                zombieListe.Add(ennemi);
                nombreEnnemisMap++;
                i++;
            }
        }

        private void GenerZombieConditions()
        {


            if (nombreEnnemisMap == 0 && killManche != nombreZombieManche)
            {
                sonsZombie.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Sons/zombie.mp3"));
                sonsZombie.Play();
                Generation_Zombies(nombreZombieMaxMemeTemps);
            }



        }

        private void AnimerZombie(Rectangle ennemi)
        {
            if (imagesZombie.Length > 0)
            {
                BitmapImage image = imagesZombie[indexImageZombie];
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = image;
                ennemi.Fill = brush;
                indexImageZombie = (indexImageZombie + 1) % imagesZombie.Length;
            }
        }


        /*----------------------------------------------------*/
        /*----------------GENERATION DE BOITE-----------------*/
        /*----------------------------------------------------*/

        private void Generation_Boite()
        {

                Rectangle caisse1 = new Rectangle
                {
                    Tag = "caisse",
                    Height = 70,
                    Width = 70,
                    Fill = caisseDecor
                };

                Canvas.SetTop(caisse1, 680);
                Canvas.SetLeft(caisse1, 490);

                boiteListe.Add(caisse1);
                fond.Children.Add(caisse1);

            Rectangle caisse2 = new Rectangle
            {
                Tag = "caisse",
                Height = 70,
                Width = 70,
                Fill = caisseDecor
            };

            Canvas.SetTop(caisse2, 700);
            Canvas.SetLeft(caisse2, 1200);

            boiteListe.Add(caisse2);
            fond.Children.Add(caisse2);

            Rectangle caisse3 = new Rectangle
            {
                Tag = "caisse",
                Height = 70,
                Width = 70,
                Fill = caisseDecor
            };

            Canvas.SetTop(caisse3, 210);
            Canvas.SetLeft(caisse3, 210);

            boiteListe.Add(caisse3);
            fond.Children.Add(caisse3);
        }

        /*----------------------------------------------------*/
        /*--------------GENERATION DE MUNITIONS---------------*/
        /*----------------------------------------------------*/

        private void Generation_Munitions(int nombreMunitionMaxMemeTemps)
        {

            Random aleatoire = new Random();

            for (int i = 0; i < nombreMunitionMaxMemeTemps; i++)
            {
                Rectangle boiteMun = new Rectangle
                {
                    Tag = "boite_munitions",
                    Height = 45,
                    Width = 52,
                    Fill = boiteMunition
                };
                int pointApparition = aleatoire.Next(1, 1);

                Canvas.SetTop(boiteMun, aleatoire.Next(80, 900));
                Canvas.SetLeft(boiteMun, aleatoire.Next(20, 1730));

                munitionListe.Add(boiteMun);
                fond.Children.Add(boiteMun);

            }



        }

        private void GenerMunitionsConditions(object sender, EventArgs e)
        {
            Generation_Munitions(nombreMunitionMaxMemeTemps);
            interval.Stop();

        }

        /*----------------------------------------------------*/
        /*-----------------GENERATION DE SOIN-----------------*/
        /*----------------------------------------------------*/

        private void GenerationKitSoin()
        {
            Random aleatoire = new Random();


            Rectangle kitSoin = new Rectangle
            {
                Tag = "Kit_Soin",
                Height = 45,
                Width = 52,
                Fill = soin
            };
            int pointApparition = aleatoire.Next(1, 1);
            Canvas.SetTop(kitSoin, aleatoire.Next((int)kitSoin.Height + BANDEAU, 900));
            Canvas.SetLeft(kitSoin, aleatoire.Next(20, 1730));
            soinListe.Add(kitSoin);
            fond.Children.Add(kitSoin);


        }

        private void GenerKitSoinConditions(object sender, EventArgs e)
        {
            // Appeler cette méthode à chaque tick du timer (toutes les 15 secondes)
            if (!difficile)
            {
                GenerationKitSoin();
                minuteur2.Stop();
            }

        }

        /*----------------------------------------------------*/
        /*-------AFFICHAGE TEXT(KILL, ENNEMIS, BALLES,)-------*/
        /*----------------------------------------------------*/

        private void NombreEnnemis()
        {
            ennemisRestants = nombreZombieManche - killManche;
            if (ennemisRestants > 1)
                nombre_ennemis.Content = ennemisRestants + " ennemis restants";
            else
                nombre_ennemis.Content = ennemisRestants + " ennemi restant";

        }

        private void NombreBalles()
        {
            if (!triche)
            {
                nombre_balles.Content = nombreDeBalles + " | " + munitionMaxJoueur;
            }
            else
            {
                nombre_balles.Content = "∞" + " | " + "∞";
            }
        }

        public void NombreKills()
        {

            nombre_kill.Content = killsJoueur;

        }

        /*----------------------------------------------------*/
        /*-----------------GESTION DES MANCHES----------------*/
        /*----------------------------------------------------*/

        public void Manche()
        {
            label_manche.Content = "Manche " + manche;
        }

        public void TricheManche()
        {
            foreach (Rectangle x in zombieListe)
            {
                objetASupprimer.Add(x);

            }
            ennemisRestants = 0;
            nombreEnnemisMap = 0;
            killManche = 0;
            nombreZombieManche = NOMBRE_ZOMBIES + 5 * (manche - 1);
            nombreZombieMaxMemeTemps = ZOMBIE_MEME_TEMPS + 2 * (manche - 1);
            if (!difficile)
                munitionMaxJoueur = MUNITIONS_MAX_DEBUT + (1 * manche) - 1;
        }

        private void FinManche()
        {
            if (killManche == nombreZombieManche)
            {
                ennemisRestants = 0;
                killManche = 0;
                nombreZombieManche = NOMBRE_ZOMBIES + 5 * manche;
                nombreZombieMaxMemeTemps = ZOMBIE_MEME_TEMPS + 2 * manche;
                if (!difficile)
                {

                    munitionMaxJoueur++;
                    nombreDeBalles = munitionMaxJoueur;
                    vieJoueur += 5;
                }
                if (manche == mancheFin && mancheFin != 0)
                {
                    Gagner();
                }
                manche++;

            }


        }

        private void Gagner()
        {
            mineuteur.Stop(); // quand le bouton pause est cliqué, le minuteur et l'interval se stoppent
            interval.Stop();
            Victoire victoire = new Victoire(); // nouvelle fenetre pause
            victoire.ShowDialog(); // ouverture fenetre pause
        }

        /*----------------------------------------------------*/
        /*-----------------GESTION DES TOUCHES----------------*/
        /*----------------------------------------------------*/

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // si la touche pour tirer est pressée, la méthode pour générer des balles est appellée
            if (e.Key == tirer)
            {
                GenerationBalle();
            }
            //Code de triche
            if (e.Key == tricher)
            {
                nombreDeBalles = munitionMaxJoueur;
                vieInfinie = !vieInfinie;
                Vie();
                ballesInfinies = !ballesInfinies;

                triche = !triche;
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

        /*----------------------------------------------------*/
        /*---------------INTERACTION BALLES-------------------*/
        /*----------------------------------------------------*/

        private bool InteractionBalles(Rectangle balle)// cette méthode sert a tester si une balle sort du canvas ou touche un zombie et retourne un booléen
        {


            Rect balleR = new Rect(Canvas.GetLeft(balle), Canvas.GetTop(balle), balle.Width, balle.Height); // convertir le Rectangle de la balle
            if (Canvas.GetTop(balle) < BANDEAU)
            {
                objetASupprimer.Add(balle); // si la balle dépasse le bandeau en allant vers le haut, elle sera supprimée
                return true;
            }
            if (Canvas.GetTop(balle) > fond.Height)
            {
                objetASupprimer.Add(balle); // si la balle dépasse la hauteur du canvas, elle sera supprimée
                return true;
            }
            if (Canvas.GetLeft(balle) < 0)
            {
                objetASupprimer.Add(balle);// si la balle dépasse du coté gauche du canvas, elle sera supprimée
                return true;
            }
            if (Canvas.GetLeft(balle) > fond.Width)
            {
                objetASupprimer.Add(balle); // si la balle dépasse la largeur du canvas, elle sera supprimée
                return true;
            }
            foreach (Rectangle zomb in zombieListe) //boucler pour chaque rectangle dans la liste des zombies
            {
                Rect ennemiZone = new Rect(Canvas.GetLeft(zomb), Canvas.GetTop(zomb), zomb.Width, zomb.Height); //convertir le rectangle zomb en Rect
                if (balleR.IntersectsWith(ennemiZone)) // tester si il y a une collision entre une balle et un ennemi
                {
                    sonsTouche.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Sons/touche_balle.mp3"));
                    sonsTouche.Play();
                    objetASupprimer.Add(balle); // ajout de la balle aux objets a supprimer
                    objetASupprimer.Add(zomb); // ajout du zombie aux objets a supprimer
                    nombreEnnemisMap--; // il y a un ennemi sur la carte en moins
                    ennemisRestants--; // il y a un ennemi restant en moins
                    killsJoueur += 1; // le joueur a tué un zombie donc il a un kill en plus
                    killManche += 1; // le joueur a tué un zombie dans la manche donc il a un kill en plus pour la manche
                    Random aleatoire = new Random();
                    if (!difficile)
                        chanceBalle = 5;// une chance sur 5 de récupérer une balle si difficulté normale
                    else
                        chanceBalle = 7;// une chance sur 7 de récupérer une balle si difficulté difficile
                    if (aleatoire.Next(1, chanceBalle) == 1)
                    {
                        nombreDeBalles++; // augmentation du nombre de balles
                    }
                    return true;
                }
                

            
            }foreach (Rectangle caisse in boiteListe)
                   {  
                    Rect caisseZone = new Rect(Canvas.GetLeft(caisse), Canvas.GetTop(caisse), caisse.Width, caisse.Height); //convertir le rectangle zomb en Rect

               
                    if (caisseZone.IntersectsWith(balleR))
                        {
                            objetASupprimer.Add(balle);
                        }
                    }
            return false;

        }

        /*----------------------------------------------------*/
        /*---------------GESTION D'INTERACTION----------------*/
        /*----------------------------------------------------*/

        private void Interactions()
        {
            Rect zoneJoueur = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height); // convertir le Rectangle joueur en Rect

            if (!triche) // si la triche est désactivée la vitesse de balles sera égale a la constante VITESSE_BALLE
                vitesseBalle = VITESSE_BALLE;
            else // si la triche est activée la vitesse de balles sera égale a la constante VITESSE_BALLE_TRICHE
                vitesseBalle = VITESSE_BALLE_TRICHE;
            foreach (Rectangle x in balleB) // boucler pour chaque rectangle présent dans la liste balleB (balles qui partent vers le bas)
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + vitesseBalle); // faire aller la balle vers le bas 
                if (InteractionBalles(x)) // appel de la fonction Interaction balles en envoyant la balle qui part vers le bas 
                    objetASupprimer.Add(x);
            }
            foreach (Rectangle x in balleD) //boucler pour chaque Rectangle présent dans la liste balleD (balle qui partent vers la droite)
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + vitesseBalle); // faire aller la balle vers la droite
                if (InteractionBalles(x))
                    objetASupprimer.Add(x); // appel de la méthode InteractionBalles en envoyant la balle qui part vers la droite

            }
            foreach (Rectangle x in balleG) // boucler pour chaque rectangle dans la liste balleG (balles qui partent vers la gauche)
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - vitesseBalle); // faire partir la balle vers la gauche
                if (InteractionBalles(x))// appel de la fonction Interaction balles en envoyant la balle qui part vers la gauche
                    objetASupprimer.Add(x);

            }
            foreach (Rectangle x in balleH) // boucler pour chaque rectangle dans la liste balleH (balles qui partent vers le haut)
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - vitesseBalle); // faire partir la balle vers le haut
                if (InteractionBalles(x))// appel de la fonction Interaction balles en envoyant la balle qui part vers le haut
                    objetASupprimer.Add(x);


            }
            foreach (Rectangle muni in munitionListe) //boucler pour chaque rectancle dans la liste des boites de munitions
            {
                Rect boiteMunitionsZone = new Rect(Canvas.GetLeft(muni), Canvas.GetTop(muni), muni.Width, muni.Height); // convertir la boite de munitions en Rect
                if (zoneJoueur.IntersectsWith(boiteMunitionsZone) && nombreDeBalles < munitionMaxJoueur) // test de la collision entre le joueur et la boite de munition et si le nombre de balles est inférieur au nombre maximal de balles
                {
                    if (!difficile)
                        nombreDeBalles = munitionMaxJoueur; // si la difficulté n'est pas en difficile mettre le nombre maximal de balles
                    else
                    {
                        if (nombreDeBalles + munitionMaxJoueur / 2 < munitionMaxJoueur)
                            nombreDeBalles += munitionMaxJoueur / 2; // si la difficulté est en difficile et que si on ajoute la moitiée des munitions maximales est inférieur au nombre maximal de balles , ajout de la moitiée du nombre maximal de balles
                        else
                            nombreDeBalles = munitionMaxJoueur; // si l'on ajoute la moitiée du nombre maximal de balles et que c'est supérieur au nombre maximal de balles, le nombre de balles sera égal au nombre maximal de balles 
                    }
                    objetASupprimer.Add(muni); // ajout de la boite de munition aux objets a supprimer
                    interval.Start(); // démarage de l'interval enrtre deux boites de munition

                }
            }
            foreach (Rectangle soin in soinListe) // boucler pour chaque rectangle dans la liste des soins
            {
                Rect kitSoinZone = new Rect(Canvas.GetLeft(soin), Canvas.GetTop(soin), soin.Width, soin.Height);
                if (zoneJoueur.IntersectsWith(kitSoinZone) && vieJoueur < 100)
                {
                    vieJoueur = VIE_JOUEUR;
                    objetASupprimer.Add(soin);
                    minuteur2.Start();
                }
            }
            foreach (Rectangle zomb in zombieListe)
            {

                Rect ennemiZone = new Rect(Canvas.GetLeft(zomb), Canvas.GetTop(zomb), zomb.Width, zomb.Height);
                if (zoneJoueur.IntersectsWith(ennemiZone) && vieJoueur > 0)
                {
                    if (!triche)
                    {
                        vieJoueur -= 2;
                        Thread.Sleep(30);
                        sonVie.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Sons/take-damage.mp3"));
                        sonVie.Play();
                    }
            

                }
                DeplacementZombie(zomb);
            }



            foreach (Rectangle caisse in boiteListe)
            {

                Rect caisseZone = new Rect(Canvas.GetLeft(caisse), Canvas.GetTop(caisse), caisse.Width, caisse.Height);
                if (zoneJoueur.IntersectsWith(caisseZone))
                {
                    if (!triche)
                    {
                        CaisseJoueur();
                    }
                    else
                    {
                        Deplacements();
                    }
                }
                
            }
            //CaisseZombieEtJoueur();
            foreach (Rectangle y in objetASupprimer) // boucler pour chaque Rectangle dans la liste objets a supprimer
            {
                fond.Children.Remove(y);
                munitionListe.Remove(y);
                soinListe.Remove(y);
                zombieListe.Remove(y);
                balles.Remove(y);
                balleG.Remove(y);
                balleD.Remove(y);
                balleH.Remove(y);
                balleB.Remove(y);
                // suppression du rectangle y de toutes les listes
            }
            Rect feuZone = new Rect(Canvas.GetLeft(Feu), Canvas.GetTop(Feu), Feu.Width, Feu.Height); // conversion du Rectangle feu vers un Rect feuZone

            if (zoneJoueur.IntersectsWith(feuZone) && vieJoueur > 0) // test de collision entre le joueur et le feu
            {
                if (!triche) // si la triche est désactivée, le joueur perdera 2 de vie toutes les 15 ms
                {
                    vieJoueur -= 2;
                    Thread.Sleep(15);
                    sonVie.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Sons/take-damage.mp3"));
                    sonVie.Play();
                }
            }
            
        }



        private void CaisseJoueur()
        {

            if (gauche == true)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + 10);
            }

            else if (droite == true)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - 10);
            }
            else if (haut == true)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + 10);
            }
            else if (bas == true)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - 10);
            }
        }

        /*----------------------------------------------------*/
        /*----------------------CHEAT VIE---------------------*/
        /*----------------------------------------------------*/

        private void Vie()
        {
            if (!triche) //si la triche est désactivée
            {
                BarreDeVie.Value = vieJoueur; // actualiser la barre de vie selon la vie du joueur
                if (vieJoueur <= 0) // si la vie du joueur est égale ou inférieure à 0 
                {
                    vieJoueur = 0; //pour que la vie ne passe pas en néfatif
                    perdu = true; // le booléen perdu passe en vrai
                }
                if (perdu) // si perdu est vrai 
                {

                    FenetreMort fenetremort = new FenetreMort(); // nouvelle fenetre de mort
                    this.Hide(); // cacher le MainWindow
                    fenetremort.ShowDialog(); // ouverture de la fenetre mort
                }
            }
            else //si la triche est activée
            {
                vieJoueur = VIE_JOUEUR; //vie joueur à 100
                BarreDeVie.Value = vieJoueur; // actualiser barre de vie selon la vie du joueur
            }

        }

        /*----------------------------------------------------*/
        /*--------------------Orientation---------------------*/
        /*----------------------------------------------------*/

        private void OrientationJoueur()
        {

            if (orientationJoueur == ORIENTATION_GAUCHE) // si l'orientaion joueur est gauche
            {

                joueur.RenderTransform = new RotateTransform(180, joueur.Width / 2, joueur.Height / 2); //faire tourner le joueur de 180 °

            }
            if (orientationJoueur == ORIENTATION_DROITE) // si l'orientaion joueur est droite
            {
                joueur.RenderTransform = new RotateTransform(0, joueur.Width / 2, joueur.Height / 2);//faire tourner le joueur de 0 °

            }
            if (orientationJoueur == ORIENTATION_HAUT) // si l'orientaion joueur est haut
            {

                joueur.RenderTransform = new RotateTransform(-90, joueur.Width / 2, joueur.Height / 2);//faire tourner le joueur de -90 °
            }
            if (orientationJoueur == ORIENTATION_BAS) // si l'orientaion joueur est bas
            {

                joueur.RenderTransform = new RotateTransform(90, joueur.Width / 2, joueur.Height / 2);//faire tourner le joueur de 90 °
            }
        }

        private void OrientationZombie(Rectangle x, String orientationZombieX, String orientationZombieY)
        {
            if (orientationZombieY == ORIENTATION_HAUT) //si l'orientation verticale du zombie est haut, faire tourner de 90 °
                x.RenderTransform = new RotateTransform(90, x.Width / 2, x.Height / 2);
            if (orientationZombieY == ORIENTATION_BAS) //si l'orientation verticale du zombie est bas, faire tourner de -90 °
                x.RenderTransform = new RotateTransform(-90, x.Width / 2, x.Height / 2); 
            if (orientationZombieX == ORIENTATION_GAUCHE) //si l'orientation horizontale du zombie est gauche, faire tourner de 180 °
                x.RenderTransform = new RotateTransform(180, x.Width / 2, x.Height / 2);
            if (orientationZombieX == ORIENTATION_DROITE) //si l'orientation horizontale du zombie est droite, faire tourner de 0 °
                x.RenderTransform = new RotateTransform(0, x.Width / 2, x.Height / 2);
            
            if (orientationZombieY == ORIENTATION_HAUT && orientationZombieX == ORIENTATION_GAUCHE) // si l'orientation du zombie verticale est haut et l'orientation horiziontale gauche, appliquer un angle de 135°
                x.RenderTransform = new RotateTransform(135, x.Width / 2, x.Height / 2);
            if (orientationZombieY == ORIENTATION_HAUT && orientationZombieX == ORIENTATION_DROITE)// si l'orientation du zombie verticale est haut et l'orientation horiziontale droite, appliquer un angle de 45°
                x.RenderTransform = new RotateTransform(45, x.Width / 2, x.Height / 2);
            if (orientationZombieY == ORIENTATION_BAS && orientationZombieX == ORIENTATION_GAUCHE)// si l'orientation du zombie verticale est bas et l'orientation horiziontale gauche, appliquer un angle de -135°
                x.RenderTransform = new RotateTransform(-135, x.Width / 2, x.Height / 2);
            if (orientationZombieY == ORIENTATION_BAS && orientationZombieX == ORIENTATION_DROITE) // si l'orientation du zombie verticale est bas et l'orientation horiziontale droite, appliquer un angle de -45°
                x.RenderTransform = new RotateTransform(-45, x.Width / 2, x.Height / 2);


        }

        /*----------------------------------------------------*/
        /*-----------------------Pause------------------------*/
        /*----------------------------------------------------*/

        private void bouton_pause_Click(object sender, RoutedEventArgs e)
        {
            mineuteur.Stop(); // quand le bouton pause est cliqué, le minuteur et l'interval se stoppent
            interval.Stop();

            Pause pause = new Pause(); // nouvelle fenetre pause
            pause.ShowDialog(); // ouverture fenetre pause
        }



    }

}