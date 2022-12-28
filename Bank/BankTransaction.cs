using Bank.Money;

namespace Bank
{
    internal class BankTransaction
    {
        public BankAccount Sender { get; init; }
        public BankAccount Receiver { get; init; }

        public Quantity Quantity { get; init; }
        public Currency Currency { get; init; }

        public BankTransaction(BankAccount sender, BankAccount receiver, Quantity quantity, Currency currency)
        {
            Sender = sender;
            Receiver = receiver;
            Quantity = quantity;
            Currency = currency;
        }

        public string GetTransactionInformation()
            => $"Sender: {Sender.FullName}";
    }
}
