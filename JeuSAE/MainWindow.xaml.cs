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
using static System.Net.Mime.MediaTypeNames;

namespace JeuSAE
{
    /// <summary>
    /// Logique d'interaction pour jeu.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool gauche, droite, haut, bas = false;
        int vitesse = 1, vie = 100, munitions = 10, kills = 0;
        string position = "haut";


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
        }
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





    }
}