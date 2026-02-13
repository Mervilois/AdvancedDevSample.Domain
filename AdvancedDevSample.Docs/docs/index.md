# 🚀 Bienvenue dans la documentation d'AdvancedDevSample

## 📋 Présentation du Projet

**AdvancedDevSample** est une API REST développée en **.NET 10** qui permet de gérer un système complet de :

- ✅ **Produits** : Gestion des produits avec suivi de stock
- ✅ **Clients** : Gestion des informations clients
- ✅ **Fournisseurs** : Gestion des fournisseurs
- ✅ **Commandes** : Création et suivi de commandes

## 🏗️ Architecture du Projet

Le projet suit une architecture en couches (Clean Architecture) :

┌─────────────────────────────────────┐
│ API (Contrôleurs) │
├─────────────────────────────────────┤
│ Application (Services) │
├─────────────────────────────────────┤
│ Domain (Entités) │
├─────────────────────────────────────┤
│ Infrastructure (Repositories) │
└─────────────────────────────────────┘


## 🛠️ Technologies Utilisées

- **.NET 10** - Framework principal
- **Entity Framework Core** - ORM pour l'accès aux données
- **Swagger/OpenAPI** - Documentation interactive
- **xUnit** - Tests unitaires et d'intégration
- **SonarCloud** - Analyse de qualité de code

## 📊 Qualité du Code

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=votre-projet&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=votre-projet)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=votre-projet&metric=coverage)](https://sonarcloud.io/summary/new_code?id=votre-projet)

## 🚀 Démarrage Rapide

```bash
# 1. Cloner le projet
git clone https://github.com/Mervilois/AdvancedDevSample.Domain.git

# 2. Restaurer les packages
dotnet restore

# 3. Lancer l'API
cd AdvancedDevSample.Api
dotnet run

# 4. Accéder à Swagger
https://localhost:5031/swagger

📈 Statut du Projet
Composant	Statut	Couverture
Produits	✅ Complété	85%
Clients	✅ Complété	82%
Fournisseurs	✅ Complété	80%
Commandes	✅ Complété	78%

