# Student Console Manager

Une application console C# pour gérer les informations des étudiants avec persistance des données en JSON.

## 📋 Description

**Student Console Manager** est une application console interactive développée en C# qui permet de gérer une base de données d'étudiants. L'application offre une interface utilisateur conviviale pour :

- ➕ Ajouter de nouveaux étudiants
- 📖 Consulter la liste des étudiants
- ✏️ Modifier les informations des étudiants
- 🗑️ Supprimer des étudiants
- 💾 Persister les données dans un fichier JSON

## 🛠️ Technologie

- **Langage** : C# (.NET 9.0)
- **Type de projet** : Application Console
- **Stockage des données** : JSON (fichier `studentsData.json`)

## 📦 Prérequis

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) ou supérieur
- Windows, macOS, ou Linux

## 🚀 Installation

### 1. Cloner le repository

```bash
git clone https://github.com/votre-nom-utilisateur/studentConsoleManager.git
cd studentConsoleManager
```

### 2. Restaurer les dépendances

```bash
dotnet restore
```

### 3. Compiler le projet

```bash
dotnet build
```

## 🎮 Utilisation

Pour lancer l'application :

```bash
dotnet run
```

L'application affichera un menu interactif vous permettant de :

1. **Ajouter un étudiant** - Entrez les informations requises (ID d'école, nom, prénom, classe, téléphone, email)
2. **Afficher tous les étudiants** - Liste complète de tous les étudiants enregistrés
3. **Rechercher un étudiant** - Trouvez un étudiant par ID ou nom
4. **Modifier un étudiant** - Mettez à jour les informations d'un étudiant
5. **Supprimer un étudiant** - Supprimez un étudiant de la base de données
6. **Quitter** - Fermer l'application

## 📁 Structure du Projet

```
studentConsoleManager/
├── Program.cs              # Point d'entrée de l'application
├── studentConsoleManager.csproj    # Fichier de configuration du projet
├── studentConsoleManager.sln       # Solution Visual Studio
├── studentsData.json       # Fichier de stockage des données
├── Models/
│   └── Student.cs          # Modèle de classe Student
├── Services/
│   └── StudentService.cs   # Logique métier pour la gestion des étudiants
├── View/
│   └── Menu.cs             # Interface utilisateur / Menu
└── bin/, obj/              # Dossiers de compilation

```

## 👨‍🎓 Modèle Student

La classe `Student` contient les propriétés suivantes :

| Propriété | Type | Description |
|-----------|------|-------------|
| `Id` | int | Identifiant unique auto-généré |
| `SchoolId` | string | Identifiant scolaire (requis) |
| `Name` | string | Nom de l'étudiant (requis) |
| `FirstName` | string | Prénom de l'étudiant |
| `ClassName` | string | Nom de la classe |
| `Phone` | string | Numéro de téléphone |
| `Email` | string | Adresse email |

## 🔐 Sécurité

⚠️ **Note** : Le projet contient actuellement un mot de passe codé en dur à titre d'exemple. Pour une utilisation en production, il est recommandé d'utiliser [User Secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets) ou un gestionnaire de secrets.

## 💾 Persistence des données

Les données sont automatiquement sauvegardées dans le fichier `studentsData.json` au format JSON. Ce fichier est créé automatiquement à la première exécution.

## 🤝 Contribution

Les contributions sont les bienvenues ! N'hésitez pas à :

1. Fork le projet
2. Créer une branche pour votre fonctionnalité (`git checkout -b feature/AmazingFeature`)
3. Commit vos changements (`git commit -m 'Add some AmazingFeature'`)
4. Push vers la branche (`git push origin feature/AmazingFeature`)
5. Ouvrir une Pull Request

## 📄 Licence

Ce projet est sous licence [MIT](LICENSE). Voir le fichier `LICENSE` pour plus de détails.

## ✉️ Contact

Pour toute question ou suggestion, n'hésitez pas à ouvrir une issue sur le repository.

---

**Développé avec ❤️ en C#**
