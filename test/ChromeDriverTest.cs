using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace test
{
    [TestClass]
    public class ChromeDriverTest
    {
        private ChromeDriver driver;

        [TestInitialize]
        public void ChromeDriverInitialize()
        {
            // Initialize chrome driver 
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            driver.Url = Config.BaseUrl;

            driver.FindElementByCssSelector("#Input_Username").SendKeys(Config.Username);
            driver.FindElementByCssSelector("#Input_Password").SendKeys(Config.Password);
            driver.FindElementByCssSelector("#account").Submit();
        }

        [TestCleanup]
        public void ChromeDriverCleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void CreateInvalidVolume()
        {
            int volume = 52;
            CreateVolume(volume);

            var warning = driver.FindElementByCssSelector("body > div > main > div.row > div > form > div:nth-child(1) > span").Text;

            Assert.AreEqual($"Volume Number {volume} already exists.", warning);
        }

        [TestMethod]
        public void CreateNewVolume()
        {
            int volume = 54;
            CreateVolume(volume);

            var httpClient = HttpClientFactory.Create();
            var response = httpClient.GetStringAsync($"{Config.BaseUrl}/api/volumes").Result;

            var data = JArray.Parse(response);
            
            bool contained = false;
            int volumeId;

            foreach (JObject _volume in data)
            {
                if (int.Parse(_volume.Value<string>("volumeNumber")) == volume)
                {
                    contained = true;
                    volumeId = int.Parse(_volume.Value<string>("id"));

                    driver.Url = $"{Config.BaseUrl}/volumes/delete/{volumeId}";
                    driver.FindElementByCssSelector("body > div > main > div > form").Submit();
                    break;
                }
            }

            Assert.IsTrue(contained);
        }

        private void CreateVolume(int volume)
        {
            int fall = volume + 1967;
            int spring = fall + 1;

            // Create new volume
            driver.FindElementByCssSelector("body > div > main > p > a").Click();

            // Fill volume data
            driver.FindElementByCssSelector("#VolumeNumber").SendKeys(volume.ToString());
            driver.FindElementByCssSelector("#FallYear").SendKeys(fall.ToString());
            driver.FindElementByCssSelector("#SpringYear").SendKeys(spring.ToString());

            // Submit form
            driver.FindElementByCssSelector("body > div > main > div.row > div > form").Submit();
        }

        [TestMethod]
        public void CreateNewIssue()
        {
            int issue = 3;
            int volume = 52;
            string arTheme = "test: this is an arabic theme";
            string enTheme = "this is an english theme test";

            // Issue list inside volume 52
            driver.FindElementByCssSelector("body > div > main > div > table > tbody > tr:nth-child(1) > td:nth-child(5) > a").Click();
            // Create new Issue
            driver.FindElementByCssSelector("body > div > main > p > a").Click();

            // Fill Issue data
            driver.FindElementByCssSelector("#IssueNumber").SendKeys(issue.ToString());
            driver.FindElementByCssSelector("#ArabicTheme").SendKeys(arTheme);
            driver.FindElementByCssSelector("#EnglishTheme").SendKeys(enTheme);

            // Submit form
            driver.FindElementByCssSelector("body > div > main > div.row > div > form").Submit();

            int volumeId;
            var httpClient = HttpClientFactory.Create();
            var response = httpClient.GetStringAsync($"{Config.BaseUrl}/api/volumes").Result;

            var data = JArray.Parse(response);
            bool contained = false;

            foreach (JObject _volume in data)
            {
                if (int.Parse(_volume.Value<string>("volumeNumber")) == volume)
                {
                    volumeId = int.Parse(_volume.Value<string>("id"));

                    var _response = httpClient.GetStringAsync($"{Config.BaseUrl}/api/issues/{volumeId}").Result;
                    var _data = JArray.Parse(_response);

                    foreach (JObject _issue in _data)
                    {
                        if (int.Parse(_issue.Value<string>("issueNumber")) == issue && _issue.Value<string>("arabicTheme") == arTheme && _issue.Value<string>("englishTheme") == enTheme) { 
                            contained = true;
                            int issueId = int.Parse(_issue.Value<string>("id"));

                            driver.Url = $"{Config.BaseUrl}/issues/delete/{issueId}";
                            driver.FindElementByCssSelector("body > div > main > div > form").Submit();
                            break;
                        }
                    }
                    break;
                }
            }

            Assert.IsTrue(contained);
        }

        [TestMethod]
        public void CreateNewArticle()
        {
            int issue = 10;
            int volume = 52;

            var language = Language.English;
            var category = "News";
            var writer = "Mohsen Shamas";
            var title = "Dummy Text";
            var subtitle = "Welcome to the Dummy Text";

            // Issues list inside volume 52
            driver.FindElementByCssSelector("body > div > main > div > table > tbody > tr:nth-child(1) > td:nth-child(5) > a").Click();
            // Articles list inside Issue 10
            driver.FindElementByCssSelector("body > div > main > div > table > tbody > tr:nth-child(3) > td:nth-child(3) > a").Click();
            // Create new Article
            driver.FindElementByCssSelector("body > div > main > p > a").Click();

            // Fill Issue data
            CreateArticle(language, category, writer, title, subtitle);

            int volumeId;
            var httpClient = HttpClientFactory.Create();
            var response = httpClient.GetStringAsync($"{Config.BaseUrl}/api/volumes").Result;

            var data = JArray.Parse(response);
            bool contained = false;

            foreach (JObject _volume in data)
            {
                if (int.Parse(_volume.Value<string>("volumeNumber")) == volume)
                {
                    volumeId = int.Parse(_volume.Value<string>("id"));

                    var _response = httpClient.GetStringAsync($"{Config.BaseUrl}/api/issues/{volumeId}").Result;
                    var _data = JArray.Parse(_response);

                    foreach (JObject _issue in _data)
                    {
                        if (int.Parse(_issue.Value<string>("issueNumber")) == issue) { 
                            int issueId = int.Parse(_issue.Value<string>("id"));

                            var __response = httpClient.GetStringAsync($"{Config.BaseUrl}/api/articles/{issueId}").Result;
                            var __data = JArray.Parse(__response);

                            foreach (JObject _article in __data)
                            {
                                var articleCategory = _article.Value<JObject>("category");
                                var articleWriter = _article.Value<JObject>("member");

                                if ((Language) int.Parse(_article.Value<string>("language")) == language &&
                                    articleCategory.Value<string>("categoryName") == category &&
                                    articleWriter.Value<string>("name") == writer &&
                                    int.Parse(_article.Value<string>("issueID")) == issueId &&
                                    _article.Value<string>("title") == title &&
                                    _article.Value<string>("subtitle") == subtitle)
                                {
                                    contained = true;
                                    int articleId = int.Parse(_article.Value<string>("id"));

                                    driver.Url = $"{Config.BaseUrl}/articles/delete/{articleId}";
                                    driver.FindElementByCssSelector("body > div > main > div > form").Submit();
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }

            Assert.IsTrue(contained);
        }

        private void CreateArticle(Language _language, string _category, string _writer, string title, string subtitle)
        {
            var language = new SelectElement(driver.FindElementByCssSelector("#Language"));
            language.SelectByText(_language.ToString());

            var category = new SelectElement(driver.FindElementByCssSelector("#Category_CategoryName"));
            category.SelectByText(_category);

            var writer = new SelectElement(driver.FindElementByCssSelector("#writers"));
            writer.SelectByText(_writer);

            driver.FindElementByCssSelector("#Title").SendKeys(title);
            driver.FindElementByCssSelector("#Subtitle").SendKeys(subtitle);

            driver.FindElementByCssSelector("body > div.container > main > form").Submit();
        }

        private enum Language { Arabic, English}
    }
}
