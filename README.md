1.1 Feaures:

A user can create, update, delete, and retrieve the following models that are defined in the database:

  - User model
  - Person model
  - Person Detail model
  - Department model
  - Salary model

These models are displayed in the views, each one inside tables with their specific fields, and have buttons that call the web API for the CRUD operations.

For authentication, there is a login view that handles user authentication with two roles:

  - Admin
  - Basic user

An admin account can be created but not deleted (mimicking account forwarding or direct database deletion). Only the admin can perform operations on users. If the admin is logged in, there is a view in the Home View that contains a table of Users. The admin can perform CRUD operations on the users, who need to be assigned to a Person. The relation between Users and Person is one-to-one, ensuring one account per employee for easier tracking.

Authentication uses a bearer token generated by the server and used for all API calls. The token is stored in session storage and retrieved when making calls. If no token is present when calling endpoints, a 401 Unauthorized code will be returned.

The authentication uses a bearer token that is generated by the server and used for all the api calls. The token is stored in the session storage, and retrieved when a call is made. If no token is present when calling endpoints, the 401 Unauthorized code will be returned.
1.2 Additional features:

Here is a list of additional features, beyond the requirements:

  - A user cannot access any view URLs before logging in; attempting to do so will redirect the user back to the login page.
  - A user cannot see or interact with the user table if their role is not Admin.
  - A back button has been implemented
  - Login credentials are encrypted during the login process and decrypted with a hidden key on the server side, making the application safer against attacks. The encryption key is stored in ``appsettings.json``.
  - Implemented a 3-layered architecture on the server side, bringing:
      1. Maintainability: easier to update.
      2. Scalability: each layer can be scaled independently.
      3. Reusability: services can be used by different controllers.
  - When deleting a salary, the group with that salary gets a temporary salary, which can be updated.
  - When deleting a Department, every Position is transferred to another one and can be reassigned to a new department with an update.
