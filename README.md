
# Prerequisites:

1. Install Visual Studio 2022 Community:
</br> https://visualstudio.microsoft.com/vs/community/
</br>Make sure that during the installation process, you check ASP .NET and web development and .NET desktop development:
![Capture456](https://github.com/AgrostemmaGithago/Internship/assets/129935966/b6dd9c96-4b5f-4e1b-93e1-6491639dfd60)
(Note: if you already have VS Community installed, make sure you install .NET Core 8 SDK in order for the project to run)


2. Install SQLite/ SQL server Compact Toolbox, after the installation of Visual Studio 2022 Community:
https://marketplace.visualstudio.com/items?itemName=ErikEJ.SQLServerCompactSQLiteToolbox <br>
(make sure the visual studio is not running before installation)

4. Create your own github repository, download the source code and open the project in Visual Studio. We'd like you to name your repository TEC-Internship-[FirstName]-[LastName]

 # Setup & Intro


1. Load the database from the repository:
![Untitled2](https://github.com/AgrostemmaGithago/Internship/assets/129935966/12d97892-8734-4834-a64f-71ace62133bc)
![Untitled5](https://github.com/AgrostemmaGithago/Internship/assets/129935966/843d414b-318d-4584-b1a7-f933e602b3c0)
![Untitled6](https://github.com/AgrostemmaGithago/Internship/assets/129935966/4dda280f-bc98-4ded-9ed0-17288b9fa162)

2. Change to run on multiple startup projects:
 ![Untitled4565](https://github.com/AgrostemmaGithago/Internship/assets/129935966/f9a34901-627d-4a8d-83b5-22b5989f3049)

3. Once you run the applications, you will notice that 2 tabs will open:
![Untitled90](https://github.com/AgrostemmaGithago/Internship/assets/129935966/1a0b5008-114e-4d4d-aee7-3a97dd2c606f)
One represents the Api app, and the other is the Web App.

5. If you for example, click on the person tab, you will get an exception:
![Untitled91](https://github.com/AgrostemmaGithago/Internship/assets/129935966/9eeff4f4-639c-4277-bd7d-84da4c037eb4)
In order to fix this, you will have to do a few things:<br>
5.a. Change the path of the database with your own, in APIDbContext.cs: <br>
![Capture92](https://github.com/AgrostemmaGithago/Internship/assets/129935966/6dfef969-09b4-42bd-bafd-0a0c58b1ca90)
5.b. Change the port in the Controller's of the Web App: <br>
![Capture000](https://github.com/AgrostemmaGithago/Internship/assets/129935966/1c7e5d17-8ed6-4c3b-93cb-cbe4cffc4cb1)

6. Now after you saved your changes and run the apps, once you clicked on the Person button in the web app, you should be able to see some people that have been fetched from the database, and their data to be displayed:
![Capture45](https://github.com/AgrostemmaGithago/Internship/assets/129935966/f07ddc36-71da-45bf-931c-cb6a6f0873da)
<br>
Neat! <br>

Now, before you get started it would be:

## Nice to read
https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database <br>
https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio <br>
https://learn.microsoft.com/en-us/visualstudio/debugger/debugger-feature-tour?view=vs-2022 <br>

and finally, let's do some fixes!

# Tasks:
Database diagram: <br>
![Capture5465465](https://github.com/AgrostemmaGithago/Internship/assets/129935966/c3931cd7-682f-40e7-b16b-f74b58714029)


1. As a User I want to be able to delete a Person
2. As a User I need to be able to update a Person's Information
3. As a User I need to be able to create a new Person in the database
4. As a User I need to be able to add a new Salary
5. As a User I want to be able to edit/add new person's details

	Implementation: 
	- create PersonDetail Model according to the database diagram
	- add the PersonDetails database set to the APIDbContext
	- create a new migration using Package Manager Console <br>
	read: https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli#create-your-first-migration <br>
	Troubleshoot: <br>
	ERROR: "dotnet : Could not execute because the specified command or file was not found." Please run "dotnet tool install --global dotnet-ef --version 8.*" <br>
	ERROR: "No project was found. Change the current working directory or use the --project option". Please change the path to the ApiApp -  run "cd .\Internship\ApiApp" <br>
	- modify the View so I could update/add a Person's Details from the web app (Birthday and PersonCity)

6. As a User I want to be able to delete a Department <br>
7. As a developer I want to not change the URL's of the api in all of the Controllers <br>
   Details:  Ctrl + Shit + F in the entire solution and search for HINT
<br><br>
Are you bored? get alot of points by: <br>
8. Adding Authentification to the app: <br>
    	- create a Login View with admin username and password. could be a popup or login in the Header of the app. <br>
    	- create an Admin Table with user information (the password is not necessary to be encrypted, but yet again, it would be nice) <br>
    	- I should not be able to do any of the RESTful api calls on person, persondetails, salary or department tables unless I am logged in <br>
9. Having too much time on your hands? Do any kind of improvements you wish. Hell, create another app and do it 3-tier architecture as far as we're concerned. 
