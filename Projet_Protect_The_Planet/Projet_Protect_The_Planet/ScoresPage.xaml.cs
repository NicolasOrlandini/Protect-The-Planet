/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : GamePage.xaml.cs
 * Date de création : 31/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 02/01/2016
 * *********************************************************************/

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Page contenant les scores des joueurs
    /// </summary>
    public sealed partial class ScoresPage : Page
    {
        private GererScore gererScore;

        /// <summary>
        /// Constructeur
        /// </summary>
        public ScoresPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Methode exécutée lorsque l'utilisateur clique sur le bouton delete (poubelle)
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void CommandDelete_Click(object sender, RoutedEventArgs e)
        {
            /// Suppression des scores
            gererScore.supprimer();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            gererScore = (GererScore)e.Parameter;
            lbListeScores.DataContext = gererScore.TabScores;
        }
    }
}
