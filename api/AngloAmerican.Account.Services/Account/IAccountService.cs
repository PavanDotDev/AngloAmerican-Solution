using System;
using System.Collections.Generic;
using System.Text;

namespace AngloAmerican.Account.Services.Account
{
    public interface IAccountService
    {
        IEnumerable<AccountResponse> GetAllAccounts();

        bool Add(AccountRequest accountRequests);
    }
}
