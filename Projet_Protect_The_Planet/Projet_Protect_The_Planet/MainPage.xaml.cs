/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : MainPage.xaml.cs
 * Date de création : 06/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 02/01/2016
 * *********************************************************************/

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Page contenant le menu hamburger (SplitView)
    /// </summary>
    public sealed partial class MainPage : Page
    {
        GererScore gererScore;
        /// <summary>
        /// Constructeur
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            /// Affichage de la page d'accueil
            gererScore = new GererScore();
            contentFrame.Navigate(typeof(AccueilPage), gererScore);
        }

        /// <summary>
        /// Clic sur le bouton permettant d'ouvrir le panel
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        /// <summary>
        /// Clic sur le bouton Paramètres
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(SettingsPage));
        }

        /// <summary>
        /// Clic sur le bouton Menu principal
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void menuPrincipal_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(AccueilPage), gererScore);
        }

        /// <summary>
        /// Clic sur le bouton Scores
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void APropos_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(AboutPage));
        }

        /// <summary>
        /// Clic sur le bouton Scores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scores_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(ScoresPage), gererScore);
        }
    }
}
