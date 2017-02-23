/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : MoteurMenace.cs
 * Date de création : 27/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 31/12/2016
 * *********************************************************************/

using System;
using Windows.Foundation;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// La classe moteur menace regroupe les methode permettant la création,
    /// la configuration et l'animation d'une menace
    /// </summary>
    public class MoteurMenace
    {
        Menace menace;
        Grid grille;
        GererScore gererScore;

        /// <summary>
        /// Constructeur de la classe MoteurMenace
        /// </summary>
        /// <param name="grille">grille de la page GamePage.xaml</param>
        /// <param name="gererScore">Classe de gestion des scores</param>
        public MoteurMenace(Grid grille, GererScore gererScore)
        {
            this.grille = grille;
            this.gererScore = gererScore;
        }

        /// <summary>
        /// Création d'une nouvelle menace de manière aléatoire
        /// parmis les éléments disponibles dans la Factory
        /// </summary>
        /// <returns>Menace (Image)</returns>
        public Image creer()
        {
            Random random = new Random();
            int choixSource = random.Next(0, 100 ) % 3;

            menace = MenaceFactory.Get(choixSource);
            Image menaceImg = menace.Ufo;
            menaceImg.Tapped += menace_Tapped;

            return menaceImg;
        }

        /// <summary>
        /// Configurer la menace
        /// </summary>
        /// <param name="menace">Image menace</param>
        /// <param name="positionActuellePlanete">Position de la planète</param>
        /// <param name="tailleMenace">Taille de la menace</param>
        /// <param name="largeurFenetre">Largeur de la fenêtre</param>
        /// <param name="hauteurFenetre">hauteur de la fenêtre</param>
        public void configurer(Image menace, Point positionActuellePlanete, double tailleMenace, double largeurFenetre, double hauteurFenetre)
        {
            menace.Width = menace.Height = tailleMenace;
            Point positionDepart = getPositionDepart(tailleMenace, largeurFenetre, hauteurFenetre);
            menace.Margin = new Thickness(positionDepart.X, positionDepart.Y, 0, 0);

            Point positionArrivee = getPositionArrivee(positionDepart, positionActuellePlanete, largeurFenetre);

            animerMenace(menace, positionArrivee);
        }


        /// <summary>
        /// Création et lancement du storyboard permettant d'animer
        /// un objet passé en paramètre.
        /// </summary>
        /// <param name="objetSpacial">objet sur lequel appliquer le storyboard</param>
        /// <param name="positionArrivee">Postion sur le plan vers lequel doit aller l'objet (Points X et Y)</param>
        private void animerMenace(Image objetSpacial, Point positionArrivee)
        {
            objetSpacial.RenderTransform = new TranslateTransform();

            /// La durée de l'animation est calculée selon la menace
            Duration duration = new Duration(TimeSpan.FromSeconds(menace.Vitesse));

            /// Création de l'animation de mouvement X
            DoubleAnimation doubleAnimation1 = new DoubleAnimation();
            doubleAnimation1.Duration = duration;
            doubleAnimation1.To = positionArrivee.X;
            doubleAnimation1.AutoReverse = false;

            /// Création de l'animation de mouvement Y
            DoubleAnimation doubleAnimation2 = new DoubleAnimation();
            doubleAnimation2.Duration = duration;
            doubleAnimation2.To = positionArrivee.Y;
            doubleAnimation2.AutoReverse = false;

            /// Liaison de l'animation et de l'objet
            Storyboard.SetTarget(doubleAnimation1, objetSpacial.RenderTransform);
            Storyboard.SetTarget(doubleAnimation2, objetSpacial.RenderTransform);

            /// Définition de l'élément cible pour chaque animation
            Storyboard.SetTargetProperty(doubleAnimation1, "X");
            Storyboard.SetTargetProperty(doubleAnimation2, "Y");

            /// Création d'un nouveau Storyboard
            Storyboard sb = new Storyboard();
            sb.Children.Add(doubleAnimation1);
            sb.Children.Add(doubleAnimation2);
            /// Démarrage du storyboard
            sb.Begin();
        }

        /// <summary>
        /// Methode évenementielle exécutée lorrsque l'utilisateur clique sur
        /// une menace.
        /// Ceci à pour effet d'ajouter des points au joueur
        /// et de détruire la menace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menace_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EtatJeu etatJeu = (EtatJeu)Application.Current.Resources["etatJeu"];

            if (etatJeu == EtatJeu.enCours)
            {
                Image menaceImg = sender as Image;
                /// Ajout des points au joueur
                gererScore.Score += menace.NbPoints;
                /// Mise à jour du score
                gererScore.mettreAJour();
                /// Un son est joué pour confirmer la destruction de la menace
                jouerSon(menace.DeathSound);
                /// La menace est détruite
                grille.Children.Remove(menaceImg);
            }
        }

        /// <summary>
        /// Création d'un mediaPlayer et lecture d'un son selon une uri passée en paramètre
        /// </summary>
        /// <param name="uri">Chemin du son</param>
        private void jouerSon(Uri uri)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = Windows.Media.Core.MediaSource.CreateFromUri(uri);
            mediaPlayer.Play();
        }

        /// <summary>
        /// Permet de paramétrer la position XY sur le plan vers laquelle l'objet
        /// doit se déplacer
        /// </summary>
        /// <param name="positionMenace">Position originale de l'objet</param>
        /// <param name="positionActuellePlanete">Position actuelle de la planète sur le plan</param>
        /// <param name="largeurFenetre">Largeur de la fenêtre</param>
        /// <returns>Retourne un point sur le plan (X, Y) correspondant à la position finale de la menace</returns>
        private Point getPositionArrivee(Point positionMenace, Point positionActuellePlanete, double largeurFenetre)
        {
            double positionXarrivee = -largeurFenetre;
            double positionYarrivee;

            if (positionMenace.Y > positionActuellePlanete.Y )
                positionYarrivee = -positionActuellePlanete.Y;
            else
                positionYarrivee = positionActuellePlanete.Y;

            return new Point(positionXarrivee, positionYarrivee);
        }

        /// <summary>
        /// Permet de définir la position de départ de la menace dans la fenêtre de jeu
        /// </summary>
        /// <param name="tailleMenace">Taille de la menace</param>
        /// <param name="largeurFenetre">Largeur de la fenêtre</param>
        /// <param name="hauteurFenetre">Hauteur de la fenêtre</param>
        /// <returns>Postion de départ (coordonnées XY)</returns>
        private Point getPositionDepart(double tailleMenace, double largeurFenetre, double hauteurFenetre)
        {
            Random random = new Random();
            
            int newPositionX = (int)largeurFenetre - (int)tailleMenace * 2;
            int newPositionY = random.Next(-((int)hauteurFenetre + (int)tailleMenace), (int)hauteurFenetre - (int)tailleMenace);

            return new Point(newPositionX, newPositionY);
        }
    } 
}
