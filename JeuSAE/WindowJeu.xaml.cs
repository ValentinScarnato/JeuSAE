using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
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

namespace JeuSAE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WindowJeu : Window
    {
        ImageBrush fondEcran = new ImageBrush();
        ImageBrush btn_parametre = new ImageBrush();
        ImageBrush LastNight = new ImageBrush();


       

        public WindowJeu()
        {




            InitializeComponent();
            
            fondEcran.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/Banger.png"));
            zombie_decor.Fill = fondEcran;
            

            btn_parametre.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/bouton_parametre.png"));
            parametre.Background = btn_parametre;
            
            LastNight.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/LastNightLogo.png"));
            rectangle_LastNight.Fill = LastNight;
        }


        private void solo_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

        }

        


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (this.DialogResult != true)
                App.Current.Shutdown();
        }

        private void quitter_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        public void parametre_Click(object sender, RoutedEventArgs e)
        {
            Parametres parametre = new Parametres();
            parametre.ShowDialog();
        }

        private void boutonPlus_Click(object sender, RoutedEventArgs e)
        {
            Plus plus = new Plus();
            plus.ShowDialog();
        }
    }
}
