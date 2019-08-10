Software Pre-Requisites
------------------------
1. Operating System : Any
2. Visual Studio : 2017/2019
3. .NET Core: V 2.2

How to Run the Solution?
-------------------------
1. Download the source code
2. Open Solution File 
3. Right click on solution and select "Restore NuGet Packages"
4. Right Click on Solution and Build
5. Right click on "FlinksDemo" Project and select "Set as Startup project"
6. Run the Application
7. On the UI Select FlinksCapital and enter userid and password as shown below the signin button
8. Enter the answer as shown below submit button
9. Wait for the Expected results on the UI

NOTE: Make a note of LoginID from the Browser Console


How to Run Test Cases?
-------------------------
1. Update LoginId in test project(FlinksDemo.Tests)(i.e. the one you took a note previously)- incase if login id expires
2. Build the Test project 
3. On Visual studio menu items, Click on test menu and select Run ---> All Tests

Note: Test Method  Test Name:	Returns_ValidAccountDetails_WhenLoginIdIsValid Takes 5 minutes beacuse it will try  30 times @ 10sec each request



