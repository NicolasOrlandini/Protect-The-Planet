/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : Joueur.cs
 * Date de création : 31/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 02/01/2016
 * *********************************************************************/

using System;

namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Classe Joueur contenant un score et un pseudo
    /// </summary>
    public class Joueur : IComparable
    {
        public string pseudo { get; set; }
        public int score { get; set; }

        /// <summary>
        /// Accesseur permettant de récupérer le score et le pseudo sous
        /// la forme d'une chaine de caractères
        /// </summary>
        public string joueur
        {
            get
            {
                return pseudo + " : " + score.ToString() + " points";
            }
        }

        public int CompareTo(object obj)
        {
            Joueur joueur1 = this;
            Joueur joueur2 = (Joueur)obj;

            if (joueur1.score < joueur2.score)
                return 1;
            else if (joueur1.score > joueur2.score)
                return -1;
            return 0;
        }
    }
}
