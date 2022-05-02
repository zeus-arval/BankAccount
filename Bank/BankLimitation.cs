using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    /// <summary>
    /// Limits of Bank Account operations
    /// </summary>
    internal static class BankLimits
    {
        internal const decimal MIN_DEBIT_SUM = 0.0M;
        internal const decimal MIN_CREDIT_SUM = -20_000.0m;
        internal const decimal MAX_PAYMENT = 1_000_000.0m;

        internal const string ERROR_MSG_NOT_ENOUGH_MONEY = "Not enough money for withdrawal of funds";
        internal const string ERROR_MSG_INACTIVE_ACCOUNT = "Your account is inactive";

        internal const string ACTION_MSG_MONEY_WAS_WITHDRAWN = "{0} {1} was withdrawn from {2} account";
        internal const string ACTION_MSG_MONEY_TO_WITHDRAW = "{0} {1} was requested to be withdrawn";


        public static decimal SetMinSumByAccountType(this BankAccount account)
        {
            if (account.AccountType != AccountTypeEnum.INACTIVE)
            {
                return Decimal.MinusOne;
            }
            switch (account.AccountType)
            {
                case AccountTypeEnum.DEBIT:
                    return MIN_DEBIT_SUM;
                case AccountTypeEnum.CREDIT:
                    return MIN_CREDIT_SUM;
                default:
                    return Decimal.Zero;
            }
        }
    }


    /// <summary>
    /// Type, which defines the limits of payments
    /// </summary>
    internal enum AccountTypeEnum
    {
        [Description("Debit Account")]
        DEBIT = 0,
        [Description("Credit Account")]
        CREDIT = 1,
        [Description("Inactive Account")]
        INACTIVE = 99
    }
}
