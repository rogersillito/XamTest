using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace XamTest.ViewModels
{
    public class MainPageViewModel: ViewModelBase
    {
        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            AddThing = new Command(DoAddThing);
            NavigateTransactions = new Command(DoNavigateTransactions);
            NavigateDog = new Command(DoNavigateDog);
        }

        private void DoNavigateDog()
        {
            Navigation.PushAsync(new NavigationPage(new Dog()));
        }


        private void DoNavigateTransactions()
        {
            Navigation.PushAsync(new NavigationPage(new Transactions()));
        }

        private void DoAddThing()
        {
            TextChoices.Add(NewThingText);
            RotatedThing = NewThingText;
            NewThingText = null;
            Navigation.PopAsync(true);
        }

        public Command AddThing { get; }
        public Command NavigateTransactions { get; set; }
        public Command NavigateDog { get; set; }

        private Test1 _test1 = new Test1 { OtherTest1 = new Test1 { SomeProp = "XYZ" } };
        public Test1 Test1
        {
            get => _test1;
            set
            {
                NotifyPropertyChanged(nameof(Test1));
                _test1 = value;
            }
        }

        private string _myAge = "41";
        public string MyAge
        {
            get => _myAge;
            set
            {
                NotifyPropertyChanged(nameof(MyAge));
                _myAge = value;
            }
        }

        private string _newThing = "";
        public string NewThingText
        {
            get => _newThing;
            set
            {
                NotifyPropertyChanged(nameof(NewThingText));
                _newThing = value;
            }
        }

        private string _rotatedThing = "";
        public string RotatedThing
        {
            get => _rotatedThing;
            set
            {
                NotifyPropertyChanged(nameof(RotatedThing));
                _rotatedThing = value;
            }
        }

        private ObservableCollection<string> _textChoices = new ObservableCollection<string>(new List<string>
        {
            "Onion",
            "Parsnip",
            "Potato",
            "Courgette"
        });

        public ObservableCollection<string> TextChoices
        {
            get => _textChoices;
            set
            {
                _textChoices = value;
                NotifyPropertyChanged(nameof(TextChoices));
            }
        }
    }

    public class Test1
    {
        public Test1 OtherTest1 { get; set; }
        public string SomeProp { get; set; }
    }
}