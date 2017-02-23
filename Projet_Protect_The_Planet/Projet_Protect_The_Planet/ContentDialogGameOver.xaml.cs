/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : ContentDialogGameOver.xaml.cs
 * Date de création : 09/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 02/01/2017
 * *********************************************************************/

using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Dialog permettant d'afficher le score du joueur ainsi que la possibilité pour l'utilisateur
    /// d'entrer un pseudo
    /// S'affiche quand l'utilisateur à perdu la partie
    /// </summary>
    public sealed partial class ContentDialogGameOver : ContentDialog
    {
        private GererScore gererScore;
        /// <summary>
        /// Constructeur de la classe ContentDialogGameOver
        /// </summary>
        public ContentDialogGameOver(GererScore gererScore)
        {
            this.InitializeComponent();
            this.gererScore = gererScore;
            /// Récupération du score du joueur
            if (Application.Current.Resources.ContainsKey("score"))
            {
                Score.Text = ((int)Application.Current.Resources["score"]).ToString();
            }

            if (isSon())
            {
                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Source = Windows.Media.Core.MediaSource.CreateFromUri(new System.Uri("ms-appx:///Assets/Sons/Game_Over_PacMan.mp3"));
                mediaPlayer.Play();
            }
        }

        /// <summary>
        /// Methode évenementielle exécutée lorsque l'utilisateur clique sur le bouton valider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ContentDialog_btnValiderClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Joueur newScore = new Joueur
            {
                pseudo = txtPseudo.Text,
                score = gererScore.Score
            };
            gererScore.TabScores.Add(newScore);
            gererScore.sauvegarder();

            Application.Current.Resources["etatJeu"] = EtatJeu.redemarrer;
        }

        /// <summary>
        /// Récupère l'état du paramètre Son
        /// </summary>
        /// <returns>Retourne un booléen correspondant au paramètre du son</returns>
        private bool isSon()
        {
            bool son = true;
            var prefs = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (prefs.Values.ContainsKey("son"))
            {
                // Mise à jour de la valeur
                son = (bool)prefs.Values["son"];
            }
            return son;
        }
    }
}
