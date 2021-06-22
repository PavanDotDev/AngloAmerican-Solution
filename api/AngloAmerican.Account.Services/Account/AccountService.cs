using AngloAmerican.Account.Services.Account;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AngloAmerican.Account.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IBalanceChecker balanceChecker;
        private readonly AddressService addressService;

        public AccountService(IAccountRepository accountRepository, 
            IBalanceChecker balanceChecker, IHttpClientFactory httpClientFactory)
        {
            this.accountRepository = accountRepository;
            this.balanceChecker = balanceChecker;
            addressService = new AddressService(httpClientFactory.CreateClient());
        }

        public bool Add(AccountRequest accountRequests)
        {
            if(balanceChecker.Process(accountRequests.Balance, new Notification(), new ExternalApi(), accountRequests.LastName))
            {
                if (accountRepository.Add(new AccountModel()
                {
                    FirstName = accountRequests.FirstName,
                    LastName = accountRequests.LastName,
                    Balance = accountRequests.Balance,
                }).Status == TaskStatus.RanToCompletion)
                    return true;
            }
            return false;
        }

        public IEnumerable<AccountResponse> GetAllAccounts()
        {
            List<AccountModel> accountModels = accountRepository.GetAllAccounts();
            List<AccountResponse> accountResponses = new List<AccountResponse>();

            foreach (var item in accountModels)
            {
                accountResponses.Add(new AccountResponse()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Balance = item.Balance,
                    Address = addressService.GetAddress(),
                    TypeId = getAccountTypeId(item.Balance),
                });
            }
            return accountResponses;
        }

        private int getAccountTypeId(int balance)
        {
            if (balance < 5000)
                return 1;
            else if (balance >= 5000 && balance < 10000)
                return 2;
            else if (balance >= 10000)
                return 3;
            else
                return 0;
        }
    }
}
