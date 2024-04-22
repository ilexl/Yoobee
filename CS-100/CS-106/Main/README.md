# CS106 Ticketing System Guide

This guide is intended to get any user to be able to setup the ticketing system on their system.

## Requirements
- Windows 10
- Region/Language Settings (English United States)
- 50 MB (Minimum for installation - increases with use)
- Minimum screen resolution 1600x900

# Installation/Setup Guide Guide

## Initial Setup
| Instruction     | Example |
| ---      | ---       |
| 1. Download the repository zip file | ![image](https://github.com/ilexl/CS106/assets/109491531/f15162f2-e60d-43de-8e05-7bde7dd8f80e) |
| 2. Extract the folder from within the zip file</br>Find the folder called "Release" which contains the TicketingSystem.exe and other folders within. Drag this folder where ever you like.<br />| ![image](https://github.com/ilexl/CS106/assets/109491531/1e77c635-bef1-40d6-ba7a-2dd27899fbe6) |
| 3. Open the TicketingSystem application | ![image](https://github.com/ilexl/CS106/assets/109491531/1c0a1f1b-33c1-4398-932b-aef669866312) |

## Admin Guide - Setup
- Initial Setup - Log in with the default username: admin | password: admin
- Navigate to 'My Account' from the side navigation menu
- :warning: Enter the old password: admin | and create a new password for the admin account :warning:
- You can then manage all the users and tickets in this application using the 'All Accounts' and 'All Tickets' buttons on the side navigation menu. (Specififc are in the user guide)
- The buttons and instructions are self explanitory when it comes to managing accounts and tickets.
- You will need to create accounts for other admins, technicians and users so they can access the application too... Initally it is just the default administrator account.

# User manual 

## Admin Guide - Features
#### Create an account
| Instruction     | Example |
| ---      | ---       |
| - Navigate to 'create account' from the side navigation menu</br>- Fill out all the new account details in each of the input boxes</br>- :warning: Make sure you select the correct account type :warning:</br>- Enter a temporary password for the new user which you will need to remember so the new user so they can login and reset their password</br>- Give the login details (ID, email, temporary password) to the new user and tell them to change the password | ![image](https://github.com/ilexl/CS106/assets/109491531/0a01481d-eaf4-4de1-bb24-7e80a0c62296) |

#### Edit an account
| Instruction     | Example |
| ---      | ---       |
| - Navigate to 'all accounts' from the side navigation menu when logged in as the admin | ![image](https://github.com/ilexl/CS106/assets/109491531/61ddb2dd-744a-4839-a755-6a879eb0f3ea) |
| - Select an account from the list of accounts shown </br>- Make edits as per sections | ![image](https://github.com/ilexl/CS106/assets/109491531/d0c63cf5-1664-4a2f-9ec8-dce601ce03f6) |
| - Click on 'save changes' button | ![image](https://github.com/ilexl/CS106/assets/109491531/d4187a39-363a-4a9b-9300-6311e21b17e3) |

#### Delete an account
| Instruction     | Example |
| ---      | ---       |
| - Navigate to 'all accounts' from the side navigation menu when logged in as the admin | ![image](https://github.com/ilexl/CS106/assets/109491531/61ddb2dd-744a-4839-a755-6a879eb0f3ea) |
| - Select an account from the list of accounts shown </br> | ![image](https://github.com/ilexl/CS106/assets/109491531/d0c63cf5-1664-4a2f-9ec8-dce601ce03f6) |
| - Click on the 'DELETE ACCOUNT' button and confirm you wish to delete the account | ![image](https://github.com/ilexl/CS106/assets/109491531/7ef18f1a-bcd8-415f-8159-ae231dc6b8f8) ![image](https://github.com/ilexl/CS106/assets/109491531/4ea616c6-2f4f-4b77-8175-cfdc5be16d43) |

## Common Feature to all Users
#### Change password of current account
| Instruction     | Example |
| ---      | ---       |
| - Navigate to 'My Account' from the side navigation menu when logged in as any user type</br>- Enter your old password for that account</br>- Enter a new password and confirmation of new password in the respective input boxes</br>- Press the 'Apply Changes' button to change your password | <img src="https://github.com/ilexl/CS106/assets/109491531/ef49019b-a7c3-4868-90ab-7f3fd5282466" height="400" /> |

#### Create a ticket
| Instructions | |
| --- | --- |
| - Navigate to 'Create Ticket" from the side navigation menu when logged in any user type </br>- Enter all data into with relevant input boxes (Max 50 characters for title), including a title, the urgency of the ticket, who the ticket is created for (default is yourself), and an intial comment for the ticket. ** You cannot change who the ticket is created for </br>- Click on the submit button to finish creating your ticket which will add it to the system |   |
| Create Ticket     | Ticket View |
| ![image](https://github.com/ilexl/CS106/assets/109491531/a4b853f9-bdfc-49bf-9a82-a854aaae6f0f)  | ![image](https://github.com/ilexl/CS106/assets/109491531/070cb344-ab04-49d8-8da5-dc3711111969)         |

#### Add comment to ticket
| Instruction     | Example |
| ---      | ---       |
| - Open a ticket from the list of tickets or create a ticket </br> - Type in the comment box below the ticket info and above the current comments</br> - Click on the 'submit comment' button to add the comment | ![image](https://github.com/ilexl/CS106/assets/109491531/c4ff46ed-14e0-4afa-9211-107bac85b23f) |

#### Resolve a ticket
| Instruction     | Example |
| ---      | ---       |
| - Open or create a ticket </br> - Click on the 'resolve' button </br> - A window will pop up to select a reason </br> - Select a reason to resolve/close the ticket </br> - Click the 'resolve ticket' button on the pop up window | ![image](https://github.com/ilexl/CS106/assets/109491531/18348647-f9ca-4b8c-8e7b-4012c2669d40) |

  
#### Creating a ticket on behalve of another user (i.e. a caller with a problem)
| Instruction     | Example |
| ---      | ---       |
| - When creating a ticket there is a greyed out 'created by' field and a 'created for' field which you can change. </br> - By default they are the same, however you can change who the ticket is created for in cases where tickets are created on behalf of somebody else </br> - To create the ticket on behalf of another user, simply change the created for input as THEIR account ID instead of yours. </br> - When you do this you will still have access as you created the ticket (This field cannot be changed) | ![image](https://github.com/ilexl/CS106/assets/109491531/f5947b8c-be68-4619-812f-e429eaed263f) |

