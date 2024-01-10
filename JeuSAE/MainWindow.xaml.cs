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
        private String ChoixFenetre;

        public String choixFenetre
        {
            get { return ChoixFenetre; }
            set
            {
                if (value != "Menu") ;
                throw new ArgumentException("Fenetre incorrecte");
                ChoixFenetre = value;

            }


        }
        public MainWindow() { 
            WindowJeu menu = new WindowJeu();
            menu.ShowDialog();
         
        }
        private bool gauche, droite, haut, bas = false;
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
        /*InitializeComponent();
        this.ChoixFenetre = "Menu";
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

    
        
    

