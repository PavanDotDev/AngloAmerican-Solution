using AngloAmerican.Account.Services.Account;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AngloAmerican.Account.Api.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        public IEnumerable<AccountResponse> GetAllAccounts()
        {
            return accountService.GetAllAccounts();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult CreateAccount([FromBody] AccountRequest accountRequest)
        {
            bool result = accountService.Add(accountRequest);
            if (result)
                return Ok();
            else
                return BadRequest();
        }

        /* TODO
            - Create a REST API to get all the accounts
                For every account you need to use AddressService to load an address (City and PostCode)
                You can use AccountResponse class
                
            - Create a REST API to save an account 
                Call BalanceChecker to verify if you can save
                You can use AccountRequest class as a payload
         */



    }
}