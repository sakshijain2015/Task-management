# TaskManagement
Task management system built with ASP.NET Core razor pages
This is a simple task management system built using ASP.NET Core Razor Pages. It allows users to perform basic CRUD operations on tasks, which includes:
Creating, Viewing tasks, Updating, Deleting tasks

The backend of the system is built using Entity Framework Core to handle data operations, and the frontend is a Razor Pages app to present the data and interact with the user.

Features-
- Create New Task: You can add a new task by specifying a title, description, and due date.
- View Tasks: All tasks are displayed on the main page, showing the title, description, status, and due date.
- Update Task Status: You can change the status of any task (e.g., Pending, Completed).
- Delete Task: If you no longer need a task, you can delete it from the system.
- Error Handling: Basic error handling is implemented to ensure the app runs smoothly.

Tech Stack-
- Frontend: Razor Pages (ASP.NET Core)
- Backend: C#, Entity Framework Core, ASP.NET Core
- Database: In-memory db(for simplicity, but can be switched to SQL Server or other databases)

Setup-
To run this project locally, follow these steps:

Requirement: .NET SDK (8.0 is the recommended version)

1. Clone the repository:

```bash
git clone https://github.com/
```
2. Open the project in Visual Studio. Alternatively, use command prompt (Go to 'tools' -> Command line -> Developer command prompt)

3. Restore NuGet packages:

```bash
dotnet restore
```
In .NET 8, this step is optional because dotnet build or dotnet run will automatically restore dependencies if needed.

4. Run the project:

```bash
dotnet run
```
Then open your browser at the url shown in the console. The url can be seen in front of something like 'Now listening on'.

Important Note: Running `dotnet run` from the terminal starts the server but does not open a browser automatically. To launch the project in a browser, either copy the URL from the console output or run the project from Visual Studio using IIS Express.
This should run the project.

Alternatively, you can also right click on the folder 'CodeProject', click on 'set as startup project'. In the dropdown below the top menu bar, you will see options like http/https/IIS Express etc. Select IIS Express and click on 'play' icon, this will run the project on your browser.

API Endpoints:
1. Home page - List of Tasks
URL: /
Method: GET
Purpose: Shows all the tasks that have been added so far.
Params: None

2. Create Task Page (form)
URL: /CreateTask
Method: GET
Purpose: Displays a form where user can fill title, description, status and due date.

3. Submit New Task
URL: /CreateTask
Method: POST
Purpose: Saves the new task into the database (in-memory db in my case).
Fields:
Required- Title, Status, DueDateTime
Optional- Description

4. View a Task
URL: /ViewTask/{id}
Method: GET
Purpose: Open a detailed view for a single task by id.
Params:
id (int)

5. Update Task
URL: /UpdateTask/{id}
Method(s):
GET: Loads the form with existing data in db
POST: Saves the updated task to db
Params:
id(int)

6. Delete a Task
URL: id={id}&handler=Delete
Method: POST
Purpose: Deletes the task based on its id.
Params:
id(int)

API Testing:
This project was initially tested using Swagger UI to verify the CRUD endpoints. Swagger was enabled in development to check that the APIs for creating, viewing, updating, and deleting tasks worked as expected.
For the final version, Swagger was commented out in Program.cs so that the Razor Pages frontend could be used directly as the main interface.

Running Tests:
To run tests for the project, you'll need to use the test project (CodeProject.Tests).They can be run as following:
1. First way is by using command line: dotnet test
2. Second way is: Open test explorer from menu bar, click 'run all' for all the tests (or individual by clicking on them)


