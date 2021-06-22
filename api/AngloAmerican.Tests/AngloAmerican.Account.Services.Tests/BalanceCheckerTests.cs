using AngloAmerican.Account.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AngloAmerican.Tests.AngloAmerican.Account.Services.Tests
{
    public class BalanceCheckerTests
    {
        private readonly BalanceChecker subject;
        public BalanceCheckerTests()
        {
            subject = new BalanceChecker();
        }

        [Fact]
        public void GivenAmountLessThan10000_Process_ReturnsTrue()
        {
            int amount = 7000;
            bool result = subject.Process(amount, new Notification(), new ExternalApi(), "Some Name");
            Assert.True(result);
        }

        [Fact]
        public void GivenAmountGreaterThan10000AndLastnameIsInTheList_Process_ReturnsFalse()
        {
            int amount = 12000;
            bool result = subject.Process(amount, new Notification(), new ExternalApi(), "Rene");
            Assert.False(result);
        }

        [Fact]
        public void GivenAmountGreaterThan10000AndLastnameIsNotInTheList_Process_ReturnsTrue()
        {
            int amount = 12000;
            bool result = subject.Process(amount, new Notification(), new ExternalApi(), "Some Name");
            Assert.True(result);
        }
    }
}
