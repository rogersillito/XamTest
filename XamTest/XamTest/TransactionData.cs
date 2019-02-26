using System;
using System.Collections.Generic;
using System.Linq;

namespace XamTest
{
    public class TransactionData
    {
        private static readonly DateTime StartDate = DateTime.Today.AddYears(-1);
        public static int AccountNumber { get; private set; }

        public static List<Transaction> Transactions = CreateTransactionData(50).ToList();
        public static decimal Balance { get; private set; }

        public static IEnumerable<Transaction> CreateTransactionData(int count)
        {
            AccountNumber = RandomDataHelper.CreateAccountNumber();
            var dt = StartDate;
            var balance = RandomDataHelper.Random.Next(10000, 20000) * 1.01m;
            Balance = balance;
            for (var i = 0; i < count; i++)
            {
                dt = dt.AddDays(-RandomDataHelper.Random.Next(1, 30)).AddSeconds(-RandomDataHelper.Random.Next(30, 9000));
                var sign = RandomDataHelper.Random.Next(-1, 2);
                var amountVal = RandomDataHelper.Random.Next(100, 200) * 1.01m;
                if (sign != 0) amountVal *= sign;
                balance += amountVal;
                yield return Transaction.DummyTransaction(dt, balance, amountVal, sign);
            }
        }
    }

    public class RandomDataHelper
    {
        public static readonly Random Random = new Random();
        public static int CreateAccountNumber() => RandomDataHelper.Random.Next(100000000, 999999999);
        public static int CreateId() => RandomDataHelper.Random.Next(10000, 99999);
    }

    /*
    Transaction     OBTransaction4
        Amount			OBActiveOrHistoricCurrencyAndAmount.Amount
        currency		OBActiveOrHistoricCurrencyAndAmount.Currency
        balance			OBTransactionCashBalance
        description		TransactionInformation
        date            ValueDateTime
        ??
     */

    public class Transaction
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public bool IsPositiveAmount { get; set; }
        public string Balance { get; set; }

        public static Transaction DummyTransaction(DateTime date, decimal balance, decimal amountVal, int sign)
        {
            var isPositiveAmount = sign > -1;
            var t = new Transaction
            {
                IsPositiveAmount = isPositiveAmount,
                Amount = $"{(isPositiveAmount ? "+" : "-")} £{Math.Abs(amountVal):##,#.00}",
                Balance = $"£{balance:##,#.00}",
                Date = date.ToString("dd MMMM yyyy"),
                Id = RandomDataHelper.CreateId(),
                Description = sign == 0 // DIRTY alert!
                    ? "Gross Interest"
                    : $"Transfer - {RandomDataHelper.CreateAccountNumber()}"
            };
            return t;
        }
    }
}
