# Student Console Manager

Application console C# .NET 9 pour gérer les informations des étudiants avec persistance JSON.

## Fonctionnalités

| Option | Description |
|--------|-------------|
| 1 | Afficher tous les étudiants |
| 2 | Rechercher un étudiant par School ID |
| 3 | Ajouter un étudiant |
| 4 | Modifier un étudiant |
| 5 | Supprimer un étudiant |
| 6 | Supprimer **tous** les étudiants (protégé par mot de passe) |
| 7 | Sauvegarder les données |
| 8 | Sauvegarder et quitter |
| 9 | Effacer la console |

## Prérequis

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

## Utilisation

```bash
dotnet run
```

## Structure

```
├── Program.cs                # Point d'entrée
├── Models/Student.cs         # Modèle Student
├── Services/StudentService.cs # Logique métier + persistance JSON
├── View/Menu.cs              # Interface console
└── studentsData.json         # Données (auto-généré)
```

## Mot de passe

La suppression totale (`option 6`) est protégée. Configurez via variable d'environnement :

```bash
export STUDENT_MANAGER_SECRET="votre_mot_de_passe"
```

Par défaut : `my safe password`.
