using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AngloAmerican.Account.Api.Controllers
{
    [ApiController]
    [Route("accounttype")]
    public class AccountTypeController : ControllerBase
    {

        /* TODO
            - Create a REST API to get all the account types
         */

        [HttpGet]
        public IEnumerable<AccountType> GetAllAccountTypes()
        {
            return GetTypes(); ;
        }

        private List<AccountType> GetTypes()
            => new List<AccountType>
            {
                new AccountType {Id = 1, Name = "Bronze"},
                new AccountType {Id = 2, Name = "Silver"},
                new AccountType {Id = 3, Name = "Gold"}
            };
    }
}