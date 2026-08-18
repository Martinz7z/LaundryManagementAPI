# Laundry Management API - Design

## Overview

The Laundry Management API is designed to manage customers, laundry services, orders and the individual services added to each order.

The system uses a REST API built with ASP.NET Core. Data is stored using SQLite and Entity Framework Core.

## Main Entities

The system contains four main entities:

- Customer
- Order
- Service
- OrderItem

A customer can have many orders. An order can contain many order items. Each order item references a laundry service.

---

## Use Case 1 - Manage Customers

### Description
A laundry employee can add and manage customer details.

### Acceptance Criteria

- A new customer can be created.
- All customers can be retrieved.
- A customer can be retrieved using their ID.
- Customer details can be updated.
- A customer can be deleted.
- A request for a customer that does not exist returns 404.

---

## Use Case 2 - Manage Laundry Services

### Description
A laundry employee can manage the services offered by the business.

### Acceptance Criteria

- A new laundry service can be created.
- All available services can be retrieved.
- A service can be retrieved using its ID.
- Service details and price can be updated.
- A service can be deleted.
- A request for a service that does not exist returns 404.

---

## Use Case 3 - Create and Manage Orders

### Description
A laundry employee can create an order for an existing customer and update the order as it progresses.

### Acceptance Criteria

- An order can only be created for an existing customer.
- A new order has a default status of Pending.
- An order stores the customer ID.
- Order information includes the customer's name.
- The order status can be updated.
- The collection date can be updated.
- An order can be deleted.

---

## Use Case 4 - Add Services to an Order

### Description
A laundry employee can add laundry services to an existing customer order.

### Acceptance Criteria

- An order item can only be added to an existing order.
- The selected laundry service must exist.
- Quantity must be greater than zero.
- The service price is obtained from the database rather than supplied by the client.
- The subtotal is calculated using price multiplied by quantity.
- Updating an item's quantity recalculates the order total.
- Removing an item recalculates the order total.
- The total price of an order is calculated from its order items.

---

## API Routes

### Customers

- GET `/api/customers`
- GET `/api/customers/{id}`
- POST `/api/customers`
- PUT `/api/customers/{id}`
- DELETE `/api/customers/{id}`

### Services

- GET `/api/services`
- GET `/api/services/{id}`
- POST `/api/services`
- PUT `/api/services/{id}`
- DELETE `/api/services/{id}`

### Orders

- GET `/api/orders`
- GET `/api/orders/{id}`
- POST `/api/orders`
- PUT `/api/orders/{id}`
- DELETE `/api/orders/{id}`

### Order Items

- GET `/api/orderitems`
- GET `/api/orderitems/{id}`
- POST `/api/orderitems`
- PUT `/api/orderitems/{id}`
- DELETE `/api/orderitems/{id}`