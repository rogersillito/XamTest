using Dynatrace;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dog : ContentPage
    {
        public Dog()
        {
            InitializeComponent();

            _httpClient = new HttpClient();
            var dynatrace = DependencyService.Get<IDynatrace>();
            dynatrace.SetupHttpClient(_httpClient);
        }

        private HttpClient _httpClient;

        private async Task<string> FetchDog()
        {
            return await _httpClient.GetStringAsync("https://api.thedogapi.com/v1/images/search?mime_types=jpeg");
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var dogJson = await FetchDog();
            var c = new ExpandoObjectConverter();
            var dogResult = JsonConvert.DeserializeObject<List<System.Dynamic.ExpandoObject>>(dogJson, c);
            dynamic firstDog = dogResult.FirstOrDefault();
            if (firstDog == null) return;
            var dogUrl = (string)firstDog.url;
            dog.Source = ImageSource.FromUri(new Uri(dogUrl));
        }
    }
}