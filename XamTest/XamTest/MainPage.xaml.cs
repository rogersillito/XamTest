using System;
using System.Linq;
using Xamarin.Forms;
using XamTest.ViewModels;

namespace XamTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel(Navigation);

            textChoice.SelectedIndex = 0;

            //var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //{
            //    System.Diagnostics.Debug.WriteLine("found resource: " + res);
            //}

            var imageSource = ImageSource.FromResource("XamTest.Assets.vegetables.jpg");
            NavigationPage.SetTitleView(this, CreateImageTitleView(imageSource));

            // setting custom fonts doesn't seem to work via CSS:
            // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/fonts

            // I've moved this to the xaml... here's how you do it in code for a specific control:
            /*
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    rotatingLabel.FontFamily = "Circus.ttf#Circus";
                    break;
                case Device.iOS:
                    rotatingLabel.FontFamily = "Circus";
                    break;
            }
            */
        }

        public static View CreateImageTitleView(ImageSource imageSource)
        {
            var view = new Image
            {
                Source = imageSource,
                HeightRequest = 40,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var titleView = new StackLayout
            {
                Children = { view },
                Margin = new Thickness(0, 0)
            };
            return titleView;
        }

        //private async void Button_OnClicked(object sender, EventArgs e)
        //{
        //    message.Text = "Loading items...";

        //    MobileServiceClient client = new MobileServiceClient("http://91.224.190.67:10240/");
        //    var items = await client.GetTable<TodoItem>().ReadAsync();
        //    var item = items.First();
        //    message.Text = item.Text;
        //}

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            // the not so nice way of binding behaviour to UI actions - this has to live in the code behind
            // using commands defined in our VM is better...
            var page = new NavigationPage(new AddSomething(BindingContext as MainPageViewModel));
            await Navigation.PushAsync(page);
        }
    }
}
