using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UIAutomationTests
{
    internal class Selenium
    {
        IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver(options);
        }
        /*
        [Test]
        public void Enter_To_List_Of_Countries_Test()
        {
            //Arrange
            //Abre una nueva ventana
            var URL = "http://localhost:8080/";

            //Maximiza la pantalla
            _driver.Manage().Window.Maximize();

            //Act
            //Navega a la página que se necesita probar
            _driver.Navigate().GoToUrl(URL);

            //Assert
            //No es un buen ejemplo de assert, use uno diferente
            Assert.IsNotNull(_driver);
        }
        */
        [Test]
        public void Create_New_Country_Test()
        {
            // Arrange
            var URL = "http://localhost:8080/";
            _driver.Navigate().GoToUrl(URL);

            // Act: Navegar al formulario de creación de país
            IWebElement addButton = _driver.FindElement(By.XPath("//button[contains(text(),'Agregar país')]"));
            addButton.Click();

            // Verificar si se encuentra en la página correcta (Formulario de país)
            Assert.AreEqual("Formulario para crear un país", _driver.FindElement(By.TagName("h1")).Text);

            // Llenar el formulario de creación de país
            _driver.FindElement(By.Id("name")).SendKeys("Brasil");
            _driver.FindElement(By.Id("continente")).SendKeys("América");
            _driver.FindElement(By.Id("idioma")).SendKeys("Portugués");

            // Enviar el formulario
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // Act: Confirmar si redirigió a la lista de países
            Thread.Sleep(2000); // Tiempo de espera para cargar la lista de países

            // Assert: Verificar si el país fue agregado a la lista
            IWebElement countryTable = _driver.FindElement(By.TagName("table"));
            Assert.IsTrue(countryTable.Text.Contains("Brasil"));
            Assert.IsTrue(countryTable.Text.Contains("América"));
            Assert.IsTrue(countryTable.Text.Contains("Portugués"));
        }
    }
}
