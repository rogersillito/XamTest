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
        }

        private void DoAddThing()
        {
            TextChoices.Add(NewThingText);
            NewThingText = null;
            Navigation.PopAsync(true);
        }

        public Command AddThing { get; private set; }

        private Test1 _test1 = new Test1 { SomeProp = "XYZ" };
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
        public string SomeProp { get; set; }
    }
}