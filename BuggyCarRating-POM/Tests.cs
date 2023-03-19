using OpenQA.Selenium.Chrome;
using BuggyCarRating_POM.Pages;
using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Remote;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace BuggyCarRating_POM
{
    public class Tests
    {
        private string BaseUrl { get; set; } = "https://buggy.justtestit.org";

        private IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            ChromeOptions chromeOptions = new ChromeOptions();
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.Navigate().GoToUrl(BaseUrl);
            Assert.That(Driver.Title, Is.EqualTo("Buggy Cars Rating"));
            Assert.That(Driver.FindElement(By.TagName("my-home")).Displayed);
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }


        //Test PopularMakeVoteCount represents the total count for that make
        [Test]
        public void PopularMakeVoteCount()
        {
            HomePage homePage = new HomePage(Driver);
            MakePage makePage = new MakePage(Driver);

            string getPopularMakeVoteCount = homePage.GetPopularMakeVoteCount();
            homePage.ClickPopularMake();

            string popularMakeVoteCountTotal = makePage.countListVotes();
            Assert.IsTrue(getPopularMakeVoteCount.Equals(popularMakeVoteCountTotal));
        }


        //Test Logging in and Logging Out as Expected
        [Test]
        public void UserCanLogInAndOut()
        {
            HomePage homePage = new HomePage(Driver);
            homePage.InputTextLoginMenu("Testtest4");
            homePage.InputTextPasswordMenu("Test123!");
            homePage.ClickLoginMenu();
            homePage.ValidateDisplayName("Hi, First Name");
            homePage.ClickLogoutMenu();
            homePage.ValidateLogout();
        }


        //Test Logging in and Logging Out as Expected
        [Test]
        public void PopularModelSelected()
        {
            HomePage homePage = new HomePage(Driver);
            OverallRatingPage overallRatingPage = new OverallRatingPage(Driver);

            var popularModelVotesHome = homePage.GetPopularModelVoteCount();
            var popularModelCarHome = homePage.GetPopularModelCar();
            homePage.ClickOverallRating();

            var topCarInList = overallRatingPage.GetOverallRatingTableMakeAndModel(0);
            var topCarVotesInList = overallRatingPage.GetOverallRatingTableVoteCount(0);

            Assert.IsTrue(popularModelCarHome.Contains(topCarInList));
            Assert.IsTrue(popularModelVotesHome.Contains(topCarVotesInList));

        }


        //Test new user setup and can vote
        [Test]
        public void UserCanVote()
        {
            //Create New User
            HomePage homePage = new HomePage(Driver);
            RegisterPage registerPage = new RegisterPage(Driver);
            OverallRatingPage overallRatingPage = new OverallRatingPage(Driver);
            ModelPage modelPage = new ModelPage(Driver);


            var username = "Testtest4";
            var password = "Test123!";
            var comment = "Test Comment 1234567890!@#$%^&*()-=_+[]{};:,.<>/?";

            homePage.ClickRegisterMenu();

            registerPage.InputTextFirstNamePage("First Name");
            registerPage.InputTextLastNamePage("Last Name");
            registerPage.InputMatchingPasswords(password);

            while (!registerPage.GetRegistrationSuccessfulMessage())
            {
                username = registerPage.GetUsername();
                Random random = new Random();
                registerPage.ClearTextLoginPage();
                registerPage.InputTextLoginPage("Test" + random.Next().ToString());
                registerPage.ClickRegisterOnPage();
            }

            homePage.InputTextLoginMenu(username);
            homePage.InputTextPasswordMenu(password);
            homePage.ClickLoginMenu();
            homePage.ClickBuggyRatingOnMenu();
            homePage.ClickOverallRating();

            //Select a random model from the first list and go to its model page
            overallRatingPage.ClickRandomModel();

            //Vote
            modelPage.InputTextCommentPage(comment);
            var voteCountBefore = modelPage.GetVoteCount();
            var voteCountBeforePlusOne = Int32.Parse(voteCountBefore) + 1;
            modelPage.ClickVotePage();
            Assert.IsTrue(modelPage.ValidateVoteThanksMessage());
            var voteCountAfter = Int32.Parse(modelPage.GetVoteCount());

            //Check to see if vote count increased by 1
            Assert.IsTrue(voteCountBeforePlusOne.Equals(voteCountAfter));

            //Validate comment is displayed on the table
            Assert.IsTrue(modelPage.ValidateCommentDisplayed(comment));
        }


        //Test User Register Validation
        [Test]
        public void UserRegistryValidation()
        {
            HomePage homePage = new HomePage(Driver);
            RegisterPage registerPage = new RegisterPage(Driver);

            homePage.ClickRegisterMenu();

            //Validate First Name Error of 50 Characters
            for (int i = 1; i < 60; i++)
            {
                registerPage.InputTextLoginPage("a");
                if (registerPage.GetFirstNameTooLongError())
                {
                    Assert.That(i.Equals(51));
                    registerPage.ClearTextLoginPage();
                    registerPage.InputTextLoginPage("Testtest4");
                    break;
                }
            }
            registerPage.InputTextFirstNamePage("First Name");
            registerPage.InputTextLastNamePage("Last Name");
            registerPage.ClickRegisterOnPage();

            //Test password minimum character length
            for (int i = 1; i < 10; i++)
            {
                registerPage.InputMatchingPasswords("a");
                registerPage.ClickRegisterOnPage();
                if (!registerPage.GetPasswordTooShortError())
                {
                    TestContext.WriteLine("Passwords with length " + i + " will stop displaying password length error");
                    break;
                }
                else
                {
                    TestContext.WriteLine("Passwords with length " + i + " is displaying password length error");
                }

            }

            //Test password must have 1 upper case
            registerPage.ClearMatchingPasswords();
            registerPage.InputMatchingPasswords("aaaaaa123!");
            registerPage.ClickRegisterOnPage();
            Assert.IsTrue(registerPage.GetPasswordUppercaseError());

            //Test password must have 1 lower case
            registerPage.ClearMatchingPasswords();
            registerPage.InputMatchingPasswords("AAAAAA123!");
            registerPage.ClickRegisterOnPage();
            Assert.IsTrue(registerPage.GetPasswordLowercaseError());

            //Test password must have 1 numeric
            registerPage.ClearMatchingPasswords();
            registerPage.InputMatchingPasswords("aaaaaaAAAA!");
            registerPage.ClickRegisterOnPage();
            Assert.IsTrue(registerPage.GetPasswordNumericError());

            //Test password must have 1 Symbol
            registerPage.ClearMatchingPasswords();
            registerPage.InputMatchingPasswords("aaaaaaAAAA123");
            registerPage.ClickRegisterOnPage();
            Assert.IsTrue(registerPage.GetPasswordSymbolError());

            //Test Legal Password / User already exists
            registerPage.ClearMatchingPasswords();
            registerPage.InputMatchingPasswords("aaaaaaAAAA123!");
            registerPage.ClickRegisterOnPage();
            Assert.IsTrue(registerPage.GetUserExistsError());
        }


    }
}