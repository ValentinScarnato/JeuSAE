using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logique d'interaction pour FenetreMort.xaml
    /// </summary>
    public partial class FenetreMort : Window
    {
        public FenetreMort()
        {
            InitializeComponent();

            label_kill.Content = ((MainWindow)Application.Current.MainWindow).killsJoueur;
            ImageBrush mort = new ImageBrush();
            mort.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/mort.png"));
            rectangleMort.Fill = mort;
        }
        public void RedemarrerApplication()
        {
            string cheminApplication = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(cheminApplication);
            Application.Current.Shutdown();
        }
        private void Button_Menu(object sender, RoutedEventArgs e)
        {
            RedemarrerApplication();
            this.DialogResult = false;

        }
        private void Button_Quitter(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();

        }

    }
}
