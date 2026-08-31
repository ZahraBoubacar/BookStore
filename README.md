# Kitab

Librairie en ligne — catalogue, panier, bibliothèque personnelle, quiz littéraire et assistant IA (KitabAI).

**Stack :** ASP.NET Core 8 MVC · authentification cookie · données mock en mémoire

**Démo vidéo :** *(lien à venir)*  
**Admin :** `/admin`  
**Compte démo :** `marie@exemple.fr` · `password123`

---

## Aperçu

| Côté client | Côté admin / compte |
| ----------------------------- | -------------------------------------- |
| Catalogue par catégorie + auteurs | CRUD livres |
| Fiche livre (détails, prix TND, note) | Tableau de bord utilisateur |
| Panier + commande | Historique des commandes |
| Bibliothèque personnelle (statut, notes, défis) | Espace admin |
| KitabAI — recommandations conversationnelles | |
| Quiz littéraire | |
| Auth (inscription / connexion) | |
| Responsive | |

---

## Stack technique

- **Backend / UI :** ASP.NET Core 8 MVC, Razor Views  
- **Auth :** Cookie Authentication + Session  
- **Données :** services mock en mémoire (aucune base de données requise pour démarrer)  
- **Front :** HTML, CSS, JS (`wwwroot`)  
- **Devise :** TND  

---

## Structure du projet

```
BookStore/
├── Controllers/     # Account, Admin, Authors, Books, Cart, Home, KitabAI, Library, Quiz
├── Models/          # Entités + ViewModels
├── Services/        # Métier + mocks
├── Views/           # Razor
├── wwwroot/         # css, js, images
├── Program.cs
├── appsettings.json
└── BookStore.csproj
```

---

## Fonctionnalités clés

1. **Librairie complète** — catalogue, auteurs, fiches livres, panier et commande  
2. **Bibliothèque personnelle** — statut de lecture, notes, défis  
3. **KitabAI** — recommandations par règles locales (sans API externe)  
4. **Quiz littéraire**  
5. **Auth & commandes** — inscription, connexion, tableau de bord, historique  
6. **Admin** — gestion des livres (CRUD)  
7. **Routes en français** — `/compte`, `/bibliotheque`, `/panier`, `/livres`, …

---

## Installation

**Prérequis :** [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

```bash
cd BookStore
dotnet restore
dotnet run
```

Puis ouvrir l’URL affichée (ex. `https://localhost:57340`).

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

- Produit complet : du catalogue à la commande, avec bibliothèque, quiz et IA  
- Stack pro : ASP.NET Core 8, auth cookie, architecture MVC claire  
- Démo immédiate grâce aux données mock (facile à brancher plus tard sur une BDD)  
- KitabAI sans dépendance à une API payante  

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
