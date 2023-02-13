# CW6: Databases in WPF Applications
Name: Zachary Rose  
Date: 2/10/2023  
Class: CSCI 352

Basic code example displaying the ability to interface an Access database with a C# wpf application and querying the database using code.

## Required Files
* CW6-Databases_in_WPF_Applications-CSCI352/ --> directory containing the project files
* CW6-Databases_in_WPF_Applications-CSCI352.sln --> Visual Studio solution file
* CW6-Databases_in_WPF_Applications.laccdb --> Access Database file containing Assets and Employees tables
## Program Usage
To launch the program, clone the repository in Visual Studio and run by pressing "Start".  
Press the appropriately labeled buttons in the form to display data from the Access database or make temporary insertions using the textbox below. 

## Design Decisions
* User interface left very basic and functional
  - I added a shortened form of the field names above the tables for reference, but didn't do any fancy text wrapping.
    * If a sufficiently large string is inserted into the table and displayed, it will push row out of sync with the rest of the table, or possibly cover the buttons.
    * In the same vein, if enough entries are added to a table and displayed, it will go off the bottom of the window.
## Attempted extra credit
  - Added ability to add entries to Employees and Assets tables
  - I used basic error handling, and simply displayed Exception information if the injection threw an exception
  - In order to try and avoid any SQL injection shenanigans, I removed all ' and " characters from the queries and replaced them with spaces.
  - I used a single text box for insertions to allow for a full request to be pasted at once, but on further thought perhaps three different text boxes would be ideal in this particular case.
    * One argument against this is if I added a new table without exactly 3 values to insert, although this too could be solved with some clever use of hiding textboxes depending on which table is currently being displayed.
  - Notably, insertions to the data tables do not persist after closing the program, or in other words the file is not updated. 
  - In order to keep with assignment specifications I didn't utilize automatic assignment of EmployeeIDs, so a unique one must be entered manually when inserting a new employee
