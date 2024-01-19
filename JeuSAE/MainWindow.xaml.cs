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
using System.IO;
using WpfAnimatedGif;



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

        /*----------------------------------------------------*/
        /*----------------------DOUBLE------------------------*/
        /*----------------------------------------------------*/

        public static double VITESSE_JOUEUR = 8, VITESSE_JOUEUR_TRICHE = 15, VITESSE_ZOMBIE = 3;

        /*----------------------------------------------------*/
        /*--------------------TIMESPAN------------------------*/
        /*----------------------------------------------------*/

        private TimeSpan minuterie;

        /*----------------------------------------------------*/
        /*----------------------STRING------------------------*/
        /*----------------------------------------------------*/

        public static String ORIENTATION_HAUT = "haut", ORIENTATION_BAS = "bas", ORIENTATION_DROITE = "droite", ORIENTATION_GAUCHE = "gauche";
        string orientationJoueur = "droite";

        /*----------------------------------------------------*/
        /*-----------------------KEY--------------------------*/
        /*----------------------------------------------------*/

        public Key avancer = Key.Z;
        Key reculer = Key.S;
        Key allerADroite = Key.D;
        Key allerAGauche = Key.Q;
        Key tirer = Key.Space;
        public Key tournerDroite = Key.E;
        Key tournerGauche = Key.A;
        Key tricher = Key.K;

        /*----------------------------------------------------*/
        /*-----------------------INT--------------------------*/
        /*----------------------------------------------------*/

        public static int TEMPS_MAXIMAL_ENTRE_ZOMBIE = 8, TEMPS_MINIMAL_ENTRE_ZOMBIE = 3, MUNITIONS_MAX_JOUEUR = 15, NOMBRE_ZOMBIES_MANCHE = 20, VIE_JOUEUR = 100;
        public static int DEGATS_PAR_ZOMBIE = 10;
        private static int VITESSE_BALLE_JOUEUR = 20, VITESSE_BALLE_TRICHE = 30;
        private static int BANDEAU = 60;
        private int nombreDeBalles = 15;
        int ennemisRestants = NOMBRE_ZOMBIES_MANCHE, nombreEnnemisMap = 0;
        int nombreSoinMaXMemeTemps = 1, nombreZombieMaxMemeTemps = 5, nombreMunitionMaxMemeTemps = 1;
        public int killsJoueur { get; set; } = 0;
        int vieJoueur = VIE_JOUEUR;
        int manche = 1;

        /*----------------------------------------------------*/
        /*-----------------------BOOLEEN----------------------*/
        /*----------------------------------------------------*/

        bool gauche, droite, haut, bas = false;
        bool FinDePartie = false;
        bool apparitionVie = true, apparitionMunitions = true, vieInfinie = false, ballesInfinies = false;
        bool triche = false;
        bool perdu = false;

        /*----------------------------------------------------*/
        /*-----------------------LISTES-----------------------*/
        /*----------------------------------------------------*/

        private List<Rectangle> objetASupprimer = new List<Rectangle>();
        private List<Rectangle> zombieListe = new List<Rectangle>();
        private List<Rectangle> munitionListe = new List<Rectangle>();
        private List<Rectangle> soinListe = new List<Rectangle>();
        private List<Rectangle> balles = new List<Rectangle>();
        private List<Rectangle> balleG = new List<Rectangle>();
        private List<Rectangle> balleD = new List<Rectangle>();
        private List<Rectangle> balleH = new List<Rectangle>();
        private List<Rectangle> balleB = new List<Rectangle>();
        private BitmapImage[] imagesZombie = new BitmapImage[16];
        private int indexImageZombie = 0;



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
        ImageBrush boiteArme = new ImageBrush();
        ImageBrush soin = new ImageBrush();
        ImageBrush pause = new ImageBrush();
        ImageBrush map = new ImageBrush();
        ImageBrush tir = new ImageBrush();

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
            caisseDecor.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/caisse_fond.png"));
            caisse_decor_1.Fill = caisseDecor;
            caisse_decor_3.Fill = caisseDecor;
            caisse_decor_4.Fill = caisseDecor;
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

        }

        public MainWindow()
        {

            WindowJeu menu = new WindowJeu();
            menu.ShowDialog();
            InitializeComponent();
            GenerationImage();
            Generation_Zombies(nombreZombieMaxMemeTemps);
            GenerationKitSoin(nombreSoinMaXMemeTemps);
            Generation_Munitions(nombreMunitionMaxMemeTemps);



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
        private void ChargerImagesZombie()
        {
            for (int i = 0; i < imagesZombie.Length; i++)
            {
                string nomFichier = $"skeleton-move_{i + 1}.png";
                string cheminImage = $"pack://siteoforigin:,,,/Image/{nomFichier}";
                imagesZombie[i] = new BitmapImage(new Uri(cheminImage));
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

        private void InitialiserAnimationZombie(Rectangle ennemi)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100); // Ajustez l'intervalle selon votre besoin
            timer.Tick += (sender, e) => AnimerZombie(ennemi);
            timer.Start();
        }


        /*----------------------------------------------------*/
        /*-------------------TEMPS DE JEU TXT-----------------*/
        /*----------------------------------------------------*/

        private void TempsDeJeu()
        {
            minuterie = minuterie.Add(TimeSpan.FromMilliseconds(1500));
            texteMinuterie.Text = minuterie.ToString(@"hh\:mm");
        }

        /*----------------------------------------------------*/
        /*--------------- GENERATION DE BALLES ---------------*/
        /*----------------------------------------------------*/

        private void GenerationBalle()
        {
            if (nombreDeBalles > 0)
            {
                Rectangle balle = new Rectangle
                {
                    Tag = "Balle",
                    Height = 5,
                    Width = 5,
                    Fill = Brushes.White,
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
        /*-------------------- DEPLACEMENTS ------------------*/
        /*----------------------------------------------------*/

        public void Deplacements()
        {

            if (gauche == true && Canvas.GetLeft(joueur) > 0)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - VITESSE_JOUEUR);
            }

            else if (droite == true && Canvas.GetLeft(joueur) + joueur.Width < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + VITESSE_JOUEUR);
            }
            else if (haut == true && Canvas.GetTop(joueur) > BANDEAU + 20)
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
            for (int i = 0; i < nombreZombieMaxMemeTemps; i++)
            {
                Rectangle ennemi = new Rectangle
                {
                    Tag = "Ennemi",
                    Height = 100,
                    Width = 100,
                };
                /*
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = new BitmapImage(new Uri("pack://siteoforigin:,,,/Image/zombieGif.gif"));
                ennemi.Fill = imageBrush;*/

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
            }
        }

        private void GenerZombieConditions()
        {
            if (nombreEnnemisMap == 0 && killsJoueur != NOMBRE_ZOMBIES_MANCHE)
                Generation_Zombies(nombreZombieMaxMemeTemps);
        }

        /*----------------------------------------------------*/
        /*--------------GENERATION DE MUNITIONS---------------*/
        /*----------------------------------------------------*/

        private void Generation_Munitions(int nombreMunitionMaxMemeTemps)
        {

            Random aleatoire = new Random();
            if (apparitionMunitions)
            {
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


        }

        private void GenerMunitionsConditions(object sender, EventArgs e)
        {
            // Appeler cette méthode à chaque tick du timer (toutes les 15 secondes)
            Generation_Munitions(nombreMunitionMaxMemeTemps);
            interval.Stop();

        }

        /*----------------------------------------------------*/
        /*-----------------GENERATION DE SOIN-----------------*/
        /*----------------------------------------------------*/

        private void GenerationKitSoin(int nombreSoinMaXMemeTemps)
        {
            Random aleatoire = new Random();
            if (apparitionVie)
            {
                for (int i = 0; i < nombreSoinMaXMemeTemps; i++)
                {
                    Rectangle kitSoin = new Rectangle
                    {
                        Tag = "Kit_Soin",
                        Height = 45,
                        Width = 52,
                        Fill = soin
                    };
                    int pointApparition = aleatoire.Next(1, 1);

                    Canvas.SetTop(kitSoin, aleatoire.Next(80, 900));
                    Canvas.SetLeft(kitSoin, aleatoire.Next(20, 1730));

                    soinListe.Add(kitSoin);
                    fond.Children.Add(kitSoin);
                }
            }
        }

        private void GenerKitSoinConditions(object sender, EventArgs e)
        {
            // Appeler cette méthode à chaque tick du timer (toutes les 15 secondes)
            GenerationKitSoin(nombreSoinMaXMemeTemps);
            minuteur2.Stop();

        }

        /*----------------------------------------------------*/
        /*-------AFFICHAGE TEXT(KILL, ENNEMIS, BALLES,)-------*/
        /*----------------------------------------------------*/

        private void NombreEnnemis()
        {
            if (ennemisRestants >= 1)
                nombre_ennemis.Content = ennemisRestants + " ennemis restants";
            else
                nombre_ennemis.Content = ennemisRestants + " ennemi restant";
        }

        private void NombreBalles()
        {
            if (!triche)
            {
                nombre_balles.Content = nombreDeBalles + " | " + MUNITIONS_MAX_JOUEUR;
            }
            else
            {
                nombre_balles.Content = nombreDeBalles + " | " + "∞";
            }
        }

        public void NombreKills()
        {

            nombre_kill.Content = killsJoueur;

        }



            /*----------------------------------------------------*/
            /*-----------------GESTION DES TOUCHES----------------*/
            /*----------------------------------------------------*/

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == tirer)
            {
                GenerationBalle();
            }
            //Code de triche
            if (e.Key == tricher)
            {
                nombreDeBalles = 15;
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
        /*-----------GESTION D'INTERACTION BALLES-------------*/
        /*----------------------------------------------------*/

        private bool InteractionBalles(Rectangle x, Rect zoneJoueur)
        {


            Rect balle = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            if (Canvas.GetTop(x) < BANDEAU)
            {
                objetASupprimer.Add(x);
                return true;
            }
            if (Canvas.GetLeft(x) < 0)
            {
                objetASupprimer.Add(x);
                return true;
            }
            if (Canvas.GetLeft(x) < 0)
            {
                objetASupprimer.Add(x);
                return true;
            }
            if (Canvas.GetLeft(x) > fond.Width)
            {
                objetASupprimer.Add(x);
                return true;
            }
            foreach (Rectangle y in zombieListe)
            {

                Rect ennemiZone = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                if (balle.IntersectsWith(ennemiZone))
                {

                    objetASupprimer.Add(x);
                    objetASupprimer.Add(y);
                    nombreEnnemisMap--;
                    ennemisRestants--;
                    killsJoueur += 1;
                    Random rdm = new Random();
                    if (rdm.Next(1, 5) == 1)
                    {
                        nombreDeBalles ++;
                    }
                    return true;
                }

            }



            return false;
        }

        /*----------------------------------------------------*/
        /*---------------GESTION D'INTERACTION----------------*/
        /*----------------------------------------------------*/

        private void Interactions()
        {
            Rect zoneJoueur = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);
            foreach (Rectangle x in balleB)
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + VITESSE_BALLE_JOUEUR);
                if (InteractionBalles(x, zoneJoueur))
                {
                    objetASupprimer.Add(x);

                }
            }
            foreach (Rectangle x in balleD)
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + VITESSE_BALLE_JOUEUR);
                if (InteractionBalles(x, zoneJoueur))
                {
                    objetASupprimer.Add(x);

                }
            }
            foreach (Rectangle x in balleG)
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - VITESSE_BALLE_JOUEUR);

                if (InteractionBalles(x, zoneJoueur))
                {
                    objetASupprimer.Add(x);

                    
                }
            }
            foreach (Rectangle x in balleH)
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - VITESSE_BALLE_JOUEUR);
                if (InteractionBalles(x, zoneJoueur))
                {
                    objetASupprimer.Add(x);

                }
            }
            foreach (Rectangle z in munitionListe)
            {
                Rect boiteMunitionsZone = new Rect(Canvas.GetLeft(z), Canvas.GetTop(z), z.Width, z.Height);
                if (zoneJoueur.IntersectsWith(boiteMunitionsZone) && nombreDeBalles < 15)
                {
                    nombreDeBalles = 15;
                    objetASupprimer.Add(z);
                    interval.Start();


                }
            }
            foreach (Rectangle w in soinListe)
            {
                Rect kitSoinZone = new Rect(Canvas.GetLeft(w), Canvas.GetTop(w), w.Width, w.Height);
                if (zoneJoueur.IntersectsWith(kitSoinZone) && vieJoueur < 100)
                {
                    vieJoueur = VIE_JOUEUR;
                    objetASupprimer.Add(w);
                    minuteur2.Start();
                }
            }
            foreach (Rectangle y in zombieListe)
            {
                String orientationZombieX = "", orientationZombieY = "";
                Rect ennemiZone = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                if (zoneJoueur.IntersectsWith(ennemiZone) && vieJoueur > 0)
                {
                    if (!triche)
                    {
                        vieJoueur -= 5;
                        Thread.Sleep(80);
                    }

                }
                if (Canvas.GetLeft(y) > Canvas.GetLeft(joueur))
                {
                    Canvas.SetLeft(y, Canvas.GetLeft(y) - VITESSE_ZOMBIE);
                    orientationZombieX = ORIENTATION_GAUCHE;

                }
                if (Canvas.GetLeft(y) < Canvas.GetLeft(joueur))
                {
                    Canvas.SetLeft(y, Canvas.GetLeft(y) + VITESSE_ZOMBIE);
                    orientationZombieX = ORIENTATION_DROITE;
                }
                if (Canvas.GetTop(y) < Canvas.GetTop(joueur))
                {
                    Canvas.SetTop(y, Canvas.GetTop(y) + VITESSE_ZOMBIE);
                    orientationZombieY = ORIENTATION_HAUT;
                }
                if (Canvas.GetTop(y) > Canvas.GetTop(joueur))
                {
                    Canvas.SetTop(y, Canvas.GetTop(y) - VITESSE_ZOMBIE);
                    orientationZombieY = ORIENTATION_BAS;
                }
                OrientationZombie(y, orientationZombieX, orientationZombieY);

            }
            foreach (Rectangle y in objetASupprimer)
            {
                fond.Children.Remove(y);
                munitionListe.Remove(y);
                soinListe.Remove(y);
                zombieListe.Remove(y);
                balles.Remove(y);
                if (balleG.Contains(y))
                    balleG.Remove(y);
                if (balleD.Contains(y))
                    balleD.Remove(y);
                if (balleH.Contains(y))
                    balleH.Remove(y);
                if (balleB.Contains(y))
                    balleB.Remove(y);
            }
            Rect feuZone = new Rect(Canvas.GetLeft(Feu), Canvas.GetTop(Feu), Feu.Width, Feu.Height);

            if (zoneJoueur.IntersectsWith(feuZone) && vieJoueur > 0)
            {
                if (!triche)
                {
                    vieJoueur -= 3;
                    Thread.Sleep(15);
                }
            }

        }
        /*----------------------------------------------------*/
        /*----------------------CHEAT VIE---------------------*/
        /*----------------------------------------------------*/

        private void Vie()
        {
            if (!triche)
            {
                BarreDeVie.Value = vieJoueur;
                if (vieJoueur <= 0)
                {
                    vieJoueur = 0;
                    perdu = true;
                }
                if (perdu == true)
                {

                    FenetreMort fenetremort = new FenetreMort();
                    fenetremort.ShowDialog();
                    fenetremort.NombreKills();
                }
            }
            else
            {
                BarreDeVie.Value = BarreDeVie.Maximum;
                vieJoueur = VIE_JOUEUR;
                VITESSE_BALLE_JOUEUR = VITESSE_BALLE_TRICHE;
                VITESSE_JOUEUR = VITESSE_JOUEUR_TRICHE;
            }

        }

        /*----------------------------------------------------*/
        /*--------------------Orientation---------------------*/
        /*----------------------------------------------------*/

        private void OrientationJoueur()
        {
            joueur.RenderTransform = new RotateTransform(0, joueur.Width / 2, joueur.Height / 2);

            if (orientationJoueur == ORIENTATION_GAUCHE)
            {

                joueur.RenderTransform = new RotateTransform(180, joueur.Width / 2, joueur.Height / 2);

            }
            if (orientationJoueur == ORIENTATION_DROITE)
            {
                joueur.RenderTransform = new RotateTransform(0, joueur.Width / 2, joueur.Height / 2);

            }
            if (orientationJoueur == ORIENTATION_HAUT)
            {

                joueur.RenderTransform = new RotateTransform(-90, joueur.Width / 2, joueur.Height / 2);
            }
            if (orientationJoueur == ORIENTATION_BAS)
            {

                joueur.RenderTransform = new RotateTransform(90, joueur.Width / 2, joueur.Height / 2);
            }
        }

        private void OrientationZombie(Rectangle x, String orientationZombieX, String orientationZombieY)
        {
            x.RenderTransform = new RotateTransform(0, x.Width / 2, x.Height / 2);

            if (orientationZombieY == ORIENTATION_HAUT)
                x.RenderTransform = new RotateTransform(90, x.Width / 2, x.Height / 2);


            if (orientationZombieY == ORIENTATION_BAS)
                x.RenderTransform = new RotateTransform(-90, x.Width / 2, x.Height / 2);
            if (orientationZombieX == ORIENTATION_GAUCHE)
                x.RenderTransform = new RotateTransform(180, x.Width / 2, x.Height / 2);
            if (orientationZombieX == ORIENTATION_DROITE)
                x.RenderTransform = new RotateTransform(0, x.Width / 2, x.Height / 2);

            if (orientationZombieY == ORIENTATION_HAUT && orientationZombieX == ORIENTATION_GAUCHE)
                x.RenderTransform = new RotateTransform(135, x.Width / 2, x.Height / 2);
            if (orientationZombieY == ORIENTATION_HAUT && orientationZombieX == ORIENTATION_DROITE)
                x.RenderTransform = new RotateTransform(45, x.Width / 2, x.Height / 2);
            if (orientationZombieY == ORIENTATION_BAS && orientationZombieX == ORIENTATION_GAUCHE)
                x.RenderTransform = new RotateTransform(-135, x.Width / 2, x.Height / 2);
            if (orientationZombieY == ORIENTATION_BAS && orientationZombieX == ORIENTATION_DROITE)
                x.RenderTransform = new RotateTransform(-45, x.Width / 2, x.Height / 2);


        }

        /*----------------------------------------------------*/
        /*-----------------------Pause------------------------*/
        /*----------------------------------------------------*/

        private void bouton_pause_Click(object sender, RoutedEventArgs e)
        {
            mineuteur.Stop();
            interval.Stop();
            Pause pause = new Pause();
            pause.ShowDialog();
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
            TempsDeJeu();
            ChargerImagesZombie();
            GenerZombieConditions();
            FinManche();

        }

        private void FinManche()
        {
            if (killsJoueur == NOMBRE_ZOMBIES_MANCHE)
            {

            }
            
        }
    }
    
}