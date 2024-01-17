# Alloy Interview Test

Welcome Hotshot and thanks for having accepted the invite to this assignment.

We are short on time and need you to provide a demoable solution for a buyer that wants a way to query thier very generic data.

We have been provided with a set of example json queries inside queries.txt within the solution directory.  The Engine project contains the domain code for working with thier generic data they call "items", only implemented as a file system database as this is a prototype that will surely never go to production.

There is a query method and model but they are empty.  It is our lucky task to find a way to model, interpret and run any query with the various examples our substitute for a user story.  There is a functioning webapi but there is no need to seperate the QueryModel into two classes unless you deem it necessary.  Also it looks like there are some datatypes missing from the given prototype code; they will need adding as you spot them.

The use cases of our buyer are… how would you put it…. Critical, so the application needs to be stable and thoroughfully tested before being delivered, otherwise this might be our last assignment. Every publicly exposed function for every project needs to have at least one test, no need to test the private or internal ones.

One thing to note is that while we are dealing with a fake filesystem database, we need to make sure that when we run the query the fewest calls to what would be a "real" database are made, and that they are well targetted - commands should be made/added to the repository that request precisely the data needed and no more.  This includes the mechanism chosen to do pagination which will need implementing across the domains as you see fit.

### The MVP

* The json queries parsed into a minimal set of models representing any type of query that could be made
* The query made and returns the correct items
* At least one unit test method written for each query shape
* Queries should be validated and invalid queries explained to the caller
