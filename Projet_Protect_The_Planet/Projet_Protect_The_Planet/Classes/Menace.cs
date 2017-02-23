/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : gererScore.cs
 * Date de création : 09/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 02/01/2016
 * *********************************************************************/

using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Classe permettant de créer et de configurer une mencace (visuel, vitesse, nom, etc)
    /// </summary>
    public class Menace
    {
        Image ufo = new Image();
        int nbPoints = 0;
        int vitesse = 0;
        Uri deathSound;
        
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nom">Nom de la menace</param>
        /// <param name="source">Source de l'image</param>
        /// <param name="nbPoints">Nombre de points que rapporte la menace</param>
        /// <param name="vitesse">Vitesse de déplacement</param>
        /// <param name="deathSound">Son joué lors de la destruction de la menace</param>
        public Menace(string nom, string source, int nbPoints, int vitesse, Uri deathSound)
        {
            ufo.Source = new BitmapImage(new Uri(source, UriKind.Absolute));
            ufo.Name = nom;

            this.nbPoints = nbPoints;
            this.vitesse = vitesse;
            this.deathSound = deathSound;
        }

        public Image Ufo
        {
            get
            {
                return ufo;
            }
        }

        public int NbPoints
        {
            get
            {
                return nbPoints;
            }
        }

        public int Vitesse
        {
            get
            {
                return vitesse;
            }
        }

        public Uri DeathSound
        {
            get
            {
                return deathSound;
            }
        }
    }

    /// <summary>
    /// Factory Menace
    /// Permet de créer une nouvelle menace selon l'id passé en paramètre
    /// </summary>
    static class MenaceFactory
    {
        /// <summary>
        /// Création de la bonne menace
        /// </summary>
        /// <param name="id">id correspondant à la menace à créer</param>
        /// <returns>Retourne la menace créée</returns>
        public static Menace Get(int id)
        {
            switch (id)
            {
                case 0: return new Menace("asteroide", "ms-appx:///Assets/Images/asteroid3.png", 1, 4, new Uri("ms-appx:///Assets/Sons/Chicken_death.mp3"));
                case 1: return new Menace("ovni", "ms-appx:///Assets/Images/spaceship.png", 2, 3, new Uri("ms-appx:///Assets/Sons/Ufo_death.mp3"));
                case 2: return new Menace("tardis", "ms-appx:///Assets/Images/Tardis.png", 10, 1, new Uri("ms-appx:///Assets/Sons/Ufo_death.mp3"));
                default: return new Menace("asteroide", "ms-appx:///Assets/Images/asteroid3.png", 1, 4, new Uri("ms-appx:///Assets/Sons/Chicken_death.mp3"));
            }
        }
    }
}
