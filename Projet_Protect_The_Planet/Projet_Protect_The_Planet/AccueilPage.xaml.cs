/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : AccueilPage.xaml.cs
 * Date de création : 09/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 01/01/2016
 * *********************************************************************/

using System;
using Windows.Foundation;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Page d'accueil contient le titre ainsi que le bouton Jouer pour accéder au jeu
    /// Contient une animation : un satellite en orbite autour d'une planète (le bouton jouer)
    /// </summary>
    public sealed partial class AccueilPage : Page
    {
        private DispatcherTimer dispatcherTimerOrbite;
        private DateTimeOffset startTime;
        private DateTimeOffset lastTime;
        private GererScore gererScore;
        private double a = 0;

        /// <summary>
        /// Constructeur de la page d'accueil
        /// </summary>
        public AccueilPage()
        {
            this.InitializeComponent();
            /// Démarage de l'animation du satellite
            DispatcherTimerOrbiteSetup();

            /// Transparence des contour de la planète et de son satellite
            satellite.Stroke.Opacity = .2;
            btnJoueur.Stroke.Opacity = .2;
        }
        
        /// <summary>
        /// Configuration du timer de l'animation de trajectoire en orbite
        /// du satellite
        /// </summary>
        private void DispatcherTimerOrbiteSetup()
        {
            dispatcherTimerOrbite = new DispatcherTimer();
            dispatcherTimerOrbite.Tick += TimerOrbite_Tick;
            dispatcherTimerOrbite.Interval = new TimeSpan(0, 0, 0, 0, 20);
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            dispatcherTimerOrbite.Start();
        }

        /// <summary>
        /// Se produit lorsque le temps interval du timer s'est écoulé (20 ms)
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void TimerOrbite_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;

            dessiner();
        }

        /// <summary>
        /// Récuperation du paramètre "son"
        /// </summary>
        /// <returns>booléen (true = son activé - false = son désactivé)</returns>
        private bool getEtatSon()
        {
            bool son = true;
            var prefs = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (prefs.Values.ContainsKey("son"))
            {
                son = (bool)prefs.Values["son"];
            }
            return son;
        }

        /// <summary>
        /// Methode evénementielle. Est exécuté lorsque l'utilisateur sélectionne 
        /// le bouton JOUER
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJouer_Click(object sender, TappedRoutedEventArgs e)
        {
            /// Arret du timer (On quitte la page donc l'animation s'arrête
            dispatcherTimerOrbite.Stop();

            /// Récupération du paramètre "son"
            bool son = getEtatSon();

            /// Lecture du son de démarrage de la partie si le son est activé
            if (son)
            {
                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Source = Windows.Media.Core.MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sons/mario-1-up.mp3"));
                mediaPlayer.Play();
            }
            /// Navigation vers la page de jeu
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(GamePage), gererScore);
        }

        /// <summary>
        /// Récupérer la position suivante du satellite pour effectuer
        /// une rotatation en orbite
        /// </summary>
        /// <param name="taille">Taille de l'objet</param>
        private Point getNouvellePosition(double taille)
        {
            double multiplicateur = grilleBtnJouer.Width * 1.75;

            double x = taille / 2 + Math.Cos(a) * multiplicateur;
            double y = taille / 2 + Math.Sin(a) * multiplicateur;

            a = a + 0.1;
            if (a > 2 * Math.PI)
            {
                a = 0;
            }
            return new Point(x, y);
        }

        /// <summary>
        /// Défini une nouvelle marge ayant pour effet de modifier la position du satellite
        /// </summary>
        /// <param name="taille">Taille de l'objet</param>
        /// <param name="nouvellePosition">Nouvelle position sur le plan</param>
        private void deplacerEllipse(double taille, Point nouvellePosition)
        {
            double left = btnJoueur.Margin.Left + taille / 2;
            double top = btnJoueur.Margin.Bottom + taille / 2;

            satellite.Margin = new Thickness(left, top, nouvellePosition.X, nouvellePosition.Y);
        }

        /// <summary>
        /// Récupère la taille et dessine le satellite à une nouvelle position
        /// pour former une trajectoire en orbite
        /// </summary>
        private void dessiner()
        {
            double taille = satellite.Width;

            Point nouvellePosition = getNouvellePosition(taille);
            deplacerEllipse(taille, nouvellePosition);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            gererScore = (GererScore)e.Parameter;
        }
    }
}
