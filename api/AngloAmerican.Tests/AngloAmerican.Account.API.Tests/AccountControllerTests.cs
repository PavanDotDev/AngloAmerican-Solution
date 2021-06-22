using AngloAmerican.Account.Api.Controllers;
using AngloAmerican.Account.Services.Account;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AngloAmerican.Tests.AngloAmerican.Account.API.Tests
{
    public class AccountControllerTests
    {
        private readonly IAccountService accountService;
        private readonly AccountController subject;

        public AccountControllerTests()
        {
            accountService = Mock.Of<IAccountService>();
            subject = new AccountController(accountService);
        }
        
        [Fact]
        public void GetAllAccounts_ReturnsAllAccounts()
        {
            //Arrange
            IEnumerable<AccountResponse> accountResponses = new List<AccountResponse>()
            {
                new AccountResponse {FirstName = "Ruby", LastName = "Curtis", Balance = 300, Address="some Address", TypeId=1},
                new AccountResponse {FirstName = "Carolyn", LastName = "Hicks", Balance = 1400, Address="some Address1", TypeId=2},
                new AccountResponse {FirstName = "Elijah", LastName = "Johnston", Balance = 5000, Address="some Address2", TypeId=3},
            };

            Mock.Get(accountService).Setup(x => x.GetAllAccounts()).Returns(accountResponses);

            //Act
            var result = subject.GetAllAccounts();

            //Assert
            Assert.Equal(accountResponses, result);
        }

        [Fact]
        public void GivenValidAccount_CreateAccount_Returns200OkResult()
        {
            //Arrange
            AccountRequest accountRequest = new AccountRequest()
            {
                FirstName = "New", LastName = "Name", Balance = 500
            };

            Mock.Get(accountService).Setup(x => x.Add(accountRequest)).Returns(true);

            //Act
            var result = subject.CreateAccount(accountRequest);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void GivenInvalidAccount_CreateAccount_Returns400BadRequestResult()
        {
            //Arrange
            AccountRequest accountRequest = new AccountRequest()
            {
                FirstName = "New",
                LastName = "Name",
                Balance = 500
            };

            Mock.Get(accountService).Setup(x => x.Add(accountRequest)).Returns(false);

            //Act
            var result = subject.CreateAccount(accountRequest);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
