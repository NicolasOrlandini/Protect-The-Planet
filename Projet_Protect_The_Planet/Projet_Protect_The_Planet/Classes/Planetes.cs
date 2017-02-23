/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : Planetes.cs
 * Date de création : 09/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 01/01/2017
 * *********************************************************************/

using System.Collections.ObjectModel;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Collection de planètes
    /// </summary>
    public class Planetes : ObservableCollection<Planete>
    {
        /// <summary>
        /// Constructeur de la collection de planètes
        /// </summary>
        public Planetes()
        {
            Add(new Planete("Terre", new SolidColorBrush(Colors.RoyalBlue), new SolidColorBrush(Colors.Cyan), 19, 1.1));
            Add(new Planete("Uranus", new SolidColorBrush(Colors.LightSkyBlue), new SolidColorBrush(Colors.GhostWhite), 20, 1.2));
            Add(new Planete("Jupiter", new SolidColorBrush(Colors.PeachPuff), new SolidColorBrush(Colors.BlanchedAlmond), 25, 1.5));
            Add(new Planete("Gallifrey", new SolidColorBrush(Colors.OrangeRed), new SolidColorBrush(Colors.Orange), 28, 1.8));
            Add(new Planete("Skaro", new SolidColorBrush(Colors.DarkRed), new SolidColorBrush(Colors.OrangeRed), 19, 1.1));
        }
    }
}