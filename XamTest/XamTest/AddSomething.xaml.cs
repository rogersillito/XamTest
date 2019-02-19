using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamTest.ViewModels;
using System.Reflection;

namespace XamTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSomething : ContentPage
    {
        public AddSomething()
        {
            InitializeComponent();

            //sunflower.Source = ImageSource.FromResource("XamTest.Assets.sunflower.png");
        }

        public AddSomething(MainPageViewModel mainPageViewModel): this()
        {
            BindingContext = mainPageViewModel;

            var imageSource = ImageSource.FromResource("XamTest.Assets.vegetables.jpg");
            NavigationPage.SetTitleView(this, MainPage.CreateImageTitleView(imageSource));

            var assembly = GetType().GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            }
        }
    }
}