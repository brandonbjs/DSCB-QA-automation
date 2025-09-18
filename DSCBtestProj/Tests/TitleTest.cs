using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace DSCBtestProj.Tests
{
    public class CharacterBuilderTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Homepage_Should_Have_Correct_Title()
        {
            driver.Navigate().GoToUrl("https://brandonbjs.github.io/Dark-Souls-Character-Builder-gh-pages/");
            string expectedTitle = "Dark Souls Character Builder";
            string actualTitle = driver.Title;

            ClassicAssert.AreEqual(expectedTitle, actualTitle);
        }

        [Test]
        public void HelmetDropdown_Should_Be_Populated_With_Items()
        {
            driver.Navigate().GoToUrl("https://brandonbjs.github.io/Dark-Souls-Character-Builder-gh-pages/");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d =>
            {
                var dropdown = d.FindElement(By.Id("helmet-select"));
                var options = new SelectElement(dropdown).Options;
                return options.Count > 1; // Wait until dropdown is populated
            });

            var dropdownElement = driver.FindElement(By.Id("helmet-select"));
            var select = new SelectElement(dropdownElement);
            var optionsList = select.Options;

            Console.WriteLine($"Found {optionsList.Count} helmet options:");
            foreach (var option in optionsList)
                Console.WriteLine($" - {option.Text}");

            ClassicAssert.IsTrue(optionsList.Count >= 3, "Helmet dropdown was not populated with enough items.");

            // Optionally check for known helmet
            bool containsEliteKnight = optionsList.Any(o => o.Text.Contains("Elite Knight Helm"));
            ClassicAssert.IsTrue(containsEliteKnight, "Expected helmet 'Elite Knight Helm' not found in dropdown.");
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
