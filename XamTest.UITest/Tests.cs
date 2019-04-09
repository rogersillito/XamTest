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
            Assert.That(views.Length == 1);
            Assert.That(views[0].Text == "XYZ");
        }

        [Ignore]
        [Test]
        public void FireUpTheRepl()
        {
            _app.Repl();
        }
    }
}
