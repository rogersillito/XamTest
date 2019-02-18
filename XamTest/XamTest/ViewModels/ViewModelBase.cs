using System.ComponentModel;
using Xamarin.Forms;

namespace XamTest.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase(INavigation navigation)
        {
            Navigation = navigation;
        }
        
        private bool loading;

        public bool IsLoading
        {
            get => loading;
            set
            {
                loading = value;
                NotifyPropertyChanged("IsLoading");
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation { get; }
    }
}