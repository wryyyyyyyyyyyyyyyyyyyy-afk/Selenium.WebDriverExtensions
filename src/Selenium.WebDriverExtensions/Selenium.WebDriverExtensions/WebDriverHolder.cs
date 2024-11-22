using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.WebDriverExtensions
{
    public static class WebDriverHolder
    {
        [ThreadStatic]
        private static IWebDriver instance;

        public static IWebDriver Driver
        {
            get
            {
                return instance = instance ?? new ChromeDriver();
            }
        }

        public static void InitDriver(IWebDriver driver)
        {
            instance = driver;
        }

        public static void Cleanup()
        {
            instance?.Quit();
            instance = null;
        }
    }
}