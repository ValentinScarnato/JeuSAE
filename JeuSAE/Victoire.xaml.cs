﻿using System;
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
    /// Logique d'interaction pour Victoire.xaml
    /// </summary>
    public partial class Victoire : Window
    {
        public Victoire()
        {
            InitializeComponent();

            label_kill.Content = ((MainWindow)Application.Current.MainWindow).killsJoueur;
            ImageBrush victoire = new ImageBrush();
            victoire.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image/victoire.png"));
            fond.Fill = victoire;
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
