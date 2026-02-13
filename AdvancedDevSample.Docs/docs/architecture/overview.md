
```markdown
# 🏗️ Vue d'ensemble de l'Architecture

## 📐 Clean Architecture

Notre projet implémente une **Clean Architecture** avec une séparation stricte des responsabilités en 4 couches :

```mermaid
graph TD
    subgraph "Couche API"
        A[Controllers] --> B[Middleware]
    end
    
    subgraph "Couche Application"
        C[Services] --> D[DTOs]
    end
    
    subgraph "Couche Domain"
        E[Entities] --> F[Interfaces]
        E --> G[Exceptions]
    end
    
    subgraph "Couche Infrastructure"
        H[Repositories] --> I[(In-Memory DB)]
    end
    
    B --> C
    C --> E
    C --> H
    H --> I


```markdown
## 📊 Diagramme de Classes

```mermaid
classDiagram
    class Product {
        + Guid Id
        + string Name
        + decimal Price
        + bool IsActive
        + int StockQuantity
        + Guid? SupplierId
        + ChangePrice()
        + UpdateStock()
    }

    class Customer {
        + Guid Id
        + string FirstName
        + string LastName
        + string Email
        + string FullName
    }

    class Supplier {
        + Guid Id
        + string Name
        + string ContactName
        + string Email
    }

    class Order {
        + Guid Id
        + string OrderNumber
        + Guid CustomerId
        + OrderStatus Status
        + decimal TotalAmount
        + List~OrderItem~ Items
        + AddItem()
        + CalculateTotal()
    }

    class OrderItem {
        + Guid ProductId
        + int Quantity
        + decimal UnitPrice
        + decimal Subtotal
    }

    class OrderStatus {
        <<enumeration>>
        Pending
        Confirmed
        Processing
        Shipped
        Delivered
        Cancelled
    }

    Customer "1" --> "0..*" Order
    Order "1" --> "1..*" OrderItem
    OrderItem "*" --> "1" Product
    Product "*" --> "0..1" Supplier
    Order --> OrderStatus