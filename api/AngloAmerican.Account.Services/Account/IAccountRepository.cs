using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AngloAmerican.Account.Services.Account
{
    public interface IAccountRepository
    {
        List<AccountModel> GetAllAccounts();
        Task Add(AccountModel accountModel);
    }
}
