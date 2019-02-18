using System;
using System.Reflection;
using Xamarin.Forms;
using XamTest.ViewModels;

namespace XamTest
{
    public partial class MainPage : ContentPage
    {
        public string MyAge { get; set; } = "33";

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

            var defaultVegImage = new Image
            {
                Source = ImageSource.FromResource("XamTest.Assets.vegetables.jpg"),
                HeightRequest = 40,
                Aspect = Aspect.AspectFill
            };

            mainStack.Children.Add(defaultVegImage);
            NavigationPage.SetTitleView(this, CreateTitleView(defaultVegImage));
            //NavigationPage.SetTitleView(this, new Label {Text = "I AM NAV"});


            // setting custom fonts doesn't seem to work via CSS:
            // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/fonts
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    rotatingLabel.FontFamily = "Circus.ttf#Circus";
                    break;
                case Device.iOS:
                    rotatingLabel.FontFamily = "Circus";
                    break;
            }
        }


        View CreateTitleView(View view)
        {
            view.HorizontalOptions = LayoutOptions.Fill;
            view.VerticalOptions = LayoutOptions.CenterAndExpand;

            var titleView = new StackLayout
            {
                Children = { view },
                BackgroundColor = Color.DarkGreen,
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
