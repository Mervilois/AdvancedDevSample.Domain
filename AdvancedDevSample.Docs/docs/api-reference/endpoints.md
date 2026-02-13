
```markdown
# 📋 Référence des Endpoints API

## 🏷️ Produits (`/api/products`)

| Méthode | Endpoint | Description | Corps Requis |
|---------|----------|-------------|--------------|
| POST | `/` | Créer un produit | `CreateProductRequest` |
| GET | `/` | Liste tous les produits | - |
| GET | `/{id}` | Détail d'un produit | - |
| PUT | `/{id}` | Modifier un produit | `UpdateProductRequest` |
| DELETE | `/{id}` | Désactiver un produit | - |
| PUT | `/{id}/price` | Changer le prix | `ChangePriceRequest` |

### Exemple de Requête
```json
POST /api/products
{
    "name": "Ordinateur Portable",
    "price": 999.99,
    "description": "15 pouces, 16GB RAM"
}


👥 Clients (/api/customers)
Méthode	Endpoint	Description	Corps Requis
POST	/	Créer un client	CreateCustomerRequest
GET	/	Liste tous les clients	-
GET	/{id}	Détail d'un client	-
PUT	/{id}	Modifier un client	UpdateCustomerRequest
DELETE	/{id}	Désactiver un client	-
GET	/search	Rechercher clients	?term=

🏭 Fournisseurs (/api/suppliers)

Méthode	Endpoint	Description	Corps Requis
POST	/	Créer un fournisseur	CreateSupplierRequest
GET	/	Liste fournisseurs	-
GET	/{id}	Détail fournisseur	-
PUT	/{id}	Modifier fournisseur	UpdateSupplierRequest
DELETE	/{id}	Désactiver fournisseur	-
GET	/search	Rechercher	?term=



🛒 Commandes (/api/orders)
Méthode	Endpoint	Description	Corps Requis
POST	/	Créer commande	CreateOrderRequest
GET	/	Liste commandes	-
GET	/{id}	Détail commande	-
PUT	/{id}/status	Changer statut	UpdateOrderStatusRequest
POST	/{id}/items	Ajouter produit	OrderItemRequest
DELETE	/{id}	Annuler commande	-


📊 Codes Statut HTTP
Code	Description
200 OK	Requête réussie
201 Created	Ressource créée
204 NoContent	Succès sans contenu
400 BadRequest	Erreur de validation
404 NotFound	Ressource non trouvée
500 InternalServerError	Erreur serveur

