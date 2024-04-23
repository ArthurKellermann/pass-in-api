# PassIn API

PassIn is a web application developed using ASP.NET Core, designed to manage event attendees and check-ins efficiently. It provides a service for event organizers to register attendees, track event participation, and perform check-ins.

## Technologies Used
- **ASP.NET Core:** The application is built on the ASP.NET Core framework.
- **Entity Framework Core:** PassIn utilizes Entity Framework Core for database operations.
- **SQLite:** SQLite is employed for data storage.
- **FluentValidation:** Form validation is handled using FluentValidation.
- **Swagger:** Swagger is integrated to provide interactive API documentation.

## What I Learned
During the development of this project, I gained experience in:

- Implementing Clean Architecture and SOLID principles to structure the project.
- Configuring and using Swagger for API documentation.

  ## Features
- **Attendee Registration:** Event organizers can register attendees for their events, providing necessary details such as name, email, etc.
- **Event Management:** Organizers can manage events, including creating, updating, and deleting events.
- **Check-In:** The application allows event organizers to perform attendee check-ins quickly and accurately during events.
- **API Integration:** PassIn offers a RESTful API for seamless integration with other applications and services.

## Installation
1. Clone the repository.
2. Navigate to the project directory and build the solution.
3. Configure the database connection string in the appsettings.json file.
4. Run the application.

## API Routes

### Attendees

- **Register Attendee on Event**
  - **Method:** POST
  - **Route:** `/api/attendees/{eventId}/register`
  - **Description:** Registers an attendee for a specific event.
  
- **Get All Attendees by Event ID**
  - **Method:** GET
  - **Route:** `/api/attendees/{eventId}`
  - **Description:** Retrieves all attendees for a specific event.

### Check-Ins

- **Check-In Attendee**
  - **Method:** POST
  - **Route:** `/api/checkin/{attendeeId}`
  - **Description:** Performs check-in for a specific attendee.

### Events

- **Register Event**
  - **Method:** POST
  - **Route:** `/api/events`
  - **Description:** Registers a new event.
  
- **Get Event by ID**
  - **Method:** GET
  - **Route:** `/api/events/{id}`
  - **Description:** Retrieves event details by ID.

## License

MIT
