using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
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
    }
}
