using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuggyCarRating_POM.Pages
{
    public class MakePage
    {
        private IWebDriver Driver;


        //Element Locators
        public MakePage(IWebDriver driver) => Driver = driver;
        IWebElement makeTable => Driver.FindElement(By.TagName("main")).FindElement(By.TagName("table")).FindElement(By.TagName("tbody"));


        //Methods
        public string countListVotes()
        {
            int intRowRecord = 0;
            var makeTableRecords = makeTable.FindElements(By.TagName("tr"));
            //Goes through each row and adds the vote counts of each car to the total
            foreach (IWebElement element in makeTableRecords)
            {
                var rowRecord = element.FindElements(By.TagName("td"));
                var addedRowRecord = int.Parse(rowRecord[3].Text);
                intRowRecord = intRowRecord + addedRowRecord;
            }
            var stringRowRecord = ("(" + intRowRecord.ToString() + " votes)");
            return stringRowRecord;
        }
    }
}
