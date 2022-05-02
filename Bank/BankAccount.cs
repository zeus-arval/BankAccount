using EnumsNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    /// <summary>
    /// A class which simulates a behavior of a real bank account in a simple way.
    /// </summary>
    internal class BankAccount
    {
        private decimal _sum;
        internal enum CurrencyEnum
        {
            [Description("Euro")]
            EUR = 0,
            [Description("United States Dollar")]
            USD,
            [Description("Japanese Yen")]
            JPY,
            [Description("Pound Sterling")]
            GBP
        }

        public string CurrencyName => Sum > 1.0m ? EnumExtensions.GetDescription<CurrencyEnum>(Currency) + "s" : EnumExtensions.GetDescription<CurrencyEnum>(Currency);
        public decimal MinPaymentLimit => BankLimits.SetMinSumByAccountType(this);
        public string PersonName { get; init; }
        public decimal MaxPaymentLimit { get; private set; }
        private CurrencyEnum Currency { get; init; }
        public AccountTypeEnum AccountType { get; private set; }

        public event EventHandler<ActionMadeEventArgs> ActionMade;

        public Error Error { get; init; }

        public decimal Sum
        {
            get => _sum;
            set
            {
                if (value < 0)
                {
                    _sum = 0;
                }
                _sum = value;
            }
        }

        public BankAccount(decimal sum, string personName, CurrencyEnum currency = CurrencyEnum.EUR)
        {
            Sum = sum;
            PersonName = personName ?? String.Empty;
            Currency = currency;
            Error = new Error();
            Error.ErrorAppeared += HandleError!;
            ActionMade += HandleActionMade!;
        }

        public void WithdrawFunds(decimal sum)
        {
            switch (AccountType)
            {
                case AccountTypeEnum.DEBIT:
                    if (sum > Sum)
                    {
                        CallError(BankLimits.ERROR_MSG_NOT_ENOUGH_MONEY);
                        return;
                    }

                    Sum -= sum;
                    OnActionMade(String.Format(BankLimits.ACTION_MSG_MONEY_WAS_WITHDRAWN, sum, CurrencyName, PersonName));
                    break;
                case AccountTypeEnum.CREDIT:
                    break;
                case AccountTypeEnum.INACTIVE:
                    CallError(BankLimits.ERROR_MSG_INACTIVE_ACCOUNT);
                    break;
            }
        }

        private void CallError(string message)
        {
            ErrorEventArgs args = new();
            args.Message = message;
            args.ErrorTime = DateTime.Now;
            Error.OnErrorAppeared(args);
        }

        private void OnActionMade(string message)
        {
            ActionMadeEventArgs args = new ActionMadeEventArgs();
            args.TimeActionMade = DateTime.Now; 
            args.Message = message;

            EventHandler<ActionMadeEventArgs> handler = ActionMade;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void HandleActionMade(object sender, ActionMadeEventArgs args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[{args.TimeActionMade}] {args.Message}");
            Console.ResetColor();
        }

        private void HandleError(object sender, ErrorEventArgs args)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{args.ErrorTime}] {args.Message}");
            Console.ResetColor();
        }

        public string GetAccountInfo()
            => $"Bank Account for {PersonName} has {Sum} {CurrencyName}";

        public void ChangeAcountType(AccountTypeEnum accountType)
        {
            AccountType = accountType;
        }
    }
    internal class ActionMadeEventArgs : EventArgs 
    {
        public string? Message { get; set; }
        public DateTime TimeActionMade { get; set; }
    }
}
