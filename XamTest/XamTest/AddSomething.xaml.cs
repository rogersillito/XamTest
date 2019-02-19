using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamTest.ViewModels;

namespace XamTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSomething : ContentPage
    {
        public AddSomething(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;

            var imageSource = ImageSource.FromResource("XamTest.Assets.vegetables.jpg");
            NavigationPage.SetTitleView(this, MainPage.CreateImageTitleView(imageSource));
        }
    }
}