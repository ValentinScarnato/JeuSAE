﻿using System;
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
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    public partial class Parametres : Window
    {
        public Parametres()
        {
            InitializeComponent();

        }


        private void click_Annuler(object sender, RoutedEventArgs e)
        {
            this.Close();
          
        }



        



      

        private void bouton_avancer_KeyDown(object sender, KeyEventArgs e)
        {
            bouton_avancer.Text = "";
            Key avancer;
            avancer = e.Key;

            bouton_avancer.Text = e.ToString().Substring(0,0);
            AFFICHAGE.Content= e.ToString();
        }

        private void bouton_droite_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
