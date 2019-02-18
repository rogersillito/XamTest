using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}