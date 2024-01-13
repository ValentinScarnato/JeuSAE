using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JeuSAE
{
    class Balle
    {
        public string direction;
        public int balleGauche, balleHaut, balleDroite, balleBas;
        private int vitesseBalle = 20; 
        Rect balle = new Rect();
        ImageBrush baballe = new ImageBrush();
        //private Timer tempsBalle = new Timer();

        public void GestionBalle(Canvas canvas)
        {
            baballe.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/munition.png"));
            //baballe.Fill = balle;
            
            
        }


    }
}
