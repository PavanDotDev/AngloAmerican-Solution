using AngloAmerican.Account.Services;
using AngloAmerican.Account.Services.Account;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AngloAmerican.Tests.AngloAmerican.Account.Services.Tests
{
    public class AccountServiceTests
    {
        private readonly AccountService subject;
        private readonly IAccountRepository accountRepository;
        private readonly IBalanceChecker balanceChecker;
        private readonly IHttpClientFactory httpClientFactory;
        public AccountServiceTests()
        {
            accountRepository = Mock.Of<IAccountRepository>();
            balanceChecker = Mock.Of<IBalanceChecker>();
            httpClientFactory = Mock.Of<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    //Content = new StringContent(@"[{ ""id"": 1, ""title"": ""Cool post!""}, { ""id"": 100, ""title"": ""Some title""}]"),
                    Content = new StringContent(@"{ ""results"" : [ { ""location"" : { ""city"" : ""Edinburgh"", ""postcode"" : ""F5 6QG"" } } ] }"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            Mock.Get(httpClientFactory).Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            subject = new AccountService(accountRepository, balanceChecker, httpClientFactory);
        }

        [Fact]
        public void GetAllAccounts_ReturnsAllAccounts()
        {
            //Arrange
            List<AccountModel> accountModels = new List<AccountModel>
            {
                new AccountModel {FirstName = "Ruby", LastName = "Curtis", Balance = 300},
                new AccountModel {FirstName = "Carolyn", LastName = "Hicks", Balance = 1400},
                new AccountModel {FirstName = "Elijah", LastName = "Johnston", Balance = 5000},
            };

            Mock.Get(accountRepository).Setup(x => x.GetAllAccounts()).Returns(accountModels);

            //Act
            var result = subject.GetAllAccounts();

            //Assert
            Assert.Equal(3, (result as List<AccountResponse>).Count);
        }

        [Fact]
        public void GivenValidAccount_Add_ReturnsTrue()
        {
            //Arrange
            AccountModel accountModel = new AccountModel
            {
                FirstName = "Ruby", LastName = "Curtis", Balance = 300,
            };

            AccountRequest accountRequest = new AccountRequest
            {
                FirstName = "Ruby",
                LastName = "Curtis",
                Balance = 300,
            };

            Mock.Get(accountRepository).Setup(x => x.Add(It.IsAny<AccountModel>())).Returns(Task.CompletedTask);
            Mock.Get(balanceChecker).Setup(x => x.Process(accountModel.Balance, It.IsAny<Notification>(), It.IsAny<ExternalApi>(), accountModel.LastName)).Returns(true);
            //Act
            var result = subject.Add(accountRequest);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenInValidAccount_Add_ReturnsFalse()
        {
            //Arrange
            AccountModel accountModel = new AccountModel
            {
                FirstName = "Ruby",
                LastName = "Curtis",
                Balance = 300,
            };

            AccountRequest accountRequest = new AccountRequest
            {
                FirstName = "Ruby",
                LastName = "Curtis",
                Balance = 300,
            };

            Mock.Get(accountRepository).Setup(x => x.Add(It.IsAny<AccountModel>())).Returns(Task.CompletedTask);
            Mock.Get(balanceChecker).Setup(x => x.Process(accountModel.Balance, It.IsAny<Notification>(), It.IsAny<ExternalApi>(), accountModel.LastName)).Returns(false);
            //Act
            var result = subject.Add(accountRequest);

            //Assert
            Assert.False(result);
        }
    }
}
