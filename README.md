---

## **Project Idea**

**"EventHub" – A Multi-Platform Event Management & Recommendation System**

A microservices-based platform where users can create, discover, and join events (concerts, sports, meetups, etc.).

* **PostgreSQL** will store structured, relational data (users, events, bookings).
* **MongoDB** will store flexible, document-oriented data (user activity logs, event comments, recommendations, analytics).
* The system will use **.NET** microservices for separation of concerns and scalability.

---

## **High-Level Functional Requirements**

### 1. **User Service (PostgreSQL)**

* **Features:**

    * User registration/login (JWT authentication)
    * Profile management
    * Role-based access (user, organizer, admin)
* **Data Stored in PostgreSQL:**

    * User accounts table
    * Roles table
    * Password hashes & salts

---

### 2. **Event Service (PostgreSQL)**

* **Features:**

    * Create, edit, delete events (organizers only)
    * Event search with filters (date, category, location)
    * Ticketing & availability
* **Data Stored in PostgreSQL:**

    * Events table (title, description, datetime, venue, price)
    * Tickets table (availability, reservations)

---

### 3. **Activity & Comments Service (MongoDB)**

* **Features:**

    * Store event comments & reviews
    * Keep track of user activity (searches, clicks, viewed events)
    * Store analytics data for recommendations
* **Data Stored in MongoDB:**

    * Comments collection (userId, eventId, text, timestamp)
    * Activity logs collection
    * Recommendation metadata

---

### 4. **Recommendation Service (MongoDB + PostgreSQL)**

* **Features:**

    * Recommend events based on:

        * User’s past activity (MongoDB logs)
        * Event popularity (PostgreSQL tickets sold)
    * Personalized home page feed

---

### 5. **Gateway/API Aggregator**

* **Purpose:**

    * Acts as an entry point for frontend
    * Routes requests to relevant microservices
    * Handles authentication & API key validation

---

### 6. **Non-Functional Requirements**

* **Scalability:** Each service runs independently, can be deployed separately.
* **Resilience:** Services should continue functioning if another fails.
* **Database Split:** PostgreSQL for structured, transactional data; MongoDB for unstructured or high-volume data.
* **Logging & Monitoring:**

    * Centralized logs for debugging
    * Basic metrics dashboard (e.g., Prometheus + Grafana)
* **Security:**

    * JWT tokens for authentication
    * Role-based authorization
    * Secure password hashing (e.g., bcrypt)

---

### **Tech Stack**

* **Backend:** .NET 8 (ASP.NET Core Web API)
* **Databases:**

    * PostgreSQL (relational data)
    * MongoDB (document data)
* **Architecture:** Microservices + API Gateway
* **Communication:** gRPC or REST between services
* **Containerization:** Docker + Docker Compose
* **Optional:** Kubernetes for orchestration
