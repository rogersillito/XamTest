using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XamTest.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp _app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void Test1LabelIsDisplayed()
        {
            var views = _app.Query(x => x.Marked("Test1Label"));
            views.Length.Should(Be.EqualTo(1));
            views[0].Text.Should(Be.EqualTo("XYZ"));
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            _app.Repl();
            //AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
            //app.Screenshot("Welcome screen.");

            //Assert.IsTrue(results.Any());
        }
    }
}
