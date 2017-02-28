/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : MACIOLEK Sebastian
 * Nom du fichier : AboutPage.xaml.cs
 * Date de création : 26/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 28/02/2017
 * *********************************************************************/

using System;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.Email;
using Windows.System;
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


        /*private async void sendEmail(string messageBody)
        {
            var emailRecipient = new Windows.ApplicationModel.Email.EmailRecipient("nicolas.orlandini@outlook.fr", "Nicolas ORLANDINI");
            EmailMessage objEmail = new EmailMessage();
            objEmail.Subject = "Contact a propos de l'application Protect The Planet";
            objEmail.Body = messageBody;
            objEmail.To.Add(emailRecipient);
            await EmailManager.ShowComposeNewEmailAsync(objEmail);
        }*/

        private async void envoyerEmail(string messageBody)
        {
            var mailto = new Uri("mailto:nicolas.orlandini@outlook.fr?subject=" + "Contact a propos de l'application Protect The Planet" + "&body=" + messageBody);
            await Launcher.LaunchUriAsync(mailto);
        }

        private void btnSendMail_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            envoyerEmail(txtBody.Text);
        }
    }
}
