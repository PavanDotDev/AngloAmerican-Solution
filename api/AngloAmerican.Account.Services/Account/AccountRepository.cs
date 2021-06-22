using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngloAmerican.Account.Services.Account
{
    public class AccountRepository : IAccountRepository
    {
        private static List<AccountModel> _accounts;

        public AccountRepository()
        {
            if(_accounts == null)
                CreateDefault();
        }

        private void CreateDefault()
            => _accounts = new List<AccountModel>
            {
                new AccountModel {FirstName = "Ruby", LastName = "Curtis", Balance = 300},
                new AccountModel {FirstName = "Carolyn", LastName = "Hicks", Balance = 1400},
                new AccountModel {FirstName = "Elijah", LastName = "Johnston", Balance = 5000},
                new AccountModel {FirstName = "Kirk", LastName = "Gibson", Balance = 7100},
                new AccountModel {FirstName = "Jessie", LastName = "Castro", Balance = 10000},
                new AccountModel {FirstName = "Anne", LastName = "Sanchez", Balance = 4900},
                new AccountModel {FirstName = "Rene", LastName = "Knight", Balance = 5555},
            };


        public List<AccountModel> GetAllAccounts()
            => _accounts;

        public async Task Add(AccountModel accountModel)
        {
            _accounts.Add(accountModel);

            await Task.CompletedTask;
        }
    }
}