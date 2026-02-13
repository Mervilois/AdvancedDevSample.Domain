# 🚀 Guide d'Installation

## Prérequis

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (ou VS Code)

## Installation

### 1. Cloner le dépôt
```bash
git clone https://github.com/Mervilois/AdvancedDevSample.Domain
cd AdvancedDevSample.Domain

2. Restaurer les packages
dotnet restore

3. Compiler le projet
dotnet build

4. Lancer l'API
cd AdvancedDevSample.Api
dotnet run

5. Accéder à Swagger
Ouvrez votre navigateur à l'adresse : https://localhost:5031/swagger