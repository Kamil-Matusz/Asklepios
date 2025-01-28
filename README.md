# Asklepios
A system supporting and offering support for medical facilities.It is a complete system that allows you to control and support medical facilities in everyday activities such as: treating patients, system management, performing tests, etc. It is a full-stack application using .Net on the backend and a GUI created using Vue and Tyepscript.

##
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

## SendGrid
To send emails, this project uses the SendGrid API. If you wish to use this functionality, you need to configure your own SendGrid account and set up your API key.
- Obtain an API key from SendGrid.
- Add your API key to the <b>secret.json</b>
- Make sure to replace "YOUR_API_KEY" with your actual SendGrid API key.

## Seq
The project uses Seq for structured logging and event monitoring. Seq allows you to collect, filter, and analyze logs from your application in real-time, helping you diagnose issues, track performance, and monitor system behavior. In this project, Seq is deployed as a Docker container.
- Docker Configuration: Seq is set up using Docker, and the settings are defined in the docker-compose.yml file.
- Ports: Seq runs on port 5341 (mapped to port 80 inside the container), which can be accessed to view and manage log data through its web interface.

## Database Diagram
![](/assets/DatabaseDiagram.png)

### Paths to additional dashboards
Hangfire:
`http://<your-domain>/hangfire`

Swagger Documentation:
`http://<your-domain>/swagger`
