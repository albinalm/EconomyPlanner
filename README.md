# EconomyPlanner

## Purpose
The purpose of this application is to be able to efficiently create and control your budget.
By having Economy Plans containing a list of expenses and incomes you can easily manage and see how much money you are left with and thus be able to increase the amount you save or just make sure you can afford something.

An economy plan is a set period of time between your salaries. When you add an expense or income you can also decide if you want it to be recurring.
A new economy plan will be generated between each period time. You can manage three economy plans ahead and see your complete history.
<br>
<br>
<br>

## History
This project first appeared as a quick WinForms project to suit my own personal needs.

At one point i accidentally purged all the source code, and since I ended up using the application a lot I felt handicapped without it. 
Therefore I decided to remake the application for real this time.

## Technicalities
The project is based on four layers; A bottom repository, an abstraction layer and the API. On the top we have the Blazor app which communicates back and forward to the API.

Security layers are still WIP but the general thought is to keep it simple and use a GUID for each household that you use to authenticate with. Since this application will be used mostly domestic between family and friends security isn't a huge concern at the moment.

In the future I might add user accounts which need to be linked to certain household to strengthen integrity and security.

![Preview of tech stack](Documentation/EconomyPlannerStack.png?raw=true "Tech stack")
