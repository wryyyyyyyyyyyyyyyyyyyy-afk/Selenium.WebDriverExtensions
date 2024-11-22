using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Selenium.WebDriverExtensions
{
    public static class WebDriverExtensions
    {
        public static void MoveToElement(this IWebDriver driver, IWebElement element)
        {
            driver.ScrollToElement(element);
            var actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        private static void ScrollToElement(this IWebDriver driver, IWebElement element)
        {
            var x = element.Location.X;
            var y = element.Location.Y;

            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"window.scrollTo({x},{y})");
            js.ExecuteScript($"window.scrollBy(0, -210)");
        }

        public static void ClickOnElement(this IWebDriver driver, IWebElement element)
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", element);
        }

        public static void ScrollToPageStart(this IWebDriver driver)
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
        }

        public static void OpenLinkInNewTab(this IWebDriver driver, string link)
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.open(arguments[0], '_blank');", link);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        public static void CloseLastPage(this IWebDriver driver)
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        public static void WaitForFullPageLoad(this IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}