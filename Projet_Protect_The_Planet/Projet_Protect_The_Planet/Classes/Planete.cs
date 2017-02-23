/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : Planete.cs
 * Date de création : 09/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 01/01/2017
 * *********************************************************************/

using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Classe qui contient les méthodes permettant de créer une
    /// nouvelle planète
    /// </summary>
    public class Planete
    {
        /// <summary>
        /// Création d'une nouvelle ellipse qui sera la planète
        /// </summary>
        Ellipse planet = new Ellipse();

        /// <summary>
        /// Constructeur de la classe Planete
        /// Permet de construire une nouvelle planète.
        /// </summary>
        /// <param name="nom">Nom de la planète</param>
        /// <param name="fill">Couleur de remplissage</param>
        /// <param name="stroke">Couleur du contour</param>
        /// <param name="strokeThickness">épaisseur du contour</param>
        /// <param name="taille">Taille de la planète</param>
        public Planete(string nom, SolidColorBrush fill, SolidColorBrush stroke, int strokeThickness, double taille)
        {
            planet.Fill = fill;
            planet.Stroke = stroke;
            planet.StrokeThickness = strokeThickness;
            planet.Width = taille;
            planet.Height = taille;
            planet.Stroke.Opacity = .2;
            planet.Name = nom;
        }

        /// <summary>
        /// Accesseur
        /// Permet de récupérer l'ellipse (planète)
        /// </summary>
        public Ellipse Planet
        {
            get
            {
                return planet;
            }
        }

        /// <summary>
        /// Retourne le nom de la planète
        /// </summary>
        public string PlaneteString
        {
            get
            {
                return planet.Name;
            }
        }
    }
}
