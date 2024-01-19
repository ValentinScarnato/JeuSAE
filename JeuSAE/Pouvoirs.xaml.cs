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
    /// Logique d'interaction pour Pouvoirs.xaml
    /// </summary>
    public partial class Pouvoirs : Window
    {
        public event EventHandler boutonVieClick;
        public event EventHandler boutonBallesClick;
        public event EventHandler boutonVitesseClick;
        public event EventHandler PouvoirsFermer;


        public Pouvoirs()
        {
            InitializeComponent();
        }

        private void boutonVie_Click(object sender, RoutedEventArgs e)
        {
            boutonVieClick?.Invoke(this, EventArgs.Empty);
            this.Close();
            
        }

        private void boutonBalles_Click(object sender, RoutedEventArgs e)
        {
            boutonBallesClick?.Invoke(this, EventArgs.Empty);
        }

        private void boutonVitesse_Click(object sender, RoutedEventArgs e)
        {
            boutonVitesseClick?.Invoke(this, EventArgs.Empty);
        }
        private void FERMER(object sender, EventArgs e)
        {
            PouvoirsFermer?.Invoke(this, EventArgs.Empty);
        }
    }
}
