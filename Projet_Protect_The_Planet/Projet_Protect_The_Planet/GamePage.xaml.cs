/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : GamePage.xaml.cs
 * Date de création : 06/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 02/01/2016
 * *********************************************************************/

using System;
using Windows.Foundation;
using Windows.Media.Playback;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Définition des différents états possibles du jeu
    /// </summary>
    public enum EtatJeu
    {
        enCours,
        pause,
        fin,
        quitter,
        redemarrer
    };
    /// <summary>
    /// Page de jeu
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private Ellipse planet = new Ellipse();
        private Image menace = new Image();

        private double tailleMenace = 100;
        private double taillePlanete = 100;

        private MoteurMenace moteurMenace;
        private MoteurPlanete moteurPlanete;
        private GererScore gererScore;
        private MediaPlayer mpTheme;

        private DispatcherTimer dispatcherTimer;
        private DateTimeOffset startTime;
        private DateTimeOffset lastTime;

        /// <summary>
        /// Constructeur
        /// </summary>
        public GamePage()
        {
            this.InitializeComponent();

            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigated += OnNavigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        /// <summary>
        /// Paramétrage et lecture de la musique d'ambiance
        /// </summary>
        private void jouerMainTheme()
        {
            mpTheme.Dispose();
            mpTheme = jouerSon(new Uri("ms-appx:///Assets/Sons/mainTheme.mp3"));
        }

        /// <summary>
        /// Methode permettant de jouer un son en créant un nouveau MediaPlayer
        /// associé au chemin du son
        /// </summary>
        /// <param name="uri">chemin du son</param>
        /// <returns>MediaPlayer</returns>
        private MediaPlayer jouerSon(Uri uri)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = Windows.Media.Core.MediaSource.CreateFromUri(uri);
            mediaPlayer.Play();
            return mediaPlayer;
        }

        /// <summary>
        /// Gestion du bouton Retour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (((Frame)sender).CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            gererScore = (GererScore)e.Parameter;
            moteurMenace = new MoteurMenace(grille, gererScore);
            moteurPlanete = new MoteurPlanete();
            mpTheme = new MediaPlayer();

            /// Création de la planète à protéger
            genererNouvellePlanete();

            initialiserJeu();
        }

        /// <summary>
        /// Gestion du bouton Retour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            /*if (rootFrame.CanGoBack)
            {
                dispatcherTimer.Stop();
                changerEtat("etatJeu", EtatJeu.fin);
                e.Handled = true;
                rootFrame.GoBack();
            }*/

            dispatcherTimer.Stop();
            mpTheme.Dispose();
            grille.Children.Clear();

            /// Etat du jeu = quitter
            /// L'utilisateur quitte la partie en cours en sélectionnant le bouton Retour
            changerEtat("etatJeu", EtatJeu.quitter);

            /// Retour à la page prinicipale
            rootFrame.Navigate(typeof(MainPage));
        }

        /// <summary>
        /// Génération et ajout d'une nouvelle menace à la page
        /// </summary>
        private void genererNouvelleMenace()
        {
            double largeurFenetre = getLargeurFenetre();
            grille.Children.Remove(menace);
            menace = moteurMenace.creer();
            tailleMenace = 0.07 * largeurFenetre;
            moteurMenace.configurer(menace, getPositionActuellePlanete(), tailleMenace, largeurFenetre, getHauteurFenetre());
            grille.Children.Add(menace);
        }

        /// <summary>
        /// Génération et ajout d'une nouvelle planète à la page
        /// </summary>
        private void genererNouvellePlanete()
        {
            grille.Children.Remove(planet);
            planet = moteurPlanete.creer();
            taillePlanete = (getLargeurFenetre() / 10) * planet.Width;
            moteurPlanete.configurer(planet, getLargeurFenetre(), taillePlanete);
            grille.Children.Add(planet);
        }

        /// <summary>
        /// Initialisation de la partie
        /// - Création de la planète
        /// - Création et configuration de la menace
        /// - Initialisation du score à 0
        /// - Initialisation de la barre de vie au maximimum (100)
        /// - Début du jeu
        /// </summary>
        private void initialiserJeu()
        {
            jouerMainTheme();

            /// Création et configuration de la menace
            //genererNouvelleMenace();

            /// Initialisation du score du joueur à 0 points
            gererScore.initialiser();
            txtScore.Text = gererScore.Score.ToString();

            /// Initialisation de la barre de vie
            initialiserLifeBar();

            /// Démarrage de la détection de collision
            initialiserControlePosition();

            /// Passage de l'état du jeu à "EnCours"
            changerEtat("etatJeu", EtatJeu.enCours);
        }

        /// <summary>
        /// Change la couleur de la barre de vie en fonction de sa valeur
        /// 100 - 50 : vert
        /// 49 - 20 : orange
        /// 19 - 0 : rouge
        /// </summary>
        private void changeLifeBarColor()
        {
            if (lifeBar.Value >= 50)
                lifeBar.Foreground = new SolidColorBrush(Colors.LightGreen);
            else if (lifeBar.Value < 50 && lifeBar.Value >= 20)
                lifeBar.Foreground = new SolidColorBrush(Colors.Orange);
            else if(lifeBar.Value < 20)
                lifeBar.Foreground = new SolidColorBrush(Colors.Red);
        }

        /// <summary>
        /// La barre de vie est initialisée à 100 (maximum)
        /// </summary>
        private void initialiserLifeBar()
        {
            lifeBar.Value = 100;
            changeLifeBarColor();
        }

        /// <summary>
        /// Affichage du dialog indiquant à l'utilisateur qu'il a perdu
        /// - Affiche le score
        /// - Demande à l'utilisateur de choisir un pseudo qui sera associé à son
        /// score dans la page des scores
        /// - Un bouton permet d'accéder à la page des scores
        /// </summary>
        private async void gameOver()
        {
            changerEtat("etatJeu", EtatJeu.fin);
            ContentDialogGameOver monDialog = new ContentDialogGameOver(gererScore);
            ContentDialogResult result = await monDialog.ShowAsync();
        }

        /// <summary>
        /// Récuperer la position de la planète sur le plan
        /// </summary>
        /// <returns>Point : coordonnées X et Y de la planète</returns>
        private Point getPositionActuellePlanete()
        {
            return planet.TransformToVisual(this).TransformPoint(new Point(0, 0));
        }

        /// <summary>
        /// Récuperer la position de la menace sur le plan
        /// </summary>
        /// <returns>Point : coordonnées X et Y de la menace</returns>
        private Point getPositionActuelleManace()
        {
            return menace.TransformToVisual(this).TransformPoint(new Point(0, 0));
        }
        
        /// <summary>
        /// Timer permettant de controler la position de la menace
        /// Répété toutes les 20 millisecondes
        /// </summary>
        public void initialiserControlePosition()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += timerPosition_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Tick : methode exécutée a chaque fois que l'interval défin dans
        /// le timer est écoulé.
        /// Suivant l'état de la variable etatJeu, differentes actions sont
        /// réalisées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timerPosition_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;
            EtatJeu etatJeu = (EtatJeu)Application.Current.Resources["etatJeu"];

            switch (etatJeu)
            {
                case EtatJeu.enCours: verifierCollision();
                    txtScore.Text = gererScore.Score.ToString();
                    break;
                case EtatJeu.quitter: dispatcherTimer.Stop();
                    break;
                case EtatJeu.fin:
                    try
                    {
                        mpTheme.Pause();
                    }catch { }
                           
                    break;
                case EtatJeu.redemarrer: initialiserJeu();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Vérification de la position de la menace
        /// - Controle si la menace entre en collision avec la planète
        /// - Controle si la menace est sortie de la fenêtre
        /// - Controle si la barre de vie n'est pas à zero
        /// </summary>
        private void verifierCollision()
        {
            if (lifeBar.Value == 0)
            {
                gameOver();
                initialiserLifeBar();
            }
            else
            {
                Point positionPlanete = getPositionActuellePlanete();
                Point positionMenace = getPositionActuelleManace();

                if (positionMenace.X < positionPlanete.X + taillePlanete && positionMenace.X > positionPlanete.X &&
                    positionMenace.Y < positionPlanete.Y + taillePlanete && positionMenace.Y + tailleMenace > positionPlanete.Y)
                {
                    lifeBar.Value -= 20;
                    changeLifeBarColor();
                    jouerSon(new Uri("ms-appx:///Assets/Sons/Collision.mp3"));
                    genererNouvelleMenace();
                }
                else if (positionMenace.X <= 0 || positionMenace.Y  > getHauteurFenetre())
                {
                    genererNouvelleMenace();
                }
            }
        }

        /// <summary>
        /// Permet de récupérer la largeur actuelle de la fenêtre
        /// </summary>
        /// <returns></returns>
        private double getLargeurFenetre()
        {
            return Window.Current.Bounds.Width;
        }

        /// <summary>
        /// Permet de récupérer la hauteur actuelle de la fenêtre
        /// </summary>
        /// <returns></returns>
        private double getHauteurFenetre()
        {
            return Window.Current.Bounds.Height;
        }

        /// <summary>
        /// Permet de changer l'état d'une ressource passée en paramètre
        /// </summary>
        /// <param name="key">Clé de la ressource à mettre à jour</param>
        /// <param name="value">Nouvelle valeur</param>
        private void changerEtat(string key, object value)
        {
            Application.Current.Resources[key] = value;
        }

        /// <summary>
        /// Methode exécutée lorsque l'utilisateur sélectionne le bouton "Musique" de la commandBar
        /// Permet de couper on de réactiver la musique du jeu
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void CommandMusique_Click(object sender, RoutedEventArgs e)
        {
            EtatJeu etatJeu = (EtatJeu)Application.Current.Resources["etatJeu"];

            if (etatJeu == EtatJeu.enCours)
            {
                if (mpTheme.PlaybackSession.PlaybackState == MediaPlaybackState.Playing)
                    mpTheme.Pause();
                else
                    mpTheme.Play();
            }
        }

        /// <summary>
        /// Methode exécutée lorsque l'utilisateur sélectionne le bouton "Pause" de la commandBar
        /// Permet de mettre le jeu en pause, les animations se mettent en pause et l'utilisateur
        /// ne peut plus marquer de point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandPause_Click(object sender, RoutedEventArgs e)
        {
            EtatJeu etatJeu = (EtatJeu)Application.Current.Resources["etatJeu"];

            if (etatJeu == EtatJeu.enCours)
            {
                changerEtat("etatJeu", EtatJeu.pause);
                if (mpTheme.PlaybackSession.CanPause)
                    mpTheme.Pause();
            }
            else
            {
                changerEtat("etatJeu", EtatJeu.enCours);
                mpTheme.Play();
            }
        }

        /// <summary>
        /// Methode exécutée lorsque l'utilisateur sélectionne le bouton "Recommancer" de la commandBar
        /// Réinitialise le score et les animations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandRestart_Click(object sender, RoutedEventArgs e)
        {
            initialiserJeu();
        }
    }
}
