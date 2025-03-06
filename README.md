# Asklepios
A system supporting and offering support for medical facilities.It is a complete system that allows you to control and support medical facilities in everyday activities such as: treating patients, system management, performing tests, etc. It is a full-stack application using .Net on the backend and a GUI created using Vue and Tyepscript.

## Instalator
Short version of the installation file: **Instalator.md**

## Stach & Technologies:
- C# 11
- .NET 7.0
- .NET JWT
- Entity Framework Core
- Fluent Validation
- PostgreSQL
- Docker
- SendGrid
- Seq
- RabbitMQ
- Hangfire

## Scope of the Application
The application provides a comprehensive hospital management system with the following features:

- User Registration and Login: Secure registration and authentication for system users.
- User Account Generation with Role Assignment: Automatic creation of user accounts with roles such as doctor, nurse, patient, and administrator.
- User Access Level Management: Configuration and enforcement of user-specific access permissions.
- Administrative Functions:System management, issue identification and resolution, employee account generation, and role assignment.
- Storage of Personal and Professional Data: Secure storage of personal information and medical staff specializations.
- Patient Management: Collecting, analyzing, and processing patient data, including treatment history, test results, and surgeries.
- Integrated Medical Documentation: Archiving patient data, generating discharge summaries and prescriptions, and reducing paper documentation.Facility Structure Management: Managing departments, operating room lists, and patient wards.
- Staff Assignment and Infrastructure Monitoring: Assigning doctors to departments and tracking resource utilization across the facility.
- Procedure and Surgery Management Module: Planning surgeries and procedures, allocating operating rooms, and scheduling staff.
- Diagnostic Integration: Incorporating test results to streamline diagnostic and outpatient processes.
- Real-Time Communication Functions: Facilitating the exchange of information and notifications for urgent incidents.
- Outpatient Clinic Operations: Patient registration for appointments and efficient visit management.
- Email Notifications: Sending reminders for scheduled appointments.
- Data Cleanup and Optimization: Deleting unnecessary data (e.g., old discharge summaries or appointments) to save storage space and reduce data loss risks.

## Database
The project uses a Postgres database using Docker. Docker settings are in the <b>docker-compose.yml</b> file.
For the database to work properly, create a migration and apply the appropriate data in the file <b>appsettings.json</b> in the Asklepios.Api project on line 16 <b>"connectionString"</b> change the database path to your database path.

## Automatic table creation and data seeding

The application has been designed to automatically create a database structure and fill it with sample data during the first run. This functionality is based on **migrations** and **seeders**, which:

- **Creates all required tables** in the database.
- **Adds sample data** (seed data), making it easier to test and run the application for the first time.

### Seed configuration
To activate this function, set the appropriate option in the `appsettings.json` file:

```json
"SeedData": {
  "Enable": true
}
```

## SendGrid
To activate this function, set the appropriate option in the `appsettings.json` file:

```json
"sendgrid": {
    "keySensGrid": ""
  }
```

## Performing background tasks – Hangfire

The application supports the execution of background tasks using the **Hangfire** service, which allows you to manage asynchronous and scheduled processes, such as sending notifications, processing data or other operations that require independent action from the user.

### Hangfire service activation
To enable background task functionality, change the appropriate setting in the `appsettings.json` file. In the `Hangfire` section, set the `Enable` value to `true`:

```json
"Hangfire": {
  "Enable": true
}
```

## How to run
1. Make sure you have Docker and Docker Compose installed.
2. Copy the project repository.
3. In the Asklepios directory -> Asklepios.Api -> appsettings.json set the SendGrid key
4. In the project root directory run:
```bash
docker-compose up --build
```

## Seq
The project uses Seq for structured logging and event monitoring. Seq allows you to collect, filter, and analyze logs from your application in real-time, helping you diagnose issues, track performance, and monitor system behavior. In this project, Seq is deployed as a Docker container.
- Docker Configuration: Seq is set up using Docker, and the settings are defined in the docker-compose.yml file.
- Ports: Seq runs on port 5341 (mapped to port 80 inside the container), which can be accessed to view and manage log data through its web interface.

## Database Diagram
![](/assets/DatabaseDiagram.png)

## Defined accounts with login details

| Email                           | Password | Role         |
|---------------------------------|----------|--------------|
| admin@asklepios.com            | password | Admin         |
| kamilmatusz@asklepios.com      | password | Doctor        |
| miloszmichalski@asklepios.com  | password | Doctor        |
| annapajak@asklepios.com        | password | Nurse         |
| andrzejkowalski@asklepios.com  | password | Doctor        |
| martapiotrowska@asklepios.com  | password | Nurse         |
| lukasznawrocki@asklepios.com   | password | Pacjent       |
| grzegorzadamczyk@asklepios.com | password | Doctor        |
| barbaranowak@asklepios.com     | password | Nurse         |

### Paths to additional dashboards
Hangfire:
[http://localhost:8080/hangfire](http://localhost:8080/hangfire)

Swagger Documentation:
[http://localhost:8080/swagger](http://localhost:8080/swagger)

Health Check:
[http://localhost:5102/health-ui#/healthchecks](http://localhost:5102/health-ui#/healthchecks)
