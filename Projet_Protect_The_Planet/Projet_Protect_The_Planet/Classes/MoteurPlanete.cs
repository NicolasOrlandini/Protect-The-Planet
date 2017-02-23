/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : MoteurPlanete.cs
 * Date de création : 30/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 01/01/2017
 * *********************************************************************/

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;

namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Cette classe permet de créer et de configurer la planète dont le
    /// nom est sauvegardé dans les paramètres
    /// La planète est crée en début de partie
    /// </summary>
    public class MoteurPlanete
    {
        Planetes planetes = new Planetes();

        /// <summary>
        /// Création d'une nouvelle planète selon les paramètres
        /// </summary>
        /// <returns>Retrourne une planète (Ellipse)</returns>
        public Ellipse creer()
        {
            Ellipse newPlanet = new Ellipse();

            foreach(Planete planete in planetes)
            {
                if (planete.PlaneteString == getSelectedPlanete())
                    newPlanet = planete.Planet;
            }
            return newPlanet;
        }

        /// <summary>
        /// Configuration de la planète :
        /// - Placement dans la fenêtre
        /// - Taille
        /// </summary>
        /// <param name="planet">Planète à configurer</param>
        /// <param name="largeurFenetre">Largeur de la fenêtre</param>
        /// <param name="taillePlanete">Taille de la planète</param>
        public void configurer(Ellipse planet, double largeurFenetre, double taillePlanete)
        {
            planet.Margin = new Thickness(-largeurFenetre / 4.5, 40, 0, 0);
            planet.Width = planet.Height = taillePlanete;
        }

        /// <summary>
        /// Récupère le nom de planète sélectionné dans les paramètres
        /// </summary>
        /// <returns>Retourne le nom de la planète</returns>
        private string getSelectedPlanete()
        {
            string planete = "Terre";
            var prefs = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (prefs.Values.ContainsKey("planete"))
            {
                planete = (string)prefs.Values["planete"];
            }
            return planete;
        }
    }
}
