using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuggyCarRating_POM.Pages
{
    public class OverallRatingPage
    {
        private readonly IWebDriver Driver;

        public OverallRatingPage(IWebDriver driver) => Driver = driver;


        //Element Locators
        IWebElement tableRowsElement => Driver.FindElement(By.TagName("table")).FindElement(By.TagName("tbody"));


        //Get Methods
        public string GetOverallRatingTableMakeAndModel(int rowNumber)
        {
            var tableRows = tableRowsElement.FindElements(By.TagName("tr"));
            var row = tableRows[rowNumber].FindElements(By.TagName("td"));
            var carMake = row[1].FindElement(By.TagName("a")).Text;
            var carModel = row[2].FindElement(By.TagName("a")).Text;

            var carFullName = carMake + " " + carModel;
            return carFullName;
        }
        public string GetOverallRatingTableVoteCount(int rowNumber)
        {
            var tableRows = tableRowsElement.FindElements(By.TagName("tr"));
            var row = tableRows[rowNumber].FindElements(By.TagName("td"));
            var carVotes = row[4].Text;
            return carVotes;
        }


        //Click Methods
        public void ClickRandomModel()
        {
            //Select a random model from the first list and go to its model page
            var carListRows = tableRowsElement.FindElements(By.TagName("tr"));
            var carListRecordCount = carListRows.Count();
            int rnd = new Random().Next(carListRecordCount);
            var rowColumns = carListRows[rnd].FindElements(By.TagName("td"));
            rowColumns[2].FindElement(By.TagName("a")).Click();
        }
    }
}
