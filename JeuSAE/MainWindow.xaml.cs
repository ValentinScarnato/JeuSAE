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
    /// Logique d'interaction pour jeu.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*private String ChoixFenetre;

        public String choixFenetre
        {
            get { return ChoixFenetre; }
            set
            {
                if (value != "Menu") ;
                throw new ArgumentException("Fenetre incorrecte");
                ChoixFenetre = value;

            }


        }*/
        ImageBrush joueur_ = new ImageBrush();
        ImageBrush iconeCrane = new ImageBrush();
        ImageBrush iconeMunition = new ImageBrush();
        ImageBrush iconeVie = new ImageBrush();



        public MainWindow()
        {
            InitializeComponent();
            joueur_.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/joueur_shotgun.png"));
            joueur.Fill = joueur_;
            iconeCrane.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/crane.png"));
            icone_crane.Fill = iconeCrane;
            iconeMunition.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/munition.png"));
            icone_munition.Fill = iconeMunition;
            iconeVie.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/coeurs.png"));
            icone_vie.Fill = iconeVie;


            /*this.ChoixFenetre = "Menu";
            while (true)
            {
                switch (ChoixFenetre)
                {
                    case "Menu":
                    {
                            WindowJeu menu = new WindowJeu();
                            menu.fenetre = this;
                            menu.ShowDialog();
                            break;
                        }
                    case "niv1":
                        {
                            FenetreJeu niv1 = new FenetreJeu();
                            niv1.fenetre = this;
                            niv1.ShowDialog();
                            break;
                        }
                }*/
        }
    }
}
