using System;
using System.Collections.Generic;
using System.Linq;

namespace XamTest
{
    public class TransactionData
    {
        private static readonly DateTime StartDate = DateTime.Today.AddYears(-10);
        public static int AccountNumber { get; private set; }
        private List<Transaction> _transactions;

        public static List<Transaction> Transactions = CreateTransactionData(50).ToList();

        public static IEnumerable<Transaction> CreateTransactionData(int count)
        {
            AccountNumber = RandomDataHelper.CreateAccountNumber();
            var dt = StartDate;
            for (var i = 0; i < count; i++)
            {
                dt = dt.AddDays(RandomDataHelper.Random.Next(1, 20)).AddSeconds(RandomDataHelper.Random.Next(30, 900));
                yield return new Transaction(dt, AccountNumber);
            }
        }
    }

    public class Transaction
    {
        private int CreateId() => RandomDataHelper.Random.Next(10000, 99999);

        public enum TransactionType
        {
            Credit,
            Transfer,
            Debit
        }

        public int Id { get; }
        public DateTime Date { get; }
        public TransactionType Type { get; }
        public string Description { get; }
        public int SourceAccount { get; }
        public int DestinationAccount { get; }
        public decimal Amount { get; }

        public Transaction(DateTime date, int accountNumber)
        {
            Date = date;
            Id = CreateId();
            SourceAccount = accountNumber;
            Type = (TransactionType)Enum.GetValues(typeof(TransactionType)).GetValue(RandomDataHelper.Random.Next(0, 2));
            Amount = RandomDataHelper.Random.Next(1, 1000) / 100.0m;
            switch (Type)
            {
                case TransactionType.Credit:
                    Description = "Money Deposited";
                    break;
                case TransactionType.Transfer:
                    if (RandomDataHelper.Random.Next(0, 1) == 1)
                    {
                        DestinationAccount = RandomDataHelper.CreateAccountNumber();
                        Description = "Money Transferred Out";
                        Amount = Amount * -1;
                    }
                    else
                    {
                        DestinationAccount = SourceAccount;
                        SourceAccount = RandomDataHelper.CreateAccountNumber();
                        Description = "Money Transferred In";
                    }
                    break;
                case TransactionType.Debit:
                    Description = "Money Withdrawn";
                    break;
            }
        }
    }

    public class RandomDataHelper
    {
        public static readonly Random Random = new Random();
        public static int CreateAccountNumber() => RandomDataHelper.Random.Next(10000000, 99999999);
    }
}