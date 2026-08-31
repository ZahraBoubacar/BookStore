# Kitab

Librairie en ligne — catalogue, panier, bibliothèque personnelle, quiz littéraire et assistant IA (KitabAI).

**Stack :** ASP.NET Core 8 MVC · authentification cookie · données mock en mémoire

> **Démo vidéo** — [à ajouter : lien YouTube / Loom / Drive]  
> *(Remplace cette ligne par ton lien une fois la vidéo en ligne)*

**Lancer en local :** voir section [Installation](#installation) ci-dessous.

**Admin :** `/admin`  
**Compte démo :** `marie@exemple.fr` · mot de passe `password123`

---

## Aperçu

| Côté client | Côté admin / compte |
| ----------------------------- | -------------------------------------- |
| Catalogue par catégorie + auteurs | CRUD livres (ajout / modification / suppression) |
| Fiche livre (détails, prix TND, note) | Tableau de bord utilisateur |
| Panier + commande | Historique des commandes |
| Bibliothèque personnelle (statut, notes, défis) | Espace admin livres |
| KitabAI — recommandations conversationnelles | — |
| Quiz littéraire | — |
| Auth (inscription / connexion) | — |
| Responsive | — |

---

## Vidéo démo

<!-- Une fois ta vidéo en ligne, décommente et adapte une des options ci-dessous -->

**Option A — lien simple :**

🎥 [Voir la démo vidéo](https://www.youtube.com/watch?v=TON_ID)

**Option B — intégration GitHub (aperçu) :**

<!-- Remplace TON_ID par l’ID YouTube -->
<!--
[![Démo Kitab](https://img.youtube.com/vi/TON_ID/maxresdefault.jpg)](https://www.youtube.com/watch?v=TON_ID)
-->

**Option C — fichier local / Drive :**

- Dépose la vidéo dans le repo (ex. `docs/demo.mp4`) ou un lien public Drive/Loom  
- Puis : 🎥 [Télécharger / voir la démo](docs/demo.mp4)

> **Astuce :** 1–2 minutes suffisent — accueil → fiche livre → panier → KitabAI → bibliothèque → admin.

---

## Captures d’écran

*(Ajoute tes images dans un dossier `docs/` ou `screenshots/` et mets à jour les chemins)*

| Accueil / catalogue | Fiche livre | Panier |
| :-----------------: | :---------: | :----: |
| *à ajouter* | *à ajouter* | *à ajouter* |

| Bibliothèque | KitabAI | Admin |
| :----------: | :-----: | :---: |
| *à ajouter* | *à ajouter* | *à ajouter* |

Exemple une fois les fichiers prêts :

```markdown
![Accueil](docs/home.png)
![KitabAI](docs/kitabai.png)
```

---

## Stack technique

- **Backend / UI :** ASP.NET Core 8 MVC, Razor Views
- **Auth :** Cookie Authentication + Session
- **Données :** services mock en mémoire (pas de base de données requise pour démarrer)
- **Front :** HTML, CSS, JS (wwwroot)
- **Devise affichée :** TND (helper dédié)
- **Hébergement possible :** Azure App Service, IIS, Docker, etc.

---

## Structure du projet

```
BookStore/
├── Controllers/          # Account, Admin, Authors, Books, Cart, Home, KitabAI, Library, Quiz
├── Models/               # Entités + ViewModels
├── Services/             # Métier + mocks (livres, auth, panier, bibliothèque, commandes)
├── Views/                # Vues Razor
├── wwwroot/              # css, js, images
├── Program.cs            # DI, auth, routes FR
├── appsettings.json
├── BookStore.csproj
└── README.md
```

---

## Fonctionnalités clés

1. **Librairie complète** — catalogue, auteurs, fiches livres, panier et commande  
2. **Bibliothèque personnelle** — statut de lecture, notes, défis de lecture  
3. **KitabAI** — assistant de recommandation par règles locales (sans API externe payante)  
4. **Quiz littéraire** — interaction ludique autour des livres  
5. **Auth & commandes** — inscription, connexion, tableau de bord, historique  
6. **Admin** — gestion des livres (CRUD)  
7. **UX française** — routes en français (`/compte`, `/bibliotheque`, `/panier`, `/livres`, …)

---

## Installation

**Prérequis :** [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

```bash
cd BookStore
dotnet restore
dotnet run
```

Ouvre l’URL affichée (ex. `https://localhost:57340`).

| Rôle | Accès |
|------|--------|
| Boutique | `/` |
| Connexion | `/compte/connexion` |
| Bibliothèque | `/bibliotheque` |
| KitabAI | `/kitabai` |
| Quiz | `/quiz` |
| Admin | `/admin` |

**Compte démo :** `marie@exemple.fr` / `password123`

---

## Points forts pour le portfolio

- **Produit complet** : du catalogue à la commande, avec couche “expérience lecture” (bibliothèque, quiz, IA)  
- **Stack pro** : ASP.NET Core 8, auth cookie, architecture MVC claire (Controllers / Services / Views)  
- **Pas de dette infra** : données mock → démo immédiate, facile à brancher plus tard sur une vraie BDD  
- **KitabAI** : valeur ajoutée différenciante sans dépendance à une API payante  
- **Prêt GitHub** : `.gitignore` .NET, README orienté portfolio, code source uniquement

---

## Auteure

**Zahra Boubacar**  
Data Analyst · Business Intelligence · Data Science  
Master Data Science & Software Development — ESEN (Tunisie)

- GitHub : [ZahraBoubacar](https://github.com/ZahraBoubacar)
- LinkedIn : [zahra-boubacar](https://www.linkedin.com/in/zahra-boubacar)
- Email : zahraboubacar9@gmail.com

---

## Licence

Projet éducatif / démonstration. Libre d’utilisation et de modification (crédits appréciés).

---

Fait avec ❤️ pour la lecture.
