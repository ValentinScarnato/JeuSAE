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
        ImageBrush iconevie = new ImageBrush();
        ImageBrush joueur_ = new ImageBrush();
         ImageBrush iconecrane = new ImageBrush();
        public MainWindow()
        {         
                InitializeComponent();
                iconevie.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/coeurs.png"));
                icone_vie.Fill = iconevie;
                joueur_.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/survivor-idle_shotgun_0.png"));
                joueur.Fill = joueur_;
                iconecrane.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/crane.png"));
                icone_crane.Fill = iconecrane;
            
        }
    }
}
