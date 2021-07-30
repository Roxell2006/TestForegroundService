# TestForegroundService
Test d'une tâche de fond avec XamarinForms

Dans le projet commun, grâce à l'interface IService, on crée un service de dépendance qui appelera les méthodes Start/Stop des projets natifs .android et .ios 
(pour l'instant il n'y a que la partie android)

Dans le projet Android j'ai 3 fichiers:

DataSource.cs
C'est dans ce fichier que se trouve le code qui doit s'excécuter en tâche de fond, ce fichier doit hériter de Service.
Ici pour le test on affiche juste une "horloge" avec un DateTime.Now que j'actualise chaque seconde.
mais on pourrait tout aussi bien appeler un service gps, un appel à une api etc... 

NotificationHelper.cs 
C'est pour la création, l'actualisation et les options de la notification.
L'"horloge" sera affiché dans cette notification et sera dispo même si l'application est kill.
Si on clique sur la notification elle réouvrira l'application.

ServiceHelper
C'est le service qui sera apelé depuis le projet commun grâce à l'interface et au service de dépendance.
