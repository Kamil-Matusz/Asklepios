# Asklepios
A system supporting and offering support for medical facilities.It is a complete system that allows you to control and support medical facilities in everyday activities such as: treating patients, system management, performing tests, etc. It is a full-stack application using .Net on the backend and a GUI created using Vue and Tyepscript.

## Stach & Technologies:
- C# 11
- .NET 7.0
- .NET JWT
- Entity Framework Core
- Fluent Validation
- PostgreSQL
- Docker
- Azure Key Vault
- SendGrid
- Seq

## Database
The project uses a Postgres database using Docker. Docker settings are in the <b>docker-compose.yml</b> file. 
For the database to work properly, create a migration and apply the appropriate data in the file <b>appsettings.json</b> in the Asklepios.Api project on line 16 <b>"connectionString"</b> change the database path to your database path.

## SendGrid
To send emails, this project uses the SendGrid API. If you wish to use this functionality, you need to configure your own SendGrid account and set up your API key.
- Obtain an API key from SendGrid.
- Add your API key to the <b>secret.json</b>
- Make sure to replace "YOUR_API_KEY" with your actual SendGrid API key.

### Database Diagram
![](/assets/DatabaseDiagram.png)
