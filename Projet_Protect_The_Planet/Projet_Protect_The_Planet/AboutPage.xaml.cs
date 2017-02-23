/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : MACIOLEK Sebastian
 * Nom du fichier : AboutPage.xaml.cs
 * Date de création : 26/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 01/01/2016
 * *********************************************************************/

using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;


namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Page A propos contient le nom, la version et les développeurs
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public AboutPage()
        {
            this.InitializeComponent();
            txtVersion.Text = "Version " + getAppVersion() + "\n";
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
