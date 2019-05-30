using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using Xamarin.Forms.Xaml;
using Label = Xamarin.Forms.Label;
using NavigationPage = Xamarin.Forms.NavigationPage;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var style = new Style(typeof(Label));
            style.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = NamedSize.Small });

            var fontFile = "sourcesanspro-semibold.ttf";
            var fontName = "Source Sans Pro Semibold";
            var boldFontFile = "sourcesanspro-bold.ttf";
            var boldFontName = "Source Sans Pro Bold";

            var regularFontFamily = GetFontFamilyForPlatform(Device.RuntimePlatform, fontName, fontFile);
            style.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = regularFontFamily });

            var boldFontFamily = GetFontFamilyForPlatform(Device.RuntimePlatform, boldFontName, boldFontFile);
            style.Triggers.Add(new Trigger(typeof(Label))
            {
                Property = Label.FontAttributesProperty,
                Value = FontAttributes.Bold,
                Setters =
                {
                    new Setter { Property = Label.FontFamilyProperty, Value = boldFontFamily }
                }
            });
            Resources.Add(style);

            //MainPage = new NavigationPage(new Transactions());
            MainPage = new NavigationPage(new MainPage());
        }

        private static string GetFontFamilyForPlatform(string runtimePlatform, string fontName, string fontFile)
        {
            var fontFamily = fontName;
            if (runtimePlatform == Device.Android)
            {
                fontFamily = $"{fontFile}#{fontName}";
            }
            return fontFamily;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
