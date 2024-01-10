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
    /// Logique d'interaction pour FenetreNiveau.xaml
    /// </summary>
    public partial class FenetreNiveau : Window
    {

        public MainWindow fenetre
        {
            get { return fenetre; }
            set { fenetre = value; }
        }

        public FenetreNiveau()
        {
            InitializeComponent();


        }
        private void solo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
