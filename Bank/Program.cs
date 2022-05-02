using Bank;

BankAccount arturAccount = new(100.0m, "Artur");
arturAccount.ChangeAcountType(AccountTypeEnum.DEBIT);

arturAccount.WithdrawFunds(90.0m);
arturAccount.WithdrawFunds(90.0m);
