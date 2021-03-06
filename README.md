# Protect-The-Planet

Projet Protect The Planet en UWP (C#/XAML) - LP SIL DAM

Novembre 2016 - Janvier 2017

Version 1.0.1

## Contexte du projet 

Le projet consiste à développer un jeu simple et addictif sur le thème des planètes et de l’espace.

## Objectif  

L'objectif de Protect the Planet est de protéger le plus longtemps possible une planète, modifiable par l’utilisateur, des astéroïdes ainsi que des objets spatiaux en tout genre qui se dirigent vers elle en les détruisant grâce à un clic ou une pression sur l'objet.

A chaque fois que le joueur détruit un objet spacial, il récupère un nombre de points proportionnel à la vitesse de ce dernier.

Le joueur à un nombre de vie limité, si plusieurs objets entrent en collision avec la planète, le joueur perd la partie.



Application développée en UWP (Universal Windows Platform) totalement responsive. L'application s'adapte aussi bien aux grand écran d'un ordinateur qu'à un petit écran de smartphone.


## Fonctionnalités

### Page d'accueil
  
  ![Page Accueil](/Images/Accueil.png "Page d'accueil du jeu")
  
  * Menu hamburger
  * Animation sans storyboard

Trajectoire d'un objet Ellipse en cercle avec un timer de 20 ms entre chaque position pour imiter la rotation d'un satellite autour d'une planète.

### Page de jeu

  ![Page Jeu](/Images/Jeu.png "Page contenant le jeu")
    
  * CommandBar : Option Pause, recomencer une partie et activer/désactiver le son
  * ProgressBar : Barre de vie
  * Animations avec Storyboard
  * Système de points
  * Jouer des sons
  * Navigation entre les pages : Bouton retour permettant de quitter le jeu
  
### Scores
  
  ![Page Scores](/Images/Scores.png  "Page des scores")
  
  * ComandBar : Suppression des scores
  * ListBox : Affichage de la liste des scores dans une ListBox
  * Utilisation du Binding
    
    XAML
    
    ```
    ItemsSource="{Binding Mode=TwoWay}" 
    DisplayMemberPath="joueur"
    ```
    C#
    
     ```
    lbListeScores.DataContext = gererScore.TabScores;
    ```
    Ou TabScore est une ArrayList
  
  * Tri des scores du plus haut au plus bas
  * Sauvegarde des scores dans un fichier XML
  
    ```
    <Joueurs> 
      <Joueur> 
        <pseudo>Nicolas</pseudo> 
        <score>9</score> 
      </Joueur> 
      <Joueur> 
        <pseudo>Nicolas</pseudo> 
        <score>9</score> 
      </Joueur> 
    </Joueurs>
    ```
### Paramètres

  ![Page Scores](/Images/Parametres.png  "Page des paramètres")
  
  * Les paramètres sont stocké et récupérés dans les LocalSettings

## Spécifications techniques

* Système d'exploitation requis : Windows 10 / Windows 10 mobile
* Langage de programmation : UWP (C#/XAML)
* Orientation du terminal : Paysage

## Installation

1. Récupérer le projet sur votre pc Windows 10
2. Aller dans le dossier suivant :
   `Projet_Protect_The_Planet/Projet_Protect_The_Planet/AppPackages/Protect The Planet_1.0.2.0_Test/`
    
3. Clic droit sur `Add-AppDevPackage.ps1` -> Exécuter avec PowerShell 
4. Une fenêtre PowerShell s'ouvre 
5. Entre O puis appuyer sur Entrée 
6. Un autre message apparaît ensuite, appuyer sur Entrée pour continuer 
7. Autoriser PowerShell dans le Controle de compte d'utilisateur 
8. Une autre fenêtre PowerShell apparaît, entrer U pour valider l'installation du cerficat 
9. Le projet se déploie 
10. Enjoy

## Contributeurs

#### Nicolas ORLANDINI

## Contact

Vous pouvez me donner vos retours, si jamais vous constatez un problème ou une amélioration possible, merci de nous contacter à l'adresse suivante :
##### nicolas.orlandini@outlook.fr
