using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Linq;
using Xamarin.Forms;
using XamTest;
using XamTest.ViewModels;

namespace App3
{
    public partial class MainPage : ContentPage
    {
        public string MyAge { get; set; } = "33";

        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel(Navigation);

            textChoice.SelectedIndex = 0;

            //label.BindingContext = slider;
            //label.SetBinding(Label.RotationProperty, "Value");
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
            var page = new NavigationPage(new AddSomething(BindingContext as MainPageViewModel));
            await Navigation.PushAsync(page);
        }
    }
}
