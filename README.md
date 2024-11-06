Parcel Delivery App

This project is a Parcel Delivery Application with a microservices architecture. It consists of multiple services managed by an API Gateway implemented with Ocelot and is containerized for ease of deployment and testing.
The Parcel Delivery App is designed to provide core functionality for parcel management, including user registration, parcel tracking, and order assignment. It supports the following roles:

User: Can create, view, update, and cancel parcel orders.
Admin: Can manage parcel orders, assign couriers, and track deliveries.
Courier: Can view and update assigned orders.
The app’s microservices are independently deployable, and all routing is managed through an API Gateway using Ocelot.

Architecture
The project uses a microservices architecture with the following main components:
![image](https://github.com/user-attachments/assets/07fba30d-5aa6-47de-94ef-a7bb20c72b7d)


Auth Service: Manages user authentication and registration.
Order Service: Manages parcel orders, allowing users to create, view, and modify orders.
Tracking Service: Allows admins to track parcels and assign orders to couriers.

API Gateway (Ocelot): Serves as the entry point, directing requests to the appropriate services.

Auth Service
Handles authentication and authorization for Users, Admins, and Couriers.

Endpoints:
POST /auth/register - Registers a new user.
POST /auth/login - Logs in a user, admin, or courier.
GET /auth/me - Retrieves information about the current user.
Order Service
Manages parcel delivery orders and related operations.

Endpoints:
POST /orders - Creates a new order.
PATCH /orders/{id}/status - Allows Admin to change the status of an order.
PATCH /orders/{id}/destination - Allows the user to change the destination (under certain conditions).
DELETE /orders/{id} - Allows the user to cancel an order (under certain conditions).

Tracking Service
Handles tracking of orders and assignment of couriers.

Endpoints:
GET /tracking/orders/{id} - Allows Admin to track an order by coordinates.
POST /tracking/orders/{id}/assign - Allows Admin to assign a courier to a delivery.
GET /tracking/couriers - Retrieves a list of all couriers with their statuses.
API Gateway
The API Gateway, built with Ocelot, routes incoming requests to the appropriate microservices. The routing configuration is specified in ocelot.json, which includes route templates and service host information.


Prerequisites
.NET 8 SDK
Docker
Docker Compose
Installation

Clone the repository:

bash
Copy code
git clone 
cd ParcelDeliveryApp
Build and run the services with Docker Compose:

bash
Copy code
docker-compose up --build
Access the API Gateway at http://localhost:5000.

Configuration
Each service can be configured in its appsettings.json file for settings such as database connections. The ocelot.json file in the ApiGateway folder configures routing for Ocelot.

Testing the API Gateway
You can test the endpoints through the API Gateway using the following routes:

Auth Service:

POST http://localhost:5000/api/auth/login
POST http://localhost:5000/api/auth/register
Order Service:

POST http://localhost:5000/api/orders (User creates an order)
PATCH http://localhost:5000/api/orders/{id}/status (Admin changes order status)
Tracking Service:

GET http://localhost:5000/api/tracking/orders/{id} (Admin tracks an order by ID)
