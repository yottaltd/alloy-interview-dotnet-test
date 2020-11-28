# Alloy Interview Test

Welcome Rookie and thanks for having accepted the invite to this assignment.

We are short on time and need you to provide a demoable solution for a buyer that wants to manage their members and the amounts of money that are collected, all completely legal stuff, of course.

We have been provided with a designModule.json file that is in the solution directory. You will need to implement the Module Parser project logic. It is a console application that needs to be able to read the provided example file and write the resulting designs file system database (more details on this later).

Moreover you should provide an Api, stubbed in the Web Api project, usable by these techsavy businessmen so that they can manage their designs remotely.

As hinted, for this demo it will not be necessary to use a real database, a fake file system based one will suffice.
The database is contained in a Database folder next to the solution. There is no requirement about where this folder should be so it can be placed wherever it is deemed easier to access, as long as the software deals with it.
Each file in that folder is considered to be like a “Table” (Sql) or a “Collection” (NoSql). One file is provided, representing the empty designs collection.

The use cases of our buyer are… how would you put it…. Critical, so the application needs to be stable and thoroughfully tested before being delivered, otherwise this might be our last assignment. Every publicly exposed function for every project needs to have at least one test, no need to test the private or internal ones. If another project is created, then a unit test project should be created for it accordingly.

One thing to note is that the Web Api, provides a way to manage data that is very different from the Module Parser, which resembles more a bulk importer. However we want nothing to do with double databases or data formats just because there are different ways of inputting data. Please deal with this well, no pressure :)
Given the fact that it is a fake database, we do not expect the highest read or write performances, however the number of "database calls" should be kept to a minimum just as it should be done against a real database.

Oh I almost forgot to mention, the meeting is in 2 hours, good luck!

### Must have

* The Module Parser project needs to be able to receive the designModule.json through command line input and save it to the file system database
* The Web Api needs to have all the stubbed out and commented out endpoints working and using the file system database
* The code needs to be able to uniformly interact with the file system databases
* At least one unit test method needs to be written for every public method of every non test project
* The correct models need to be inferred from the files provided and only from the files provided. No need for overthinking but what about polymorphic behaviour?

### Nice to have

* Model Validation through Data Annotations for Web Api request data models
* Model Validation through Data Annotations for models inserted in the database