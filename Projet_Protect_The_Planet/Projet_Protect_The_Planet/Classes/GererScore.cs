/* ********************************************************************* 
 * Projet : Protect The Planet
 * Auteur : ORLANDINI Nicolas
 * Nom du fichier : gererScore.cs
 * Date de création : 27/12/2016
 * Modifié par : ORLANDINI Nicolas
 * Date de modification : 02/01/2016
 * *********************************************************************/

using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using System;
using Windows.Data.Xml.Dom;
using Windows.Storage.Streams;
using System.IO;
using System.Collections;

namespace Projet_Protect_The_Planet
{
    /// <summary>
    /// Classe permettant de gerer le score du joueur
    /// </summary>
    public class GererScore
    {
        private int score = 0;

        private ArrayList tabScores;
        /// <summary>
        /// Nouveau document XML
        /// </summary>
        XmlDocument dom = new XmlDocument();
        /// <summary>
        /// Element Xml (correspond à la balise root)
        /// </summary>
        XmlElement x;

        /// <summary>
        /// Constructeur 
        /// Initialisation du document Xml (Commentaire et balise root)
        /// </summary>
        public GererScore()
        {
            XmlComment dec = dom.CreateComment("Les joueurs de Protect The Planet");
            dom.AppendChild(dec);
            x = dom.CreateElement("Joueurs");
            dom.AppendChild(x);
            tabScores = new ArrayList();
            /// Chargement des scores déjà présents dans le XML
            charger();
        }

        /// <summary>
        /// Propriété Score
        /// </summary>
        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        /// <summary>
        /// Propriété de collection de scores
        /// </summary>
        public ArrayList TabScores
        {
            get
            {
                return tabScores;
            }

            set
            {
                tabScores = value;
            }
        }

        /// <summary>
        /// Initialistation du score à 0 points
        /// </summary>
        public void initialiser()
        {
            score = 0;
            mettreAJour();
        }

        /// <summary>
        /// Mise à jour de la ressource score
        /// </summary>
        public void mettreAJour()
        {
            Application.Current.Resources["score"] = score;
        }

        /// <summary>
        /// Sauvegarde des scores dans un fichier XML
        /// Si le fichier existe déjà, celui-ci est remplacé
        /// </summary>
        public async void sauvegarder()
        {
            /// Récupération du dossier d'installation local
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            /// Création ou remplacement du fichier XML si celui-ci existe déjà
            StorageFile sampleFile = await storageFolder.CreateFileAsync("scores.xml", CreationCollisionOption.ReplaceExisting);
            tabScores.Sort();
            foreach (Joueur joueur in tabScores)
            {
                XmlElement x1 = dom.CreateElement("Joueur");
                XmlElement x11 = dom.CreateElement("pseudo");
                x11.InnerText = joueur.pseudo;
                x1.AppendChild(x11);
                XmlElement x12 = dom.CreateElement("score");
                x12.InnerText = joueur.score.ToString();
                x1.AppendChild(x12);
                x.AppendChild(x1);
            }
            await dom.SaveToFileAsync(sampleFile);
        }

        /// <summary>
        /// Chargement du contenu du fichier XML contenant
        /// les scores des joueurs dans une collection de joueurs
        /// </summary>
        public async void charger()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            XDocument doc = new XDocument();

            try
            {
                StorageFile textFile = await localFolder.GetFileAsync("scores.xml");
                using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
                {
                    Stream s = textStream.AsStreamForRead();
                    doc = XDocument.Load(s);

                    int nbJoueurs = doc.Root.Elements().Count();
                    var joueurs = doc.Descendants("Joueur");
                    foreach (var joueur in joueurs)
                    {
                        string pseudo = joueur.Element("pseudo").Value;
                        int score = int.Parse(joueur.Element("score").Value);

                        Joueur leJoueur = new Joueur();
                        leJoueur.pseudo = pseudo;
                        leJoueur.score = score;

                        tabScores.Add(leJoueur);
                    }
                    tabScores.Sort();
                }
            }
            catch { }
        }

        /// <summary>
        /// Réinitialisation de la collection de joueurs et suppression des scores dans le XML
        /// </summary>
        public async void supprimer()
        {
            TabScores.Clear();
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.CreateFileAsync("scores.xml", CreationCollisionOption.ReplaceExisting);
            
            try
            {
                dom.RemoveChild(x);
                await dom.SaveToFileAsync(sampleFile);
            }
            catch { }
        }
    }
}
