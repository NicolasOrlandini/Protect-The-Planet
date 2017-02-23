/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : SettingPage.xaml.cs
 * Date de création : 09/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 01/01/2016
 * *********************************************************************/

using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Page des paramètres permettant de gérer les paramètres du jeu
    /// - Son : activer ou désactiver les sons du jeu (ne comprend pas la musique du jeu)
    /// - Sélection de la planète à défendre parmis les planètes proposées
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private Planetes planetes = new Planetes();
        /// <summary>
        /// Paramètres d'application
        /// </summary>
        private ApplicationDataContainer localSettings =  ApplicationData.Current.LocalSettings;

        /// <summary>
        /// Constructeur de la page des paramètres
        /// </summary>
        public SettingsPage()
        {
            this.InitializeComponent();
            listBoxPlanete.DataContext = planetes;

            /// Configuration initiale des paramètres
            /// et récupération de l'état actuel si le paramètre existe déjà
            bool son = (bool)configurerParametre("son", true);
            string planete = (string)configurerParametre("planete", "Terre");
            configurerParametre("difficulte", true);

            /// Configuration de l'affichage (préremplissage)
            configurerParamSon(son);
            configurerParamPlanete(planete);

            txtVersion.Text = "Version " +  getAppVersion();
        }
        
        /// <summary>
        /// Permet de créer un nouveau paramètre qui sera sauvegardé et récupéré
        /// à la réouverture de l'application
        /// </summary>
        /// <param name="key">Clé</param>
        /// <param name="value"></param>
        public void creerParametre(string key, object value)
        {
            localSettings.CreateContainer(key, ApplicationDataCreateDisposition.Always);

            if (localSettings.Values[key] == null)
            {
                localSettings.Values[key] = value;
            }
        }

        private object configurerParametre(string key, object value)
        {
            object valeur = value;

            if (localSettings.Values.ContainsKey(key))
            {
                if (valeur == null)
                {
                    localSettings.Values[key] = valeur;
                }
                else
                {
                    valeur = localSettings.Values[key];
                }
            }
            else
            {
                /// Création de la clé et ecriture de la valeur (selement si la clé n'existe pas)
                creerParametre(key, value);
            }
            return valeur;
        }

        /// <summary>
        /// Configuration de l'affichage pour le paramètre "son"
        /// Affiche le switch dans le bon état
        /// </summary>
        /// <param name="valeur"></param>
        private void configurerParamSon(bool valeur)
        {
            if ((bool)localSettings.Values["son"])
                SwitchSon.IsOn = true;
            else
                SwitchSon.IsOn = false;
        }

        /// <summary>
        /// Configuration de l'affichage pour le paramètre "planete"
        /// Préselectionne l'élément sauvegardé dans les paramètres
        /// </summary>
        /// <param name="valeur"></param>
        private void configurerParamPlanete(string valeur)
        {
            /// Les éléments de la listBox sont parcourus
            foreach(Planete item in listBoxPlanete.Items)
            {
                if (item.PlaneteString == (string)localSettings.Values["planete"])
                    /// Préselectionne l'item correspondant au nom de la planète
                    /// sauvegardé dans le paramètre "planete"
                    listBoxPlanete.SelectedItem = item;
            }
        }

        /// <summary>
        /// Methode évenementielle exécutée lorsque l'état du switchSon change
        /// Met à jour le paramètre "son"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchSon_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn)
                {
                    localSettings.Values["son"] = true;
                }
                else
                {
                    localSettings.Values["son"] = false;
                }
            }
        }

        /// <summary>
        /// Methode évenementielle qui est exécutée lorsque l'utilisateur sélectionne un élément
        /// dans la listbox contenant les planètes (listBoxPlanete)
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void listBoxPlanete_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ListBox currentListBox = (ListBox)sender;
            /// Récupération de l'élément sélectionné
            string selectedPlanete = ((Planete)currentListBox.SelectedItem).PlaneteString;

            if (selectedPlanete != null)
            {
                /// Mise à jour du paramètre "planete"
                localSettings.Values["planete"] = selectedPlanete;
            }
        }

        /// <summary>
        /// Récupération de la version du package
        /// </summary>
        /// <returns>Version du package</returns>
        public string getAppVersion()
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }
    }
}
