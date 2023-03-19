# BuggyCarRating-POM

This repo contains the bug report as well as the automation scripts for the buggy cars application


## Prerequisites 

Visual Studio 2022 (Community edition prefered)


## Steps to Execute
1. Download the Repo
2. Open the BuggyCarRating-POM.sln file

Before running the tests if you are wanting to run on headless mode, un-comment the arguments from the [Setup] block in the Test.cs file
Left commented, to display a little bit on what is being automated

![image](https://user-images.githubusercontent.com/47126256/226157136-abc57345-29c6-4027-81da-3a5579ae89d7.png)
![image](https://user-images.githubusercontent.com/47126256/226157177-f637eebb-df9c-4416-b610-58644f925f0b.png)


3. Click on the Test menu > Click Run All Tests
![image](https://user-images.githubusercontent.com/47126256/226156820-be88c645-a709-481e-a17c-b9ea7325df09.png)

Note the UserSignUpValidation is a little flakey, so if it is failing run all the tests again via 'Test > Run All Tests'
Or in the Test Explorer window that pops up during execution, right click the test you want to run and click 'Run'
![image](https://user-images.githubusercontent.com/47126256/226157348-8b09945d-38b8-4699-bd8f-e953e7aad0eb.png)



## Conclusion
I went the route of Selenium with NUnit using a Page Object Model. I would have used SpecFlow, but as there was no buisness value weighted on any of the functionality, I opted out of adding it, along with time constraints to implement it. Ideally, it would be good to have it in the future so that most users can understand what the system is doing by displaying the more readable and user-friendly steps.


/n
**Additional Important Areas to Automate**

- Automate each model has data and is correct.
- Automate each make has data and is correct.
- Automate update profile
/n
**Improvements that can be made**

- Improve the table selector and select next page.
- Implement reporting.
- Implement Specflow.
- Have some of the inputs be read by a csv file. (I.e having the username be read through a list of already known username and passwords)
- Incorporate password encryption and decryption functionality.
- Have the new logins created to output to the same csv file.
- Determine what is causing the 'Unknown Error' during register validation and in other areas.
- Make use of Try Catch to display better error logging.
- Add cross-browser tests (dependent on data from your user base, stuck with chrome).
- Add a base class so other pages can inherit from it. (All menu and footer elements and methods would be contained here)
