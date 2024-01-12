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
        ImageBrush background_01 = new ImageBrush();
        ImageBrush btn_solo = new ImageBrush();
        ImageBrush btn_1vs1 = new ImageBrush();
        ImageBrush btn_parametre = new ImageBrush();


        public MainWindow fenetre
        {
            get { return fenetre; }
            set { fenetre = value; }
        }

        public WindowJeu()
        {


            InitializeComponent();
            
            background_01.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/zombie_decor.jpg"));
            zombie_decor.Fill = background_01;

            btn_solo.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/bouton_solo.png"));
            solo.Background = btn_solo;

            btn_1vs1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/bouton_1vs1.png"));
            _1vs1.Background = btn_1vs1;

            btn_parametre.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/bouton_parametre.png"));
            parametre.Background = btn_parametre;
            


        }


        private void solo_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

        }

        private void parametre_Click(object sender, RoutedEventArgs e)
        {
            Parametres parametre = new Parametres();
            parametre.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Je vais fermer la fenêtre");
            if ( this.DialogResult != true )
            App.Current.Shutdown();
        }


    }
}
