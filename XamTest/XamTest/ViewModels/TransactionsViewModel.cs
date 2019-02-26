using System.Collections.Generic;
using Xamarin.Forms;

namespace XamTest.ViewModels
{
    public class TransactionsViewModel : ViewModelBase
    {
        public TransactionsViewModel(INavigation navigation) : base(navigation)
        {
            Transactions = TransactionData.Transactions;
            Balance = TransactionData.Balance;
        }

        private IList<Transaction> _transactions;
        public IList<Transaction> Transactions
        {
            get => _transactions;
            set
            {
                NotifyPropertyChanged(nameof(Transactions));
                _transactions = value;
            }
        }

        private decimal _balance;
        public decimal Balance
        {
            get => _balance;
            set
            {
                NotifyPropertyChanged(nameof(Balance));
                _balance = value;
            }
        }
    }
}