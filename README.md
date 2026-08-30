# Kitab — Librairie en ligne (ASP.NET Core 8)

**Kitab** est une application web de librairie en ligne développée avec **ASP.NET Core 8 MVC**.  
Elle propose un catalogue de livres, un panier, une bibliothèque personnelle, un quiz littéraire et un assistant IA conversationnel (mock).

## Fonctionnalités

- 📚 Catalogue de livres (détails, catégories, auteurs)
- 🛒 Panier et processus de commande
- 👤 Authentification (cookies) — compte, tableau de bord, historique de commandes
- 📖 Bibliothèque personnelle (statut de lecture, notes, défis)
- 🤖 **KitabAI** — assistant de recommandation de livres (règles locales, sans API externe)
- ❓ Quiz littéraire
- 🛠️ Espace admin (CRUD livres)

Les données sont en mémoire (services mock) — aucun backend base de données requis pour démarrer.

## Prérequis

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Lancer le projet

```bash
cd BookStore
dotnet restore
dotnet run
```

Puis ouvrir l’URL affichée (ex. `https://localhost:57340` ou `http://localhost:57341`).

### Compte de démonstration

- Email : (selon le seed dans `Services/AuthOrderService.cs`)
- Mot de passe démo : `password123`

## Structure du projet

```
BookStore/
├── Controllers/     # Account, Admin, Authors, Books, Cart, Home, KitabAI, Library, Quiz
├── Models/          # Entités et ViewModels
├── Services/        # Logique métier + mocks (livres, auth, panier, bibliothèque)
├── Views/           # Razor views
├── wwwroot/         # CSS, JS, images
├── Program.cs       # Configuration, routes, DI
└── appsettings.json
```

## Routes principales (français)

| Route | Description |
|-------|-------------|
| `/` | Accueil |
| `/compte/connexion` | Connexion |
| `/compte/inscription` | Inscription |
| `/compte/tableau-de-bord` | Tableau de bord |
| `/bibliotheque` | Bibliothèque personnelle |
| `/kitabai` | Assistant IA |
| `/quiz` | Quiz littéraire |
| `/admin` | Administration |

## Technologies

- ASP.NET Core 8 MVC
- Cookie Authentication + Session
- Razor Views
- Données mock en mémoire

## Licence

Projet éducatif / démonstration. Libre d’utilisation et de modification.

---

Fait avec ❤️ pour la lecture.
